/*
* ====================================================================
*
* NTORPEDO
* A Testbed of Object Relational Products for Enterprise Distributed Objects
* Copyright (c) 2005 torpedo-group
* http://www.torpedo-group.org
* @author Bruce Martin
* @version 8.25.04
* Translated to c# by Mats Helander
*
* This library is free software; you can redistribute it and/or modify it
* under the terms of the GNU Lesser General Public License 2.1 or later, as
* published by the Free Software Foundation. See the included license.txt
* or http://www.gnu.org/copyleft/lesser.html for details.
* ====================================================================
*/

using System;
using System.Collections;

namespace NTorpedo.Auction
{
	/// <summary>
	/// Summary description for CollectionWithSQL.
	/// </summary>
	[Serializable()]
	public class CollectionWithSQL
	{
		protected IList theCollection=null;
		protected String theSQL="";
		public CollectionWithSQL(IList collection)
		{
			theCollection = collection;
		}

		public CollectionWithSQL()
		{
			theCollection = new ArrayList() ;
		}

		public IList TheCollection
		{
			get { return this.theCollection; }
			set { this.theCollection = value; }
		}

		public string TheSql
		{
			get { return this.theSQL; }
			set { this.theSQL = value; }
		}

	}
}
