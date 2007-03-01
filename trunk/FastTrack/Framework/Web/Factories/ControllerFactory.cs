using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.FastTrack.Framework.Web.Controllers;

namespace Puzzle.FastTrack.Framework.Web.Factories
{
    public class ControllerFactory
    {
        public static IDomainController CreateDomainController()
        {
            return new NPersistController();
        }
    }
}
