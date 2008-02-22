using System;

namespace NTorpedo.Auction.NPersist
{
	/// <summary>
	/// Summary description for PersistenceFactory.
	/// </summary>
	public class PersistenceFactory : NTorpedo.Auction.IPersistenceFactory 
	{
		public PersistenceFactory()
		{
		}

		public NTorpedo.Auction.Persistence CreatePersistence()
		{
			return new NTorpedo.Auction.NPersist.Persistence();
		}
	}
}
