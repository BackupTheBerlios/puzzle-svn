using System;
using System.Collections.Generic;
using System.Text;

namespace AlbinoHorse.Model
{
    public class DefaultUmlInstanceTypeData : IUmlInstanceTypeData
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public bool Expanded { get; set; }
        public string TypeName { get; set; }
        public string InheritsTypeName { get; set; }
        public bool IsAbstract { get; set; }

        private List<UmlTypeMember> members = new List<UmlTypeMember>(); 

        public void RemoveTypeMember(UmlTypeMember property)
        {
            members.Remove(property);
        }

        public IList<UmlTypeMember> GetTypeMembers()
        {
            return members;
        }

        public UmlTypeMember CreateTypeMember(string sectionName)
        {
            UmlTypeMember member = new UmlTypeMember();
            DefaultUmlTypeMemberData data = new DefaultUmlTypeMemberData();
            data.SectionName = sectionName;
            member.DataSource = data;            
            members.Add(member);
            return member;
        }       
    }
}
