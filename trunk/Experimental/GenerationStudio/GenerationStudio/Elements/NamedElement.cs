using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenerationStudio.Elements
{
    [Serializable]
    public abstract class NamedElement : Element
    {
        private string name;
        public virtual string Name 
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnNotifyChange();
            }
        }

        public override string GetDisplayName()
        {
            return Name;
        } 
    }

    
}
