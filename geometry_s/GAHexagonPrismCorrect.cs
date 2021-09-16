using System;
using System.Collections.Generic;

namespace geometry_s
{
    public class GAHexagonPrismCorrect
    {
        /// <summary>
        /// 
        /// </summary>
        public GAPoint center_point { get; set; }

        /// <summary>
        /// Height
        /// </summary>
        public double H { get; set; }

        /// <summary>
        /// inside diametr
        /// </summary>
        public double r { get; }

        /// <summary>
        /// outside diametr
        /// </summary>
        public double R { get; }

        /// <summary>
        /// outside diametr
        /// </summary>
        public double D { get; }

        /// <summary>
        /// inside diametr
        /// </summary>
        public double d { get; }

        /// <summary>
        /// tilt angle - space orientation
        /// угол наклона
        /// </summary>
        public double angle1 { get; }

        /// <summary>
        /// tilt angle - space orientation
        /// угол наклона
        /// </summary>
        public double angle2 { get; }

        /// <summary>
        /// tilt angle - space orientation
        /// угол наклона
        /// </summary>
        public double angle3 { get; }

        public List<GAPoint> points { get; set; }
        public List<GALine> lines { get; set; }
        public List<GALine> lines_axes { get; set; }

        /// <summary>
        /// Perimetr
        /// </summary>
        double Perimetr { get; }

        /// <summary>
        /// Area bottom or top hexagon surface
        /// </summary>
        double Area_hex { get; }

        /// <summary>
        /// Area rectangular surface
        /// </summary>
        double Area_side { get; }

        /// <summary>
        /// full are
        /// </summary>
        double Area_full { get; }

        double volume { get; set; }

        /// <summary>
        /// Центр, диаметр, внутр/внеш, угол, угол, угол
        /// </summary>
        /// <param name="center_point_">Center point</param>
        /// <param name="diametr_">diametr</param>
        /// <param name="diametr_inside">true - diametr is inside; false - diametr is outside</param>
        /// <param name="angle_1">DEG, XY rotation</param>
        /// <param name="angle_2">DEG, XZ rotation</param>
        /// <param name="angle_3">DEG, YZ rotation</param>
        public GAHexagonPrismCorrect(GAPoint center_point_, double diametr_, bool diametr_inside, double height, double angle_1 = 0, double angle_2 = 0, double angle_3 = 0)
        {
            center_point = center_point_;
            angle1 = angle_1;
            angle2 = angle_2;
            angle3 = angle_3;
            H = height;

            if (diametr_inside)
            {
                d = diametr_;
                r = diametr_ / 2;
                R = r * 2 / Math.Pow(3, 0.5);
                D = R * 2;
            }
            else
            {
                D = diametr_;
                R = diametr_ / 2;
                r = R * (Math.Pow(3, 0.5) / 2);
                d = r * 2;
            }

            Perimetr = (6 * R) * 2 + H * 6;
            Area_hex = r * Perimetr / 2;
            Area_side = R * H;
            Area_full = Area_hex * 2 + Area_side * 6;
            volume = Area_hex * H;

            points = new List<GAPoint>();
            points.Add(new GAPoint(center_point.X - R, center_point.Y, center_point.Z, (int)GAGeometry.word.основа, 0));
            points.Add(new GAPoint(center_point.X - R / 2, center_point.Y + r, center_point.Z, (int)GAGeometry.word.основа, 1));
            points.Add(new GAPoint(center_point.X + R / 2, center_point.Y + r, center_point.Z, (int)GAGeometry.word.основа, 2));
            points.Add(new GAPoint(center_point.X + R, center_point.Y, center_point.Z, (int)GAGeometry.word.основа, 3));
            points.Add(new GAPoint(center_point.X + R / 2, center_point.Y - r, center_point.Z, (int)GAGeometry.word.основа, 4));
            points.Add(new GAPoint(center_point.X - R / 2, center_point.Y - r, center_point.Z, (int)GAGeometry.word.основа, 5));

            points.Add(new GAPoint(center_point.X - R, center_point.Y, center_point.Z + H, (int)GAGeometry.word.верх, 0));
            points.Add(new GAPoint(center_point.X - R / 2, center_point.Y + r, center_point.Z + H, (int)GAGeometry.word.верх, 1));
            points.Add(new GAPoint(center_point.X + R / 2, center_point.Y + r, center_point.Z + H, (int)GAGeometry.word.верх, 2));
            points.Add(new GAPoint(center_point.X + R, center_point.Y, center_point.Z + H, (int)GAGeometry.word.верх, 3));
            points.Add(new GAPoint(center_point.X + R / 2, center_point.Y - r, center_point.Z + H, (int)GAGeometry.word.верх, 4));
            points.Add(new GAPoint(center_point.X - R / 2, center_point.Y - r, center_point.Z + H, (int)GAGeometry.word.верх, 5));

            lines_axes = new List<GALine>();
            lines_axes.Add(new GALine(new GAPoint(center_point.X - R - 5, center_point.Y, center_point.Z), new GAPoint(center_point.X + R + 5, center_point.Y, center_point.Z)));
            lines_axes.Add(new GALine(new GAPoint(center_point.X, center_point.Y - R - 5, center_point.Z), new GAPoint(center_point.X, center_point.Y + R + 5, center_point.Z)));

            lines_axes.Add(new GALine(new GAPoint(center_point.X - R - 5, center_point.Y, center_point.Z + H), new GAPoint(center_point.X + R + 5, center_point.Y, center_point.Z + H)));
            lines_axes.Add(new GALine(new GAPoint(center_point.X, center_point.Y - R - 5, center_point.Z + H), new GAPoint(center_point.X, center_point.Y + R + 5, center_point.Z + H)));

            lines_axes.Add(new GALine(new GAPoint(center_point.X, center_point.Y, center_point.Z - 5), new GAPoint(center_point.X, center_point.Y, center_point.Z + H + 5)));

            transform(angle_1, angle_2, angle_3);
        }

