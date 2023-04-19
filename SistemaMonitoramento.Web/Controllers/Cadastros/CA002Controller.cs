using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaMonitoramento.Model.Class;
using SistemaMonitoramento.Model.IoC;
using SistemaMonitoramento.Web.Models;
using System.Security.Claims;

namespace SistemaMonitoramento.Web.Controllers.Cadastros
{

    [Authorize(Roles = "CA002")]
    public class CA002Controller : Controller
    {
        private readonly ILogger<CA002Controller> _logger;        
        private readonly IContext _ctx;
        
        public CA002Controller(ILogger<CA002Controller> logger, IContext ctx)
        {
            _logger = logger;                        
            _ctx = ctx;
        }
        
        [HttpGet("Cadastros/CA002/Index")]        
        public IActionResult Index()
        {
            try
            {                

                return View("~/Views/Cadastros/CA002/Index.cshtml");
            }
            catch (Exception ex)
            {
                return View("~/Views/Home/Dashboard/Error.cshtml", ex);
            }
        }

        public PartialViewResult Grid()
        {
            try
            {
                return PartialView("~/Views/Cadastros/CA002/Grid.cshtml");
            }
            catch (Exception ex)
            {
                return PartialView("~/Views/Shared/_Exception.cshtml", ex);
            }

        }

        public JsonResult GridJson(DataTableAjaxPostModel model)
        {
            try
            {
                return Json(DataTable.GerarTabela(model, _ctx.ctxTela.BuscarTodos(model.search.value)));
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }

        }

        public PartialViewResult Form(int tela_i_id)
        {
            try
            {                
                return PartialView("~/Views/Cadastros/CA002/Form.cshtml", _ctx.ctxTela.BuscarPorId(tela_i_id));
            }
            catch (Exception ex)
            {
                return PartialView("~/Views/Shared/_Exception.cshtml", ex);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Form(Tela obj)
        {
            try
            {
                var message = "";
                var status = "";
                
                if (obj.tela_i_id == 0)
                {
                    obj.tela_d_created = DateTime.Now;
                    obj.tela_s_createdby = Services.GetUsuario(User.Identity as ClaimsIdentity).usuario_s_usuario;                    
                }
                else
                {
                    obj.tela_d_updated = DateTime.Now;
                    obj.tela_s_updatedby = Services.GetUsuario(User.Identity as ClaimsIdentity).usuario_s_usuario;
                }

                _ctx.ctxTela.Salvar(obj, out message, out status);

                return Json(new { status = status, message = message });
            }
            catch (Exception ex)
            {
                return Json(new { status = "error", message = $"Erro: {ex.Message}" });
            }

        }

        [HttpPost]
        public JsonResult Excluir(int tela_i_id)
        {
            try
            {
                var obj = _ctx.ctxTela.BuscarPorId(tela_i_id);

                var message = "";
                var status = "";

                obj.tela_d_archived = DateTime.Now;
                obj.tela_s_archivedby = Services.GetUsuario(User.Identity as ClaimsIdentity).usuario_s_usuario;

                _ctx.ctxTela.Excluir(obj, out message, out status);

                return Json(new { status = status, message = message });
            }
            catch (Exception ex)
            {
                return Json(new { status = "error", message = $"Erro: {ex.Message}" });
            }

        }
    }
}
