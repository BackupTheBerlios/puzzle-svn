using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using AlbinoHorse.Infrastructure;
using AlbinoHorse.Windows.Forms;


namespace AlbinoHorse.Model
{
    public class UmlType : Shape
    {
        #region Bounds property
        public override Rectangle Bounds
        {
            get
            {
                if (Expanded)
                {
                    int propertiesHeight = ((DataSource.GetPropertyCount () + 1) * (PropertiesExpanded ? 1 : 0)) * 16 + 20;
                    int methodsHeight = ((DataSource.GetMethodCount() + 1) * (MethodsExpanded ? 1 : 0)) * 16 + 20;
                    return new Rectangle(base.Bounds.X, base.Bounds.Y, base.Bounds.Width, 55 + propertiesHeight + methodsHeight);
                }
                else
                {
                    return new Rectangle(base.Bounds.X, base.Bounds.Y, base.Bounds.Width, 63 - 3);
                }
            }
            set
            {
                base.Bounds = value;
            }
        }
        #endregion

        #region SelectedObject property
        private object selectedObject;
        public object SelectedObject
        {
            get
            {
                return selectedObject;
            }

            set
            {
                selectedObject = value;
                OnSelectedObjectChanged(EventArgs.Empty);
            }
        }
        #endregion

        public IUmlTypeData DataSource { get; set; }

        //bounding box identifiers
        protected readonly object CaptionIdentifier = new object();
        protected readonly object TypeExpanderIdentifier = new object();
        protected readonly object LeftResizeIdentifier = new object();
        protected readonly object RightResizeIdentifier = new object();
        protected readonly object BodyIdentifier = new object();
        protected readonly object PropertiesIdentifier = new object();
        protected readonly object MethodsIdentifier = new object();
        protected readonly object AddNewPropertyIdentifier = new object();
        protected readonly object AddNewMethodIdentifier = new object();


        public bool Expanded { get; set; }        
        public bool PropertiesExpanded { get; set; }
        public bool MethodsExpanded { get; set; }

        public Pen BorderPen { get; set; }

        private Point mouseDownPos;
        private Point mouseDownShapePos;


        private static class Settings
        {
            private const string fontName = "Tahoma";
            public const int typeBoxSideMargin = 10;
            public const int memberNameIndent = 30;

            public static Font typeNameFont = new Font(fontName, 8f, FontStyle.Bold);
            public static Font typeKindFont = new Font(fontName, 7f, FontStyle.Regular);
            public static Font typeInheritsFont = new Font(fontName, 7f, FontStyle.Regular);
            public static Font sectionCaptionFont = new Font(fontName, 8f, FontStyle.Regular);
            public static Font memberFont = new Font(fontName, 8f, FontStyle.Regular);
            public static Font newMemberFont = new Font(fontName, 8f, FontStyle.Underline);
            public static SolidBrush sectionCaptionBrush = new SolidBrush(Color.FromArgb(240, 242, 249));
            public static SolidBrush selectedMemberBrush = new SolidBrush(SystemColors.Highlight);
            public static Pen borderPen = new Pen(Color.FromArgb(100, 100, 100), 1);
            public static Pen selectionPen1 = MakeSelectonPen();
            public static Pen selectionPen2 = new Pen(Color.FromArgb(220, 220, 220), 1);

            private static Pen MakeSelectonPen()
            {
                Pen pen = new Pen(Color.Gray, 1);
                pen.DashStyle = DashStyle.Dash;
                return pen;
            }
        }



        private void OnSelectedObjectChanged(EventArgs eventArgs)
        {

        }

        public UmlType()
        {
            DataSource = new DefaultUmlTypeData();
        }


