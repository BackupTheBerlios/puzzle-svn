using System;
using System.Collections;

namespace Puzzle.NFactory.Framework.ConfigurationElements
{
	public class ListConfiguration : IListConfiguration
	{
		#region Public Property Name

		private string name;

		public string Name
		{
			get { return this.name; }
			set { this.name = value; }
		}

		#endregion

		#region Public Property ListItemConfigurations

		private ArrayList listItemConfigurations = new ArrayList();

		public ArrayList ListItemConfigurations
		{
			get { return this.listItemConfigurations; }
		}

		#endregion

		public object Invoke(IContainer owner, Type requestedType, InstanceMode instanceMode)
		{
			ArrayList arr = new ArrayList();
			foreach (IListItemConfiguration itemConfig in ListItemConfigurations)
			{
				if (itemConfig.Type == null)
					itemConfig.Type = typeof (string);

				object value = itemConfig.GetValue(owner);
				arr.Add(value);
			}

			return arr;
		}
	}
}