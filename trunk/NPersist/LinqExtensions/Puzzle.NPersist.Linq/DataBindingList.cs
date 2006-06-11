using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Puzzle.NPersist.Framework;
using System.Reflection;

namespace Puzzle.NPersist.Linq
{
    public class EntityBindingList<T> : BindingList<T>
    {
        private IContext context;
        public EntityBindingList(IList<T> fromItems, IContext context)
        {
            FieldInfo field = this.GetType ().BaseType.GetField ("raiseItemChangedEvents",BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetField);
            field.SetValue (this,true);

            this.AddingNew += new AddingNewEventHandler(DataBindingList_AddingNew);

            this.context = context;
            foreach (T item in fromItems)
            {
                this.Add(item);
            }
        }

        void DataBindingList_AddingNew(object sender, AddingNewEventArgs e)
        {
            e.NewObject = context.CreateObject<T>();
        }

        protected override void RemoveItem(int index)
        {
            T entity = this[index];
            base.RemoveItem(index);
            context.DeleteObject(entity);
        }

        protected override bool SupportsSearchingCore
        {
            get
            {
                return false;
            }
        }
        protected override bool SupportsSortingCore
        {
            get
            {
                return false;
            }
        }
    }
}
