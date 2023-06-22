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

                var dispositivo = _ctx.ctxDispositivo.BuscarPorId(obj.monitoramento_i_dispositivo);

                if (obj.monitoramento_f_temperatura >=  24 || obj.monitoramento_f_umidade >= 80)
                {
                    var emaiL_s_destinatario = "kevinaguiar09@gmail.com";
                    var email_s_assunto = "[SDM] Alerta";
                    var email_s_corpo = "<html><body><p><span style=\"font-size:24px\"><strong><font color=blue>Sistema de Monitoramento</font></strong></span></p>\r\n\r\n<p><strong>Temperatura Atual: </strong>" + obj.monitoramento_f_temperatura + "<strong>&deg; C</strong></p>\r\n\r\n<p><strong>Umidade Atual: " + obj.monitoramento_f_umidade + " %</strong></p>\r\n\r\n<p>&nbsp;</p>\r\n\r\n<p><strong>Data e Hora da Leitura: " + obj.monitoramento_d_data + "</strong></p>\r\n</html></body>";
                    email_s_corpo = "<h1><span style=\"color:#0000FF\"><strong>Sistema de Monitoramento - </strong>" + dispositivo.dispositivo_s_dispositivo + "</span></h1>\r\n\r\n<p>&nbsp;</p>\r\n\r\n<p><strong><span style=\"font-size:16px\">Temperatura Atual: " + obj.monitoramento_f_temperatura + "&deg; C</span></strong></p>\r\n\r\n<p><strong><span style=\"font-size:16px\">Umidade Atual: " + obj.monitoramento_f_umidade + " %</span></strong></p>\r\n\r\n<p><strong><span style=\"font-size:16px\">Data e Hora da Leitura: " + obj.monitoramento_d_data + "</span></strong></p>\r\n";
                    Email.Enviar(emaiL_s_destinatario, email_s_assunto, email_s_corpo);
                }

                return "OK";
            }
            catch (Exception ex)
            {
                return $"ERRO;{ex.Message}";
            }
        }
    }
}