        public override void Draw(RenderInfo info)
        {
            int grid = info.GridSize;
            Rectangle renderBounds = Bounds;

            BoundingBox bboxThis = new BoundingBox();
            bboxThis.Bounds = renderBounds;
            bboxThis.Target = this;
            bboxThis.Data = this.BodyIdentifier;
            info.BoundingBoxes.Add(bboxThis);

            GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();

            int x = renderBounds.X;
            int y = renderBounds.Y;
            int radius = 16;
            int width = renderBounds.Width;
            int height = renderBounds.Height;

            path.AddLine(x + radius, y, x + width - radius, y);
            path.AddArc(x + width - radius, y, radius, radius, 270, 90);
            path.AddLine(x + width, y + radius, x + width, y + height - radius);
            path.AddArc(x + width - radius, y + height - radius, radius, radius, 0, 90);
            path.AddLine(x + width - radius, y + height, x + radius, y + height);
            path.AddArc(x, y + height - radius, radius, radius, 90, 90);
            path.AddLine(x, y + height - radius, x, y + radius);
            path.AddArc(x, y, radius, radius, 180, 90);

            path.CloseFigure();

            LinearGradientBrush captionBrush = null;
            if (Selected)
                captionBrush = new LinearGradientBrush(renderBounds, Color.FromArgb(190, 202, 230), Color.White, 0, true);
            else
                captionBrush = new LinearGradientBrush(renderBounds, Color.FromArgb(210, 222, 240), Color.White, 0, true);

            Pen borderPen = null;
            if (BorderPen == null)
                borderPen = Settings.borderPen;
            else
                borderPen = BorderPen;

            if (Expanded)
            {
                DrawExpanded(info, path, x, y, width, height, captionBrush, borderPen);
            }
            else
            {
                DrawCollapsed(info, path, x, y, width, height, captionBrush, borderPen);
            }



            Rectangle typeNameBounds = new Rectangle(x + Settings.typeBoxSideMargin, y + 4, width - Settings.typeBoxSideMargin * 2, 10);
            Rectangle typeKindBounds = new Rectangle(x + Settings.typeBoxSideMargin, y + 4 + 15, width - Settings.typeBoxSideMargin * 2, 10);

            info.Graphics.DrawString(DataSource.TypeName, Settings.typeNameFont, Brushes.Black, typeNameBounds, StringFormat.GenericTypographic);
            info.Graphics.DrawString("Class", Settings.typeKindFont, Brushes.Black, typeKindBounds, StringFormat.GenericTypographic);

            Rectangle typeExpanderBounds = new Rectangle(x + width - 20, y + 6, 13, 13);

            #region add type expander bbox
            BoundingBox bboxTypeExpander = new BoundingBox();
            bboxTypeExpander.Target = this;
            bboxTypeExpander.Bounds = typeExpanderBounds;
            bboxTypeExpander.Data = this.TypeExpanderIdentifier;
            info.BoundingBoxes.Add(bboxTypeExpander);
            #endregion

            if (Expanded)
                info.Graphics.DrawImage(global::AlbinoHorse.ClassDesigner.Properties.Resources.Collapse, typeExpanderBounds);
            else
                info.Graphics.DrawImage(global::AlbinoHorse.ClassDesigner.Properties.Resources.Expand, typeExpanderBounds);

            if (DataSource.InheritsType != null)
            {
                info.Graphics.DrawImage(global::AlbinoHorse.ClassDesigner.Properties.Resources.InheritanceArrow, x + Settings.typeBoxSideMargin, y + 35);
                Rectangle typeInheritsBounds = new Rectangle(x + 24, y + 33, width - 26, 10);
                info.Graphics.DrawString(DataSource.InheritsType, Settings.typeInheritsFont, Brushes.Black, typeInheritsBounds, StringFormat.GenericTypographic);
            }


            RenderSelection(info);

        }

