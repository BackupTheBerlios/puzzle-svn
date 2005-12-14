using System;
using System.Collections;
using System.ComponentModel;

namespace Puzzle.Design
{
	public class FilteredTypeConverter : TypeConverter
	{
		public FilteredTypeConverter() : base()
		{
		}

		protected virtual void FilterProperties(IDictionary Properties, object value)
		{
		}

		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			PropertyDescriptorCollection propps = propps = TypeDescriptor.GetProperties(value, attributes, false);

			Hashtable arr = new Hashtable();
			foreach (PropertyDescriptor pd in propps)
				arr[pd.Name] = pd;

			FilterProperties(arr, value);

			//copy the modified propp arr into a typed propertydescriptor[] 
			PropertyDescriptor[] arr2 = new PropertyDescriptor[arr.Values.Count];
			arr.Values.CopyTo(arr2, 0);

			//return the new propertydescriptorcollection
			return new PropertyDescriptorCollection(arr2);
		}

		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}
}