using System;
using System.Collections.Generic;
using System.Text;

namespace Puzzle.FastTrack.Framework.Controllers
{
    public class TypeControllerBase : ITypeController
    {
        public TypeControllerBase()
        {
        }

        public TypeControllerBase(IDomainController domainController)
        {
            this.domainController = domainController;
        }

        private IDomainController domainController;

        public IDomainController DomainController
        {
            get { return domainController; }
            set { domainController = value; }
        }

        #region ITypeController Members

        public virtual object CreateObject(Type type)
        {
            if (this.domainController != null)
                return this.domainController.ExecuteCreateObject(type);
            return null;
        }

        public virtual void SaveObject(object obj)
        {
            if (this.domainController != null)
                this.domainController.ExecuteSaveObject(obj);
        }

        public virtual void DeleteObject(object obj)
        {
            if (this.domainController != null)
                this.domainController.ExecuteDeleteObject(obj);
        }

        #endregion
    }
}
