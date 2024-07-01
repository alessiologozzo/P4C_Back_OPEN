using Microsoft.EntityFrameworkCore;
using P4C_Back.Data;
using P4C_Back.DTOs.All;
using P4C_Back.DTOs.Criterio;
using P4C_Back.Models;
using P4C_Back.Requests.Criterio;
using P4C_Back.Responses.All;
using P4C_Back.Responses.Criterio;
using P4C_Back.Exceptions;
using System.Linq;

namespace P4C_Back.Services
{
    public class CriterioService(AppDbContext appDbContext)
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        public async Task<IndexCriterioResponse> PrepareIndexResponse()
        {
            List<CriterioDto> criterioDtos = await _appDbContext.Criteri.Include(c => c.Kpis).Select(c => new CriterioDto(c)).ToListAsync();
            List<IdNameDto> kpiIdNames = await _appDbContext.Kpis.Where(k => k.NomeKpi != null).Select(k => new IdNameDto(k.Id, k.NomeKpi!)).ToListAsync();
            EnumDto? enumDto = await _appDbContext.ValoreEnum.Where(e => e.ValoriCampoEnum != null && (e.NomeCampoEnum == "TipoCriterio")).Select(e => new EnumDto(e.NomeCampoEnum!, e.ValoriCampoEnum!)).FirstOrDefaultAsync();

            return new IndexCriterioResponse(criterioDtos, kpiIdNames, enumDto);
        }

        public async Task<CriterioDto> Create(CreateCriterioRequest request, string user)
        {
            Criterio criterio = new(request, user);

            await _appDbContext.AddAsync(criterio);
            await _appDbContext.SaveChangesAsync();

            return new CriterioDto(criterio);
        }

        public async Task<CriterioDto> Edit(EditCriterioRequest request, string user)
        {
            Criterio criterio = await _appDbContext.Criteri.Where(c => c.Id == request.Id).SingleOrDefaultAsync() ?? throw new ValidationException($"Couldn't edit criterio with id {request.Id}. This criterio doesn't exist.");

            criterio.Edit(request, user);
            await _appDbContext.SaveChangesAsync();

            return new CriterioDto(criterio);
        }

        public async Task<IdResponse> Delete(int id)
        {
            Criterio criterio = await _appDbContext.Criteri.Where(c => c.Id == id).Include(c => c.Kpis).SingleOrDefaultAsync() ?? throw new ValidationException($"Couldn't delete criterio with id {id}. This criterio doesn't exist.");
            if(criterio.Kpis.Count > 0)
            {
                throw new ValidationException($"Couldn't delete criterio with id {id}. This criterio has critical relationships with some kpis. Remove the relationships and try again.");
            }

            _appDbContext.Remove(criterio);
            await _appDbContext.SaveChangesAsync();

            return new IdResponse(id);
        }
    }
}
