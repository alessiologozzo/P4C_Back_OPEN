using FluentValidation;
using P4C_Back.Data;
using P4C_Back.Requests.Kpi;

namespace P4C_Back.Validators.Kpi
{
    public class EditKpiValidator : AbstractValidator<EditKpiRequest>
    {
        private readonly AppDbContext _appDbContext;

        public EditKpiValidator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Id).NotNull().GreaterThan(0);

            RuleFor(x => x.NomeKpi).NotEmpty().MaximumLength(50).Must((x, nomeKpi) => IsNomeKpiUniqueEdit(nomeKpi, x.Id));

            RuleFor(x => x.DescKpi).NotEmpty().MaximumLength(500).When(x => x.DescKpi != null);

            RuleFor(x => x.CategoriaKpi).NotEmpty().Must(categoriaKpi => DoesEnumExist("CategoriaKpi", categoriaKpi!)).When(x => x.CategoriaKpi != null);

            RuleFor(x => x.UMKpi).NotEmpty().Must(uMKpi => DoesEnumExist("UMKpi", uMKpi!)).When(x => x.UMKpi != null);

            RuleFor(x => x.Benchmark).NotEmpty().MaximumLength(500).When(x => x.Benchmark != null);
        }

        private bool IsNomeKpiUniqueEdit(string nomeKpi, int id)
        {
            string? kpiName = _appDbContext.Kpis.Where(k => k.NomeKpi == nomeKpi && k.Id != id).Select(k => k.NomeKpi).FirstOrDefault();

            return kpiName == null;
        }

        private bool DoesEnumExist(string enumName, string enumValue)
        {
            string? value = _appDbContext.ValoreEnum.Where(e => e.NomeCampoEnum == enumName && e.ValoriCampoEnum != null).Select(e => e.ValoriCampoEnum).FirstOrDefault();
            List<string> values = value != null ? [.. value.Split('|')] : [];

            return values.Contains(enumValue);
        }
    }
}
