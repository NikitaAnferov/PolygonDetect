using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PolygonDetectClassLibrary
{
    public class Draw
    {
        static Graphics g;
        static DataGridView dataGridView;

        public Draw(IntPtr handle, DataGridView DataGridView)
        {
            g = Graphics.FromHwnd(handle);
            dataGridView = DataGridView;
        }

        public static void DrawingFormLine(Point pointLast, Point pointNew)
        {
            g.DrawLine(new Pen(Brushes.Black, 2), pointLast, pointNew);
            g.FillRectangle(Brushes.Red, pointNew.X - 4, pointNew.Y - 4, 7, 7);
            g.FillRectangle(Brushes.Red, pointLast.X - 4, pointLast.Y - 4, 7, 7);
        }

        public static void DrawingFormLine(Point pointNew)
        {
            g.FillRectangle(Brushes.Red, pointNew.X - 4, pointNew.Y - 4, 7, 7);
        }

        public static void DrawingFormPoint(Point pointNew, int count)
        {
            dataGridView.Rows.Add(count, pointNew.X, pointNew.Y);
        }
        
    }
}
