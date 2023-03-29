using FluentValidation;
using SistemaMonitoramento.Model.Class;

namespace SistemaMonitoramento.Model.Validation
{
    internal class ParametroValidator : AbstractValidator<Parametro>
    {
        public ParametroValidator()
        {
            RuleFor(x => x.parametro_s_valor).NotEmpty();
            RuleFor(x => x.parametro_s_parametro).NotEmpty();
        }

    }
}
