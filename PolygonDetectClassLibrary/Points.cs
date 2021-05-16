using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolygonDetectClassLibrary
{
    class Points : List <Point>
    {
        public void AddC(int X, int Y)
        {
            this.Add(new Point(X, Y));
        }
    }
}
