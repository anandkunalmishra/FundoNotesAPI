using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Common_Layer.Utility
{
    public class Send
    {
        public string SendMail(string ToEmail, string Token)
        {
            string FromEmail = "am9233@srmist.edu.in";
            MailMessage Message = new MailMessage(FromEmail, ToEmail);
            string MailBody = "Token Generated : " + Token;
            Message.Subject = "Token Generated For Resetting Password";
            Message.Body = MailBody.ToString();
            Message.BodyEncoding = Encoding.UTF8;
            Message.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            NetworkCredential credential
                = new NetworkCredential(FromEmail, "kgfb nypp nsbk sznv");

            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = credential;

            smtp.Send(Message);
            return ToEmail;
        }
    }
}