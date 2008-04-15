using System;
using System.Collections.Generic;
using System.Text;

namespace AlbinoHorse.Model
{
    public class UmlTypeMemberGroup<T>  where T:UmlTypeMember 
    {
        protected UmlType owner;
        public string Name { get; set; }
        public bool Expanded { get; set; }
        internal readonly object AddNewIdentifier = new object();

        private List<T> members = new List<T>();
        public List<T> Members
        {
            get
            {
                return members;
            }
        }


        public UmlTypeMemberGroup(UmlType owner)
        {
            this.owner = owner;
            members = new List<T>();
            Expanded = true;
        }
    }
}
