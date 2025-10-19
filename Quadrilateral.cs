using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_Figures
{
    public class Quadrilateral
    {
        private Point2D p1, p2, p3, p4;

        public Quadrilateral(Point2D start, int width, int height)
        {
            p1 = start;
            p2 = new Point2D(start.getX() + width, start.getY());
            p3 = new Point2D(start.getX() + width, start.getY() + height);
            p4 = new Point2D(start.getX(), start.getY() + height);
        }

        public Point2D getP1() => p1;
        public Point2D getP2() => p2;
        public Point2D getP3() => p3;
        public Point2D getP4() => p4;

        public void addX(int x) { p1.addX(x); p2.addX(x); p3.addX(x); p4.addX(x); }
        public void addY(int y) { p1.addY(y); p2.addY(y); p3.addY(y); p4.addY(y); }
    }
}
