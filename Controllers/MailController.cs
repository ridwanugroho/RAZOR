using System;
using MailKit.Net.Smtp;
using MimeKit;

namespace belajarRazor.Controllers
{
    public class MailController
    {
        public static int sendMail(string sender, string receiver, string subject="", string body="")
        {
            var message = new MimeMessage();
            var _body = new BodyBuilder();
            
            message.From.Add(new MailboxAddress("enc-toko-aneh", sender));
            message.To.Add(new MailboxAddress("enc-toko-aneh", receiver));
            
            message.Subject = subject;
            _body.HtmlBody = body;

            message.Body = _body.ToMessageBody();

            using(var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                
                client.Connect("smtp.mailtrap.io", 587, false);
                client.Authenticate("b6a962e816b68d", "f846ddc6b49ade");
                client.Send(message);
                client.Disconnect(true);

            }
            
            return 0;
        }

        private static string bodyFormatter(string body)
        {
            return "";
        }
    }
}