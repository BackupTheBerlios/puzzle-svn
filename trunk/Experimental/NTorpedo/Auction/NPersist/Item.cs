using System;
namespace NTorpedo.Auction.NPersist
{

    public class Item : IItem
    {

        private System.String m_Id;
        private IAuction m_Auction;
        private System.String m_Description;
        private System.String m_GraphicFilename;
        private System.String m_ItemName;
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

        public virtual System.String Description
        {
            get
            {
                return m_Description;
            }
            set
            {
                m_Description = value;
            }
        }

        public virtual System.String GraphicFilename
        {
            get
            {
                return m_GraphicFilename;
            }
            set
            {
                m_GraphicFilename = value;
            }
        }

        public virtual System.String ItemName
        {
            get
            {
                return m_ItemName;
            }
            set
            {
                m_ItemName = value;
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
