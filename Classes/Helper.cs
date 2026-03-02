using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace M17BE_Loja_equipamentos.Classes
{
    public static class Helper
    {
        public static void enviarMail(string nomeDe, string passwordDe,
            string para, string assunto, string texto, string anexo = null)
        {
            //objetos mail
            System.Net.Mail.MailMessage mensagem = new System.Net.Mail.MailMessage();
            System.Net.NetworkCredential credenciais = new System.Net.NetworkCredential(nomeDe, passwordDe);
            System.Net.Mail.MailAddress dequem = new System.Net.Mail.MailAddress("exemplo@exemplo.com");// nomeDe);
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();

            //mensagem
            mensagem.To.Add(para);
            mensagem.From = dequem;
            mensagem.Subject = assunto;
            mensagem.Body = texto;
            mensagem.IsBodyHtml = true;
           
            //servidor
            string servidor = ConfigurationManager.AppSettings["servidor_smtp"];
            int porta = int.Parse(ConfigurationManager.AppSettings["porta"]);
            smtp.Host = servidor;
            smtp.Port = porta;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = credenciais;

            //anexo
            if (anexo != null && anexo != "")
            {
                if (System.IO.File.Exists(anexo) == true)
                {
                    System.Net.Mail.Attachment ficheiroAnexo = new System.Net.Mail.Attachment(anexo);
                    mensagem.Attachments.Add(ficheiroAnexo);
                }
            }
            //enviar
            smtp.Send(mensagem);
        }
    }

}