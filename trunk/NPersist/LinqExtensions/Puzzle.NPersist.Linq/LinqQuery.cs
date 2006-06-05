using System;
using System.Collections.Generic;
using System.Text;
using System.Query;
using Puzzle.NPersist.Framework;

namespace Puzzle.NPersist.Linq
{
    public class LinqQuery<T>
    {
        #region Property Context
        private IContext context;
        public virtual IContext Context
        {
            get
            {
                return this.context;
            }
            set
            {
                this.context = value;
            }
        }
        #endregion

        string selectClause = "select *";
        public string SelectClause
        {
            get
            {
                return selectClause;
            }
            set
            {
                selectClause = value;
            }
        }

        public string FromClause
        {
            get
            {
                return "from " + this.GetType().GetGenericArguments()[0].Name;
            }
        }

        #region Property WhereClause
        private string whereClause = "";
        public virtual string WhereClause
        {
            get
            {
                return this.whereClause;
            }
            set
            {
                this.whereClause = value;
            }
        }
        #endregion

        public string ToNPath()
        {
            return SelectClause + " " + FromClause + " " + WhereClause + " " + OrderByClause ;
        }

        #region Property OrderByClause
        private string orderByClause = "";
        public virtual string OrderByClause
        {
            get
            {
                return this.orderByClause;
            }
            set
            {
                this.orderByClause = value;
            }
        }
        #endregion
    }
}