using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;


namespace AlbinoHorse.Model
{
    public class UmlTypeMemberSection
    {
        public readonly object CaptionIdentifier = new object();
        public readonly object ExpanderIdentifier = new object();
        public readonly object AddNewIdentifier = new object();
        public UmlInstanceType Owner { get; set; }
        public string Name { get; set; }
        public bool Expanded { get; set; }


        public IList<UmlTypeMember> TypeMembers
        {
            get
            {
                var members = new List<UmlTypeMember>();
                foreach (var member in Owner.TypeMembers)
                {
                    if (member.SectionName == Name)
                        members.Add(member);
                }

                return members;
            }
        }

        public UmlTypeMemberSection(UmlInstanceType owner)
        {
            Expanded = true;
            Owner = owner;
        }

        public UmlTypeMemberSection(UmlInstanceType owner,string caption) : this(owner)
        {
            Name = caption;            
        }
    }
}