        private void RenderSelection(RenderInfo info)
        {
            if (Selected && SelectedObject == null)
            {
                Rectangle outerBounds = this.Bounds;
                outerBounds.Inflate(4, 4);
                outerBounds.Offset(1, 0);
                info.Graphics.DrawRectangle(Settings.selectionPen2, outerBounds);
                outerBounds.Offset(-1, 1);
                info.Graphics.DrawRectangle(Settings.selectionPen1, outerBounds);

                Rectangle leftHandle = new Rectangle(outerBounds.X - 4, (outerBounds.Top + outerBounds.Bottom) / 2 - 4 , 8, 8);
                info.Graphics.FillRectangle(Brushes.Gray,leftHandle);
                leftHandle.Inflate(-1, -1);
                info.Graphics.FillRectangle(Brushes.White, leftHandle);

                BoundingBox leftResizeHandle = new BoundingBox();
                leftResizeHandle.Bounds = leftHandle;
                leftResizeHandle.Data = this.LeftResizeIdentifier;
                leftResizeHandle.Target = this;
                info.BoundingBoxes.Add(leftResizeHandle);

                Rectangle rightHandle = new Rectangle(outerBounds.Right - 4, (outerBounds.Top + outerBounds.Bottom) / 2 - 4, 8, 8);
                info.Graphics.FillRectangle(Brushes.Gray, rightHandle);
                rightHandle.Inflate(-1, -1);
                info.Graphics.FillRectangle(Brushes.White, rightHandle);

                BoundingBox rightResizeHandle = new BoundingBox();
                rightResizeHandle.Bounds = rightHandle;
                rightResizeHandle.Data = this.RightResizeIdentifier;
                rightResizeHandle.Target = this;
                info.BoundingBoxes.Add(rightResizeHandle);
            }
        }

        private void DrawCollapsed(RenderInfo info, GraphicsPath path, int x, int y, int width, int height, LinearGradientBrush captionBrush, Pen borderPen)
        {
            Rectangle captionBounds = new Rectangle(x, y, width, height);

            BoundingBox bboxCaption = new BoundingBox();
            bboxCaption.Bounds = captionBounds;
            bboxCaption.Target = this;
            bboxCaption.Data = this.CaptionIdentifier;
            info.BoundingBoxes.Add(bboxCaption);

            info.Graphics.FillPath(captionBrush, path);
            info.Graphics.DrawPath(borderPen, path);
        }

        private void DrawExpanded(RenderInfo info, GraphicsPath path, int x, int y, int width, int height, LinearGradientBrush captionBrush, Pen borderPen)
        {
            int currentY =  y + RenderCaption(info, path, x, y, width, height, captionBrush);

            currentY = RenderProperties(info, x, currentY, width);
            currentY = RenderMethods(info, x, currentY, width);



            info.Graphics.ResetClip();

            //  info.Graphics.FillPath(captionBrush, path);
            info.Graphics.DrawPath(borderPen, path);
        }

        private int RenderCaption(RenderInfo info, GraphicsPath path, int x, int y, int width, int height, LinearGradientBrush captionBrush)
        {
            int captionHeight = 48;
            Rectangle captionBounds = new Rectangle(x, y, width, captionHeight);
            #region add caption bbox
            BoundingBox bboxCaption = new BoundingBox();
            bboxCaption.Bounds = captionBounds;
            bboxCaption.Target = this;
            bboxCaption.Data = this.CaptionIdentifier;
            info.BoundingBoxes.Add(bboxCaption);
            #endregion

            //GraphicsPath captionPath = (GraphicsPath)path.Clone();
            info.Graphics.SetClip(path);
            info.Graphics.FillRectangle(captionBrush, captionBounds);
            info.Graphics.FillRectangle(Brushes.White, x, y + captionHeight, width, height - captionHeight);
            info.Graphics.DrawLine(Pens.LightGray, x, y + captionHeight, x + width, y + captionHeight);

            


            return captionHeight;
        }

