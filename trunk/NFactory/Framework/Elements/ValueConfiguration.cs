using System;
using System.ComponentModel;

namespace Puzzle.NFactory.Framework.ConfigurationElements
{
	public class ValueConfiguration : IValueConfiguration
	{
		#region Public Property Value

		private object value;

		public object Value
		{
			get { return this.value; }
			set { this.value = value; }
		}

		#endregion

		#region Public Property TypeConverter

		private TypeConverter typeConverter;

		public TypeConverter TypeConverter
		{
			get { return this.typeConverter; }
			set { this.typeConverter = value; }
		}

		#endregion

		public object Invoke(IContainer owner, Type requestedType, InstanceMode instanceMode)
		{
			if (TypeConverter != null && value is string)
			{
				string typeString = (string) this.Value;
				return TypeConverter.ConvertFromString(typeString);
			}

			if (requestedType.IsEnum && Value is string)
			{
				return Enum.Parse(requestedType, Value.ToString());
			}
			else
			{
				return Convert.ChangeType(Value, requestedType);
			}
		}
	}
}