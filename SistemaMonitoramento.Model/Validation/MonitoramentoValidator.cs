using FluentValidation;
using SistemaMonitoramento.Model.Class;

namespace SistemaMonitoramento.Model.Validation
{
    internal class MonitoramentoValidator : AbstractValidator<Monitoramento>
    {
        public MonitoramentoValidator()
        {
            RuleFor(x => x.MONITORAMENTO_F_TEMPERATURA).NotEmpty();
            RuleFor(x => x.MONITORAMENTO_F_UMIDADE).NotEmpty();
        }

    }
}
