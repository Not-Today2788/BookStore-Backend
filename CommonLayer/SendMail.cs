using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace CommonLayer
{
    public class SendMail
    {
        public string SendMailtoUser(string ToEmail, string Token)
        {
            string FromEmail = "prakhar2788@gmail.com";
            MailMessage Message = new MailMessage(FromEmail, ToEmail);
            string MailBody = $"Token to Reset Password: <a href=http://localhost:4200/reset/{Token}> clickhere</a>";
            Message.Subject = "Token Generated for resetting password";
            Message.Body = MailBody.ToString();
            Message.BodyEncoding = Encoding.UTF8;
            Message.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            NetworkCredential credential = new NetworkCredential(FromEmail, "jftw fsfq fmhf neej");

            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = credential;

            smtpClient.Send(Message);
            return ToEmail;
        }
    }
}
