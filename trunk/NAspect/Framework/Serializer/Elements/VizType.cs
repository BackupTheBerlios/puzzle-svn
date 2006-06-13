using System;
using System.Collections.Generic;
using System.Text;

namespace Puzzle.NAspect.Debug.Serialization.Elements
{
    /// <summary>
    /// DTO for the VS.NET 2005 debugger visualiser
    /// </summary>
    [Serializable]
    public class VizType
    {
        #region Property Name
        private string name;
        /// <summary>
        /// 
        /// </summary>
        public virtual string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }
        #endregion

        #region Property FullName
        private string fullName;
        /// <summary>
        /// 
        /// </summary>
        public virtual string FullName
        {
            get
            {
                return this.fullName;
            }
            set
            {
                this.fullName = value;
            }
        }
        #endregion

        #region Property BaseName
        private string baseName;
        /// <summary>
        /// 
        /// </summary>
        public virtual string BaseName
        {
            get
            {
                return this.baseName;
            }
            set
            {
                this.baseName = value;
            }
        }
        #endregion

        #region Property Methods
        private List<VizMethodBase> methods = new List<VizMethodBase> ();
        /// <summary>
        /// 
        /// </summary>
        public virtual List<VizMethodBase> Methods
        {
            get
            {
                return this.methods;
            }
        }
        #endregion

        #region Property Mixins
        private List<VizMixin> mixins = new List<VizMixin>();
        /// <summary>
        /// 
        /// </summary>
        public virtual List<VizMixin> Mixins
        {
            get
            {
                return this.mixins;
            }
        }
        #endregion

        #region Property Aspects
        private List<VizAspect> aspects = new List<VizAspect> ();
        /// <summary>
        /// 
        /// </summary>
        public virtual List<VizAspect> Aspects
        {
            get
            {
                return this.aspects;
            }
        }
        #endregion
    }
}
