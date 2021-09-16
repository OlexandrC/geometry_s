using System;
using System.Collections.Generic;

namespace geometry_s
{
    /// <summary>
    /// Цилиндр
    /// </summary>
    public class GACilinder : ICloneable
    {
        /// <summary>
        /// центр цилинда, базовая точка
        /// </summary>
        public GAPoint center { get; set; }

        /// <summary>
        /// радиус
        /// </summary>
        public double R { get; set; }

        /// <summary>
        /// диаметр
        /// </summary>
        public double D { get; set; }

        /// <summary>
        /// высота
        /// </summary>
        public double H { get; set; }

        public List<GALine> ribs { get; set; }
        public List<GALine> axes { get; set; }
        public List<GAPoint> section_points { get; set; }
        public List<GALine> section_lines { get; set; }
        public List<GAPoint> base_points { get; set; }
        public List<GAPoint> top_points { get; set; }
        public List<GALine> base_lines { get; set; }
        public List<GALine> top_lines { get; set; }
        public double circle_length { get; set; }

        /// <summary>
        /// цилиндр
        /// </summary>
        /// <param name="center_point">центр цилиндра</param>
        /// <param name="Rarius">радиус</param>
        /// <param name="Height">высота</param>
        /// <param name="angle1">угол трансформации 1</param>
        /// <param name="angle2">угол трансформации 2</param>
        /// <param name="angle3">угол трансформации 3</param>
        /// <param name="ribs_count">количество ребер</param>
        /// <param name="cilinder_tolerance">количество точек на кругах</param>
        public GACilinder(GAPoint center_point, double Radius, double Height, double angle1 = 0, double angle2 = 0, double angle3 = 0, int ribs_count = 12, int cilinder_tolerance = 60)
        {
            center = center_point;
            R = Radius;
            D = R * 2;
            H = Height;
            circle_length = Math.PI * D;

            ribs = new List<GALine>();
            base_points = new List<GAPoint>();
            top_points = new List<GAPoint>();
            base_lines = new List<GALine>();
            top_lines = new List<GALine>();

            double angle_rotate_1 = 360 / ribs_count;
            for (int i = 0; i < ribs_count; i++)
            {
                GAPoint p1 = new GAPoint(center_point.X + R * GAGeometry.Cos(angle_rotate_1 * i), center_point.Y + R * GAGeometry.Sin(angle_rotate_1 * i), center_point.Z, (int)GAGeometry.word.ребро, i);
                p1 = GAGeometry.transform_rotate(p1, center_point, angle1, angle2, angle3);

                GAPoint p2 = new GAPoint(center_point.X + R * GAGeometry.Cos(angle_rotate_1 * i), center_point.Y + R * GAGeometry.Sin(angle_rotate_1 * i), center_point.Z + H, (int)GAGeometry.word.ребро, i);
                p2 = GAGeometry.transform_rotate(p2, center_point, angle1, angle2, angle3);

                ribs.Add(new GALine(p1, p2, i, 0, (int)GAGeometry.word.ребро));
            }

            double angle_rotate_2 = 360.0 / cilinder_tolerance;
            for (int j = 0; j < cilinder_tolerance; j++)
            {
                GAPoint p1 = new GAPoint(center_point.X + R * GAGeometry.Cos(angle_rotate_2 * j), center_point.Y + R * GAGeometry.Sin(angle_rotate_2 * j), center_point.Z, (int)GAGeometry.word.основа, j);
                p1 = GAGeometry.transform_rotate(p1, center_point, angle1, angle2, angle3);

                base_points.Add(p1);
                if (base_points.Count > 1)
                {
                    base_lines.Add(new GALine(base_points[j - 1], base_points[j]));
                }

                GAPoint p2 = new GAPoint(center_point.X + R * GAGeometry.Cos(angle_rotate_2 * j), center_point.Y + R * GAGeometry.Sin(angle_rotate_2 * j), center_point.Z + H, (int)GAGeometry.word.верх, j);
                p2 = GAGeometry.transform_rotate(p2, center_point, angle1, angle2, angle3);

                top_points.Add(p2);
                if (top_points.Count > 1)
                {
                    top_lines.Add(new GALine(top_points[j - 1], top_points[j]));
                }
            }
            base_lines.Add(new GALine(base_points[base_points.Count - 1], base_points[0]));
            top_lines.Add(new GALine(top_points[top_points.Count - 1], top_points[0]));
        }

        public object Clone()
        {
            return (GACilinder)this.MemberwiseClone();
        }
    }
}
