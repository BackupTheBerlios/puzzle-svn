using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.FastTrack.WebForms.Framework.Controllers;

namespace Puzzle.FastTrack.WebForms.Framework.Factories
{
    public class ControllerFactory
    {
        public static IDomainController CreateDomainController()
        {
            return new NPersistController();
        }
    }
}
