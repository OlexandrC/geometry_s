using System.Collections.Generic;

namespace geometry_s
{
    /// <summary>
    /// Правильная пятиугольная пирамида
    /// </summary>
    public class GAPentagonPiramCorrect
    {
        /// <summary>
        /// высота
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// диаметр описанной окружности
        /// </summary>
        public double Diametr_out { get; set; }

        /// <summary>
        /// диаметр вписанной окружности
        /// </summary>
        public double diametr_ins { get; set; }

        /// <summary>
        /// радиус описанной окружности
        /// </summary>
        public double R { get; set; }

        /// <summary>
        /// радиус вписанной окружности
        /// </summary>
        public double r { get; set; }

        public List<GALine> lines { get; set; }
        public List<GAPoint> points { get; set; }
        public List<GALine> axes { get; set; }

        /// <summary>
        /// Правильная пятиугольная пирамида
        /// </summary>
        /// <param name="height_">высота</param>
        /// <param name="diametr_">диаметр</param>
        /// <param name="inside_diam">true - внутренний диаметр (вписанный), false - наружный диаметр (описанный)</param>
        /// <param name="angle1">угол поворота</param>
        /// <param name="angle2">угол поворота</param>
        /// <param name="angle3">угол поворота</param>
        public GAPentagonPiramCorrect(GAPoint center_point, double height_, double diametr_, bool inside_diam, double angle1, double angle2, double angle3)
        {
            Height = height_;
            if (inside_diam)
            {
                diametr_ins = diametr_;
                r = diametr_ins / 2.0;
                R = r / (GAGeometry.Sin(54));
                Diametr_out = R * 2;
            }
            else
            {
                Diametr_out = diametr_;
                R = Diametr_out / 2;
                r = R * GAGeometry.Sin(54);
                diametr_ins = r * 2;
            }

            points = new List<GAPoint>();
            points.Add(new GAPoint(center_point.X, center_point.Y, center_point.Z, (int)GAGeometry.word.основа, 0));
            points.Add(new GAPoint(center_point.X - R, center_point.Y, center_point.Z, (int)GAGeometry.word.основа, 1));
            points.Add(new GAPoint(center_point.X - R * GAGeometry.Cos(72), center_point.Y + R * GAGeometry.Sin(72), center_point.Z, (int)GAGeometry.word.основа, 2));
            points.Add(new GAPoint(center_point.X + R * GAGeometry.Cos(36), center_point.Y + R * GAGeometry.Sin(36), center_point.Z, (int)GAGeometry.word.основа, 3));
            points.Add(new GAPoint(center_point.X + R * GAGeometry.Cos(36), center_point.Y - R * GAGeometry.Sin(36), center_point.Z, (int)GAGeometry.word.основа, 4));
            points.Add(new GAPoint(center_point.X - R * GAGeometry.Cos(72), center_point.Y - R * GAGeometry.Sin(72), center_point.Z, (int)GAGeometry.word.основа, 5));
            points.Add(new GAPoint(center_point.X, center_point.Y, center_point.Z + height_, (int)GAGeometry.word.верх, 6));

            axes = new List<GALine>();
            axes.Add(new GALine(
                new GAPoint(GAGeometry.transform_rotate(new GAPoint(center_point.X - R - 5, center_point.Y, center_point.Z), points[0], angle1, angle2, angle3)),
                new GAPoint(GAGeometry.transform_rotate(new GAPoint(center_point.X + R + 5, center_point.Y, center_point.Z), points[0], angle1, angle2, angle3)),
                0, 0, (int)GAGeometry.word.ось));

            axes.Add(new GALine(
                new GAPoint(GAGeometry.transform_rotate(new GAPoint(center_point.X, center_point.Y + R + 5, center_point.Z), points[0], angle1, angle2, angle3)),
                new GAPoint(GAGeometry.transform_rotate(new GAPoint(center_point.X, center_point.Y - R - 5, center_point.Z), points[0], angle1, angle2, angle3)),
                0, 0, (int)GAGeometry.word.ось));

            axes.Add(new GALine(
                new GAPoint(GAGeometry.transform_rotate(new GAPoint(center_point.X, center_point.Y, center_point.Z - 5), points[0], angle1, angle2, angle3)),
                new GAPoint(GAGeometry.transform_rotate(new GAPoint(center_point.X, center_point.Y, center_point.Z + Height + 5), points[0], angle1, angle2, angle3)),
                0, 0, (int)GAGeometry.word.ось));

            for (int i = 0; i < points.Count; i++)
            {
                points[i] = GAGeometry.transform_rotate(points[i], center_point, angle1, angle2, angle3);
            }

            lines = new List<GALine>();
            lines.Add(new GALine(points[1], points[2], 1, 0, (int)GAGeometry.word.основа));
            lines.Add(new GALine(points[2], points[3], 2, 0, (int)GAGeometry.word.основа));
            lines.Add(new GALine(points[3], points[4], 3, 0, (int)GAGeometry.word.основа));
            lines.Add(new GALine(points[4], points[5], 4, 0, (int)GAGeometry.word.основа));
            lines.Add(new GALine(points[5], points[1], 5, 0, (int)GAGeometry.word.основа));

            lines.Add(new GALine(points[1], points[6], 16, 0, (int)GAGeometry.word.ребро));
            lines.Add(new GALine(points[2], points[6], 26, 0, (int)GAGeometry.word.ребро));
            lines.Add(new GALine(points[3], points[6], 36, 0, (int)GAGeometry.word.ребро));
            lines.Add(new GALine(points[4], points[6], 46, 0, (int)GAGeometry.word.ребро));
            lines.Add(new GALine(points[5], points[6], 56, 0, (int)GAGeometry.word.ребро));

        }
    }
}
