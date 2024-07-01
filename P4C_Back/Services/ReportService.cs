using Microsoft.EntityFrameworkCore;
using P4C_Back.Data;
using P4C_Back.DTOs.All;
using P4C_Back.DTOs.Report;
using P4C_Back.Exceptions;
using P4C_Back.Models;
using P4C_Back.Requests.Report;
using P4C_Back.Responses.All;
using P4C_Back.Responses.Report;

namespace P4C_Back.Services
{
    public class ReportService(AppDbContext appDbContext)
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        public async Task<IndexReportResponse> PrepareIndexResponse()
        {
            List<ReportDto> reportDtos = await _appDbContext.Reports.Include(r => r.Kpis).Include(r => r.Canali).ThenInclude(c => c.Piattaforma).Select(r => new ReportDto(r)).ToListAsync();
            List<NameDto> kpiNames = await _appDbContext.Kpis.Where(k => k.NomeKpi != null).Select(k => new NameDto(k.NomeKpi!)).ToListAsync();
            List<CanalePiattaformaDto> canalePiattaformaNames = await _appDbContext.Canali.Where(c => c.NomeCanale != null && c.Piattaforma != null && c.Piattaforma.NomePiattaforma != null).Include(c => c.Piattaforma).Select(c => new CanalePiattaformaDto(c.NomeCanale!, c.Piattaforma!.NomePiattaforma!)).ToListAsync();
            List<EnumDto> enumsDtos = await _appDbContext.ValoreEnum.Where(e => e.ValoriCampoEnum != null && (e.NomeCampoEnum == "TipoOggetto" || e.NomeCampoEnum == "LivelloAccessibilita" || e.NomeCampoEnum == "Dataset/ReportPadre")).Select(e => new EnumDto(e.NomeCampoEnum!, e.ValoriCampoEnum!)).ToListAsync();

            return new IndexReportResponse(reportDtos, kpiNames, canalePiattaformaNames, enumsDtos);
        }

        public async Task<ReportDto> Create(CreateReportRequest request, string user)
        {
            List<Kpi> kpis = [];
            List<Canale> canali = [];

            if(request.KpiNames != null && request.KpiNames.Count > 0)
            {
                kpis = [.. _appDbContext.Kpis.Where(k => k.NomeKpi != null && request.KpiNames.Contains(k.NomeKpi))];
                if(kpis.Count != request.KpiNames.Count)
                {
                    throw new ValidationException("Couldn't allocate kpis. Kpi names are not valid.");
                }
            }

            if(request.CanaleNames != null && request.CanaleNames.Count > 0) {
                canali = [.. _appDbContext.Canali.Where(c => c.NomeCanale != null && request.CanaleNames.Contains(c.NomeCanale)).Include(c => c.Piattaforma)];
                if(canali.Count != request.CanaleNames.Count)
                {
                    throw new ValidationException("Couldn't allocate canali. Canale names are not valid.");
                }
            }

            Report report = new(request, user)
            {
                Kpis = kpis,
                Canali = canali
            };

            _appDbContext.Add(report);
            await _appDbContext.SaveChangesAsync();

            return new ReportDto(report);
        }

        public async Task<ReportDto> Edit(EditReportRequest request, string user)
        {
            Report report = await _appDbContext.Reports.Where(r => r.Id == request.Id).Include(r => r.Kpis).Include(r => r.Canali).ThenInclude(c => c.Piattaforma).SingleOrDefaultAsync() ?? throw new ValidationException($"Couldn't edit report with id {request.Id}. This report doesn't exist.");
            List<Kpi> kpis = [];
            List<Canale> canali = [];

            if (request.KpiNames != null && request.KpiNames.Count > 0)
            {
                kpis = [.. _appDbContext.Kpis.Where(k => k.NomeKpi != null && request.KpiNames.Contains(k.NomeKpi))];
                if (kpis.Count != request.KpiNames.Count)
                {
                    throw new ValidationException("Couldn't allocate kpis. Kpi names are not valid.");
                }
            }

            if (request.CanaleNames != null && request.CanaleNames.Count > 0)
            {
                canali = [.. _appDbContext.Canali.Where(c => c.NomeCanale != null && request.CanaleNames.Contains(c.NomeCanale)).Include(c => c.Piattaforma)];
                if (canali.Count != request.CanaleNames.Count)
                {
                    throw new ValidationException("Couldn't allocate canali. Canale names are not valid.");
                }
            }

            report.Edit(request, kpis, canali, user);
            await _appDbContext.SaveChangesAsync();
            
            return new ReportDto(report);
        }

        public async Task<IdResponse> Delete(int id)
        {
            Report report = await _appDbContext.Reports.Where(r => r.Id == id).Include(r => r.Kpis).Include(r => r.Canali).SingleOrDefaultAsync() ?? throw new ValidationException($"Couldn't delete report with id {id}. This report doesn't exist.");
            report.Kpis.Clear();
            report.Canali.Clear();
            _appDbContext.Remove(report);

            await _appDbContext.SaveChangesAsync();

            return new IdResponse(id);
        }
    }
}
