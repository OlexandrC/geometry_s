using System;
using System.Collections.Generic;

namespace geometry_s
{
    public class GAHexagonCorrect
    {
        /// <summary>
        /// 
        /// </summary>
        public GAPoint center_point { get; set; }

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

        public List<GAPoint> points { get; }
        public List<GALine> lines { get; set; }
        public List<GALine> lines_axes { get; }

        /// <summary>
        /// Perimetr
        /// </summary>
        double P { get; }

        /// <summary>
        /// Area
        /// </summary>
        double A { get; }


        /// <summary>
        /// Центр, диаметр, внутр/внеш, угол, угол, угол
        /// </summary>
        /// <param name="center_point_">Center point</param>
        /// <param name="diametr_">diametr</param>
        /// <param name="diametr_inside">true - diametr is inside; false - diametr is outside</param>
        /// <param name="angle_1">DEG, XY rotation</param>
        /// <param name="angle_2">DEG, XZ rotation</param>
        /// <param name="angle_3">DEG, YZ rotation</param>
        public GAHexagonCorrect(GAPoint center_point_, double diametr_, bool diametr_inside, double angle_1 = 0, double angle_2 = 0, double angle_3 = 0)
        {
            center_point = center_point_;
            angle1 = angle_1;
            angle2 = angle_2;
            angle3 = angle_3;

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

            P = 6 * R;
            A = r * P / 2;

            points = new List<GAPoint>();
            points.Add(new GAPoint(center_point.X - R, center_point.Y, center_point.Z, (int)GAGeometry.word.основа, 0));
            points.Add(new GAPoint(center_point.X - R / 2, center_point.Y + r, center_point.Z, (int)GAGeometry.word.основа, 1));
            points.Add(new GAPoint(center_point.X + R / 2, center_point.Y + r, center_point.Z, (int)GAGeometry.word.основа, 2));
            points.Add(new GAPoint(center_point.X + R, center_point.Y, center_point.Z, (int)GAGeometry.word.основа, 3));
            points.Add(new GAPoint(center_point.X + R / 2, center_point.Y - r, center_point.Z, (int)GAGeometry.word.основа, 4));
            points.Add(new GAPoint(center_point.X - R / 2, center_point.Y - r, center_point.Z, (int)GAGeometry.word.основа, 5));

            lines = new List<GALine>();
            lines.Add(new GALine(points[0], points[1], 1, 0, (int)GAGeometry.word.основа));
            lines.Add(new GALine(points[1], points[2], 12, 0, (int)GAGeometry.word.основа));
            lines.Add(new GALine(points[2], points[3], 23, 0, (int)GAGeometry.word.основа));
            lines.Add(new GALine(points[3], points[4], 34, 0, (int)GAGeometry.word.основа));
            lines.Add(new GALine(points[4], points[5], 45, 0, (int)GAGeometry.word.основа));
            lines.Add(new GALine(points[5], points[0], 50, 0, (int)GAGeometry.word.основа));

            lines_axes = new List<GALine>();
            lines_axes.Add(new GALine(new GAPoint(center_point.X - R - 5, center_point.Y, center_point.Z), new GAPoint(center_point.X + R + 5, center_point.Y, center_point.Z)));
            lines_axes.Add(new GALine(new GAPoint(center_point.X, center_point.Y - R - 5, center_point.Z), new GAPoint(center_point.X, center_point.Y + R + 5, center_point.Z)));

            transform(angle_1, angle_2, angle_3);
        }

        GAPoint transform_a1(GAPoint gp)
        {
            //GAPoint gp = points[i];
            gp.X -= center_point.X;       // преобразование координат в систему координат с началом в базовой точке 
            gp.Y -= center_point.Y;
            gp.Z -= center_point.Z;

            GAPoint temp = new GAPoint(
                (double)(gp.X * Math.Cos(angle1 / 180 * Math.PI) + gp.Y * Math.Sin(angle1 / 180 * Math.PI)),
                (double)(gp.Y * Math.Cos(angle1 / 180 * Math.PI) - gp.X * Math.Sin(angle1 / 180 * Math.PI)),
                gp.Z
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
                (double)(gp.X * Math.Cos(angle2 / 180 * Math.PI) + gp.Z * Math.Sin(angle2 / 180 * Math.PI)),
                gp.Y,
                (double)(gp.Z * Math.Cos(angle2 / 180 * Math.PI) - gp.X * Math.Sin(angle2 / 180 * Math.PI))
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
                (double)(gp.Y * Math.Cos(angle3 / 180 * Math.PI) + gp.Z * Math.Sin(angle3 / 180 * Math.PI)),
                (double)(gp.Z * Math.Cos(angle3 / 180 * Math.PI) - gp.Y * Math.Sin(angle3 / 180 * Math.PI))
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
        public void transform(double angle1, double angle2, double angle3)
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


            this.lines = new List<GALine>();
            lines.Add(new GALine(points[0], points[1]));
            lines.Add(new GALine(points[1], points[2]));
            lines.Add(new GALine(points[2], points[3]));
            lines.Add(new GALine(points[3], points[4]));
            lines.Add(new GALine(points[4], points[5]));
            lines.Add(new GALine(points[5], points[0]));
        }
    }
}
