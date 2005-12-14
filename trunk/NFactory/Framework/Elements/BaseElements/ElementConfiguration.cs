using System;

namespace Puzzle.NFactory.Framework.ConfigurationElements
{
	public class ElementConfiguration
	{
		#region Public Property Value

		private IValue value;

		public IValue Value
		{
			get { return this.value; }
			set { this.value = value; }
		}

		#endregion

		#region Public Property Type

		private Type type;

		public Type Type
		{
			get { return this.type; }
			set { this.type = value; }
		}

		#endregion

		#region Public Property InstanceMode

		private InstanceMode instanceMode;

		public InstanceMode InstanceMode
		{
			get { return this.instanceMode; }
			set { this.instanceMode = value; }
		}

		#endregion

		public virtual object GetValue(IContainer owner)
		{
			return this.Value.Invoke(owner, Type, InstanceMode);
		}
	}
}