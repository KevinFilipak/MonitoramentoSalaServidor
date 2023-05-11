using FluentValidation;
using SistemaMonitoramento.Model.Class;

namespace SistemaMonitoramento.Model.Validation
{
    internal class DispositivoValidator : AbstractValidator<Dispositivo>
    {
        public DispositivoValidator()
        {
            RuleFor(x => x.dispositivo_s_dispositivo).NotEmpty();
        }

    }
}
