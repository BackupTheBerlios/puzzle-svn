using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Puzzle.NContext.Framework
{
    public static class ExceptionHelper
    {
        public static Exception NamedConfigurationNotFoundException(string configId)
        {
            return new Exception(string.Format("Named configuration '{0}' was not found", configId));
        }

        public static Exception NamedFactoryNotFoundException(string factoryId)
        {
            return new Exception(string.Format("Named factory '{0}' was not found", factoryId));
        }

        public static Exception NotConfigurationMethod()
        {
            return new Exception("Method is not a configuration method");
        }

        public static Exception NotFactoryMethodException()
        {
            return new Exception("Method is not a factory method");
        }

        public static Exception RegisterObjectTypeMismatchException()
        {
            return new ArgumentException("Parameter 'item' must be assignable to parameter 'objectType'", "item");
        }

        public static Exception TypedConfigurationNotFoundException(Type configType)
        {
            return new Exception(string.Format("Typed configuration '{0}' was not found", configType.Name));
        }

        public static Exception TypedFactoryNotFoundException(Type factoryType)
        {
            return new Exception(string.Format("Typed factory '{0}' was not found", factoryType.Name));
        }

        public static Exception FactoryParameterCountException()
        {
            return new NotSupportedException("Factory methods may not have any parameters");
        }
    }
}
