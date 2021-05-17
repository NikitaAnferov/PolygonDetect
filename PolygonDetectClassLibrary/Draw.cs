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
            DrawingFormPoint(pointNew, 2);
            DrawingFormPoint(pointLast, 2);
        }

        public static void DrawingFormPoint(Point point, int selector)
        {
            switch (selector)
            {
                case (0):
                    g.FillRectangle(Brushes.Yellow, point.X - 4, point.Y - 4, 7, 7);
                    break;
                case (1):
                    g.FillRectangle(Brushes.Green, point.X - 4, point.Y - 4, 7, 7);
                    break;
                case (2):
                    g.FillRectangle(Brushes.Red, point.X - 4, point.Y - 4, 7, 7);
                    break;
                default:
                    break;
            }
           
        }

        public static void DrawingFormLine2(Point pointNew)
        {
            g.FillRectangle(Brushes.Green, pointNew.X - 4, pointNew.Y - 4, 7, 7);
        }

        public static void DrawingFormLine2(Point pointLast, Point pointNew)
        {
            g.DrawLine(new Pen(Brushes.Blue, 1), pointLast, pointNew);
        }

        public static void WriteDGV(Point pointNew, int count)
        {
            dataGridView.Rows.Add(count, pointNew.X, pointNew.Y);
        }

        public static void ClearForm()
        {
            g.Clear(Color.White);
            dataGridView.Rows.Clear();
        }
        
    }
}
