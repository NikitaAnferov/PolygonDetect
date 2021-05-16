using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using PolygonDetectClassLibrary;

namespace PolygonDetect
{
    public partial class Form1 : Form
    {
        String selectorDo = "0";
        Logic l;
        Draw d;
 //       List<Size> arrayPF = new List<Size>();


        public Form1()
        {
            InitializeComponent();
            l = new Logic(); // Инициализируем объект отвечающий за логику программы
            d = new Draw(pictureBox1.Handle, dataGridView1); // Инициализируем объект отвечающий за отрисовку данных на форме
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            //Use default property values
            //myPoints.Add(new MyPoints.DrawingPoint(e.Location));




        }

    /*    private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Pen blackPen = new Pen(Color.Black, 3);

            PointF point1 = new PointF(50.0F, 50.0F);
            PointF point2 = new PointF(100.0F, 25.0F);
            PointF point3 = new PointF(200.0F, 5.0F);
            PointF point4 = new PointF(250.0F, 50.0F);
            PointF point5 = new PointF(300.0F, 100.0F);
            PointF point6 = new PointF(350.0F, 200.0F);
            PointF point7 = new PointF(250.0F, 250.0F);
            PointF[] curvePoints =
                     {
                 point1,
                 point2,
                 point3,
                 point4,
                 point5,
                 point6,
                 point7
             };

            // Draw polygon curve to screen.
            e.Graphics.DrawPolygon(blackPen, curvePoints);
        }*/

        private void button1_Click(object sender, EventArgs e)
        {
         
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            switch (selectorDo)
            {
                case ("Draw"):
                    Logic.Drawing(e.X, e.Y);





                    break;
                case ("Test"):
                 /*   g.FillRectangle(Brushes.Green, e.X - 4, e.Y - 4, 7, 7);
                    g.DrawLine(new Pen(Brushes.Blue, 2), new Point(e.X, e.Y), new Point(e.X+1000, e.Y));
                    Size[] arrayS = arrayPF.ToArray();

                    Point p2Dot1;
                    Point p2Dot2;
                    int q = 0;

                    for (int i = 0; i < arrayS.Length; i++)
                    {
                        if (i == arrayS.Length - 1)
                        {
                             p2Dot1 = new Point(arrayS[i].Width, arrayS[i].Height);
                             p2Dot2 = new Point(arrayS[0].Width, arrayS[0].Height);
                        }
                        else 
                        {
                             p2Dot1 = new Point(arrayS[i].Width, arrayS[i].Height);
                             p2Dot2 = new Point(arrayS[i + 1].Width, arrayS[i + 1].Height);
                        }


                        Point p1Dot1 = new Point (e.X, e.Y);
                        Point p1Dot2 = new Point(e.X + 1000, e.Y);


                        Point V12 = new Point(p1Dot2.X - p1Dot1.X, p1Dot2.Y - p1Dot1.Y);
                        Point V34 = new Point(p2Dot2.X - p2Dot1.X, p2Dot2.Y - p2Dot1.Y);

                        Point V31 = new Point(p1Dot1.X - p2Dot1.X, p1Dot1.Y - p2Dot1.Y);
                        Point V32 = new Point(p1Dot2.X - p2Dot1.X, p1Dot2.Y - p2Dot1.Y);

                        Point V13 = new Point(p2Dot1.X - p1Dot1.X, p2Dot1.Y - p1Dot1.Y);
                        Point V14 = new Point(p2Dot2.X - p1Dot1.X, p2Dot2.Y - p1Dot1.Y);*/

                        /*  int a1 = p1Dot2.Y - p1Dot1.Y;---------------------------
                          int b1 = p1Dot1.X - p1Dot2.X;
                          int c1 = -p1Dot1.X * p1Dot2.Y + p1Dot1.Y * p1Dot2.X;

                          int a2 = p2Dot2.Y - p2Dot1.Y;
                          int b2 = p2Dot1.X - p2Dot2.X;
                          int c2 = -p2Dot1.X * p2Dot2.Y + p2Dot1.Y * p2Dot2.X;

                          Point pCross = new Point();

                          pCross.X = (b1 * c2 - b2 * c1) / (a1 * b2 - a2 * b1);
                          pCross.Y = (a2 * c1 - a1 * c2) / (a1 * b2 - a2 * b1);*/

                    /*    long v1 = V34.X * V31.Y - V34.Y * V31.X;

                        long v2 = V34.X * V32.Y - V34.Y * V32.X;

                        long v3 = V12.X * V13.Y - V12.Y * V13.X;

                        long v4 = V12.X * V14.Y - V12.Y * V14.X;

                         if(v1 * v2 < 0 && v3 * v4 < 0)
                            q++;

                        
                    }

                    if(q %2  == 1)
                        g.FillRectangle(Brushes.Black, e.X - 6, e.Y - 6, 11, 11);
                    int s = 0;*/
                    //вызов алгоритма для получения рещультата
                    /*
                    if (true)
                    {
                        button1.Text = "Внутри";
                    }
                    else
                    {
                        button1.Text = "Снаружи";
                    }
                    */
                    break;
                default:
                    break;

            }
        }

