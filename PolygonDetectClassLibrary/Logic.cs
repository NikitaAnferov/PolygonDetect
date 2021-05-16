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
                Draw.DrawingFormLine(pointNew);
            }

            if (pointNew != pointLast && pointNew != pointOne)
                Draw.DrawingFormPoint(pointNew, points.Count);
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
            /*  XmlTextWriter textWritter = new XmlTextWriter(FileName, Encoding.UTF8);
              textWritter.WriteStartDocument();
              textWritter.WriteStartElement("data");
              textWritter.WriteEndElement();
              textWritter.Close();

              XmlDocument document = new XmlDocument();

              document.Load(FileName);



              for (int i = 0; i < arrayPoints.Length; i++)
              {
                  XmlNode element = document.CreateElement("point");
                  document.DocumentElement.AppendChild(element); // указываем родителя
                  XmlAttribute attribute = document.CreateAttribute("number"); // создаём атрибут
                  attribute.Value = Convert.ToString(i + 1); // устанавливаем значение атрибута
                  element.Attributes.Append(attribute); // добавляем атрибут

                  XmlNode subElement1 = document.CreateElement("X"); // даём имя
                  subElement1.InnerText = Convert.ToString(arrayPoints[i].X); // и значение
                  element.AppendChild(subElement1); // и указываем кому принадлежит

                  XmlNode subElement2 = document.CreateElement("Y"); // даём имя
                  subElement2.InnerText = Convert.ToString(arrayPoints[i].Y); // и значение
                  element.AppendChild(subElement2); // и указываем кому принадлежит


              }
              document.Save(FileName);*/
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
