using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

            
            if(!EndDrawingQuestion())
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
    }
}
