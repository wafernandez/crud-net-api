using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace ServiceRequests.Utilities
{
    public class EmailNotifier : INotifier
    {
        private static readonly string FROM_EMAIL = "";
        private static readonly string TO_EMAIL = "";
        private static readonly string SUBJECT = "Service Request was CLOSED.";
        private static readonly string SMTP_HOST = "smtp.gmail.com";
        private static readonly int SMTP_PORT = 587;
        private static readonly string SMTP_USER = "";
        private static readonly string SMTP_PASSWORD = "";

        public void SendNotification(string messageText)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(FROM_EMAIL);
                    mail.To.Add(TO_EMAIL);
                    mail.Subject = SUBJECT;
                    mail.Body = messageText;
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient(SMTP_HOST, SMTP_PORT))
                    {
                        smtp.Credentials = new NetworkCredential(SMTP_USER, SMTP_PASSWORD);
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
