using System;
using System.Collections.Generic;
using System.Text;

namespace Puzzle.NPersist.Linq
{
    public interface ILoadSpan
    {
        string[] PropertyPaths {get;}

    }

    public class LoadSpan<T> : ILoadSpan
    {
        private string[] propertyPaths;
        public string[] PropertyPaths
        {
            get
            {
                return propertyPaths;
            }
        }
        public LoadSpan()
        {
            this.propertyPaths = new string[] { };
        }

        public LoadSpan(params string[] propertyPaths)
        {
            if (propertyPaths == null)
                this.propertyPaths = new string[] {};
            else
                this.propertyPaths = propertyPaths;
        }
    }
}