        private int RenderProperties(RenderInfo info, int x, int y, int width) 
        {
            Rectangle memberCaptionBounds = new Rectangle(x, y , width, 20);
            #region add properties header bbox
            BoundingBox bboxGroup = new BoundingBox();
            bboxGroup.Bounds = memberCaptionBounds;
            bboxGroup.Target = this;
            bboxGroup.Data = PropertiesIdentifier;
            info.BoundingBoxes.Add(bboxGroup);
            #endregion

            if (this.SelectedObject == PropertiesIdentifier && Selected)
            {
                info.Graphics.FillRectangle(SystemBrushes.Highlight, memberCaptionBounds);
                memberCaptionBounds.X += 20;
                memberCaptionBounds.Width -= 30;
                memberCaptionBounds.Y += 3;
                info.Graphics.DrawString("Properties", Settings.sectionCaptionFont, SystemBrushes.HighlightText, memberCaptionBounds);
            }
            else
            {
                info.Graphics.FillRectangle(Settings.sectionCaptionBrush, memberCaptionBounds);
                memberCaptionBounds.X += 20;
                memberCaptionBounds.Width -= 30;
                memberCaptionBounds.Y += 3;
                info.Graphics.DrawString("Properties", Settings.sectionCaptionFont, Brushes.Black, memberCaptionBounds);
            }

            

            


            int currentY = y  + 20;
            if (PropertiesExpanded)
            {
                StringFormat sf = StringFormat.GenericTypographic;
                sf.Trimming = StringTrimming.EllipsisCharacter;
                foreach (UmlTypeMember member in DataSource.GetProperties ())
                {
                    Rectangle memberBounds = new Rectangle(x + Settings.typeBoxSideMargin, currentY, width - 20, 16);
                    #region add property bbox
                    BoundingBox memberBBox = new BoundingBox();
                    memberBBox.Target = this;
                    memberBBox.Bounds = memberBounds;
                    memberBBox.Data = member;
                    info.BoundingBoxes.Add(memberBBox);
                    #endregion

                    Rectangle layoutBounds = new Rectangle(x + Settings.typeBoxSideMargin + Settings.memberNameIndent, currentY, width - 5 - Settings.memberNameIndent, 16);


                    if (member == SelectedObject && this.Selected)
                    {
                        Rectangle selectionBounds = new Rectangle(x, currentY, width, 16);
                        info.Graphics.FillRectangle(SystemBrushes.Highlight, selectionBounds);
                        info.Graphics.DrawString(member.Name, Settings.memberFont, SystemBrushes.HighlightText, layoutBounds, sf);                        
                    }
                    else
                    {
                        info.Graphics.DrawString(member.Name, Settings.memberFont, Brushes.Black, layoutBounds, sf);                                                
                    }
                    info.Graphics.DrawImage(global::AlbinoHorse.ClassDesigner.Properties.Resources.Property, x + 13, currentY);                        
                    currentY += 16;
                }

                Rectangle newLayoutBounds = new Rectangle(x + Settings.typeBoxSideMargin + Settings.memberNameIndent, currentY, width - 5 - Settings.memberNameIndent, 16);
                info.Graphics.DrawString("Add new", Settings.newMemberFont, Brushes.Blue, newLayoutBounds, sf);

                BoundingBox newMemberBBox = new BoundingBox();
                newMemberBBox.Target = this;
                newMemberBBox.Bounds = new Rectangle(x + Settings.typeBoxSideMargin, currentY, width - 20, 16);
                newMemberBBox.Data = AddNewPropertyIdentifier;
                info.BoundingBoxes.Add(newMemberBBox);

                currentY += 16;
            }
            return currentY;
        }

