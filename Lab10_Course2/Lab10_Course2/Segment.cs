using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;

namespace Lab10_Course2
{
    public class Segment
    {
        private PointF start, end;
        public PointF Start
        {
            set
            {
                start = value;
            }
            get
            {
                return start;
            }
        }
        public PointF End
        {
            set
            {
                end = value;
            }
            get
            {
                return end;
            }
        }
        public Segment(PointF a, PointF b)
        {
            start = a;
            end = b;
        }

        public bool OnLeft(PointF p)
        {
            var ab = new PointF(end.X - start.X, end.Y - start.Y);
            var ap = new PointF(p.X - start.X, p.Y - start.Y);
            return ab.Cross(ap) >= 0;
        }

        public PointF Normal
        {
            get
            {
                return new PointF(end.Y - start.Y, start.X - end.X);
            }
        }

        public PointF Direction
        {
            get
            {
                return new PointF(end.X - start.X, end.Y - start.Y);
            }
        }

        public float IntersectionParameter(Segment that)
        {
            var segment = this;
            var edge = that;

            var segmentToEdge = edge.start.Sub(segment.start);
            var segmentDir = segment.Direction;
            var edgeDir = edge.Direction;

            var t = edgeDir.Cross(segmentToEdge) / edgeDir.Cross(segmentDir);

            if (float.IsNaN(t))
            {
                t = 0;
            }

            return t;
        }

        public Segment Morph(float tA, float tB)
        {
            var d = Direction;
            return new Segment(start.Add(d.Mul(tA)), start.Add(d.Mul(tB)));
        }
    }
}
