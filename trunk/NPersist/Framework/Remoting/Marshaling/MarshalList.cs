// *
// * Copyright (C) 2005 Mats Helander
// *
// * This library is free software; you can redistribute it and/or modify it
// * under the terms of the GNU Lesser General Public License 2.1 or later, as
// * published by the Free Software Foundation. See the included license.txt
// * or http://www.gnu.org/copyleft/lesser.html for details.
// *
// *

using System;
using System.Collections;
using System.Xml.Serialization;

namespace Puzzle.NPersist.Framework.Remoting.Marshaling
{
	/// <summary>
	/// Summary description for MarshalList.
	/// </summary>
	[Serializable()] public class MarshalList
	{
		public MarshalList()
		{
		}

		private string name = "";
		private ArrayList values = new ArrayList();
		private ArrayList originalValues = new ArrayList();

		public string Name
		{
			get{ return this.name; } 
			set{ this.name = value; }
		}

		[XmlArrayItem(typeof(string))] public ArrayList Values
		{
			get{ return this.values; }
			set{ this.values = value; }
		} 

		[XmlArrayItem(typeof(string))] public ArrayList OriginalValues
		{
			get{ return this.originalValues; }
			set{ this.originalValues = value; }
		} 
	
	}
}