        GAPoint transform_a1(GAPoint gp)
        {
            //GAPoint gp = points[i];
            gp.X -= center_point.X;       // преобразование координат в систему координат с началом в базовой точке 
            gp.Y -= center_point.Y;
            gp.Z -= center_point.Z;

            GAPoint temp = new GAPoint(
                (double)(gp.X * GAGeometry.Cos(angle1) + gp.Y * GAGeometry.Sin(angle1)),
                (double)(gp.Y * GAGeometry.Cos(angle1) - gp.X * GAGeometry.Sin(angle1)),
                gp.Z
                , gp.description, gp.index
                );      // применяем матрицу поворота

            temp.X += center_point.X;     // обратное преобразование координат
            temp.Y += center_point.Y;
            temp.Z += center_point.Z;

            return temp;
        }
        GAPoint transform_a2(GAPoint gp)
        {
            //GAPoint gp = points[i];
            gp.X -= center_point.X;       // преобразование координат в систему координат с началом в базовой точке 
            gp.Y -= center_point.Y;
            gp.Z -= center_point.Z;

            GAPoint temp = new GAPoint(
                (double)(gp.X * GAGeometry.Cos(angle2) + gp.Z * GAGeometry.Sin(angle2)),
                gp.Y,
                (double)(gp.Z * GAGeometry.Cos(angle2) - gp.X * GAGeometry.Sin(angle2))
                , gp.description, gp.index
                );      // применяем матрицу поворота

            temp.X += center_point.X;     // обратное преобразование координат
            temp.Y += center_point.Y;
            temp.Z += center_point.Z;

            return temp;
        }
        GAPoint transform_a3(GAPoint gp)
        {
            gp.X -= center_point.X;       // преобразование координат в систему координат с началом в базовой точке 
            gp.Y -= center_point.Y;
            gp.Z -= center_point.Z;

            GAPoint temp = new GAPoint(
                gp.X,
                (double)(gp.Y * GAGeometry.Cos(angle3) + gp.Z * GAGeometry.Sin(angle3)),
                (double)(gp.Z * GAGeometry.Cos(angle3) - gp.Y * GAGeometry.Sin(angle3))
                , gp.description, gp.index
                );      // применяем матрицу поворота

            temp.X += center_point.X;     // обратное преобразование координат
            temp.Y += center_point.Y;
            temp.Z += center_point.Z;
            return temp;
        }