        private void buttonDraw_Click(object sender, EventArgs e)
        {
            string drawClick = "Draw";
            if (selectorDo == drawClick)
            { 
                selectorDo = "0";
                Logic.StopDrawing();
            }
            else
                selectorDo = drawClick;
        }



        private void buttonTest_Click(object sender, EventArgs e)
        {
            string testClick = "Test";
            if (selectorDo == testClick)
                selectorDo = "0";
            else
            {
                Logic.StopDrawing();
                selectorDo = testClick;
            }
                
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            textBoxX.Text = e.X.ToString();
            textBoxY.Text = e.Y.ToString();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
           /* Size[] arrayS = arrayPF.ToArray();
            XmlTextWriter textWritter = new XmlTextWriter("test.xml", Encoding.UTF8);
            textWritter.WriteStartDocument();
            textWritter.WriteStartElement("data");
            textWritter.WriteEndElement();
            textWritter.Close();

            XmlDocument document = new XmlDocument();

            document.Load("test.xml");

            

            for (int i = 0; i < arrayS.Length; i++)
            {
                XmlNode element = document.CreateElement("point");
                document.DocumentElement.AppendChild(element); // указываем родителя
                XmlAttribute attribute = document.CreateAttribute("number"); // создаём атрибут
                attribute.Value = Convert.ToString(i + 1); // устанавливаем значение атрибута
                element.Attributes.Append(attribute); // добавляем атрибут

                XmlNode subElement1 = document.CreateElement("X"); // даём имя
                subElement1.InnerText = Convert.ToString(arrayS[i].Width); // и значение
                element.AppendChild(subElement1); // и указываем кому принадлежит

                XmlNode subElement2 = document.CreateElement("Y"); // даём имя
                subElement2.InnerText = Convert.ToString(arrayS[i].Height); // и значение
                element.AppendChild(subElement2); // и указываем кому принадлежит


            }
            document.Save("test.xml");



            */
        }

        private void buttonInput_Click(object sender, EventArgs e)
        {
            Logic.Drawing(Convert.ToInt32(textBoxX.Text), Convert.ToInt32(textBoxY.Text));
        }



        /*  MyPoints myPoints = new MyPoints();

          private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
          {
              //Use default property values
              //myPoints.Add(new MyPoints.DrawingPoint(e.Location));



              MyPoints.DrawingPoint newPoint = new MyPoints.DrawingPoint();
              newPoint.Dot = new Rectangle(e.Location, new Size(4, 4));
              newPoint.DrawingPen = new Pen(Color.Red, 2);
              myPoints.DrawingPoints.Add(newPoint);
              ((Control)sender).Invalidate();
          }

          private void pictureBox1_Paint(object sender, PaintEventArgs e)
          {
              foreach (MyPoints.DrawingPoint mypoint in myPoints.DrawingPoints)
              {
                  e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                  e.Graphics.DrawEllipse(mypoint.DrawingPen, mypoint.Dot);
              }
          }

          internal class MyPoints : IDisposable
          {
              bool IsDisposed = false;
              public MyPoints() => DrawingPoints = new List<DrawingPoint>();


              public List<DrawingPoint> DrawingPoints { get; set; }
              public void Add(DrawingPoint NewPoint)
              {
                  if (NewPoint.Dot.Size.Width > 1 && NewPoint.Dot.Size.Height > 1)
                      DrawingPoints.Add(NewPoint);
              }



              public void Clear()
              {
                  this.Dispose();
                  this.DrawingPoints.Clear();
                  this.DrawingPoints = new List<DrawingPoint>();
              }



              public void Remove(Point point)
              {
                  Remove(this.DrawingPoints.Select((p, i) => { if (p.Dot.Contains(point)) return i; return -1; }).First());
              }



              public void Remove(int Index)
              {
                  if (Index > -1)
                  {
                      this.DrawingPoints[Index].Delete();
                      this.DrawingPoints.RemoveAt(Index);
                  }
              }



              public void Dispose()
              {
                  Dispose(true);
                  GC.SuppressFinalize(this);
              }



              protected void Dispose(bool IsSafeDisposing)
              {
                  if (IsSafeDisposing && (!this.IsDisposed) && (this.DrawingPoints.Count > 0))
                  {
                      foreach (DrawingPoint dp in this.DrawingPoints)
                          if (dp != null) dp.Delete();
                  }
              }



              public class DrawingPoint
              {
                  Pen m_Pen = null;
                  Rectangle m_Dot = Rectangle.Empty;



                  public DrawingPoint() : this(Point.Empty) { }
                  public DrawingPoint(Point newPoint)
                  {
                      this.m_Pen = new Pen(Color.Red, 1);
                      this.m_Dot = new Rectangle(newPoint, new Size(2, 2));
                  }



                  public Pen DrawingPen { get => this.m_Pen; set => this.m_Pen = value; }
                  public Rectangle Dot { get => this.m_Dot; set => this.m_Dot = value; }



                  public void Delete()
                  {
                      if (this.m_Pen != null) this.m_Pen.Dispose();
                  }




              }
          }
        */
    }

}
