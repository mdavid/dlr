#if CODEPLEX_40
using System;
#else
using System; using Microsoft;
#endif
using System.Collections.Generic;
#if CODEPLEX_40
using System.Linq;
#else
using Microsoft.Linq;
#endif
using System.Text;
using Shapes;

namespace Shapes{
    public class Circle : Ellipse {
        public Circle(int x, int y, int radius) : base(x,y,radius,radius) {

        }
    }
}
