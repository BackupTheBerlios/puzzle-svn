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

        public readonly IDictionary<string, object> namedPerContextObjects = new Dictionary<string, object>();
        public readonly IDictionary<Type, object> typedPerContextObjects = new Dictionary<Type, object>();

        [ThreadStatic]
        private static IDictionary<string, object> _namedPerGraphObjects = new Dictionary<string, object>();
        [ThreadStatic]
        private static IDictionary<Type, object> _typedPerGraphObjects = new Dictionary<Type, object>();
        [ThreadStatic]
        private static IDictionary<string, object> _namedPerThreadObjects = new Dictionary<string, object>();
        [ThreadStatic]
        private static IDictionary<Type, object> _typedPerThreadObjects = new Dictionary<Type, object>();

        [ThreadStatic]
        private static Stack<ObjectFactoryInfo> _configStack = new Stack<ObjectFactoryInfo>();

        public Stack<ObjectFactoryInfo> configStack
        {
            get
            {
                if (ContextState._configStack == null)
                    ContextState._configStack = new Stack<ObjectFactoryInfo>();

                return _configStack;
            }
        }

        public IDictionary<string, object> namedPerGraphObjects
        {
            get
            {
                if (ContextState._namedPerGraphObjects == null)
                    ContextState._namedPerGraphObjects = new Dictionary<string, object>();

                return ContextState._namedPerGraphObjects;
            }
        }

        public IDictionary<Type, object> typedPerGraphObjects
        {
            get
            {
                if (ContextState._typedPerGraphObjects == null)
                    ContextState._typedPerGraphObjects = new Dictionary<Type, object>();

                return ContextState._typedPerGraphObjects;
            }
        }

        public IDictionary<string, object> namedPerThreadObjects
        {
            get
            {
                if (ContextState._namedPerThreadObjects == null)
                    ContextState._namedPerThreadObjects = new Dictionary<string, object>();

                return ContextState._namedPerThreadObjects;
            }
        }

        public IDictionary<Type, object> typedPerThreadObjects
        {
            get
            {
                if (ContextState._typedPerThreadObjects == null)
                    ContextState._typedPerThreadObjects = new Dictionary<Type, object>();

                return ContextState._typedPerThreadObjects;
            }
        }
    }
}
