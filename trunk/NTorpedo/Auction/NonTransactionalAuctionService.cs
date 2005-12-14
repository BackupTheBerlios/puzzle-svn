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

namespace NTorpedo.Auction
{
	/// <summary>
	/// Summary description for NonTransactionalAuctionService.
	/// </summary>
	public class NonTransactionalAuctionService
	{
		protected IPersistenceFactory persistenceFactory;
		protected Persistence persistentAuctions;

		public NonTransactionalAuctionService()
		{
		}
		public void SetPersistenceFactory(IPersistenceFactory persistenceFactory)
		{
			this.persistenceFactory = persistenceFactory;
			persistentAuctions = persistenceFactory.CreatePersistence() ;
		}

		public AuctionInfo ListAuctionTwiceWithoutTransaction(String auctionId)
		{
			try 
			{
				TransactionalAuctionService service = new TransactionalAuctionService();
				service.SetPersistenceFactory(this.persistenceFactory);
				AuctionInfo ai = service.ListAuction(auctionId);				

				string sql = ai.Sql;

				service = new TransactionalAuctionService();
				service.SetPersistenceFactory(this.persistenceFactory);
				ai = service.ListAuction(auctionId);

				ai.Sql = sql + ai.Sql;

				return ai;
			} 
			catch (Exception e) 
			{
				// The next operation is for Object-Relational mapping software that needs to be explicitly cleaned up.
				// Those that do not simply ignore this operation.
				if (persistentAuctions!=null) persistentAuctions.Close();
				throw new AuctionServiceException(e);
			}
		}

	}
}
