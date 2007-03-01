using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.FastTrack.Framework.Controllers;

namespace Puzzle.FastTrack.Framework.Factories
{
    public class ControllerFactory
    {
        public static IDomainController CreateDomainController()
        {
            return null; // new NPersistController();
        }
    }
}
