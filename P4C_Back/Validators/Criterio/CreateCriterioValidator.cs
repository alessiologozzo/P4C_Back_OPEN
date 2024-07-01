using FluentValidation;
using P4C_Back.Data;
using P4C_Back.Requests.Criterio;

namespace P4C_Back.Validators.Criterio
{
    public class CreateCriterioValidator : AbstractValidator<CreateCriterioRequest>
    {
        private readonly AppDbContext _appDbContext;

        public CreateCriterioValidator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.DettaglioCriterio).NotEmpty().MaximumLength(40000).Must(IsDettaglioCriterioUnique);

            RuleFor(x => x.DescCriterio).NotEmpty().MaximumLength(500).Must(IsDescCriterioUnique);

            RuleFor(x => x.TipoCriterio).NotEmpty().Must(tipoCriterio => DoesEnumExist("TipoCriterio", tipoCriterio!)).When(x => x.TipoCriterio != null);

            RuleFor(x => x.KpiOrigine).NotEmpty().MaximumLength(100).Matches("^\\d+(\\,\\d+)*$").When(x => x.KpiOrigine != null).When(x => x.KpiOrigine != null);
        }

        private bool IsDettaglioCriterioUnique(string dettaglioCriterio)
        {
            string? criterioValue = _appDbContext.Criteri.Where(c => c.DettaglioCriterio == dettaglioCriterio).Select(c => c.DettaglioCriterio).FirstOrDefault();

            return criterioValue == null;
        }

        private bool IsDescCriterioUnique(string descCriterio)
        {
            string? criterioValue = _appDbContext.Criteri.Where(c => c.DettaglioCriterio == descCriterio).Select(c => c.DettaglioCriterio).FirstOrDefault();

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
