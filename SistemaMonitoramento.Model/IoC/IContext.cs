
using SistemaMonitoramento.Model.Domain;

namespace SistemaMonitoramento.Model.IoC
{
    public interface IContext
    {
        dmMonitoramento ctxMonitoramento { get; }
    }

    public class Context : IContext
    {
        public dmMonitoramento ctxMonitoramento { get; set; }

        public Context()
        {
            ctxMonitoramento = new dmMonitoramento();
        }

    }
}
