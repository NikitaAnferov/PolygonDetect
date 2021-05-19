using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace PolygonDetectClassLibrary
{
    public class Logic
    {
        Draw d;
        SaveLoad sl;
        public Logic(Bitmap Bmp, DataGridView DataGridView, PictureBox PictureBox)
        {
            points = new Points();
            pointNew = new Point();
            pointLast = new Point();
            pointOne = new Point();
            d = new Draw(Bmp, DataGridView, PictureBox);
            sl = new SaveLoad();
        }

        Points points;

        Point pointNew;
        Point pointLast;
        Point pointOne;
        public void Drawing(int X, int Y)
        {
            pointNew = new Point(X, Y);

            if (!EndDrawingQuestion())
                points.Add(pointNew);

            SendDrawing();

        }

        public void Calculate(int X, int Y)
        {
            Point[] arrayPoints = points.ToArray();

            Point point = new Point(X, Y);
            

            Point pointCheck1 = new Point(X, Y);
            Point pointCheck2 = new Point(X + 1000, Y);

            Point pointLine1;
            Point pointLine2;

            int previous;
            int next;
            int point1;
            int point2;

            int intersection = 0;

            for (int i = 0; i < arrayPoints.Length; i++)
            {
                if (i == arrayPoints.Length - 1)
                {
                    point2 = 0;
                }
                else
                {
                    point2 = i + 1; 
                }

                point1 = i;

                pointLine1 = arrayPoints[point1];
                pointLine2 = arrayPoints[point2];

                if (((point.X - pointLine2.X) * (pointLine1.Y - pointLine2.Y)) == ((point.Y - pointLine2.Y) * (pointLine1.X - pointLine2.X)))
                {
                    if (((pointLine1.X < pointLine2.X) && (pointLine1.X <= point.X) && (point.X <= pointLine2.X)) || ((pointLine1.X > pointLine2.X) && (pointLine2.X <= point.X) && (point.X <= pointLine1.X)))
                    {
                        intersection = -1;
                        break;
                    }
                        
                }

                else if (pointCheck1.Y == pointLine1.Y && pointCheck1.Y == pointLine2.Y)
                {
                    if (point1 == 0)
                        previous = arrayPoints.Length - 1;
                    else
                        previous = point1 - 1;
                    if (point1 == arrayPoints.Length - 1)
                        next = 0;
                    else
                        next = point2 + 1;

                    if (((pointCheck1.Y - arrayPoints[previous].Y) * (pointCheck1.Y - arrayPoints[next].Y)) < 0)
                        intersection++;
                    else if (((pointCheck1.Y - arrayPoints[previous].Y) * (pointCheck1.Y - arrayPoints[next].Y)) > 0)
                        intersection += 2;
                }

                else if (point.Y == pointLine1.Y && point.X < pointLine1.X)
                {
                    if (point1 == 0)
                        previous = arrayPoints.Length - 1;
                    else
                        previous = point1 - 1;

                    if (((point.Y - arrayPoints[previous].Y) * (point.Y - arrayPoints[point2].Y)) < 0)
                        intersection++;
                    else if (((point.Y - arrayPoints[previous].Y) * (point.Y - arrayPoints[point2].Y)) > 0)
                        intersection += 2;
                }
                else
                {

                    Point V12 = new Point(pointCheck2.X - pointCheck1.X, pointCheck2.Y - pointCheck1.Y);
                    Point V34 = new Point(pointLine2.X - pointLine1.X, pointLine2.Y - pointLine1.Y);

                    Point V31 = new Point(pointCheck1.X - pointLine1.X, pointCheck1.Y - pointLine1.Y);
                    Point V32 = new Point(pointCheck2.X - pointLine1.X, pointCheck2.Y - pointLine1.Y);

                    Point V13 = new Point(pointLine1.X - pointCheck1.X, pointLine1.Y - pointCheck1.Y);
                    Point V14 = new Point(pointLine2.X - pointCheck1.X, pointLine2.Y - pointCheck1.Y);


                    long v1 = V34.X * V31.Y - V34.Y * V31.X;

                    long v2 = V34.X * V32.Y - V34.Y * V32.X;

                    long v3 = V12.X * V13.Y - V12.Y * V13.X;

                    long v4 = V12.X * V14.Y - V12.Y * V14.X;

                    if (v1 * v2 < 0 && v3 * v4 < 0)
                        intersection++;
                }
            }


            if (intersection % 2 == 1)
                d.DrawingFormPoint(point, "Green");
            else if (intersection == -1)
                d.DrawingFormPoint(point, "Yellow");
            else if (intersection % 2 == 0)
                d.DrawingFormPoint(point, "Red");

        }
      
        bool EndDrawingQuestion()
        {
            bool endDraving = false;

            if (points.Count != 0)
            {
                pointOne = points.ElementAt(0);
                pointLast = points.Last();

                if (Math.Abs(pointOne.X - pointNew.X) <= 10 && Math.Abs(pointOne.Y - pointNew.Y) <= 10)
                {
                    DialogResult result = MessageBox.Show(
                         "Закончить рисование фигуры?",
                         "Сообщение",
                         MessageBoxButtons.YesNo,
                         MessageBoxIcon.Question,
                         MessageBoxDefaultButton.Button1);

                    if (result == DialogResult.Yes)
                    {
                        pointNew = pointOne;
                        endDraving = true;
                    }
                    else points.Add(pointNew);
                }
            }

            return endDraving;
        }

        public void StopDrawing()
        {
            if (points.Count > 1)
            {
                pointLast = points.Last();
                pointNew = pointOne;
                SendDrawing();
            }
        }

        public void ClearData()
        {
            points.Clear();
            points = new Points();
            pointNew = new Point();
            pointLast = new Point();
            pointOne = new Point();
            d.ClearForm();
        }

        void SendDrawing()
        {
            if (points.Count > 1)
            {
                d.DrawingFormLine(pointLast, pointNew);
            }
            else
            {
                d.DrawingFormPoint(pointNew, "Red");
            }

            if (pointNew != pointLast && pointNew != pointOne)
                d.WriteDGV(pointNew, points.Count);
        }

        public SaveFileDialog LoadSaveFileDialog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog(); // открытие окна для праметров сохранения файла
            saveFileDialog.Filter = "XML files(.xml)|*.xml|all Files(*.*)|*.*"; // задаем тип сохраняемого файла
            saveFileDialog.FileName = "Шаблон"; // задаем название сохраняемого файла;
            return saveFileDialog;
        }

        public OpenFileDialog LoadOpenFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog(); // открытие окна для праметров сохранения файла
            openFileDialog.Filter = "XML files(.xml)|*.xml|all Files(*.*)|*.*"; // задаем тип сохраняемого файла
            openFileDialog.FileName = "Шаблон"; // задаем название сохраняемого файла;
            return openFileDialog;
        }

        public void Save(String FileName)
        {
            if(sl.Save(FileName, points))
                MessageBox.Show("Данные о координатах фигуры успешно сохранены",
                         "Сообщение",
                         MessageBoxButtons.OK,
                         MessageBoxIcon.Information,
                         MessageBoxDefaultButton.Button1);
        }

        public void Open(String FileName)
        {
            int [,] XY = sl.Open(FileName, points);

            if(XY != null)
            {
                ClearData();

                for (int i = 0; i < XY.GetLength(0); i++)
                    Drawing(XY[i, 0], XY[i, 1]);
            }

            StopDrawing();
        }
    }
}
