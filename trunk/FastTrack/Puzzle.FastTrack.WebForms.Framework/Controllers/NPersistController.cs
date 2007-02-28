using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.NPersist.Framework;

namespace Puzzle.FastTrack.WebForms.Framework.Controllers
{
    public class NPersistController : DomainControllerBase
    {
        public NPersistController()
        {
            string mapPath = System.Configuration.ConfigurationManager.AppSettings["MapPath"];
            string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];

            context = new Context(mapPath);
            context.SetConnectionString(connectionString);
        }

        private IContext context;
        public virtual IContext Context
        {
            get { return context; }
            set { context = value; }
        }
	

        public override object GetObjectByIdentity(object identity, Type type)
        {
            return context.GetObjectById(identity, type); 
        }

        public override object CreateObject(Type type)
        {
            return context.CreateObject(type);
        }

        public override void SaveObject(object obj)
        {
            context.Commit();
        }

        public override void DeleteObject(object obj)
        {
            context.DeleteObject(obj);
            context.Commit();
        }

        public override void Dispose()
        {
            base.Dispose();

            if (this.context != null)
                this.context.Dispose();
        }
    }
}
