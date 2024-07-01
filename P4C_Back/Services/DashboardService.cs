using System.Text;
using Microsoft.EntityFrameworkCore;
using P4C_Back.Data;
using P4C_Back.DTOs.All;
using P4C_Back.DTOs.Criterio;
using P4C_Back.DTOs.Dashboard;
using P4C_Back.DTOs.Kpi;
using P4C_Back.DTOs.Report;
using P4C_Back.Responses.Dashboard;

namespace P4C_Back.Services
{
    public class DashboardService(AppDbContext appDbContext)
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        public async Task<byte[]> GetCsvFileBytes()
        {
            List<CsvDto> data = await _appDbContext.Database.SqlQueryRaw<CsvDto>("SELECT pi.NomePiattaforma, ca.NomeCanale, r.PathReport, r.Link, r.IdReport, r.NomeReport, r.DescReport, r.TipoOggetto, r.LivelloAccessibilita, k.IdKpi, k.NomeKpi, k.DescKpi, k.CategoriaKpi, k.UMKpi, c.TipoCriterio, c.IdCriterio, c.DescCriterio, c.DettaglioCriterio FROM tbl_report AS r INNER JOIN tbl_reportkpi AS rk ON r.IdReport=rk.IdReport INNER JOIN tbl_kpi AS k ON rk.IdKpi=k.IdKpi INNER JOIN tbl_kpicriteri AS kc ON k.IdKpi=kc.IdKpi INNER JOIN tbl_criteri AS c ON kc.IdCriterio=c.IdCriterio INNER JOIN tbl_reportcanale AS rc ON r.IdReport=rc.IdReport INNER JOIN tbl_canale AS ca ON rc.IdCanale=ca.IdCanale INNER JOIN tbl_piattaforma AS pi ON ca.fk_Piattaforma=pi.IdPiattaforma UNION\r\nSELECT a.NomePiattaforma, a.NomeCanale, a.PathReport, a.Link, a.IdReport, a.NomeReport, a.DescReport, a.TipoOggetto, a.LivelloAccessibilita, k.IdKpi, k.NomeKpi, k.DescKpi, k.CategoriaKpi, k.UMKpi, c.TipoCriterio, c.IdCriterio, c.DescCriterio, c.DettaglioCriterio FROM tbl_kpi AS k INNER JOIN (SELECT pi.NomePiattaforma, ca.NomeCanale, r.PathReport, r.Link, r.IdReport, r.NomeReport, r.DescReport, r.TipoOggetto, r.LivelloAccessibilita, k.NomeKpi, k.DescKpi, k.CategoriaKpi, k.UMKpi, c.TipoCriterio, c.IdCriterio, c.DescCriterio, c.KpiOrigine FROM tbl_report AS r INNER JOIN tbl_reportkpi AS rk ON r.IdReport=rk.IdReport INNER JOIN tbl_kpi AS k ON rk.IdKpi=k.IdKpi INNER JOIN tbl_kpicriteri AS kc ON k.IdKpi=kc.IdKpi INNER JOIN tbl_criteri AS c ON kc.IdCriterio=c.IdCriterio INNER JOIN tbl_reportcanale AS rc ON r.IdReport=rc.IdReport INNER JOIN tbl_canale AS ca ON rc.IdCanale=ca.IdCanale INNER JOIN tbl_piattaforma AS pi ON ca.fk_Piattaforma=pi.IdPiattaforma WHERE KpiOrigine IS NOT NULL) AS a ON INSTR(CONCAT(',',a.KpiOrigine,','), CONCAT(',',k.IdKpi,',')) > 0 INNER JOIN tbl_kpicriteri AS kc ON k.IdKpi=kc.IdKpi INNER JOIN tbl_criteri AS c ON kc.IdCriterio=c.IdCriterio ORDER BY 6, 1, 2, 13 DESC, 11, 17;").ToListAsync();

            var csvContent = new StringBuilder();
            var properties = typeof(CsvDto).GetProperties();

            string header = string.Join(";", properties.Select(p => $"\"{p.Name}\""));
            header = header.Replace("NomePiattaforma", "Piattaforma");
            header = header.Replace("NomeCanale", "Canale");
            header = header.Replace("PathReport", "Path Report");
            header = header.Replace("IdReport", "Id Report");
            header = header.Replace("NomeReport", "Nome Report");
            header = header.Replace("DescReport", "Descrizione Report");
            header = header.Replace("TipoOggetto", "Tipo Oggetto");
            header = header.Replace("LivelloAccessibilita", "Livello Accessibilità");
            header = header.Replace("IdKpi", "Id KPI");
            header = header.Replace("NomeKpi", "Nome KPI");
            header = header.Replace("DescKpi", "Descrizione KPI");
            header = header.Replace("CategoriaKpi", "Categoria KPI");
            header = header.Replace("UMKpi", "Unità di Misura");
            header = header.Replace("TipoCriterio", "Tipo Criterio");
            header = header.Replace("IdCriterio", "Id Criterio");
            header = header.Replace("DescCriterio", "Descrizione Criterio");
            header = header.Replace("DettaglioCriterio", "Dettaglio Criterio");

            csvContent.AppendLine(header);

            StringBuilder row = new();

            data.ForEach(d =>
            {
                for(int i = 0; i < properties.Length; i++)
                {
                    string? value = properties[i].GetValue(d, null)?.ToString();
                    if(value != null && value != string.Empty)
                    {
                        value = value.Replace("\"", "\"\"");
                        if (i + 1 < properties.Length)
                        {
                            row.Append($"\"{value}\";");
                        }
                        else
                        {
                            row.Append($"\"{value}\"");
                        }
                    }
                    else
                    {
                        if(i + 1 < properties.Length)
                        {
                            row.Append("null;");
                        }
                        else
                        {
                            row.Append("null");
                        }
                    }
                }

                csvContent.AppendLine(row.ToString());
                row.Clear();
            });

            byte[] bytes = Encoding.UTF8.GetBytes(csvContent.ToString());
            return bytes;
        }

        public async Task<IndexDashboardResponse> Index()
        {
            List<ReportWithoutRelationsDto> reports = await _appDbContext.Reports.OrderBy(r => r.DataInserimento).Select(r => new ReportWithoutRelationsDto(r)).ToListAsync();
            List<KpiWithoutRelationsDto> kpis = await _appDbContext.Kpis.OrderBy(k => k.DataInserimento).Select(k => new KpiWithoutRelationsDto(k)).ToListAsync();
            List<CriterioDto> criteri = await _appDbContext.Criteri.OrderBy(c => c.DataInserimento).Select(c => new CriterioDto(c)).ToListAsync();
            List<EnumDto> enums = await _appDbContext.ValoreEnum.Where(e => e.NomeCampoEnum != null && e.ValoriCampoEnum != null).Select(e => new EnumDto(e.NomeCampoEnum!, e.ValoriCampoEnum!)).ToListAsync();

            return new IndexDashboardResponse(reports, kpis, criteri, enums);
        }
    }
}
