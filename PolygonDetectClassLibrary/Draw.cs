using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PolygonDetectClassLibrary
{
    internal class Draw
    {
        const int SIZE_POINT = 7; // настройка размера точки
        const int SIZE_LINE = 2;  // настройка размера линии

        Graphics g;
        DataGridView dataGridView;
        Bitmap bmp;
        PictureBox pictureBox;
        Dictionary <String, Brush> brushes;

        internal Draw(Bitmap bmp, DataGridView dataGridView, PictureBox pictureBox)
        {
            g = Graphics.FromImage(bmp);
            this.dataGridView = dataGridView;
            this.bmp = bmp;
            this.pictureBox = pictureBox;

            brushes = new Dictionary<String, Brush>(3);
            brushes.Add("Yellow",Brushes.Yellow);
            brushes.Add("Green",Brushes.Green);
            brushes.Add("Red",Brushes.Red);
        }

        internal void DrawingFormLine(Point pointLast, Point pointNew)
        {
            int sizeLine = SIZE_LINE;

            g.DrawLine(new Pen(Brushes.Black, sizeLine), pointLast, pointNew);
            DrawingFormPoint(pointNew, "Red");
            DrawingFormPoint(pointLast, "Red");
            pictureBox.Image = bmp;
        }

        internal void DrawingFormPoint(Point point, string color)
        {
            int sizePoint = SIZE_POINT + 1 - SIZE_POINT % 2;
            int coef = sizePoint / 2 + 1;

            g.FillRectangle(brushes[color], point.X - coef, point.Y - coef, sizePoint, sizePoint);
            pictureBox.Image = bmp;

        }

        internal void WriteDGV(Point pointNew, int count)
        {
            dataGridView.Rows.Add(count, pointNew.X, pointNew.Y);
        }

        internal void ClearForm()
        {
            g.Clear(Color.White);
            dataGridView.Rows.Clear();
            pictureBox.Image = bmp;
        }

    }
}
