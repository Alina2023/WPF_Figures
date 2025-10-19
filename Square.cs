using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_Figures
{
    public class Square : Quadrilateral
    {
        public Square(Point2D start, int side) : base(start, side, side) { }
    }
}
