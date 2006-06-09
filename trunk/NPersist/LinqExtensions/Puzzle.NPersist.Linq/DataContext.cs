using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Puzzle.NPersist.Framework;

namespace Puzzle.NPersist.Linq
{
    public class DataContext
    {
        protected IContext context;

        public DataContext(string mappingPath)
        {
            this.context = new Context(mappingPath);
            Setup();
        }

        public DataContext(string mappingName,Assembly mappingAssembly)
        {
            this.context = new Context(mappingAssembly,mappingName);
            Setup();
        }

        protected virtual void Setup()
        {
            FieldInfo[] fields = this.GetType ().GetFields ();
            foreach(FieldInfo field in fields)
            {
                if (field.FieldType.Name.StartsWith ("LinqList"))
                {
                    Type genericType = field.FieldType.GetGenericArguments ()[0];

                    ILinqList instance = (ILinqList)Activator.CreateInstance(field.FieldType);
                    instance.AttachContext (this.context);

                    field.SetValue (this,instance);                    
                }
            }
        }

        public virtual void SubmitChanges()
        {
            this.context.Commit ();
        }
    }
}
