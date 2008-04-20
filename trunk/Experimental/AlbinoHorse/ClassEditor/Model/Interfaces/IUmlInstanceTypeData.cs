using System;
using System.Collections.Generic;
using System.Text;

namespace AlbinoHorse.Model
{
    public interface IUmlInstanceTypeData : IUmlTypeData
    {
        

        UmlTypeMember CreateTypeMember(string sectionName);
        void RemoveTypeMember(UmlTypeMember member);
        IList<UmlTypeMember> GetTypeMembers();
    }
}
