using FluentValidation;
using P4C_Back.Data;
using P4C_Back.Requests.Criterio;

namespace P4C_Back.Validators.Criterio
{
    public class EditCriterioValidator : AbstractValidator<EditCriterioRequest>
    {
        private readonly AppDbContext _appDbContext;

        public EditCriterioValidator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Id).NotNull().GreaterThan(0);

            RuleFor(x => x.DettaglioCriterio).NotEmpty().MaximumLength(40000).Must((x, dettaglioCriterio) => IsDettaglioCriterioUniqueEdit(dettaglioCriterio, x.Id));

            RuleFor(x => x.DescCriterio).NotEmpty().MaximumLength(500).Must((x, descCriterio) => IsDescrizioneCriterioUniqueEdit(descCriterio, x.Id));

            RuleFor(x => x.TipoCriterio).NotEmpty().Must(tipoCriterio => DoesEnumExist("TipoCriterio", tipoCriterio!)).When(x => x.TipoCriterio != null);

            RuleFor(x => x.KpiOrigine).NotEmpty().MaximumLength(100).Matches("^\\d+(\\,\\d+)*$").When(x => x.KpiOrigine != null);
        }

        private bool IsDettaglioCriterioUniqueEdit(string dettaglioCriterio, int id)
        {
            string? criterioValue = _appDbContext.Criteri.Where(c => c.DettaglioCriterio == dettaglioCriterio && c.Id != id).Select(c => c.DettaglioCriterio).FirstOrDefault();

            return criterioValue == null;
        }

        private bool IsDescrizioneCriterioUniqueEdit(string descCriterio, int id)
        {
            string? criterioValue = _appDbContext.Criteri.Where(c => c.DescCriterio == descCriterio && c.Id != id).Select(c => c.DescCriterio).FirstOrDefault();

            return criterioValue == null;
        }

        private bool DoesEnumExist(string enumName, string enumValue)
        {
            string? value = _appDbContext.ValoreEnum.Where(e => e.NomeCampoEnum == enumName && e.ValoriCampoEnum != null).Select(e => e.ValoriCampoEnum).FirstOrDefault();
            List<string> values = value != null ? [.. value.Split('|')] : [];

            return values.Contains(enumValue);
        }
    }
}
