using System;
namespace NTorpedo.Auction.NPersist
{

    public class Auction : IAuction
    {

        private System.String m_Id;
        private System.Collections.IList m_Bids;
        private IItem m_Item;
        private System.Double m_LowPrice;
        private IUser m_Seller;
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

        public virtual IItem Item
        {
            get
            {
                return m_Item;
            }
            set
            {
                m_Item = value;
            }
        }

        public virtual System.Double LowPrice
        {
            get
            {
                return m_LowPrice;
            }
            set
            {
                m_LowPrice = value;
            }
        }

        public virtual IUser Seller
        {
            get
            {
                return m_Seller;
            }
            set
            {
                m_Seller = value;
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
