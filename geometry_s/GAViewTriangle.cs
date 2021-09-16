using System;
using System.Collections.Generic;

namespace geometry_s
{
    /// <summary>
    /// triangle
    /// </summary>
    public class GAViewTriangle
    {
        /// <summary>
        /// Центр
        /// </summary>
        //public GAViewPoint center_point { get; set; }

        ///// <summary>
        ///// inside diametr
        ///// </summary>
        //public double r { get; }

        ///// <summary>
        ///// outside diametr
        ///// </summary>
        //public double R { get; }

        ///// <summary>
        ///// outside diametr
        ///// </summary>
        //public double D { get; }

        ///// <summary>
        ///// inside diametr
        ///// </summary>
        //public double d { get; }

        /// <summary>
        /// tilt angle - space orientation
        /// угол наклона
        /// </summary>
        //public double angle1 { get; }

        /// <summary>
        /// tilt angle - space orientation
        /// угол наклона
        /// </summary>
        //public double angle2 { get; }

        /// <summary>
        /// tilt angle - space orientation
        /// угол наклона
        /// </summary>
        //public double angle3 { get; }

        /// <summary>
        /// угол между линиями AB AC (проверить!!!)
        /// </summary>
        //public double angleA { get; }

        /// <summary>
        /// угол между линиями BA BC (проверить!!!)
        /// </summary>
        //public double angleB { get; }

        /// <summary>
        /// угол между линиями CA CB (проверить!!!)
        /// </summary>
        //public double angleC { get; }

        /// <summary>
        /// Плоскость в которой лежит треугольник
        /// </summary>
        //public GAPlane plane { get; }

        /// <summary>
        /// Точки треугольника
        /// </summary>
        public List<GAViewPoint> points { get; }

        /// <summary>
        /// Линии треугольника
        /// </summary>
        public List<GAViewLine> lines { get; set; }

        /// <summary>
        /// Perimetr
        /// </summary>
        public double P { get; }

        /// <summary>
        /// Area (площадь по формуле Герона)
        /// </summary>
        public double Area { get; }

        /// <summary>
        /// Центр треугольника - среднее по точкам
        /// </summary>
        /// <param name="A_">Point</param>
        /// <param name="B_">Point</param>
        /// <param name="C_">Point</param>
        /// <param name="angle_1">DEG, XY rotation</param>
        /// <param name="angle_2">DEG, XZ rotation</param>
        /// <param name="angle_3">DEG, YZ rotation</param>
        /// 
        public GAViewTriangle(GAViewPoint A_, GAViewPoint B_, GAViewPoint C_)
        {
            //center_point = new GAViewPoint((A_.X + B_.X + C_.X) / 3, (A_.Y + B_.Y + C_.Y) / 3, (A_.Z + B_.Z + C_.Z) / 3);

            //angle1 = angle_1;
            //angle2 = angle_2;
            //angle3 = angle_3;

            lines = new List<GAViewLine>();
            lines.Add(new GAViewLine(A_, B_));
            lines.Add(new GAViewLine(B_, C_));
            lines.Add(new GAViewLine(C_, A_));

            //angleA = get_angle(lines[0], new GALine(A_, C_));
            //angleB = get_angle(new GALine(B_, A_), lines[1]);
            //angleC = get_angle(lines[2], new GALine(C_, B_));

            P = lines[0].length + lines[1].length + lines[2].length;
            Area = Math.Pow(((P / 2) * ((P / 2) - lines[0].length) * ((P / 2) - lines[1].length) * ((P / 2) - lines[2].length)), 0.5);
            if (double.IsNaN(Area)) { Area = 0; }

            points = new List<GAViewPoint>();
            points.Add(A_);
            points.Add(B_);
            points.Add(C_);

            //plane = new GAPlane(A_, B_, C_);

            //transform(angle_1, angle_2, angle_3);
        }

    }
}
