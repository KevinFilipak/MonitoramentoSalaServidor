﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
                return Json(DataTable.GerarTabela(model, _ctx.ctxDispositivo.BuscarTodos(model.search.value)));
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }

        }

        public PartialViewResult Form(int dispositivo_i_id)
        {
            try
            {
                ViewData["dispositivo_s_status"] = new SelectList(new List<string> { "Ativo", "Inativo" });

                return PartialView("~/Views/Cadastros/CA002/Form.cshtml", _ctx.ctxDispositivo.BuscarPorId(dispositivo_i_id));
            }
            catch (Exception ex)
            {
                return PartialView("~/Views/Shared/_Exception.cshtml", ex);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Form(Dispositivo obj)
        {
            try
            {
                var message = "";
                var status = "";

                if (obj.dispositivo_i_id == 0)
                {
                    obj.dispositivo_d_created = DateTime.Now;
                    obj.dispositivo_s_createdby = Services.GetUsuario(User.Identity as ClaimsIdentity).usuario_s_usuario;
                }
                else
                {
                    obj.dispositivo_d_updated = DateTime.Now;
                    obj.dispositivo_s_updatedby = Services.GetUsuario(User.Identity as ClaimsIdentity).usuario_s_usuario;
                }

                _ctx.ctxDispositivo.Salvar(obj, out message, out status);

                return Json(new { status = status, message = message });
            }
            catch (Exception ex)
            {
                return Json(new { status = "error", message = $"Erro: {ex.Message}" });
            }

        }

        [HttpPost]
        public JsonResult Excluir(int dispositivo_i_id)
        {
            try
            {
                var obj = _ctx.ctxDispositivo.BuscarPorId(dispositivo_i_id);

                var message = "";
                var status = "";

                obj.dispositivo_d_archived = DateTime.Now;
                obj.dispositivo_s_archivedby = Services.GetUsuario(User.Identity as ClaimsIdentity).usuario_s_usuario;

                _ctx.ctxDispositivo.Excluir(obj, out message, out status);

                return Json(new { status = status, message = message });
            }
            catch (Exception ex)
            {
                return Json(new { status = "error", message = $"Erro: {ex.Message}" });
            }

        }

        public JsonResult Codigo(int dispositivo_i_id)
        {
            try
            {
                //        var obj = _ctx.ctxDispositivo.BuscarPorId(dispositivo_i_id);
                //        string Caminho = Path.Combine(Server.MapPath("~/Anexos/Cadastros/Codigo/Codigo.ino"));
                //        string CaminhoDownload = $"~/Export/{Guid.NewGuid()}/";
                //        string NomeArquivo = $"Dispositivo_{obj.dispositivo_s_codigo}.ino";
                //        _ctx.ctxDispositivo.GerarCodigo(obj, Caminho, Server.MapPath(CaminhoDownload), NomeArquivo);

                var message = "Arquivo baixado com sucesso!";
                var status = "success";

                return Json(new { status = status, message = message });
            }
            catch (Exception ex)
            {
                return Json(new { status = "error", message = $"Erro: {ex.Message}" });
            }

        }
    }
}
