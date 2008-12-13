using System; using Microsoft;
using System.Collections.Generic;
using Microsoft.Linq;
using System.Text;
using Shapes;

namespace Shapes{
    public class Circle : Ellipse {
        public Circle(int x, int y, int radius) : base(x,y,radius,radius) {

        }
    }
}
