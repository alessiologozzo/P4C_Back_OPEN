using FluentValidation;
using P4C_Back.Data;
using P4C_Back.Requests.Report;

namespace P4C_Back.Validators.Report
{
    public class CreateReportValidator : AbstractValidator<CreateReportRequest>
    {
        private readonly AppDbContext _appDbContext;

        public CreateReportValidator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.NomeReport).NotEmpty().MaximumLength(50).Must(IsNomeReportUnique);

            RuleFor(x => x.TipoOggetto).NotEmpty().Must(tipoOggetto => DoesEnumExist("TipoOggetto", tipoOggetto));

            RuleFor(x => x.LivelloAccessibilita).NotEmpty().Must(livelloAccessibilita => DoesEnumExist("LivelloAccessibilita", livelloAccessibilita));

            RuleFor(x => x.DescReport).NotEmpty().MaximumLength(500).When(x => x.DescReport != null);
            RuleFor(x => x.PathReport).NotEmpty().MaximumLength(500).When(x => x.PathReport != null);

            RuleFor(x => x.Link).NotEmpty().MaximumLength(500).When(x => x.Link != null);

            RuleFor(x => x.ListaDataset).NotEmpty().MaximumLength(500).Must(listaDataset => DoesEnumListExist("Dataset/ReportPadre", listaDataset!)).When(x => x.ListaDataset != null);
        }

        private bool IsNomeReportUnique(string nomeReport)
        {
            string? reportName = _appDbContext.Reports.Where(r => r.NomeReport == nomeReport).Select(r => r.NomeReport).FirstOrDefault();

            return reportName == null;
        }

        private bool DoesEnumExist(string enumName, string enumValue)
        {
            string? value = _appDbContext.ValoreEnum.Where(e => e.NomeCampoEnum == enumName && e.ValoriCampoEnum != null).Select(e => e.ValoriCampoEnum).FirstOrDefault();
            List<string> values = value != null ? [.. value.Split('|')] : [];

            return values.Contains(enumValue);
        }

        private bool DoesEnumListExist(string enumName, string enumValue)
        {
            bool isValid = true;
            List<string> splittedInputValues = [.. enumValue.Split(',')];
            
            string? value = _appDbContext.ValoreEnum.Where(e => e.NomeCampoEnum == enumName && e.ValoriCampoEnum != null).Select(e => e.ValoriCampoEnum).FirstOrDefault();
            List<string> splittedValues = value != null ? [.. value.Split('|')] : [];

            if (splittedInputValues.Count < 1 || splittedValues.Count < 1)
            {
                isValid = false;
            } else
            {
                for (int i = 0; i < splittedInputValues.Count && isValid; i++)
                {
                    bool found = false;
                    for (int j = 0; j < splittedValues.Count && !found; j++)
                    {
                        if (splittedInputValues[i] == splittedValues[j])
                        {
                            found = true;
                        }
                    }
                    if(!found)
                    {
                        isValid = false;
                    }
                }
            }

            return isValid;
        }
    }
}
