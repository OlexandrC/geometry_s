using System.Collections.Generic;

namespace geometry_s
{
    /// <summary>
    /// плоскость Ax + Bx + Cx + D = 0;
    /// </summary>
    public class GAPlane
    {
        public double A;
        public double B;
        public double C;
        public double D;

        /// <summary>
        /// плоскость по трем точкам
        /// </summary>
        /// <param name="A_"></param>
        /// <param name="B_"></param>
        /// <param name="C_"></param>
        public GAPlane(GAPoint A_, GAPoint B_, GAPoint C_)
        {
            double a21, a22, a23, a31, a32, a33;

            a21 = B_.X - A_.X;
            a22 = B_.Y - A_.Y;
            a23 = B_.Z - A_.Z;

            a31 = C_.X - A_.X;
            a32 = C_.Y - A_.Y;
            a33 = C_.Z - A_.Z;

            A = a22 * a33 - a32 * a23;
            B = -(a21 * a33 - a31 * a23);
            C = a21 * a32 - a22 * a31;
            D = -A * A_.X - B * A_.Y - C * A_.Z;
        }

        /// <summary>
        /// плоскость по набору точек, встроен алгоритм выборки точек, что бы точки не лежали на одной прямой
        /// </summary>
        /// <param name="points"></param>
        public GAPlane(List<GAPoint> points)
        {
            if (points.Count < 3) { return; }
            GAPoint A_ = points[0];
            GAPoint B_ = null;
            GAPoint C_ = null;
            for (int i = 1; i < points.Count; i++)
            {
                B_ = points[i];
                if (!GAGeometry.is_the_same_coordinates(A_, B_))
                {
                    for (int j = i + 1; j < points.Count; j++)
                    {
                        if (!GAGeometry.is_points_on_line(A_, B_, points[j]))
                        {
                            C_ = points[j];
                            break;
                        }
                    }
                }
                if (C_ != null) { break; }
            }

            if (A_ == null) { return; }
            if (B_ == null) { return; }
            if (C_ == null) { return; }

            double a21, a22, a23, a31, a32, a33;

            a21 = B_.X - A_.X;
            a22 = B_.Y - A_.Y;
            a23 = B_.Z - A_.Z;

            a31 = C_.X - A_.X;
            a32 = C_.Y - A_.Y;
            a33 = C_.Z - A_.Z;

            A = a22 * a33 - a32 * a23;
            B = -(a21 * a33 - a31 * a23);
            C = a21 * a32 - a22 * a31;
            D = -A * A_.X - B * A_.Y - C * A_.Z;
        }

        /// <summary>
        /// плоскость по уравнению Ax+By+Cy+D=0
        /// </summary>
        /// <param name="A_"></param>
        /// <param name="B_"></param>
        /// <param name="C_"></param>
        /// <param name="D_"></param>
        public GAPlane(double A_, double B_, double C_, double D_)
        {
            A = A_;
            B = B_;
            C = C_;
            D = D_;
        }

        /// <summary>
        /// Плоскость, перпендикулярна вектору и через точку
        /// </summary>
        /// <param name="point">точка</param>
        /// <param name="vector">вектор {x,y,z}</param>
        public GAPlane(GAPoint point, double[] vector)
        {
            A = vector[0];
            B = vector[1];
            C = vector[2];
            D = -(A * point.X + B * point.Y + C * point.Z);
        }

        /// <summary>
        /// Базовая плоскость XY
        /// </summary>
        /// <returns></returns>
        public static GAPlane GetPlaneXY() 
        {
            return new GAPlane(new GAPoint(0, 0, 0), new GAPoint(1, 0, 0), new GAPoint(0, 1, 0));
        }

        /// <summary>
        /// Базовая плоскость XZ
        /// </summary>
        /// <returns></returns>
        public static GAPlane GetPlaneXZ()
        {
            return new GAPlane(new GAPoint(0, 0, 0), new GAPoint(1, 0, 0), new GAPoint(0, 0, 1));
        }

        /// <summary>
        /// Базовая плоскость YZ
        /// </summary>
        /// <returns></returns>
        public static GAPlane GetPlaneYZ()
        {
            return new GAPlane(new GAPoint(0, 0, 0), new GAPoint(0, 1, 0), new GAPoint(0, 0, 1));
        }
 
    }
}