        private int RenderMethods(RenderInfo info, int x, int y, int width)
        {
            Rectangle memberCaptionBounds = new Rectangle(x, y, width, 20);
            #region add methods header bbox
            BoundingBox bboxGroup = new BoundingBox();
            bboxGroup.Bounds = memberCaptionBounds;
            bboxGroup.Target = this;
            bboxGroup.Data = MethodsIdentifier;
            info.BoundingBoxes.Add(bboxGroup);
            #endregion

            if (this.SelectedObject == MethodsIdentifier && Selected)
            {
                info.Graphics.FillRectangle(SystemBrushes.Highlight, memberCaptionBounds);
                memberCaptionBounds.X += 20;
                memberCaptionBounds.Width -= 30;
                memberCaptionBounds.Y += 3;
                info.Graphics.DrawString("Methods", Settings.sectionCaptionFont, SystemBrushes.HighlightText, memberCaptionBounds);
            }
            else
            {
                info.Graphics.FillRectangle(Settings.sectionCaptionBrush, memberCaptionBounds);
                memberCaptionBounds.X += 20;
                memberCaptionBounds.Width -= 30;
                memberCaptionBounds.Y += 3;
                info.Graphics.DrawString("Methods", Settings.sectionCaptionFont, Brushes.Black, memberCaptionBounds);
            }






            int currentY = y + 20;
            if (MethodsExpanded)
            {
                StringFormat sf = StringFormat.GenericTypographic;
                sf.Trimming = StringTrimming.EllipsisCharacter;
                foreach (UmlTypeMember member in DataSource.GetMethods())
                {
                    Rectangle memberBounds = new Rectangle(x + Settings.typeBoxSideMargin, currentY, width - 20, 16);
                    #region add method bbox
                    BoundingBox memberBBox = new BoundingBox();
                    memberBBox.Target = this;
                    memberBBox.Bounds = memberBounds;
                    memberBBox.Data = member;
                    info.BoundingBoxes.Add(memberBBox);
                    #endregion

                    Rectangle layoutBounds = new Rectangle(x + Settings.typeBoxSideMargin + Settings.memberNameIndent, currentY, width - 5 - Settings.memberNameIndent, 16);


                    if (member == SelectedObject && this.Selected)
                    {
                        Rectangle selectionBounds = new Rectangle(x, currentY, width, 16);
                        info.Graphics.FillRectangle(SystemBrushes.Highlight, selectionBounds);
                        info.Graphics.DrawString(member.Name, Settings.memberFont, SystemBrushes.HighlightText, layoutBounds, sf);
                    }
                    else
                    {
                        info.Graphics.DrawString(member.Name, Settings.memberFont, Brushes.Black, layoutBounds, sf);
                    }
                    info.Graphics.DrawImage(global::AlbinoHorse.ClassDesigner.Properties.Resources.Property, x + 13, currentY);
                    currentY += 16;
                }

                Rectangle newLayoutBounds = new Rectangle(x + Settings.typeBoxSideMargin + Settings.memberNameIndent, currentY, width - 5 - Settings.memberNameIndent, 16);
                info.Graphics.DrawString("Add new", Settings.newMemberFont, Brushes.Blue, newLayoutBounds, sf);

                BoundingBox newMemberBBox = new BoundingBox();
                newMemberBBox.Target = this;
                newMemberBBox.Bounds = new Rectangle(x + Settings.typeBoxSideMargin, currentY, width - 20, 16);
                newMemberBBox.Data = AddNewMethodIdentifier;
                info.BoundingBoxes.Add(newMemberBBox);

                currentY += 16;
            }
            return currentY;
        }

        public override void DrawBackground(RenderInfo info)
        {
            int grid = info.GridSize;
            Rectangle renderBounds = Bounds;
  
            GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();

            int x = renderBounds.X +4;
            int y = renderBounds.Y +3;
            int radius = 16;
            int width = renderBounds.Width;
            int height = renderBounds.Height;

            path.AddLine(x + radius, y, x + width - radius, y);
            path.AddArc(x + width - radius, y, radius, radius, 270, 90);
            path.AddLine(x + width, y + radius, x + width, y + height - radius);
            path.AddArc(x + width - radius, y + height - radius, radius, radius, 0, 90);
            path.AddLine(x + width - radius, y + height, x + radius, y + height);
            path.AddArc(x, y + height - radius, radius, radius, 90, 90);
            path.AddLine(x, y + height - radius, x, y + radius);
            path.AddArc(x, y, radius, radius, 180, 90);

            path.CloseFigure();

            GraphicsPath shadowPath = path;

            try
            {
                info.Graphics.FillPath(Brushes.LightGray, shadowPath);
            }
            catch
            {
            }
        }


        
        


