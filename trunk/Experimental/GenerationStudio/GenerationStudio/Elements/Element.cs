using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using GenerationStudio.AppCore;
using GenerationStudio.Attributes;
using System.Xml.Serialization;
using System.Collections;
using System.Runtime.Serialization;
using GenerationStudio.Gui;
using System.Drawing;
using System.IO;

namespace GenerationStudio.Elements
{
    [Serializable]
    [ElementIcon("GenerationStudio.Images.dummy.bmp")]
    public abstract class Element
    {
        private IList<Element> children = new List<Element>();

        [Browsable(false)]
        public IEnumerable<Element> AllChildren
        {
            get
            {
                var res = from child in children
                          select child;

                return res;
            }
        }

        [Browsable(false)]
        public IEnumerable<Element> Children
        {
            get
            {
                
                var res = from child in children
                          where !child.Excluded
                          select child;

                return res;
            }
        }

        public IList<T> GetChildren<T>() where T : Element
        {
            var res = from child in children
                      where !child.Excluded &&
                      child is T
                      select child;

            return res.Cast<T>().ToList();
        }

        public T GetChild<T>() where T : Element
        {
            var res = from child in children
                      where !child.Excluded &&
                      child is T
                      select child;

            return res.Cast<T>().FirstOrDefault();
        }

        private Element parent;
        [Browsable(false)]
        public Element Parent
        {
            get
            {
                return parent;
            }
            set
            {
                parent = value;
            }
        }

        private bool excluded;
        [Browsable(false)]
        public bool Excluded
        {
            get
            {
                return excluded;
            }
            set
            {
                excluded = value;
                OnNotifyChange();
            }
        }
     
        public virtual int GetSortPriority()
        {
            return 0;
        }

        


        public Element()
        {            
        }

        public virtual string GetDisplayName()
        {
            return "foo";
        }

        public virtual void OnNotifyChange()
        {
            Engine.OnNotifyChange();
        }

        public virtual bool GetDefaultExpanded()
        {
            return true;
        }

        public virtual string GetIconName()
        {
            return this.GetType().GetElementIconName();
        }

        public virtual Image GetIcon()
        {
            Stream stream = this.GetType().Assembly.GetManifestResourceStream(GetIconName ());
            Image img = Image.FromStream(stream);

            if (Excluded )
            {
                try
                {
                    stream = typeof(Element).Assembly.GetManifestResourceStream("GenerationStudio.Images.exclude.gif");
                    Image exclude = Image.FromStream(stream);
                    Image bw = GenerationStudio.Drawing.Utils.MakeGrayscale((Bitmap)img);
                    Image tmp = new Bitmap(16, 16);
                    Graphics g = Graphics.FromImage(tmp);
                    g.DrawImage(bw, 0, 0);
                    g.DrawImage(exclude, 0, 0);
                    return tmp;
                }
                catch (Exception x)
                {
                    throw;
                }
            }
            else if (GetErrorsRecursive().Count > 0)
            {
                try
                {
                    stream = typeof(Element).Assembly.GetManifestResourceStream("GenerationStudio.Images.error.gif");
                    Image exclude = Image.FromStream(stream);
                    Image tmp = new Bitmap(16, 16);
                    Graphics g = Graphics.FromImage(tmp);
                    g.DrawImage(img, 0, 0);
                    g.DrawImage(exclude, 0, 0);
                    return tmp;
                }
                catch (Exception x)
                {
                    throw;
                }
            }

            return img;
        }

        public IList<ElementError> GetErrorsRecursive()
        {
            
            List<ElementError> allErrors = new List<ElementError>();
            
            allErrors.AddRange(GetErrors());

            foreach (Element child in AllChildren)
            {
                //ignore excluded items
                if (child.Excluded)
                    continue;

                allErrors.AddRange(child.GetErrorsRecursive());
            }

            return allErrors;
        }

        public virtual IList<ElementError> GetErrors()
        {
            return new List<ElementError>();
        }

        public string GetIconKey()
        {
            return string.Format("{0}|{1}|{2}", GetIconName(), Excluded, GetErrorsRecursive().Count > 0);
        }

        public void ClearChildren()
        {
            children.Clear();
        }

        public void AddChild(Element child)
        {
            children.Add(child);
            child.parent = this;
            OnNotifyChange();
        }

        public void RemoveChild(Element child)
        {
            if (!child.AllowDelete())
                return;

            this.children.Remove(child);
            OnNotifyChange();
        }

        [Browsable(false)]
        public RootElement Root
        {
            get
            {
                Element current = this;
                while (current.Parent != null)
                    current = current.Parent;

                return (RootElement)current;
            }
        }

        public virtual bool AllowDelete()
        {
            return true;
        }

        public virtual bool HideChildren()
        {
            return false;
        }
    }
}
