using System;
namespace NTorpedo.Auction.NPersist
{

    public class Bid: IBid
    {

        private System.String m_Id;
        private System.Double m_Amount;
        private IAuction m_Auction;
        private IUser m_Buyer;
        private System.Double m_MaxAmount;
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

        public virtual System.Double Amount
        {
            get
            {
                return m_Amount;
            }
            set
            {
                m_Amount = value;
            }
        }

        public virtual IAuction Auction
        {
            get
            {
                return m_Auction;
            }
            set
            {
                m_Auction = value;
            }
        }

        public virtual IUser Buyer
        {
            get
            {
                return m_Buyer;
            }
            set
            {
                m_Buyer = value;
            }
        }

        public virtual System.Double MaxAmount
        {
            get
            {
                return m_MaxAmount;
            }
            set
            {
                m_MaxAmount = value;
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
