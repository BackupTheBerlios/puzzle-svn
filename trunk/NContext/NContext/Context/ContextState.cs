using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Puzzle.NContext.Framework
{
    public class ContextState
    {
        public readonly IList<IObjectInitializer> ObjectFactories = new List<IObjectInitializer>();
        public readonly IDictionary<string, ObjectFactoryInfo> NamedObjectFactories = new Dictionary<string, ObjectFactoryInfo>();
        public readonly IDictionary<Type, ObjectFactoryInfo> TypedObjectFactories = new Dictionary<Type, ObjectFactoryInfo>();
        public readonly IDictionary<string, ObjectConfigurationInfo> NamedObjectConfigurations = new Dictionary<string, ObjectConfigurationInfo>();
        public readonly IDictionary<Type, ObjectConfigurationInfo> TypedObjectConfigurations = new Dictionary<Type, ObjectConfigurationInfo>();
        public readonly IDictionary<Type, ObjectConfigurationInfo> ApplyToAllObjectConfigurations = new Dictionary<Type, ObjectConfigurationInfo>();
        public readonly IDictionary<Type, Type> TypeSubstitutes = new Dictionary<Type, Type>();
    }
}
