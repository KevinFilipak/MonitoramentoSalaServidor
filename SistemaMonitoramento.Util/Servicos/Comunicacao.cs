using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace SistemaMonitoramento.Util.Servicos
{
    public class Comunicacao
    {
        public void EnviarSMS(string temperatura, string umidade)
        {
            try
            {
                var accountSid = "ACa507288b0ee6e91a33e9e1751e80ace5";
                var authToken = "75588178a1149258a5acd7b20ffcb696";
                TwilioClient.Init(accountSid, authToken);

                var messageOptions = new CreateMessageOptions(
                new PhoneNumber("+5541991214268"));
                messageOptions.From = new PhoneNumber("+14344438451");
                messageOptions.Body = "-\n\nTeste de Envio de SMS \n- Temperatura: " + temperatura + "\n- Umidade: " + umidade;

                var message = MessageResource.Create(messageOptions);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void EnviarEmail()
        {

        }
    }
}
