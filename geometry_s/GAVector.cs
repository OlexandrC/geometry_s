namespace geometry_s
{
    /// <summary>
    /// 3d Vector
    /// </summary>
    public class GAVector
    {
        /// <summary>
        /// 
        /// </summary>
        public double X;
        /// <summary>
        /// 
        /// </summary>
        public double Y;
        /// <summary>
        /// 
        /// </summary>
        public double Z;

        /// <summary>
        /// Vector by 3 parameters
        /// </summary>
        /// <param name="x_"></param>
        /// <param name="y_"></param>
        /// <param name="z_"></param>
        public GAVector(double x_, double y_, double z_)
        {
            X = x_;
            Y = y_;
            Z = z_;
        }

        /// <summary>
        /// vector by point
        /// </summary>
        /// <param name="A_"></param>
        public GAVector(GAPoint A_)
        {
            X = A_.X;
            Y = A_.Y;
            Z = A_.Z;
        }

        /// <summary>
        /// vector by points (B_X-A_X, B_Y-A_Y, B_Z-A_Z)
        /// </summary>
        /// <param name="A_"></param>
        /// <param name="B_"></param>
        public GAVector(GAPoint A_, GAPoint B_)
        {
            X = B_.X - A_.X;
            Y = B_.Y - A_.Y;
            Z = B_.Z - A_.Z;
        }

        public static GAVector operator +(GAVector A, GAVector B)
        {
            return new GAVector(A.X + B.X, A.Y + B.Y, A.Z + B.Z);
        }

        public static GAVector operator -(GAVector A, GAVector B)
        {
            return new GAVector(B.X - A.X, B.Y - A.Y, B.Z - A.Z);
        }

        /// <summary>
        /// Vector product, returns normal which is perpendicular to given vectors ( A.Y * B.Z - A.Z * B.Y,  -(A.X* B.Z - A.Z* B.X), A.X* B.Y - A.Y* B.X))
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public static GAVector operator *(GAVector A, GAVector B)
        {
            return new GAVector(
                A.Y * B.Z - A.Z * B.Y, 
                -(A.X * B.Z - A.Z * B.X), 
                A.X * B.Y - A.Y * B.X);
        }

    }
}
