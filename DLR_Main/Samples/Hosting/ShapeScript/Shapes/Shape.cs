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
using System.Drawing;
using ShapeScript;

namespace Shapes {
    public abstract class Shape {
        public abstract void Draw(Graphics g, Pen pen);
        public abstract void Move(int x, int y);

        public virtual void Draw() {
            Graphics g1 = Form1.Canvas.CreateGraphics();
            Pen pen = new Pen(Color.YellowGreen, 2);
            Draw(g1, pen);
        }
    }
}