        /// <summary>
        ///  трансформация поворота
        /// </summary>
        /// <param name="angle_1">DEG, XY rotation</param>
        /// <param name="angle_2">DEG, XZ rotation</param>
        /// <param name="angle_3">DEG, YZ rotation</param>
        void transform(double angle1, double angle2, double angle3)
        {
            if (angle1 > 0)
            {


                for (int i = 0; i < lines_axes.Count; i++)
                {
                    lines_axes[i] = (new GALine(transform_a1(lines_axes[i].A), transform_a1(lines_axes[i].B)));
                }

                for (int i = 0; i < points.Count; i++)
                {
                    points[i] = transform_a1(points[i]);
                }

            }

            if (angle2 > 0)
            {

                for (int i = 0; i < lines_axes.Count; i++)
                {
                    lines_axes[i] = (new GALine(transform_a2(lines_axes[i].A), transform_a2(lines_axes[i].B)));
                }

                for (int i = 0; i < points.Count; i++)
                {
                    points[i] = transform_a2(points[i]);
                }
            }

            if (angle3 > 0)
            {

                for (int i = 0; i < lines_axes.Count; i++)
                {
                    lines_axes[i] = (new GALine(transform_a3(lines_axes[i].A), transform_a3(lines_axes[i].B)));
                }

                for (int i = 0; i < points.Count; i++)
                {
                    points[i] = transform_a3(points[i]);
                }
            }

            lines = new List<GALine>();
            lines.Add(new GALine(points[0], points[1], 0, 0, (int)GAGeometry.word.основа));
            lines.Add(new GALine(points[1], points[2], 1, 0, (int)GAGeometry.word.основа));
            lines.Add(new GALine(points[2], points[3], 2, 0, (int)GAGeometry.word.основа));
            lines.Add(new GALine(points[3], points[4], 3, 0, (int)GAGeometry.word.основа));
            lines.Add(new GALine(points[4], points[5], 4, 0, (int)GAGeometry.word.основа));
            lines.Add(new GALine(points[5], points[0], 5, 0, (int)GAGeometry.word.основа));

            lines.Add(new GALine(points[6], points[7], 0, 0, (int)GAGeometry.word.верх));
            lines.Add(new GALine(points[7], points[8], 1, 0, (int)GAGeometry.word.верх));
            lines.Add(new GALine(points[8], points[9], 2, 0, (int)GAGeometry.word.верх));
            lines.Add(new GALine(points[9], points[10], 3, 0, (int)GAGeometry.word.верх));
            lines.Add(new GALine(points[10], points[11], 4, 0, (int)GAGeometry.word.верх));
            lines.Add(new GALine(points[11], points[6], 5, 0, (int)GAGeometry.word.верх));

            lines.Add(new GALine(points[0], points[6], 0, 0, (int)GAGeometry.word.ребро));
            lines.Add(new GALine(points[1], points[7], 1, 0, (int)GAGeometry.word.ребро));
            lines.Add(new GALine(points[2], points[8], 2, 0, (int)GAGeometry.word.ребро));
            lines.Add(new GALine(points[3], points[9], 3, 0, (int)GAGeometry.word.ребро));
            lines.Add(new GALine(points[4], points[10], 4, 0, (int)GAGeometry.word.ребро));
            lines.Add(new GALine(points[5], points[11], 5, 0, (int)GAGeometry.word.ребро));
        }

    }
}
