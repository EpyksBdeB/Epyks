using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace Epyks.Application
{
    internal class EmailManager
    {
        private static EmailManager instance = new EmailManager();

        private SmtpClient SmtpServer;

        internal static EmailManager GetInstance(){
            return instance;
        }

        private EmailManager(){
            SmtpServer = new SmtpClient("smtp.live.com");
            SmtpServer.Port = 587;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential("epyks_ogc@hotmail.com", "EpyksEpyks");
            SmtpServer.EnableSsl = true;
        }

        internal bool EnvoyerEmail(string password, string emailDest){
            SmtpFailedRecipientException exception = null;
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("epyks_ogc@hotmail.com");
            mail.To.Add(emailDest);
            mail.Subject = "Epyks Account Recover Password";
            mail.IsBodyHtml = true;
            string htmlBody;
            htmlBody = "You password for you Epyks account is : " + password;
            mail.Body = htmlBody;
            try
            {
                SmtpServer.Send(mail);
            }
            catch (SmtpFailedRecipientException ex)
            {
                exception = ex;
            }
            return exception == null;
        }
    }
}
