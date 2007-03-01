using System;
using System.Collections.Generic;
using System.Text;

namespace AdvancedConfig
{
    public class MyMailer : IMailer
    {
        public void SendMail(string recipients, string subject, string body)
        {
            Console.WriteLine("Pretend that we send a mail now");
            Console.WriteLine("to {0} subject {1} body {2} via server {3}",recipients,subject,body,SmtpServer);
            
        }

        #region Property SmtpServer
        private string smtpServer;
        public virtual string SmtpServer
        {
            get
            {
                return this.smtpServer;
            }
            set
            {
                this.smtpServer = value;
            }
        }
        #endregion
    }
}
