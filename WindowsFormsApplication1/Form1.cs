using DevExpress.Utils;
using DevExpress.Utils.Drawing;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars.Ribbon.ViewInfo;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private RibbonHitInfo dragItemHitInfo;

        public Form1()
        {
            InitializeComponent();
            galleryControlClient1.Paint += galleryControlClient1_Paint;
        }

        void galleryControlClient1_Paint(object sender, PaintEventArgs e)
        {
            GalleryControlClient client = sender as GalleryControlClient;
            if (dragItemHitInfo != null)
            {
                Point p = galleryControl1.PointToClient(Cursor.Position);
                RibbonHitInfo hitInfo = galleryControl1.CalcHitInfo(p);
                if (hitInfo.InGalleryGroup)
                {
                    GalleryControlGalleryViewInfo info = galleryControl1.Gallery.GetViewInfo() as GalleryControlGalleryViewInfo;
                    Rectangle rect = info.Groups[galleryControl1.Gallery.Groups.IndexOf(hitInfo.GalleryItemGroup)].Bounds;
                    rect.Offset(0, client.Bounds.Top * -1);
                    e.Graphics.DrawLine(new Pen(Color.PowderBlue, 3), new Point(rect.Left, rect.Bottom), new Point(rect.Right, rect.Bottom));
                }
            }
        }

        private void galleryControl1_MouseDown(object sender, MouseEventArgs e)
        {
            GalleryControl gc = sender as GalleryControl;
            RibbonHitInfo hInfo = gc.CalcHitInfo(e.Location);

            if (hInfo.InGalleryItem)
            {
                dragItemHitInfo = hInfo;
                ((DXMouseEventArgs)e).Handled = true;
                return;
            }

            dragItemHitInfo = null;         
        }

        private void galleryControl1_MouseMove(object sender, MouseEventArgs e)
        {
            GalleryControl gc = sender as GalleryControl;
            if (e.Button != MouseButtons.Left || Control.ModifierKeys != Keys.None || dragItemHitInfo == null)
                return;

            Size dragSize = SystemInformation.DragSize;
            Rectangle dragRect = new Rectangle(dragItemHitInfo.HitPoint.X - dragSize.Width / 2, dragItemHitInfo.HitPoint.Y - dragSize.Height / 2, dragSize.Width, dragSize.Height);
            if (!(dragRect.Contains(e.Location)))
                gc.DoDragDrop(dragItemHitInfo.GalleryItem, DragDropEffects.Move);
        }

        private void galleryControl1_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(GalleryItem)))
                e.Effect = DragDropEffects.Move;

            galleryControlClient1.Invalidate();
        }

        private void galleryControl1_DragDrop(object sender, DragEventArgs e)
        {
            if (!(e.Data.GetDataPresent(typeof(GalleryItem))))
                return;

            GalleryItem draggedItem = e.Data.GetData(typeof(GalleryItem)) as GalleryItem;            

            GalleryControl galControl = (GalleryControl)sender;
            RibbonHitInfo hitInfo = galControl.CalcHitInfo(galControl.PointToClient(new Point(e.X, e.Y)));
            if (hitInfo.InGalleryGroup)
            {
                draggedItem.GalleryGroup.Items.Remove(draggedItem);
                hitInfo.GalleryItemGroup.Items.Add((GalleryItem)draggedItem);
            }

            dragItemHitInfo = null;
            galleryControlClient1.Invalidate();
        }
    }
}
