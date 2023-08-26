using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CommonUI
{
    public class ComboBoxExt : ComboBox
    {
        private ImageList imgList;


        public ComboBoxExt()
        {
            DrawMode = DrawMode.OwnerDrawVariable;
            DropDownStyle = ComboBoxStyle.DropDownList;

            DropDownHeight = 300;
        }

        /// <summary>
		/// The imagelist holds the images displayed with the items in the combobox.
		/// </summary>
		[Category("Behavior")]
        [Browsable(true)]
        [Description("The ImageList control from which the images to be displayed with the items are taken.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public ImageList ImageList
        {
            get
            {
                return imgList;
            }
            set
            {
                imgList = value;
                // prepare the dropdown Images List from which user can choose an image for corresponding item
                //DropDownImages.imageList = imgList;
            }

        }




        /// <summary>
        /// when the imagecombobox drawmode is ownerdraw variable, 
        /// each item's height and width need to be measured, inorder to display them properly.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMeasureItem(MeasureItemEventArgs e)
        {
            base.OnMeasureItem(e);
            if (this.DataSource != null)
            {
                return; // currently ownerdrawvariable support is implemented only for Items Collection.
            }
            if (e.Index >= 0 && this.Items.Count > 0 && e.Index < this.Items.Count)
            {
                Font itemFont = this.Font;
                SizeF TextSize = e.Graphics.MeasureString(this.Items[e.Index].ToString(), itemFont);
                e.ItemHeight = (int)TextSize.Height * 2;
                e.ItemWidth = (int)TextSize.Width;
            }
        }

        /// <summary>
		/// Because the combobox is ownerdrawn we have draw each item along with the associated image.
		/// In the case of datasource the displaymember and imagemember are taken from the datasource.
		/// If datasource is not set the items in the Items collection are drawn.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnDrawItem(DrawItemEventArgs e)
        {
            base.OnDrawItem(e);
            if (e.Index >= 0 && Items.Count > 0 && e.Index < Items.Count)
            {
                e.DrawBackground();
                e.DrawFocusRectangle();

                StringFormat format = new StringFormat();
                //format.FormatFlags = StringFormatFlags.NoWrap;
                format.FormatFlags = StringFormatFlags.NoClip;
                format.Alignment = StringAlignment.Near;
                format.LineAlignment = StringAlignment.Center;

                if (ImageList != null && ImageList.Images.Count > 0)
                {
                    Image theIcon = this.ImageList.Images[e.Index % ImageList.Images.Count];
                    Bitmap ImageToDraw = new Bitmap(theIcon, e.Bounds.Height - 1, e.Bounds.Height - 1);
                    int IconHeight = ImageToDraw.Height;
                    int IconWidth = ImageToDraw.Width;

                    RectangleF itemRect = new RectangleF((float)(e.Bounds.X + IconWidth + 3), (float)(e.Bounds.Y), (float)(e.Bounds.Width - IconWidth - 3), (float)(e.Bounds.Height));
                    e.Graphics.DrawString(Items[e.Index].ToString(), this.Font, new SolidBrush(e.ForeColor), itemRect, format);
                    Rectangle imageRect = new Rectangle(e.Bounds.X + 1, e.Bounds.Y, IconWidth, IconHeight);
                    e.Graphics.DrawImage(ImageToDraw, imageRect);
                }
                else
                {
                    e.Graphics.DrawString(Items[e.Index].ToString(), this.Font, new SolidBrush(e.ForeColor), new RectangleF((float)(e.Bounds.X + 5), (float)e.Bounds.Y, (float)e.Bounds.Width, (float)e.Bounds.Height), format);
                }
            }
        }
    }

}
