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
        public Logic()
        {
            points = new Points();
            pointNew = new Point();
            pointLast = new Point();
            pointOne = new Point();
        }

        static Points points;

        static Point pointNew;
        static Point pointLast;
        static Point pointOne;


        /*    public static Points Points
            {
                get
                {
                    if (points != null)
                        return points;
                    return null; //error 
                }
            }*/




        public static void Drawing(int X, int Y)
        {
            pointNew = new Point(X, Y);


            if (!EndDrawingQuestion())
            {
                points.Add(pointNew);
                //   pointLast = points.Last();
            }

            SendDrawing();

            /*
                        if (points.Count > 1)
                        {
                            Draw.DrawingFormLine(pointLast, pointNew);
                        }
                        else
                        {
                            Draw.DrawingFormLine(pointNew);
                        }

                        if(pointNew != pointLast && pointNew != pointOne)
                        Draw.DrawingFormPoint(pointNew, points.Count);
            */

        }

        public static void Calculate(int X, int Y)
        {
            


            Point[] arrayPoints = points.ToArray();

            Point point = new Point(X, Y);
            


            Point pointCheck1 = new Point(X, Y);
            Point pointCheck2 = new Point(X + 1000, Y);

           // Draw.DrawingFormLine2(pointCheck1, pointCheck2);

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

                else if (point.Y == pointLine1.Y)
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
                Draw.DrawingFormPoint(point, 1);
            else if (intersection == -1)
                Draw.DrawingFormPoint(point, 0);
            else if (intersection % 2 == 0)
                Draw.DrawingFormPoint(point, 2);

        }
      

                static bool EndDrawingQuestion()
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
                         MessageBoxDefaultButton.Button1,
                         MessageBoxOptions.DefaultDesktopOnly);

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

        public static void StopDrawing()
        {
            pointLast = points.Last();
            pointNew = pointOne;
            SendDrawing();
        }

        public static void ClearDate()
        {
            points.Clear();
            Draw.ClearForm();
        }



        static void SendDrawing()
        {
            if (points.Count > 1)
            {
                Draw.DrawingFormLine(pointLast, pointNew);
            }
            else
            {
                Draw.DrawingFormPoint(pointNew, 2);
            }

            if (pointNew != pointLast && pointNew != pointOne)
                Draw.WriteDGV(pointNew, points.Count);
        }

        public static void Save(String FileName)
        {
            Point[] arrayPoints = points.ToArray();
            DataSet ds = new DataSet(); // создаем пока что пустой кэш данных
            DataTable dt = new DataTable(); // создаем пока что пустую таблицу данных
            dt.TableName = "DataList"; // название таблицы
            dt.Columns.Add("Number"); // название колонок
            dt.Columns.Add("X");
            dt.Columns.Add("Y");
            ds.Tables.Add(dt); //в ds создается таблица, с названием и колонками, созданными выше

            for (int i =0; i< arrayPoints.Length; i++) // пока в dataGridView1 есть строки
            {
                DataRow row = ds.Tables["DataList"].NewRow(); // создаем новую строку в таблице, занесенной в ds
                row["Number"] = (i + 1).ToString();  //в столбец этой строки заносим данные из первого столбца dataGridView1
                row["X"] = arrayPoints[i].X.ToString(); // то же самое со вторыми столбцами
                row["Y"] = arrayPoints[i].Y.ToString(); //то же самое с третьими столбцами
                ds.Tables["DataList"].Rows.Add(row); //добавление всей этой строки в таблицу ds.
            }
            ds.WriteXml(FileName);
        }

        public static void Open(String FileName)
        {
            Point[] arrayPoints = points.ToArray();
            int X;
            int Y;
            ClearDate();
            
            DataSet ds = new DataSet(); // создаем пока что пустой кэш данных
            ds.ReadXml(FileName); // записываем в него XML-данные из файла
            

            foreach (DataRow item in ds.Tables["DataList"].Rows) // пока в dataGridView1 есть строки
            {
                 X = Convert.ToInt32(item["X"]);
                 Y = Convert.ToInt32(item["Y"]);

                Drawing(X, Y);     
            }
            StopDrawing();

        }
    }
}
