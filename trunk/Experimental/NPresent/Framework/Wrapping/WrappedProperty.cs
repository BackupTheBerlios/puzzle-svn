using System;
using System.ComponentModel;
using Puzzle.NPresent.Framework.ViewModel;

namespace Puzzle.NPresent.Framework.Wrapping
{
	/// <summary>
	/// Summary description for WrappedProperty.
	/// </summary>
	public class WrappedProperty : PropertyDescriptor
	{
		public WrappedProperty(MemberDescriptor descr, PropertyView propertyView, Attribute[] attrs) : base(descr, attrs)
		{
			this.realPropertyDescriptor = (PropertyDescriptor) descr;
			this.name = propertyView.Name;
			this.displayName = propertyView.DisplayName;

			Attribute[] attribs = new Attribute[descr.Attributes.Count + 4];

			int i = 0;
			foreach (Attribute attrib in descr.Attributes)
			{
				attribs[i] = attrib;
				i++;
			}
			attribs[i] = new DescriptionAttribute(propertyView.Description);
			attribs[i + 1] = new CategoryAttribute(propertyView.Category);
			attribs[i + 2] = new DescriptionAttribute(propertyView.Description);
			attribs[i + 3] = new ReadOnlyAttribute(propertyView.IsReadOnly);
			
			attributes = new AttributeCollection(attribs);
		}

		private PropertyDescriptor realPropertyDescriptor;
		private AttributeCollection attributes;
		private string name;
		private string displayName;

		private object GetActualObject(object component)
		{
			if (component is IWrappedObject)
				return ((IWrappedObject) component).GetObject();
			else
				return component;			
		}
		public override AttributeCollection Attributes
		{
			get
			{
				return this.attributes ;
			}
		}

		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		public override string DisplayName
		{
			get
			{
				if (this.displayName.Length > 0)
					return this.displayName;
				return this.Name;
			}
		}


		public override bool CanResetValue(object component)
		{
			return this.realPropertyDescriptor.CanResetValue(GetActualObject(component));
		}

		public override object GetValue(object component)
		{
			return this.realPropertyDescriptor.GetValue(GetActualObject(component));
		}

		public override void ResetValue(object component)
		{
			this.realPropertyDescriptor.ResetValue(GetActualObject(component));
		}

		public override void SetValue(object component, object value)
		{
			this.realPropertyDescriptor.SetValue(GetActualObject(component), value);
		}

		public override bool ShouldSerializeValue(object component)
		{
			return this.realPropertyDescriptor.ShouldSerializeValue(GetActualObject(component));
		}

		public override Type ComponentType
		{
			get 
			{
				return this.realPropertyDescriptor.ComponentType;
			}
		}

		public override bool IsReadOnly
		{
			get 
			{
				return this.realPropertyDescriptor.IsReadOnly;
			}
		}

		public override Type PropertyType
		{
			get 
			{
				return this.realPropertyDescriptor.PropertyType;
			}
		}


	}
}
