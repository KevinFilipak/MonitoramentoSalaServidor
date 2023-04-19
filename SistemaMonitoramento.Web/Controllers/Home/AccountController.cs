using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using SistemaMonitoramento.Web.Models;
using SistemaMonitoramento.Model.Class;
using SistemaMonitoramento.Model.IoC;
using SistemaMonitoramento.Util.Crypto;

namespace SistemaMonitoramento.Web.Controllers.Home
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IContext _ctx;        

        public AccountController(ILogger<AccountController> logger, IContext ctx)
        {
            _logger = logger;
            _ctx = ctx;
        }

        [AllowAnonymous]
        [HttpGet("Home/Account/Login")]
        public IActionResult Login(string returnUrl)
        {
            HttpContext.SignOutAsync();

            if (returnUrl == null)
            {
                returnUrl = "";
            }

            var usuario = new Usuario
            {
                usuario_s_pagina_anterior = returnUrl,
                usuario_s_lembrar_me = true
            };

            return View("~/Views/Home/Account/Login.cshtml");            
        }

        [AllowAnonymous]
        [HttpPost("Home/Account/Login")]
        public ActionResult Login(Usuario usuario)
        {

            try
            {
                _ctx.ctxUsuario.Autenticar(usuario);

                if (usuario.usuario_i_id > 0)
                {
                    //HttpContext.Session.Set(EnumSession.Usuario.ToString(), Converter.ObjectToByteArray(usuario));

                    var claims = new List<Claim>();

                    claims.Add(new Claim("usuario_i_id", usuario.usuario_i_id.ToString()));
                    claims.Add(new Claim("usuario_s_usuario", usuario.usuario_s_usuario));
                    claims.Add(new Claim("usuario_s_email", usuario.usuario_s_email));
                    claims.Add(new Claim("usuario_s_nome", usuario.usuario_s_nome));
                    claims.Add(new Claim("usuario_s_foto", usuario.usuario_s_foto));
                    claims.Add(new Claim("usuario_s_funcao", usuario.usuario_s_funcao));

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var _permissoes = _ctx.ctxPermissao.BuscarTodos(usuario.usuario_i_id);

                    _permissoes.ForEach(a =>
                    {
                        claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, a.permissao_s_controller));
                        claimsIdentity.AddClaim(new Claim("Tela", a.permissao_s_titulo));
                    });
                    

                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    

                    HttpContext.SignInAsync(claimsPrincipal);

                    if (Url.IsLocalUrl(usuario.usuario_s_pagina_anterior) && usuario.usuario_s_pagina_anterior.Length > 1 && usuario.usuario_s_pagina_anterior.StartsWith("/")
                                                    && !usuario.usuario_s_pagina_anterior.StartsWith("//") && !usuario.usuario_s_pagina_anterior.StartsWith("/\\"))
                    {
                        return Redirect(usuario.usuario_s_pagina_anterior);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Dashboard");
                    }
                }
                else
                {
                    TempData["error_message"] = $"Usuário ou Senha incorretos!";
                    return View("~/Views/Home/Account/Login.cshtml", usuario);
                }

            }
            catch (Exception ex)
            {
                TempData["error_message"] = $"Erro: {ex.Message}";
                return View("~/Views/Home/Account/Login.cshtml", usuario);
            }            
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        public IActionResult Profile()
        {            
            return View("~/Views/Home/Account/Profile.cshtml", Services.GetUsuario(User.Identity as ClaimsIdentity));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public JsonResult ChangePassword(Usuario obj, IFormFile file)
        {
            try
            {
                var message = "";
                var status = "";

                obj.usuario_i_id = Services.GetUsuario(User.Identity as ClaimsIdentity).usuario_i_id;
                obj.usuario_d_updated = DateTime.Now;
                obj.usuario_s_updatedby = Services.GetUsuario(User.Identity as ClaimsIdentity).usuario_s_usuario;

                if (obj.usuario_s_senha != obj.usuario_s_confirmar_senha)
                {
                    return Json(new { status = "error", message = $"As senhas não conferem!" });
                }

                _ctx.ctxUsuario.AlterarSenha(obj, out message, out status);

                return Json(new { status = status, message = message });
            }
            catch (Exception ex)
            {
                return Json(new { status = "error", message = $"Erro: {ex.Message}" });
            }

        }
    }
}
