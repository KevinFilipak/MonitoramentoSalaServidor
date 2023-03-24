using FluentValidation;
using SistemaMonitoramento.Model.Class;

namespace SistemaMonitoramento.Model.Validation
{
    internal class MonitoramentoValidator : AbstractValidator<Monitoramento>
    {
        public MonitoramentoValidator()
        {
            RuleFor(x => x.monitoramento_f_temperatura).NotEmpty();
            RuleFor(x => x.monitoramento_f_umidade).NotEmpty();
        }

    }
}
