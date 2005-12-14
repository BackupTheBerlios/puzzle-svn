using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace Puzzle.NFactory.Framework.TypeConverters
{
	public class AssemblyTypeConverter : StringConverter
	{
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (destinationType == typeof (Assembly))
				return true;

			return base.CanConvertTo(context, destinationType);
		}


		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string assemblyName = (string) value;
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			foreach (Assembly assembly in assemblies)
			{
				if (assembly.GetName().Name == assemblyName)
					return assembly;
			}

			throw new Exception(string.Format("Assembly '{0}' was not found", assemblyName));
		}
	}
}