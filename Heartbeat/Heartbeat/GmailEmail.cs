using System;
using System.Net;
using System.Net.Mail;

namespace Heartbeat
{
    class GmailEmail
    {
        public static void SendEmail()
        {
            try
            {
                var fromAddress = new MailAddress("612WGeneseeHeartbeat@gmail.com", "612WGenesee");
                var toAddress = new MailAddress("pvishayan@gmail.com", "Patrick");
                const string fromPassword = "fes15043";
                const string subject = "Heartbeat";
                string body = DateTime.Now.ToString();

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
            }
            catch (Exception ex)
            {
                Environment.ExitCode = 55;
                Program.PreserveStackTrace(ex);
                throw;
            }
        }
    }
}
