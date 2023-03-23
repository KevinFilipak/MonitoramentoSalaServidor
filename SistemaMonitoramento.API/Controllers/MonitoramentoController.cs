using Microsoft.AspNetCore.Mvc;
using SistemaMonitoramento.Model.Class;
using SistemaMonitoramento.Model.IoC;

namespace SistemaMonitoramento.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MonitoramentoController : ControllerBase
    {
        private readonly ILogger<MonitoramentoController> _logger;
        private readonly IContext _ctx;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MonitoramentoController(ILogger<MonitoramentoController> logger, IContext ctx, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _ctx = ctx;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet(Name = "Monitoramento")]
        public string Get(string Temperatura, string Umidade)
        {
            try
            {
                var message = "";
                var status = "";

                var obj = new Monitoramento();

                obj.MONITORAMENTO_F_TEMPERATURA = Convert.ToDouble(Temperatura);
                obj.MONITORAMENTO_F_UMIDADE = Convert.ToDouble(Umidade);
                obj.MONITORAMENTO_D_DATA = DateTime.Now;

                _ctx.ctxMonitoramento.Salvar(obj, out message, out status);

                return "OK";
            }
            catch (Exception ex)
            {
                return $"ERRO;{ex.Message}";
            }
        }
    }
}