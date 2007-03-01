using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.FastTrack.Framework.Controllers;
using System.Reflection;

namespace Puzzle.FastTrack.Framework.Factories
{
    public class ControllerFactory
    {
        public static IDomainController CreateDomainController()
        {
            string assemblyName = System.Configuration.ConfigurationManager.AppSettings["ControllerAssembly"];
            Assembly controllerAssembly = Assembly.Load(assemblyName);

            string controllerName = System.Configuration.ConfigurationManager.AppSettings["ControllerType"];
            IDomainController controller = (IDomainController)controllerAssembly.CreateInstance(controllerName);

            return controller;
        }
    }
}
