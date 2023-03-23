using SistemaMonitoramento.Model.Class;
using SistemaMonitoramento.Model.DataObject;
using SistemaMonitoramento.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
