using System;
using System.ComponentModel;
using System.Globalization;
using System.Web;

namespace Puzzle.NFactory.Framework.TypeConverters
{
	public class WebPathTypeConverter : StringConverter
	{
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (destinationType == typeof (string))
				return true;

			return base.CanConvertTo(context, destinationType);
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string path = (string) value;
			return HttpContext.Current.Server.MapPath(path);
		}
	}
}