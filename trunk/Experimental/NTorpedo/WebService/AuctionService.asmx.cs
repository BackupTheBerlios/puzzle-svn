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
using System.ComponentModel;
using System.Runtime.Remoting;
using System.Web.Services;
using NTorpedo.Auction;

//This web service corresponds to the TORPEDOAuctionServiceBean.java class
//in the original Torpedo implementation
namespace NTorpedo.Auction
{
	/// <summary>
	/// Summary description for AuctionService.
	/// </summary>
	public class AuctionService : System.Web.Services.WebService
	{
		public AuctionService()
		{
			//CODEGEN: This call is required by the ASP.NET Web Services Designer
			InitializeComponent();

		}

		#region Component Designer generated code
		
		//Required by the Web Services Designer 
		private IContainer components = null;
				
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if(disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);		
		}
		
		#endregion

		protected TransactionalAuctionService GetTransactionalAuctionService()
		{
			IPersistenceFactory persistenceFactory = GetPersistenceFactory();
			TransactionalAuctionService service =  new TransactionalAuctionService() ; 			
			service.SetPersistenceFactory(persistenceFactory);
			return service;
		}

		protected NonTransactionalAuctionService GetNonTransactionalAuctionService()
		{
			IPersistenceFactory persistenceFactory = GetPersistenceFactory();
			NonTransactionalAuctionService service =  new NonTransactionalAuctionService() ; 			
			service.SetPersistenceFactory(persistenceFactory);
			return service;
		}

		protected IPersistenceFactory GetPersistenceFactory()
		{
			try
			{
				string asmName = System.Configuration.ConfigurationSettings.AppSettings["PersistenceFactoryAssembly"];
				string typeName = System.Configuration.ConfigurationSettings.AppSettings["PersistenceFactoryType"];
				ObjectHandle factoryHandle = Activator.CreateInstance(asmName, typeName, new object[0]);
				return (IPersistenceFactory) factoryHandle.Unwrap() ;				
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		[WebMethod]
		public string PlaceBid(string auctionId, string bidId, string userId, System.Double amount, System.Double maxAmount)
		{
			return GetTransactionalAuctionService().PlaceBid(auctionId,bidId,userId,amount,maxAmount);
		}

		[WebMethod]
		public string Place2Bids(string auctionId1, string auctionId2, string bidId1, string bidId2, string userId, System.Double amount1, System.Double amount2, System.Double maxAmount1, System.Double maxAmount2) 
		{
			return GetTransactionalAuctionService().Place2Bids(auctionId1, auctionId2, bidId1,  bidId2,  userId,  amount1,  amount2,  maxAmount1,  maxAmount2);
		}

		[WebMethod]
		public AuctionInfo ListAuction(string auctionId) 
		{
			TransactionalAuctionService svc = GetTransactionalAuctionService();
			AuctionInfo ai = svc.ListAuction(auctionId);
			return ai;
		}

		[WebMethod]
		public AuctionInfo ListAuctionTwiceWithTransaction(string auctionId)
		{
			AuctionInfo ai=GetTransactionalAuctionService().ListAuctionTwiceWithTransaction(auctionId);
			return ai;
		}

		[WebMethod]
		public AuctionInfo ListAuctionTwiceWithoutTransaction(string auctionId)
		{
			AuctionInfo ai=GetNonTransactionalAuctionService().ListAuctionTwiceWithoutTransaction(auctionId);
			return ai;
		}

		[WebMethod]
		public AuctionInfo listPartialAuction(string auctionId) 
		{
			AuctionInfo ai=GetTransactionalAuctionService().ListPartialAuction(auctionId);
			return ai;
		}

		[WebMethod]
		public CollectionWithSQL FindAllAuctions()
		{
			CollectionWithSQL cwsq = GetTransactionalAuctionService().FindAllAuctions();
			return cwsq;
		}


		[WebMethod]
		public CollectionWithSQL FindHighBids(string auctionId)
		{
			CollectionWithSQL cwsq = GetTransactionalAuctionService().FindHighBids(auctionId);
			return cwsq;
		}

	}
}
