using System;
using System.Collections.Generic;
using System.Text;

namespace AdvancedConfig
{
    public class SomeClass
    {
        private IMailer mailer;

        public SomeClass(IMailer mailer)
        {
            if (mailer == null)
                throw new ArgumentNullException("mailer");

            this.mailer = mailer;            
        }

        //dumb method using our injected resources
        public void DoStuff()
        {
            if (LogManager != null)

                LogManager.Log("Inside SomeClass.DoStuff()");

            this.mailer.SendMail("info@puzzleframework.com", "hello", "hello NFactory");            
        }

        // some property path that is not DI enabled, it creates its own objects
        #region Property Some        
        private Some some = new Some ();
        public virtual Some Some
        {
            get
            {
                return this.some;
            }
        }
        #endregion

        #region Property LogManager
        private LogManager logManager;
        public virtual LogManager LogManager
        {
            get
            {
                return this.logManager;
            }
            set
            {
                this.logManager = value;
            }
        }
        #endregion
    }
}
