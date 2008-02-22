using System;
using System.ComponentModel;
using Puzzle.NPresent.Framework.ViewModel;
// *
// * Copyright (C) 2005 Mats Helander
// *
// * This library is free software; you can redistribute it and/or modify it
// * under the terms of the GNU Lesser General Public License 2.1 or later, as
// * published by the Free Software Foundation. See the included license.txt
// * or http://www.gnu.org/copyleft/lesser.html for details.
// *
// *
namespace Puzzle.NPresent.Framework.Wrapping
{
	/// <summary>
	/// Summary description for WrappedObject.
	/// </summary>
	public class WrappedObject : ViewObject, IWrappedObject
	{
		public WrappedObject(object obj, ClassView classView) : base(classView)
		{
			this.obj = obj;
		}

		private object obj;
		
		public object GetObject()
		{
			return obj;
		}

		public AttributeCollection GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this.obj);
		}

		public string GetClassName()
		{
			return TypeDescriptor.GetClassName(this.obj);
		}

		public string GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this.obj);
		}

		public TypeConverter GetConverter()
		{
			return TypeDescriptor.GetConverter(this.obj);
		}

		public EventDescriptor GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(this.obj);
		}

		public PropertyDescriptor GetDefaultProperty()
		{
			return TypeDescriptor.GetDefaultProperty(this.obj);
		}

		public object GetEditor(Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(this.obj,  editorBaseType);
		}

		public EventDescriptorCollection GetEvents()
		{
			return TypeDescriptor.GetEvents(this.obj);
		}

		public EventDescriptorCollection GetEvents(Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(this.obj, attributes);
		}

		public PropertyDescriptorCollection GetProperties()
		{
			return this.GetProperties(null);
		}

		private PropertyDescriptorCollection properties = null;

		public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			if (this.GetClassView() == null)
				
				return TypeDescriptor.GetProperties(this.obj, attributes);

			if (properties == null)
			{
				PropertyDescriptorCollection realPropertyDescriptors = TypeDescriptor.GetProperties(this.obj, attributes, true);

				PropertyDescriptor[] wrappedProperties = new PropertyDescriptor[this.GetClassView().PropertyViews.Count];

				int i = 0;
				foreach (PropertyView propertyView in this.GetClassView().PropertyViews)
				{
					foreach (PropertyDescriptor realPropertyDescriptor in realPropertyDescriptors)
					{
						//TODO: fix real property path traversal...
						if (realPropertyDescriptor.Name.Equals(propertyView.Path))
						{
							wrappedProperties[i] = new WrappedProperty(realPropertyDescriptor, propertyView, attributes);							
							i++;
							break;
						}
					}
				}

				properties = new PropertyDescriptorCollection(wrappedProperties);				
			}

			return properties;
		}

		public object GetPropertyOwner(PropertyDescriptor pd)
		{
			return this.obj;
		}

	}
}
