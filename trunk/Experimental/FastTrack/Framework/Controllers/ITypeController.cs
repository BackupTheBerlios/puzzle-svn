using System;
using System.Collections.Generic;
using System.Text;

namespace Puzzle.FastTrack.Framework.Controllers
{
    public interface ITypeController
    {
        IDomainController DomainController { get; set; }
 
        object CreateObject(Type type);

        void SaveObject(object obj);

        void DeleteObject(object obj);
    }
}
