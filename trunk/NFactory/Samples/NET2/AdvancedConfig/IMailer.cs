using System;
using System.Collections.Generic;
using System.Text;

namespace AdvancedConfig
{
    public interface IMailer
    {
        void SendMail(string recipients, string subject, string body);

        string SmtpServer
        {
            get;
            set;
        }
    }
}
