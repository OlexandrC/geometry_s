using System.Numerics;

namespace geometry_s
{
    /// <summary>
    /// 3d line
    /// </summary>
    public class GALine_Big
    {
        /// <summary>
        /// point of line
        /// </summary>
        public GAPoint_Big A { get; set; }

        /// <summary>
        /// point of line
        /// </summary>
        public GAPoint_Big B { get; set; }

        /// <summary>
        /// line length
        /// </summary>
        public BigInteger length { get; }

        /// <summary>
        /// 3D line with BigInteger calculation
        /// </summary>
        /// <param name="a">начальная точка</param>
        /// <param name="b">конечная точка</param>
        public GALine_Big(GAPoint_Big a, GAPoint_Big b)
        {
            A = a; B = b;

            length = GAGeometry.Sqrt_Big(
                BigInteger.Add(
                    BigInteger.Pow(BigInteger.Subtract(A.X, B.X), 2),
                    BigInteger.Add(
                        BigInteger.Pow(BigInteger.Subtract(A.Y, B.Y), 2)
                        ,
                        BigInteger.Pow(BigInteger.Subtract(A.Z, B.Z), 2)
                        )
                    )
             );
        }
    }
}
