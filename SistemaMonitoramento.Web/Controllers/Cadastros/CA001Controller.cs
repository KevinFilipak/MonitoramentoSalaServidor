using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaMonitoramento.Model.Class;
using SistemaMonitoramento.Model.Enumerators;
using SistemaMonitoramento.Model.IoC;
using SistemaMonitoramento.Web.Models;
using System.Security.Claims;

namespace SistemaMonitoramento.Web.Controllers.Cadastros
{

    [Authorize(Roles = "CA001")]
    public class CA001Controller : Controller
    {
        private readonly ILogger<CA001Controller> _logger;        
        private readonly IContext _ctx;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CA001Controller(ILogger<CA001Controller> logger, IContext ctx, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;                        
            _ctx = ctx;
            _webHostEnvironment = webHostEnvironment;
        }
        
        [HttpGet("Cadastros/CA001/Index")]        
        public IActionResult Index()
        {
            try
            {                

                return View("~/Views/Cadastros/CA001/Index.cshtml");
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
                return PartialView("~/Views/Cadastros/CA001/Grid.cshtml");
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
                return Json(DataTable.GerarTabela(model, _ctx.ctxUsuario.BuscarTodos(model.search.value)));
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }

        }

        public PartialViewResult Form(int usuario_i_id)
        {
            try
            {

                ViewData["usuario_s_status"] = new SelectList(new List<string> { "Ativo", "Inativo" });

                return PartialView("~/Views/Cadastros/CA001/Form.cshtml", _ctx.ctxUsuario.BuscarPorId(usuario_i_id));
            }
            catch (Exception ex)
            {
                return PartialView("~/Views/Shared/_Exception.cshtml", ex);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Form(Usuario obj, IFormFile file)
        {
            try
            {
                var message = "";
                var status = "";

                if (obj.usuario_i_id == 0)
                {
                    obj.usuario_d_created = DateTime.Now;
                    obj.usuario_s_createdby = Services.GetUsuario(User.Identity as ClaimsIdentity).usuario_s_usuario;                    
                }
                else
                {
                    obj.usuario_d_updated = DateTime.Now;
                    obj.usuario_s_updatedby = Services.GetUsuario(User.Identity as ClaimsIdentity).usuario_s_usuario;
                }

                if (file != null)
                {
                    var foto = file;
                    var extension = Path.GetExtension(foto.FileName);

                    if (foto.Length > 0)
                    {
                        obj.usuario_s_foto = obj.usuario_s_usuario + extension;
                        

                        var path = Path.Combine($"{this._webHostEnvironment.WebRootPath}\\images\\perfil", obj.usuario_s_usuario + extension);

                        if (foto != null && foto.Length > 0)
                        {
                            using (var stream = System.IO.File.Create(path))
                            {
                                foto.CopyTo(stream);
                            }
                        }
                    }

                }
                else if (obj.usuario_i_id == 0)
                {
                    obj.usuario_s_foto = "unknown.jpg";
                }

                
                _ctx.ctxUsuario.Salvar(obj, out message, out status);




                return Json(new { status = status, message = message });
            }
            catch (Exception ex)
            {
                return Json(new { status = "error", message = $"Erro: {ex.Message}" });
            }

        }

        [HttpPost]
        public JsonResult Excluir(int usuario_i_id)
        {
            try
            {
                var obj = _ctx.ctxUsuario.BuscarPorId(usuario_i_id);


                obj.usuario_d_archived = DateTime.Now;
                obj.usuario_s_archivedby = Services.GetUsuario(User.Identity as ClaimsIdentity).usuario_s_usuario;

                _ctx.ctxUsuario.Excluir(obj, out string? message, out string? status);

                return Json(new { status, message });
            }
            catch (Exception ex)
            {
                return Json(new { status = "error", message = $"Erro: {ex.Message}" });
            }

        }

        [HttpGet]
        public PartialViewResult Permissoes(int usuario_i_id)
        {
            ViewBag.usuario_i_id = usuario_i_id;

            return PartialView("~/Views/Cadastros/CA001/Permissoes.cshtml");
        }

        public JsonResult PermissoesJson(DataTableAjaxPostModel model, int usuario_i_id)
        {
            try
            {
                return Json(DataTable.GerarTabela(model, _ctx.ctxPermissao.BuscarPorUsuario(usuario_i_id)));
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }

        }

        [HttpGet]
        public JsonResult SetPermissao(int permissao_i_tela, int permissao_i_usuario)
        {
            var permissao = new Permissao
            {
                permissao_i_tela = permissao_i_tela,
                permissao_i_usuario = permissao_i_usuario,
                permissao_d_updated = DateTime.Now,
                permissao_s_updatedby = Services.GetUsuario(User.Identity as ClaimsIdentity).usuario_s_usuario,
        };

            string? message;
            string? status;

            _ctx.ctxPermissao.Salvar(permissao, out message, out status);

            return Json(new { status, message });
        }

        [HttpGet]
        public PartialViewResult PermissoesEspeciais(int usuario_i_id)
        {
            ViewBag.usuario_i_id = usuario_i_id;

            return PartialView("~/Views/Cadastros/CA001/PermissoesEspeciais.cshtml");
        }

        public JsonResult PermissoesEspeciaisJson(DataTableAjaxPostModel model, int usuario_i_id)
        {
            try
            {
                return Json(DataTable.GerarTabela(model, _ctx.ctxPermissaoEspecial.BuscarPorUsuario(usuario_i_id)));
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }

        }

        [HttpGet]
        public JsonResult SetPermissaoEspecial(string permissao_s_permissao, int permissao_i_usuario)
        {
            var permissao = new PermissaoEspecial
            {
                permissao_s_permissao = (EnumPermissaoEspecial)Enum.Parse(typeof(EnumPermissaoEspecial), permissao_s_permissao),
                permissao_i_usuario = permissao_i_usuario,
                permissao_d_updated = DateTime.Now,
                permissao_s_updatedby = Services.GetUsuario(User.Identity as ClaimsIdentity).usuario_s_usuario,
            };

            string? message;
            string? status;

            _ctx.ctxPermissaoEspecial.Salvar(permissao, out message, out status);

            return Json(new { status, message });
        }


    }
}