        public override void OnMouseDown(ShapeMouseEventArgs args)
        {
            args.Sender.ClearSelection();
            this.Selected = true;

            if (args.BoundingBox.Data == RightResizeIdentifier)
            {
                mouseDownPos = new Point(args.X, args.Y);
                this.SelectedObject = null;
                args.Redraw = true;
            }
            else if (args.BoundingBox.Data == LeftResizeIdentifier)
            {
                mouseDownPos = new Point(args.X, args.Y);
                this.SelectedObject = null;
                args.Redraw = true;
            }
            else if (args.BoundingBox.Data == PropertiesIdentifier)
            {
                this.SelectedObject = PropertiesIdentifier;
                args.Redraw = true;
            }
            else if (args.BoundingBox.Data == MethodsIdentifier)
            {
                this.SelectedObject = MethodsIdentifier;
                args.Redraw = true;
            }
            else if (args.BoundingBox.Data is UmlTypeMember)
            {
                this.SelectedObject = args.BoundingBox.Data as UmlTypeMember;
                args.Redraw = true;
            }
            else
            {
                mouseDownPos = new Point(args.X, args.Y);
                mouseDownShapePos = this.Bounds.Location;
                this.SelectedObject = null;


                args.Redraw = true;
            }
        }

        public override void OnMouseUp(ShapeMouseEventArgs args)
        {
            if (args.BoundingBox.Data == AddNewPropertyIdentifier)
            {
                UmlProperty newProperty = new UmlProperty();
                newProperty.Name = "";
                newProperty.Type = "string";

                DataSource.AddProperty(newProperty);                
                this.SelectedObject = newProperty;
                
                BeginRenameProperty(args.Sender, newProperty);
                
                args.Redraw = true;
            }

            if (args.BoundingBox.Data == AddNewMethodIdentifier)
            {
                MessageBox.Show("Not implemented yet");
            }  

            if (args.BoundingBox.Data == TypeExpanderIdentifier)
            {
                this.Expanded = !this.Expanded;
               
            }
            args.Redraw = true;
        }

        public override void OnMouseMove(ShapeMouseEventArgs args)
        {
            if (args.BoundingBox.Data == RightResizeIdentifier && args.Button == MouseButtons.Left)
            {
                int diff = args.X - this.Bounds.Left;
                if (diff < 100)
                    diff = 100;

                Bounds = new Rectangle(Bounds.X, Bounds.Y, diff, Bounds.Height);
                args.Redraw = true;
            }

            if (args.BoundingBox.Data == LeftResizeIdentifier && args.Button == MouseButtons.Left)
            {
                int diff = this.Bounds.Right - args.X;
                if (diff < 100)
                    diff = 100;

                if (diff + args.X > Bounds.Right)
                {
                    Bounds = new Rectangle(Bounds.Right-100, Bounds.Y, 100, Bounds.Height);
                    args.Redraw = true;
                }
                else
                {
                    Bounds = new Rectangle(args.X, Bounds.Y, diff, Bounds.Height);
                    args.Redraw = true;
                }
            }

            if (args.BoundingBox.Data == CaptionIdentifier && args.Button == MouseButtons.Left)
            {
                int dx = args.X - mouseDownPos.X;
                int dy = args.Y - mouseDownPos.Y;

                int shapeX = mouseDownShapePos.X + dx;
                int shapeY = mouseDownShapePos.Y + dy;

                if (args.SnapToGrid)
                {
                    shapeX = shapeX - shapeX % args.GridSize;
                    shapeY = shapeY - shapeY % args.GridSize;
                }

                if (shapeX < 0)
                    shapeX = 0;

                if (shapeY < 0)
                    shapeY = 0;

                this.Bounds = new Rectangle(shapeX, shapeY, Bounds.Width, Bounds.Height);
                args.Redraw = true;
            }
        }

