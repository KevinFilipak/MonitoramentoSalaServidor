using AspNetCore.Reporting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaMonitoramento.Model.Class;
using SistemaMonitoramento.Model.IoC;
using SistemaMonitoramento.Util.Servicos;

namespace SistemaMonitoramento.Web.Controllers.Home
{

    [Authorize]
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
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }


        [Route("")]
        [HttpGet("Home/Dashboard/Index")]        
        public IActionResult Index()
        {
            var obj = _ctx.ctxDashboard.BuscarTodos("");

            ViewBag.Objetos = obj;

            return View("~/Views/Home/Dashboard/Index.cshtml") ;
        }        

        [HttpGet("Home/Dashboard/Error")]
        public IActionResult Error()
        {
            var teste = 1 / (Convert.ToDouble(0));

            return View("~/Views/Home/Dashboard/Error.cshtml");
        }

        [Route("PageNotFound")]
        [HttpGet("Home/Dashboard/PageNotFound")]
        public IActionResult PageNotFound()
        {
            string originalPath = "unknown";
            
            if (HttpContext.Items.ContainsKey("originalPath"))
            {
                originalPath = HttpContext.Items["originalPath"] as string;
            }

            return View("~/Views/Home/Dashboard/PageNotFound.cshtml");
        }

        [HttpGet("Denied")]
        public IActionResult Denied()
        {
            return View("~/Views/Home/Dashboard/Denied.cshtml");
        }

        [HttpGet("Home/Dashboard/Visualizar")]
        public ActionResult Visualizar()
        {
            var obj = _ctx.ctxDashboard.BuscarTodos("");

            ViewBag.Objetos = obj;

            return PartialView("~/Views/Home/Dashboard/Render.cshtml");
        }


        [HttpGet("Home/Dashboard/Print")]
        public IActionResult Print()
        {
            string mimType = "";
            int extension = 1;
            var path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\Report2.rdlc";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            LocalReport localReport = new LocalReport(path);

            var _usuarios = new List<Usuario>()
            {
                new Usuario{
                    usuario_s_usuario = "admin",
                    usuario_s_nome = "nome",
                    usuario_s_email = "email"
                }
            };

            localReport.AddDataSource("DataSet1", _usuarios);


            var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimType);
            return File(result.MainStream, "application/pdf");
        }

    }
}
