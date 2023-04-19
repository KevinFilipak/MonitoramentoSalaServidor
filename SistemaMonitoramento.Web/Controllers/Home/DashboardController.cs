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
            var obj = new List<Dashboard>();

            var item = new Dashboard();
            item.dashboard_s_dispositivo = "Sala do Servidor";
            item.dashboard_s_temperatura = "25 °C";
            item.dashboard_s_umidade = "25 %";

            var item2 = new Dashboard();
            item2.dashboard_s_dispositivo = "CPD 02";
            item2.dashboard_s_temperatura = "22 °C";
            item2.dashboard_s_umidade = "32 %";


            var item3 = new Dashboard();
            item3.dashboard_s_dispositivo = "CPD 03";
            item3.dashboard_s_temperatura = "18 °C";
            item3.dashboard_s_umidade = "67 %";

            obj.Add(item);
            obj.Add(item2);
            obj.Add(item3);

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