        public override void PreviewDraw(RenderInfo info)
        {
            info.Graphics.FillRectangle(Brushes.White, this.Bounds);
            info.Graphics.DrawRectangle(Pens.Black, this.Bounds);
        }

        public override void OnDoubleClick(ShapeMouseEventArgs args)
        {
            

            if (args.BoundingBox.Data == PropertiesIdentifier)
            {
                PropertiesExpanded = !PropertiesExpanded;
                args.Redraw = true;
            }

            if (args.BoundingBox.Data == MethodsIdentifier)
            {
                MethodsExpanded = !MethodsExpanded;
                args.Redraw = true;
            }

            if (args.BoundingBox.Data == CaptionIdentifier)
            {                
                BeginRenameType(args.Sender);
            }

            if (args.BoundingBox.Data is UmlProperty)
            {
                BeginRenameProperty(args.Sender,args.BoundingBox.Data as UmlProperty);
                args.Redraw = true;
            }

            
        }

        public override void OnKeyPress(ShapeKeyEventArgs args)
        {
            if (SelectedObject == null && args.Key == Keys.Enter)
            {
                BeginRenameType(args.Sender);
            }

            if (SelectedObject != null && args.Key == Keys.Enter)
            {
                if (this.SelectedObject is UmlProperty)
                {
                    BeginRenameProperty(args.Sender, (UmlProperty)SelectedObject);
                }
            }


            if (this.SelectedObject == null && args.Key == Keys.Delete)
            {
                MessageBox.Show("Begin delete type");
                args.Sender.Diagram.Shapes.Remove(this);
                args.Redraw = true;
            }

            if (this.SelectedObject != null && args.Key == Keys.Delete)
            {
                
                if (this.SelectedObject is UmlTypeMember)
                {
                    DeleteSelectedMember();
                    args.Redraw = true;
                }
            }
        }

        private void DeleteSelectedMember()
        {
            //delete property
            if (this.SelectedObject is UmlProperty)
            {
                MessageBox.Show("Begin delete property");
                DataSource.RemoveProperty ((UmlProperty)this.SelectedObject);
                this.SelectedObject = null;                
            }

            //delete method
            if (this.SelectedObject is UmlMethod)
            {
                MessageBox.Show("Begin delete method");
               DataSource.RemoveMethod((UmlMethod)this.SelectedObject);
                this.SelectedObject = null;
            }
        }

        private void BeginRenameProperty(UmlDesigner owner, UmlProperty property)
        {
            Rectangle bounds = owner.GetItemBounds(property);
            if (bounds == Rectangle.Empty)
                bounds = owner.GetItemBounds(AddNewPropertyIdentifier);

            Rectangle inputBounds = new Rectangle(bounds.X + Settings.memberNameIndent, bounds.Y, bounds.Width - Settings.memberNameIndent, bounds.Height);
            object oldSelectedObject = SelectedObject;
            SelectedObject = null;
            Action endRenameProperty = () =>
            {
                property.Name = owner.GetInput();
                if (property.Name == "")
                {
                    SelectedObject = property;
                    DeleteSelectedMember();
                }
                else
                {
                    SelectedObject = oldSelectedObject;
                }
            };

            owner.BeginInput(inputBounds, property.Name, Settings.memberFont, endRenameProperty);
        }

        private void BeginRenameType(UmlDesigner owner)
        {
            Rectangle inputBounds = new Rectangle (Bounds.Left + Settings.typeBoxSideMargin ,Bounds.Top + 4 ,Bounds.Width - 25 -Settings.typeBoxSideMargin,20);

            Action endRenameType = () =>
                {
                    DataSource.TypeName = owner.GetInput();
                };

            owner.BeginInput(inputBounds, DataSource.TypeName, Settings.typeNameFont, endRenameType);
        }

        
    }
}
