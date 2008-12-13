using System; using Microsoft;
using System.Collections.Generic;
using Microsoft.Linq;
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
