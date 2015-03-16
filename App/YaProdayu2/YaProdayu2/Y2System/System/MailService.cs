using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mail;

namespace YaProdayu2.Y2System
{
    public class MailService
    {
        public void Send(MailServiceParams mailParams)
        {
            var mailMessage = new MailMessage();

            mailMessage.From = mailParams.From;
            mailMessage.To = mailParams.To;

            mailMessage.Subject = mailParams.Title;
            mailMessage.Body = mailParams.Body;

            foreach(var param in mailParams.Params)
            {
                mailMessage.Body.Replace(param.Key, param.Value);
            }

            SmtpMail.SmtpServer = mailParams.SmtpServer;
            SmtpMail.Send(mailMessage);
        }
    }

    public class MailServiceParams
    {
        public string From { get; set; }

        public string To { get; set; }

        public string Title { get; set; }

        public string Key { get; set; }

        public string Body { get; set; }

        public Dictionary<string, string> Params { get; set; }

        public string MailLogin { get; set; }

        public string Password { get; set; }

        public string SmtpServer { get; set; }
    }
}