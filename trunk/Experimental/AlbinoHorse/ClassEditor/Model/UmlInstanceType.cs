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
    public class UmlInstanceType : UmlType
    {
        #region Properties (+ fields)

        #region Bounds property
        public override Rectangle Bounds
        {
            get
            {
                if (Expanded)
                {
                    int propertiesHeight = ((TypedDataSource.GetPropertyCount() + 1) * (PropertiesExpanded ? 1 : 0)) * 16 + 20;
                    int methodsHeight = ((TypedDataSource.GetMethodCount() + 1) * (MethodsExpanded ? 1 : 0)) * 16 + 20;
                    return new Rectangle(DataSource.X, DataSource.Y, DataSource.Width, 55 + propertiesHeight + methodsHeight);
                }
                else
                {
                    return new Rectangle(DataSource.X, DataSource.Y, DataSource.Width, 63 - 3);
                }
            }
            set
            {
                DataSource.X = value.X;
                DataSource.Y = value.Y;
                DataSource.Width = value.Width;

                base.Bounds = value;
            }
        }
        #endregion

        #region InheritsTypeName property
        public string InheritsTypeName
        {
            get
            {
                return TypedDataSource.InheritsTypeName;
            }
            set
            {
                TypedDataSource.InheritsTypeName = value;
            }
        }
        #endregion

        #region IsAbstract property
        public bool IsAbstract
        {
            get
            {
                return TypedDataSource.IsAbstract;
            }
            set
            {
                TypedDataSource.IsAbstract = value;
            }
        }
        #endregion

        #region PropertiesExpanded
        public bool PropertiesExpanded { get; set; }
        #endregion

        #region MethodsExpanded
        public bool MethodsExpanded { get; set; }
        #endregion

        public IUmlInstanceTypeData TypedDataSource
        {
            get
            {
                return DataSource as IUmlInstanceTypeData;
            }
        }

        #endregion

        #region Identifiers
        //bounding box identifiers        
        protected readonly object PropertiesIdentifier = new object();
        protected readonly object MethodsIdentifier = new object();
        protected readonly object AddNewPropertyIdentifier = new object();
        protected readonly object AddNewMethodIdentifier = new object();
        #endregion

        

        #region Ctor
        public UmlInstanceType()
        {
            DataSource = new DefaultUmlTypeData();
            PropertiesExpanded = true;
            MethodsExpanded = true;
        }
        #endregion

        #region Draw

        

        public override void Draw(RenderInfo info)
        {
            int grid = info.GridSize;
            Rectangle renderBounds = Bounds;

            BoundingBox bboxThis = new BoundingBox();
            bboxThis.Bounds = renderBounds;
            bboxThis.Target = this;
            bboxThis.Data = this.BodyIdentifier;
            info.BoundingBoxes.Add(bboxThis);

            int x = renderBounds.X;
            int y = renderBounds.Y;
            int radius = 16;
            int width = renderBounds.Width;
            int height = renderBounds.Height;

            GraphicsPath path =  GetOutlinePath(radius, x, y, width, height);

            LinearGradientBrush captionBrush = null;
            if (Selected)
                captionBrush = new LinearGradientBrush(renderBounds, Color.FromArgb(190, 202, 230), Color.White, 0, true);
            else
                captionBrush = new LinearGradientBrush(renderBounds, Color.FromArgb(210, 222, 240), Color.White, 0, true);

            Pen borderPen = null;

            if (IsAbstract)
                borderPen = Settings.abstractBorderPen;
            else
                borderPen = Settings.normalBorderPen;


            if (Expanded)
            {
                DrawExpanded(info, path, x, y, width, height, captionBrush, borderPen);
            }
            else
            {
                DrawCollapsed(info, path, x, y, width, height, captionBrush, borderPen);
            }


            DrawTypeExpander(info, x, y, width);
            DrawSelection(info);

            Rectangle typeNameBounds = new Rectangle(x + Settings.typeBoxSideMargin, y + 4, width - Settings.typeBoxSideMargin * 2, 10);
            Rectangle typeKindBounds = new Rectangle(x + Settings.typeBoxSideMargin, y + 4 + 15, width - Settings.typeBoxSideMargin * 2, 10);

            Font typeNameFont = null;
            string kind = "";
            if (IsAbstract)
            {
                typeNameFont = Settings.abstractTypeNameFont;
                kind = "Abstract class";
            }
            else
            {
                typeNameFont = Settings.normalTypeNameFont;
                kind = "Class";
            }

            info.Graphics.DrawString(DataSource.TypeName, typeNameFont, Brushes.Black, typeNameBounds, StringFormat.GenericTypographic);
            info.Graphics.DrawString(kind, Settings.typeKindFont, Brushes.Black, typeKindBounds, StringFormat.GenericTypographic);

            

            if (InheritsTypeName != null)
            {
                info.Graphics.DrawImage(global::AlbinoHorse.ClassDesigner.Properties.Resources.InheritanceArrow, x + Settings.typeBoxSideMargin, y + 35);
                Rectangle typeInheritsBounds = new Rectangle(x + 24, y + 33, width - 26, 10);
                info.Graphics.DrawString(InheritsTypeName, Settings.typeInheritsFont, Brushes.Black, typeInheritsBounds, StringFormat.GenericTypographic);
            }


            

        }



        protected override int DrawExpandedBody(RenderInfo info, int x, int width, int currentY)
        {
            currentY = DrawProperties(info, x, currentY, width);
            currentY = DrawMethods(info, x, currentY, width);
            return currentY;
        }

        

        private int DrawProperties(RenderInfo info, int x, int y, int width)
        {
            Rectangle memberCaptionBounds = new Rectangle(x, y, width, 20);
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






            int currentY = y + 20;
            if (PropertiesExpanded)
            {
                StringFormat sf = StringFormat.GenericTypographic;
                sf.Trimming = StringTrimming.EllipsisCharacter;
                foreach (UmlProperty member in TypedDataSource.GetProperties())
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
                        info.Graphics.DrawString(member.DataSource.Name, Settings.memberFont, SystemBrushes.HighlightText, layoutBounds, sf);
                    }
                    else
                    {
                        info.Graphics.DrawString(member.DataSource.Name, Settings.memberFont, Brushes.Black, layoutBounds, sf);
                    }
                    info.Graphics.DrawImage(member.DataSource.GetImage(), x + 13, currentY);
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

        private int DrawMethods(RenderInfo info, int x, int y, int width)
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
                foreach (UmlMethod member in TypedDataSource.GetMethods())
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

        


        #endregion

        #region Mouse Events
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
            else if (args.BoundingBox.Data is UmlProperty)
            {
                this.SelectedObject = args.BoundingBox.Data as UmlProperty;
                args.Redraw = true;
            }
            else if (args.BoundingBox.Data is UmlMethod)
            {
                this.SelectedObject = args.BoundingBox.Data as UmlMethod;
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
                UmlProperty newProperty = TypedDataSource.CreateProperty();
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
                    Bounds = new Rectangle(Bounds.Right - 100, Bounds.Y, 100, Bounds.Height);
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
                BeginRenameProperty(args.Sender, args.BoundingBox.Data as UmlProperty);
                args.Redraw = true;
            }


        }
        #endregion

        

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
                args.Sender.Diagram.Shapes.Remove(this);
                args.Redraw = true;
            }

            if (this.SelectedObject != null && args.Key == Keys.Delete)
            {

                if (this.SelectedObject is UmlProperty)
                {
                    DeleteSelectedMember();
                    args.Redraw = true;
                }
                else if (this.SelectedObject is UmlMethod)
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
                TypedDataSource.RemoveProperty((UmlProperty)this.SelectedObject);
                this.SelectedObject = null;
            }

            //delete method
            if (this.SelectedObject is UmlMethod)
            {
                MessageBox.Show("Begin delete method");
                TypedDataSource.RemoveMethod((UmlMethod)this.SelectedObject);
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
                property.DataSource.Name = owner.GetInput();
                if (property.DataSource.Name == "")
                {
                    SelectedObject = property;
                    DeleteSelectedMember();
                }
                else
                {
                    SelectedObject = oldSelectedObject;
                }
            };

            owner.BeginInput(inputBounds, property.DataSource.Name, Settings.memberFont, endRenameProperty);
        }

        private void BeginRenameType(UmlDesigner owner)
        {
            Rectangle inputBounds = new Rectangle(Bounds.Left + Settings.typeBoxSideMargin, Bounds.Top + 4, Bounds.Width - 25 - Settings.typeBoxSideMargin, 20);

            Action endRenameType = () =>
                {
                    DataSource.TypeName = owner.GetInput();
                };

            owner.BeginInput(inputBounds, DataSource.TypeName, Settings.normalTypeNameFont, endRenameType);
        }


    }
}
