using FastMember;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SistemaMonitoramento.Util.Servicos
{
    public static class Email
    {
        public static string Enviar(string emaiL_s_destinatario,string email_s_assunto, string email_s_corpo)
        {

            string email_s_remetente = "contato@garagem41.com";
            string email_s_senha = "Chevette#41";

            SmtpClient client = new SmtpClient("smtp.umbler.com", 587);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(email_s_remetente, email_s_senha);

            MailMessage mensagem = new MailMessage(email_s_remetente, emaiL_s_destinatario, email_s_assunto, email_s_corpo);

            client.Send(mensagem);

            return "";
        }
    }
}