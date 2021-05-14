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

namespace PolygonDetect
{
    public partial class Form1 : Form
    {
        String selectorDo = "0";
        List<Size> arrayPF = new List<Size>();
        Graphics g;

        public Form1()
        {
            InitializeComponent();
            g = Graphics.FromHwnd(pictureBox1.Handle);
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            //Use default property values
            //myPoints.Add(new MyPoints.DrawingPoint(e.Location));




        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
         
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            switch (selectorDo)
            {
                case ("Draw"):

                    Size a = new Size(0, 0);
                    if (arrayPF.Count != 0)
                        a = arrayPF.Last();

                    arrayPF.Add(new Size(e.X, e.Y));

                    

                    if (arrayPF.Count != 1)
                        g.DrawLine(new Pen(Brushes.Black, 2), new Point(a), new Point(arrayPF.Last()));
                    g.FillRectangle(Brushes.Red, arrayPF.Last().Width - 4, arrayPF.Last().Height - 4, 7, 7);
                    g.FillRectangle(Brushes.Red, a.Width - 4, a.Height - 4, 7, 7);
                    break;
                case ("Test"):
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
                selectorDo = "0";
            else
                selectorDo = drawClick;
        }



        private void buttonTest_Click(object sender, EventArgs e)
        {
            string testClick = "Test";
            if (selectorDo == testClick)
                selectorDo = "0";
            else
                selectorDo = testClick;
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
