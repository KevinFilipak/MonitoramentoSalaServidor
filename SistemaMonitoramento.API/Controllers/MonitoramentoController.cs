using Microsoft.AspNetCore.Mvc;
using SistemaMonitoramento.Model.Class;
using SistemaMonitoramento.Model.IoC;
using SistemaMonitoramento.Util.Servicos;

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
        public string Get(string Dispositivo, string Temperatura, string Umidade)
        {
            try
            {
                var message = "";
                var status = "";

                var obj = new Monitoramento();

                obj.monitoramento_f_temperatura = Convert.ToDouble(Temperatura);
                obj.monitoramento_i_dispositivo = Convert.ToInt32(Dispositivo);
                obj.monitoramento_f_umidade = Convert.ToDouble(Umidade);
                obj.monitoramento_d_data = DateTime.Now;

                _ctx.ctxMonitoramento.Salvar(obj, out message, out status);

                var enviarSMS = new Comunicacao();

                //Comunicacao.EnviarSMS(Temperatura, Umidade);

                return "OK";
            }
            catch (Exception ex)
            {
                return $"ERRO;{ex.Message}";
            }
        }
    }
}