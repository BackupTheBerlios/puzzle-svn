using System.Collections.Generic;
using System.Drawing;
using AlbinoHorse.Infrastructure;

namespace AlbinoHorse.Model
{
    public class UmlDiagram
    {
        public UmlDiagram()
        {
            DataSource = new DefaultUmlDiagramData();
        }

        #region Property Shapes        

        public IList<Shape> Shapes
        {
            get { return DataSource.GetShapes(); }
        }

        #endregion

        public IUmlDiagramData DataSource { get; set; }

        //public void AutoLayout()
        //{
        //    Hashtable classLookup = new Hashtable();
        //    Hashtable nodeLookup = new Hashtable();
        //    Graph graph = new Graph();
        //    int scaleFactor = 7;
        //    foreach (Shape shape in Shapes)
        //    {
        //        if (shape is UmlInstanceType)
        //        {
        //            Node node = new Node();
        //            node.Bounds = new RectangleF(shape.Bounds.Left, shape.Bounds.Top, shape.Bounds.Width / scaleFactor, shape.Bounds.Height / scaleFactor);
        //            classLookup.Add(shape, node);
        //            nodeLookup.Add(node, shape);
        //            graph.Nodes.Add(node);

        //        }
        //    }

        //    foreach (Shape shape in Shapes)
        //    {
        //        if (shape is UmlConnection)
        //        {
        //            UmlConnection connection = (UmlConnection)shape;
        //            if (connection.Start == null || connection.End == null)
        //                continue;
        //            Node start = (Node)classLookup[connection.Start];
        //            Node end = (Node)classLookup[connection.End];

        //            start.Connections.Add(end);
        //            end.Connections.Add(start);
        //        }
        //    }
        //    Random r = new Random();
        //    //foreach (Node node in graph.Nodes)
        //    //{
        //    //    node.Bounds.X = r.Next(0, 200);
        //    //    node.Bounds.Y = r.Next(0, 200);
        //    //}

        //    graph.AutoLayout();

        //    foreach (Node node in graph.Nodes)
        //    {
        //        UmlInstanceType umlType = (UmlInstanceType)nodeLookup[node];
        //        umlType.Bounds = new Rectangle((int)(node.Bounds.X * scaleFactor), (int)(node.Bounds.Y * scaleFactor), umlType.Bounds.Width, umlType.Bounds.Height);
        //    }
        //}

        public virtual void Draw(RenderInfo info)
        {
            if (!info.Preview && info.ShowGrid)
            {
                int xo = info.VisualBounds.X%info.GridSize;
                int yo = info.VisualBounds.Y%info.GridSize;

                for (int y = info.VisualBounds.Y - yo;
                     y < (info.VisualBounds.Bottom + info.GridSize)/info.Zoom;
                     y += info.GridSize)
                {
                    for (int x = info.VisualBounds.X - xo;
                         x < (info.VisualBounds.Right + info.GridSize)/info.Zoom;
                         x += info.GridSize)
                    {
                        info.Graphics.FillRectangle(Brushes.Gray, x, y, 1, 1);
                    }
                }
            }

            int maxWidth = int.MinValue;
            int maxHeight = int.MinValue;
            int minWidth = int.MaxValue;
            int minHeight = int.MaxValue;

            foreach (Shape shape in Shapes)
            {
                if (info.Preview)
                    shape.PreviewDrawBackground(info);
                else
                    shape.DrawBackground(info);
            }

            foreach (Shape shape in Shapes)
            {
                if (info.Preview)
                    shape.DrawPreview(info);
                else
                    shape.Draw(info);


                if (shape.Bounds.Left*info.Zoom < minWidth)
                    minWidth = (int) (shape.Bounds.Left*info.Zoom);

                if (shape.Bounds.Top*info.Zoom < minHeight)
                    minHeight = (int) (shape.Bounds.Top*info.Zoom);

                if (shape.Bounds.Right*info.Zoom > maxWidth)
                    maxWidth = (int) (shape.Bounds.Right*info.Zoom);

                if (shape.Bounds.Bottom*info.Zoom > maxHeight)
                    maxHeight = (int) (shape.Bounds.Bottom*info.Zoom);
            }

            maxWidth += (int) (info.GridSize*info.Zoom);
            maxHeight += (int) (info.GridSize*info.Zoom);

            info.ReturnedBounds = new Rectangle(new Point(minWidth, minHeight),
                                                new Size(maxWidth - minWidth, maxHeight - minHeight));
        }
    }
}