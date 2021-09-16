using System.Linq;

namespace geometry_s
{
    /// <summary>
    /// 3d point
    /// </summary>
    public class GAPoint
    {
        /// <summary>
        /// X
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Y
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// Z
        /// </summary>
        public double Z { get; set; }

        /// <summary>
        /// description as string
        /// </summary>
        public string description_t { get; set; }

        /// <summary>
        /// description as integer
        /// </summary>
        public int description { get; set; }

        /// <summary>
        /// index
        /// </summary>
        public int index { get; set; }

        /// <summary>
        /// 3D point
        /// </summary>
        public GAPoint()
        {
            X = 0;
            Y = 0;
            Z = 0;
        }

        /// <summary>
        /// 3D point
        /// </summary>
        public GAPoint(double x, double y, double z, int description_ = 0, int index_ = 0, string description_t_ = "")
        {
            X = x;
            Y = y;
            Z = z;

            description_t = description_t_;
            description = description_;
            index = index_;
        }

        /// <summary>
        /// 3D point
        /// </summary>
        public GAPoint(int x, int y, int z, int description_ = 0, int index_ = 0, string description_t_ = "")
        {
            X = x;
            Y = y;
            Z = z;

            description_t = description_t_;
            description = description_;
            index = index_;
        }

        /// <summary>
        /// 3D point
        /// </summary>
        public GAPoint(double[] A, int description_ = 0, int index_ = 0, string description_t_ = "")
        {
            description = description_;
            index = index_;
            description_t = description_t_;

            if (A.Count() == 3)
            {
                X = A[0];
                Y = A[1];
                Z = A[2];
            }
            else if (A.Count() == 2)
            {
                X = A[0];
                Y = A[1];
                Z = 0;
            }
            else if (A.Count() == 1)
            {
                X = A[0];
                Y = 0;
                Z = 0;
            }
            else
            {
                X = 0;
                Y = 0;
                Z = 0;
            }
        }

        /// <summary>
        /// 3D point
        /// </summary>
        public GAPoint(GAPoint p)
        {
            X = p.X;
            Y = p.Y;
            Z = p.Z;
            description = p.description;
            index = p.index;
            description_t = p.description_t;
        }

        /// <summary>
        /// new GAPoint(A.X + B.X, A.Y + B.Y, A.Z + B.Z);
        /// </summary>
        public static GAPoint operator +(GAPoint A, GAPoint B)
        {
            return new GAPoint(A.X + B.X, A.Y + B.Y, A.Z + B.Z);
        }

        /// <summary>
        /// new GAPoint(B.X - A.X, B.Y - A.Y, B.Z - A.Z);
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public static GAPoint operator -(GAPoint A, GAPoint B)
        {
            return new GAPoint(B.X - A.X, B.Y - A.Y, B.Z - A.Z);
        }

        /// <summary>
        /// new GAPoint(point.X * val, point.Y * val, point.Z * val, point.description, point.index, point.description_t);
        /// </summary>
        /// <param name="point">point</param>
        /// <param name="val">number</param>
        /// <returns></returns>
        public static GAPoint operator *(GAPoint point, double val)
        {
            return new GAPoint(point.X * val, point.Y * val, point.Z * val, point.description, point.index, point.description_t);
        }

        /// <summary>
        ///  new GAPoint(point.X / val, point.Y / val, point.Z / val, point.description, point.index, point.description_t);
        /// </summary>
        /// <param name="point">point</param>
        /// <param name="val">number</param>
        /// <returns></returns>
        public static GAPoint operator /(GAPoint point, double val)
        {
            return new GAPoint(point.X / val, point.Y / val, point.Z / val, point.description, point.index, point.description_t);
        }
    }
}
