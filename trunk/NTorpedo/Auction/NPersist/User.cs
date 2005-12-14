using System;
namespace NTorpedo.Auction.NPersist
{

    public class User : IUser
    {

        private System.String m_Id;
        private System.Collections.IList m_Auctions;
        private System.Collections.IList m_Bids;
        private System.Int32 m_Version;

        public virtual System.String Id
        {
            get
            {
                return m_Id;
            }
            set
            {
                m_Id = value;
            }
        }

        public virtual System.Collections.IList Auctions
        {
            get
            {
                return m_Auctions;
            }
            set
            {
                m_Auctions = value;
            }
        }

        public virtual System.Collections.IList Bids
        {
            get
            {
                return m_Bids;
            }
            set
            {
                m_Bids = value;
            }
        }

        public virtual System.Int32 Version
        {
            get
            {
                return m_Version;
            }
            set
            {
                m_Version = value;
            }
        }







    }
}
