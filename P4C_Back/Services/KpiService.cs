using Microsoft.EntityFrameworkCore;
using P4C_Back.Data;
using P4C_Back.DTOs.All;
using P4C_Back.Exceptions;
using P4C_Back.DTOs.Kpi;
using P4C_Back.Models;
using P4C_Back.Requests.Kpi;
using P4C_Back.Responses.All;
using P4C_Back.Responses.Kpi;

namespace P4C_Back.Services
{
    public class KpiService(AppDbContext appDbContext)
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        public async Task<IndexKpiResponse> PrepareIndexResponse()
        {
            List<KpiDto> kpiDtos = await _appDbContext.Kpis.Include(k => k.Reports).Include(k => k.Criteri).Select(k => new KpiDto(k)).ToListAsync();
            List<IdNameDto> criterioDtos = await _appDbContext.Criteri.Where(c => c.DescCriterio != null).Select(c => new IdNameDto(c.Id, c.DescCriterio!)).ToListAsync();
            List<EnumDto> enumsDtos = await _appDbContext.ValoreEnum.Where(e => e.ValoriCampoEnum != null && (e.NomeCampoEnum == "CategoriaKpi" || e.NomeCampoEnum == "UMKpi")).Select(e => new EnumDto(e.NomeCampoEnum!, e.ValoriCampoEnum!)).ToListAsync();

            return new IndexKpiResponse(kpiDtos, criterioDtos, enumsDtos);
        }

        public async Task<KpiDto> Create(CreateKpiRequest request, string user)
        {
            List<Criterio> criteri = [];
            if (request.CriterioIds != null && request.CriterioIds.Count > 0)
            {
                criteri = await _appDbContext.Criteri.Where(c => request.CriterioIds.Contains(c.Id)).ToListAsync();
                if (criteri.Count != request.CriterioIds.Count)
                {
                    throw new ValidationException("Couldn't allocate criteri. Criteri ids are not valid.");
                }
            }

            Kpi kpi = new(request, user)
            {
                Criteri = criteri
            };

            await _appDbContext.AddAsync(kpi);
            await _appDbContext.SaveChangesAsync();

            return new KpiDto(kpi);
        }

        public async Task<KpiDto> Edit(EditKpiRequest request, string user)
        {
            Kpi kpi = await _appDbContext.Kpis.Where(k => k.Id == request.Id).Include(k => k.Criteri).SingleOrDefaultAsync() ?? throw new ValidationException($"Couldn't edit kpi with id {request.Id}. This kpi doesn't exist.");

            List<Criterio> criteri = [];
            if (request.CriterioIds != null && request.CriterioIds.Count > 0)
            {
                criteri = await _appDbContext.Criteri.Where(c => request.CriterioIds.Contains(c.Id)).ToListAsync();
                if (criteri.Count != request.CriterioIds.Count)
                {
                    throw new ValidationException("Couldn't allocate criteri. Criteri ids are not valid.");
                }
            }

            kpi.Edit(request, criteri, user);
            await _appDbContext.SaveChangesAsync();

            return new KpiDto(kpi);
        }

        public async Task<IdResponse> Delete(int id)
        {
            Kpi? kpi = await _appDbContext.Kpis.Where(k => k.Id == id).Include(k => k.Reports).Include(k => k.Criteri).SingleOrDefaultAsync() ?? throw new ValidationException($"Couldn't delete kpi with id {id}. This kpi doesn't exist.");
            if(kpi.Reports != null && kpi.Reports.Count > 0)
            {
                throw new ValidationException($"Couldn't delete kpi with id {id}. This kpi has critical relationships with some reports. Remove the relationships and try again.");
            }

            kpi.Criteri.Clear();
            _appDbContext.Remove(kpi);
            await _appDbContext.SaveChangesAsync();

            return new IdResponse(id);
        }
    }
}
