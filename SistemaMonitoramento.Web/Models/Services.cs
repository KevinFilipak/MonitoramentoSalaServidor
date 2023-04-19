using AspNetCore.ReportingServices.ReportProcessing.ReportObjectModel;
using SistemaMonitoramento.Model.Class;
using System.Security.Claims;

namespace SistemaMonitoramento.Web.Models
{
    public static class Services
    {
        public static Usuario GetUsuario(ClaimsIdentity _userIdentity)
        {            

            var usuario = new SistemaMonitoramento.Model.Class.Usuario()
            {
                usuario_i_id = Convert.ToInt32(_userIdentity.Claims.Where(a => a.Type == "usuario_i_id").FirstOrDefault().Value),
                usuario_s_usuario = _userIdentity.Claims.Where(a => a.Type == "usuario_s_usuario").FirstOrDefault().Value,
                usuario_s_email = _userIdentity.Claims.Where(a => a.Type == "usuario_s_email").FirstOrDefault().Value,
                usuario_s_nome = _userIdentity.Claims.Where(a => a.Type == "usuario_s_nome").FirstOrDefault().Value,
                usuario_s_foto = _userIdentity.Claims.Where(a => a.Type == "usuario_s_foto").FirstOrDefault().Value,
                usuario_s_funcao = _userIdentity.Claims.Where(a => a.Type == "usuario_s_funcao").FirstOrDefault().Value,
            };

            return usuario;

        }

    }
}
