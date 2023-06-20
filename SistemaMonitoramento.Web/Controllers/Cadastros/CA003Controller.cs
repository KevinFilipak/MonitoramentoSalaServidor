using AspNetCore.Reporting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaMonitoramento.Model.Class;
using SistemaMonitoramento.Model.IoC;
using SistemaMonitoramento.Web.Models;
using System.Data;
using System.Security.Claims;

namespace SistemaMonitoramento.Web.Controllers.Cadastros
{

    [Authorize(Roles = "CA003")]
    public class CA003Controller : Controller
    {
        private readonly ILogger<CA003Controller> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IContext _ctx;

        public CA003Controller(ILogger<CA003Controller> logger, IWebHostEnvironment webHostEnvironment, IContext ctx)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
            _ctx = ctx;

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

        }

        [HttpGet("Cadastros/CA003/Index")]
        public IActionResult Index()
        {
            try
            {
                ViewData["formato"] = new SelectList(new List<string> { "PDF", "Excel" });

                var dispositivos = _ctx.ctxDispositivo.BuscarTodos("");

                ViewData["dispositivo_i_dispositivo"] = new SelectList(dispositivos, "dispositivo_i_id", "dispositivo_s_dispositivo");

                return View("~/Views/Cadastros/CA003/Index.cshtml");
            }
            catch (Exception ex)
            {
                return View("~/Views/Home/Dashboard/Error.cshtml", ex);
            }
        }

        public IActionResult GerarDados(int? dispositivo_i_dispositivo, string data_inicial, string data_final, string formato)
        {
            try
            {

                string _report = "rpt_RL002";

                var _parametros = new Dictionary<KeyValuePair<string, string>, DbType>
                {
                    { new KeyValuePair<string, string>("@dispositivo_i_dispositivo", dispositivo_i_dispositivo.ToString()), DbType.Int32 },
                    { new KeyValuePair<string, string>("@data_inicial", DateTime.Parse(data_inicial).ToString("yyyy-MM-dd")), DbType.DateTime },
                    { new KeyValuePair<string, string>("@data_final", DateTime.Parse(data_final).ToString("yyyy-MM-dd")), DbType.DateTime },
                };

                var _ds = _ctx.ctxRelatorio.BuscarDados(Model.Enumerators.EnumStoreProcedures.sp_Report_CA003, _parametros);


                if (formato == "PDF")
                {
                    var _result = ReportModel.GerarRelatorio(RenderType.Pdf, _report, _ds, _webHostEnvironment);
                    return File(_result.MainStream, "application/pdf");
                }
                else
                {
                    var _result = ReportModel.GerarRelatorio(RenderType.ExcelOpenXml, _report, _ds, _webHostEnvironment);
                    return File(_result.MainStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "RL_001.xlsx");

                }

            }
            catch (Exception ex)
            {
                return View("~/Views/Home/Dashboard/Error.cshtml", ex);
            }
        }

    }
}
