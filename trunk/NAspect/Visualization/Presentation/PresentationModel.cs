using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Puzzle.NAspect.Visualization.Presentation
{
    public class PresentationModel
    {
        private IList aspects = new ArrayList();
        public virtual IList Aspects
        {
            get { return aspects; }
            set { aspects = value; }
        }	
    }
}
