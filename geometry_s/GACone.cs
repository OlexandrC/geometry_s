using System;
using System.Collections.Generic;

namespace geometry_s
{
    /// <summary>
    /// правильный конус
    /// </summary>
    public class GACone : ICloneable
    {
        public GAPoint centr { get; set; }
        public double Diametr { get; set; }
        public double Radius { get; set; }
        public double Height { get; set; }
        public List<GALine> ribs { get; set; }
        public List<GALine> ribs_help { get; set; }
        public List<GALine> base_lines_help { get; set; }
        public List<GAPoint> base_points_help { get; set; }
        public double circle_length { get; set; }

        public List<GAPoint> section_points { get; set; }
        public List<GAPoint> section_points_help { get; set; }
        public List<GALine> section_lines { get; set; }
        public List<GALine> section_lines_help { get; set; }


        public GACone(GAPoint center_point, double Radius_, double Height_, double angle1 = 0, double angle2 = 0, double angle3 = 0, int ribs_count = 12, int help_ribs = 100)
        {
            centr = center_point;
            Radius = Radius_;
            Diametr = Radius * 2;
            Height = Height_;
            circle_length = Math.PI * Diametr;

            ribs = new List<GALine>();
            ribs_help = new List<GALine>();
            base_lines_help = new List<GALine>();
            base_points_help = new List<GAPoint>();


            GAPoint p2 = new GAPoint(center_point.X, center_point.Y, center_point.Z + Height, (int)GAGeometry.word.верх, 0);
            p2 = GAGeometry.transform_rotate(p2, center_point, angle1, angle2, angle3);

            double angle_rotate_1 = 360 / ribs_count;
            for (int i = 0; i < ribs_count; i++)
            {
                GAPoint p1 = new GAPoint(center_point.X + Radius * GAGeometry.Cos(angle_rotate_1 * i), center_point.Y + Radius * GAGeometry.Sin(angle_rotate_1 * i), center_point.Z, (int)GAGeometry.word.ребро, i);
                p1 = GAGeometry.transform_rotate(p1, center_point, angle1, angle2, angle3);

                ribs.Add(new GALine(p1, p2, i, 0, (int)GAGeometry.word.ребро));
            }

            double angle_rotate_2 = 360.0 / help_ribs;
            for (int j = 0; j < help_ribs; j++)
            {
                GAPoint p1 = new GAPoint(center_point.X + Radius * GAGeometry.Cos(angle_rotate_2 * j), center_point.Y + Radius * GAGeometry.Sin(angle_rotate_2 * j), center_point.Z, (int)GAGeometry.word.основа, j);
                p1 = GAGeometry.transform_rotate(p1, center_point, angle1, angle2, angle3);

                base_points_help.Add(p1);
                if (base_points_help.Count > 1)
                {
                    base_lines_help.Add(new GALine(base_points_help[j - 1], base_points_help[j]));
                }

                ribs_help.Add(new GALine(p1, p2, j, 0, -1));
            }
            base_lines_help.Add(new GALine(base_points_help[base_points_help.Count - 1], base_points_help[0]));
        }

        public object Clone()
        {
            return (GACone)this.MemberwiseClone();
        }

    }
}
