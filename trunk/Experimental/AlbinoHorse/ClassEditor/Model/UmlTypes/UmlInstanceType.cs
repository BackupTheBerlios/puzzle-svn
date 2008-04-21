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
    public abstract class UmlInstanceType : UmlType
    {
        #region Properties (+ fields)

        public IList<UmlTypeMemberSection> TypeMemberSections { get; set; }

        #region Bounds property
        public override Rectangle Bounds
        {
            get
            {
                if (Expanded)
                {
                    int sectionsHeight = 0;
                    foreach (UmlTypeMemberSection section in TypeMemberSections)
                    {
                        int sectionHeight = section.Expanded ? (section.TypeMembers.Count + 1) * 16 + 20 : 20;
                        sectionsHeight += sectionHeight;
                    }
                    return new Rectangle(DataSource.X, DataSource.Y, DataSource.Width, 55 + sectionsHeight);
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





        #region PropertiesExpanded
        public bool PropertiesExpanded { get; set; }
        #endregion

        #region TypeMembers property
        public IList<UmlTypeMember> TypeMembers
        {
            get
            {
                return TypedDataSource.GetTypeMembers();
            }
        }
        #endregion

        #region TypedDataSource property
        private IUmlInstanceTypeData TypedDataSource
        {
            get
            {
                return DataSource as IUmlInstanceTypeData;
            }
        }
        #endregion

        #endregion

        #region Identifiers
        //bounding box identifiers        
        protected readonly object PropertiesIdentifier = new object();
        protected readonly object AddNewPropertyIdentifier = new object();
        #endregion

        #region Ctor
        public UmlInstanceType()
        {
            TypeMemberSections = GetTypeMemberSections();

            DataSource = new DefaultUmlInstanceTypeData();
            PropertiesExpanded = true;
        }

        #endregion

        #region Draw

        public override void Draw(RenderInfo info)
        {
            

            base.Draw(info);
        }

        

        

        protected override int DrawExpandedBody(RenderInfo info, int x, int width, int currentY)
        {
            foreach (UmlTypeMemberSection section in TypeMemberSections)
            {
                currentY = DrawTypeMembers(info, x, currentY, width, section);
            }
            return currentY;
        }

        private int DrawTypeMembers(RenderInfo info, int x, int y, int width,UmlTypeMemberSection section)
        {
            Rectangle memberCaptionBounds = new Rectangle(x, y, width, 20);
            #region add properties header bbox
            BoundingBox bboxGroup = new BoundingBox();
            bboxGroup.Bounds = memberCaptionBounds;
            bboxGroup.Target = this;
            bboxGroup.Data = section.CaptionIdentifier;
            info.BoundingBoxes.Add(bboxGroup);
            #endregion

            if (this.SelectedObject == PropertiesIdentifier && Selected)
            {
                info.Graphics.FillRectangle(SystemBrushes.Highlight, memberCaptionBounds);
                memberCaptionBounds.X += 20;
                memberCaptionBounds.Width -= 30;
                memberCaptionBounds.Y += 3;
                info.Graphics.DrawString(section.Name, Settings.Fonts.SectionCaption, SystemBrushes.HighlightText, memberCaptionBounds);
            }
            else
            {
                Brush sectionCaptionBrush = GetSectionCaptionBrush();                
                info.Graphics.FillRectangle(sectionCaptionBrush, memberCaptionBounds);
                
                memberCaptionBounds.X += 20;
                memberCaptionBounds.Width -= 30;
                memberCaptionBounds.Y += 3;
                info.Graphics.DrawString(section.Name, Settings.Fonts.SectionCaption, Brushes.Black, memberCaptionBounds);
            }

            if (section.Expanded)
                info.Graphics.DrawImage(global::AlbinoHorse.ClassDesigner.Properties.Resources.CollapseSection, x+3, y+3);
            else
                info.Graphics.DrawImage(global::AlbinoHorse.ClassDesigner.Properties.Resources.ExpandSection, x+3, y+3);


            int currentY = y + 20;
            if (section.Expanded)
            {
                StringFormat sf = StringFormat.GenericTypographic;
                sf.Trimming = StringTrimming.EllipsisCharacter;
                foreach (UmlTypeMember member in section.TypeMembers)
                {
                    Rectangle memberBounds = new Rectangle(x + Settings.Margins.typeBoxSideMargin, currentY, width - 20, 16);
                    #region add property bbox
                    BoundingBox memberBBox = new BoundingBox();
                    memberBBox.Target = this;
                    memberBBox.Bounds = memberBounds;
                    memberBBox.Data = member;
                    info.BoundingBoxes.Add(memberBBox);
                    #endregion

                    Rectangle layoutBounds = new Rectangle(x + Settings.Margins.typeBoxSideMargin + Settings.Margins.TypeMemberNameIndent, currentY, width - 5 - Settings.Margins.TypeMemberNameIndent, 16);

                    Font font = GetTypeMemberFont();
                    if (member == SelectedObject && this.Selected)
                    {
                        Rectangle selectionBounds = new Rectangle(x, currentY, width, 16);
                        info.Graphics.FillRectangle(SystemBrushes.Highlight, selectionBounds);                        
                        info.Graphics.DrawString(member.DataSource.Name, font, SystemBrushes.HighlightText, layoutBounds, sf);
                    }
                    else
                    {
                        info.Graphics.DrawString(member.DataSource.Name, font, Brushes.Black, layoutBounds, sf);
                    }
                    info.Graphics.DrawImage(member.DataSource.GetImage(), x + 13, currentY);
                    currentY += 16;
                }

                Rectangle newLayoutBounds = new Rectangle(x + Settings.Margins.typeBoxSideMargin + Settings.Margins.TypeMemberNameIndent, currentY, width - 5 - Settings.Margins.TypeMemberNameIndent, 16);
                info.Graphics.DrawString("Add new", Settings.Fonts.NewTypeMember, Brushes.Blue, newLayoutBounds, sf);

                BoundingBox newMemberBBox = new BoundingBox();
                newMemberBBox.Target = this;
                newMemberBBox.Bounds = new Rectangle(x + Settings.Margins.typeBoxSideMargin, currentY, width - 20, 16);
                newMemberBBox.Data = section.AddNewIdentifier;
                info.BoundingBoxes.Add(newMemberBBox);

                currentY += 16;
            }
            return currentY;
        }

        protected abstract Font GetTypeMemberFont();
        protected abstract Brush GetSectionCaptionBrush();        

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
                UmlTypeMember newProperty = TypedDataSource.CreateTypeMember("Property");
                this.SelectedObject = newProperty;

                BeginRenameProperty(args.Sender, newProperty);

                args.Redraw = true;
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

            if (args.BoundingBox.Data == CaptionIdentifier)
            {
                BeginRenameType(args.Sender);
            }

            if (args.BoundingBox.Data is UmlTypeMember)
            {
                BeginRenameProperty(args.Sender, args.BoundingBox.Data as UmlTypeMember);
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
                if (this.SelectedObject is UmlTypeMember)
                {
                    BeginRenameProperty(args.Sender, (UmlTypeMember)SelectedObject);
                }
            }

            if (this.SelectedObject == null && args.Key == Keys.Delete)
            {
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
            if (this.SelectedObject is UmlTypeMember)
            {
                TypedDataSource.RemoveTypeMember((UmlTypeMember)this.SelectedObject);
                this.SelectedObject = null;
            }
        }

        private void BeginRenameProperty(UmlDesigner owner, UmlTypeMember property)
        {
            Rectangle bounds = owner.GetItemBounds(property);
            if (bounds == Rectangle.Empty)
                bounds = owner.GetItemBounds(AddNewPropertyIdentifier);

            Rectangle inputBounds = new Rectangle(bounds.X + Settings.Margins.TypeMemberNameIndent, bounds.Y, bounds.Width - Settings.Margins.TypeMemberNameIndent, bounds.Height);
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

            Font font = GetTypeMemberFont();
            owner.BeginInput(inputBounds, property.DataSource.Name, font, endRenameProperty);
        }

        protected abstract IList<UmlTypeMemberSection> GetTypeMemberSections();        
    }
}
