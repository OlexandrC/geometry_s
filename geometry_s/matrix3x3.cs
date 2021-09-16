namespace geometry_s
{
    /// <summary>
    /// матрица третьего порядка
    /// </summary>
    public class matrix3x3
    {
        /// <summary>
        /// определитель матрицы
        /// </summary>
        public double A { get; }

        double r11 { get; set; }
        double r12 { get; set; }
        double r13 { get; set; }

        double r21 { get; set; }
        double r22 { get; set; }
        double r23 { get; set; }

        double r31 { get; set; }
        double r32 { get; set; }
        double r33 { get; set; }

        /// <summary>
        /// matrix 3 x 3
        /// </summary>
        /// <param name="a11"></param>
        /// <param name="a12"></param>
        /// <param name="a13"></param>
        /// <param name="a21"></param>
        /// <param name="a22"></param>
        /// <param name="a23"></param>
        /// <param name="a31"></param>
        /// <param name="a32"></param>
        /// <param name="a33"></param>
        public matrix3x3(double a11, double a12, double a13, double a21, double a22, double a23, double a31, double a32, double a33)
        {
            A = a11 * a22 * a33 - a11 * a23 * a32 -
                a12 * a21 * a33 + a12 * a23 * a31 +
                a13 * a21 * a32 - a13 * a22 * a31;

            //2019-08-08
            r11 = a11;
            r12 = a12;
            r13 = a13;

            r21 = a21;
            r22 = a22;
            r23 = a23;

            r31 = a31;
            r32 = a32;
            r33 = a33;
        }

        //2019-08-08
        /// <summary>
        /// matrix 3 x 3
        /// </summary>
        /// <param name="a">first 3 value is a first row of the matrix</param>
        public matrix3x3(double[] a)
        {
            double a11 = a[0];
            double a12 = a[1];
            double a13 = a[2];

            double a21 = a[3];
            double a22 = a[4];
            double a23 = a[5];

            double a31 = a[6];
            double a32 = a[7];
            double a33 = a[8];

            A = a11 * a22 * a33 - a11 * a23 * a32 -
                a12 * a21 * a33 + a12 * a23 * a31 +
                a13 * a21 * a32 - a13 * a22 * a31;

            r11 = a11;
            r12 = a12;
            r13 = a13;

            r21 = a21;
            r22 = a22;
            r23 = a23;

            r31 = a31;
            r32 = a32;
            r33 = a33;
        }

        //2019-08-12
        /// <summary>
        /// matrix 3 x 3
        /// </summary>
        /// <param name="a">first 3 value is a first row of the matrix</param>
        public matrix3x3()
        {
            double a11 = 0;
            double a12 = 0;
            double a13 = 0;

            double a21 = 0;
            double a22 = 0;
            double a23 = 0;

            double a31 = 0;
            double a32 = 0;
            double a33 = 0;

            A = a11 * a22 * a33 - a11 * a23 * a32 -
                a12 * a21 * a33 + a12 * a23 * a31 +
                a13 * a21 * a32 - a13 * a22 * a31;

            r11 = a11;
            r12 = a12;
            r13 = a13;

            r21 = a21;
            r22 = a22;
            r23 = a23;

            r31 = a31;
            r32 = a32;
            r33 = a33;
        }
    }
}
