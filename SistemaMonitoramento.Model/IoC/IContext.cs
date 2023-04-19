
using SistemaMonitoramento.Model.Domain;

namespace SistemaMonitoramento.Model.IoC
{
    public interface IContext
    {
        dmMonitoramento ctxMonitoramento { get; }
        dmUsuario ctxUsuario { get; }
        dmPermissao ctxPermissao { get; }
        dmTela ctxTela { get; }
        dmPermissaoEspecial ctxPermissaoEspecial { get; }

    }

    public class Context : IContext
    {
        public dmMonitoramento ctxMonitoramento { get; set; }
        public dmUsuario ctxUsuario { get; set; }
        public dmPermissao ctxPermissao { get; set; }
        public dmTela ctxTela { get; set; }
        public dmPermissaoEspecial ctxPermissaoEspecial { get; set; }




        public Context()
        {
            ctxMonitoramento = new dmMonitoramento();
            ctxUsuario = new dmUsuario();
            ctxPermissao = new dmPermissao();
            ctxTela = new dmTela();
            ctxPermissaoEspecial = new dmPermissaoEspecial();


        }

    }
}
