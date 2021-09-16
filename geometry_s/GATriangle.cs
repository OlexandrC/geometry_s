using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace geometry_s
{

    /// <summary>
    /// 3d triangle - angles are not finished
    /// </summary>
    public class GATriangle
    {
        /// <summary>
        /// Центр
        /// </summary>
        public GAPoint center_point { get; set; }

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

        /// <summary>
        /// угол между линиями AB AC (проверить!!!)
        /// </summary>
        public double angleA { get; }

        /// <summary>
        /// угол между линиями BA BC (проверить!!!)
        /// </summary>
        public double angleB { get; }

        /// <summary>
        /// угол между линиями CA CB (проверить!!!)
        /// </summary>
        public double angleC { get; }

        /// <summary>
        /// Плоскость в которой лежит треугольник
        /// </summary>
        public GAPlane plane { get; }

        /// <summary>
        /// Точки треугольника
        /// </summary>
        public List<GAPoint> points { get; }

        /// <summary>
        /// Линии треугольника
        /// </summary>
        public List<GALine> lines { get; set; }

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
        public GATriangle(GAPoint A_, GAPoint B_, GAPoint C_, double angle_1 = 0, double angle_2 = 0, double angle_3 = 0)
        {
            center_point = new GAPoint((A_.X + B_.X + C_.X) / 3, (A_.Y + B_.Y + C_.Y) / 3, (A_.Z + B_.Z + C_.Z) / 3);

            angle1 = angle_1;
            angle2 = angle_2;
            angle3 = angle_3;

            lines = new List<GALine>();
            lines.Add(new GALine(A_, B_));
            lines.Add(new GALine(B_, C_));
            lines.Add(new GALine(C_, A_));

            angleA = GAGeometry.get_angle(lines[0], new GALine(A_, C_));
            angleB = GAGeometry.get_angle(new GALine(B_, A_), lines[1]);
            angleC = GAGeometry.get_angle(lines[2], new GALine(C_, B_));

            P = Math.Round(lines[0].length + lines[1].length + lines[2].length, 5);
            double P2 = P / 2.0;

            Area = Math.Pow(((P / 2) * (P2 - lines[0].length) * (P2 - lines[1].length) * (P2 - lines[2].length)), 0.5);
            if (double.IsNaN(Area)) { Area = 0; }

            points = new List<GAPoint>();
            points.Add(A_);
            points.Add(B_);
            points.Add(C_);

            plane = new GAPlane(A_, B_, C_);

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

                for (int i = 0; i < points.Count; i++)
                {// перебираем точки 
                    points[i] = transform_a1(points[i]);
                }
            }

            if (angle2 > 0)
            {
                for (int i = 0; i < points.Count; i++)
                {// перебираем точки 
                    points[i] = transform_a2(points[i]);
                }
            }

            if (angle3 > 0)
            {
                for (int i = 0; i < points.Count; i++)
                {// перебираем точки 
                    points[i] = transform_a3(points[i]);
                }
            }


            this.lines = new List<GALine>();
            lines.Add(new GALine(points[0], points[1]));
            lines.Add(new GALine(points[1], points[2]));
            lines.Add(new GALine(points[2], points[0]));

        }

        /// <summary>
        /// returns GAFaceFlatSimple from the triangle points
        /// </summary>
        /// <returns></returns>
        public GAFaceFlatSimple toFaceFlatSimple()
        {
            return new GAFaceFlatSimple(points);
        }

    }
}
