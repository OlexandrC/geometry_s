using System.Numerics;

namespace geometry_s
{
    /// <summary>
    /// 3d point BigInteger calculations
    /// </summary>
    public class GAPoint_Big
    {
        /// <summary>
        /// X coordinate
        /// </summary>
        public BigInteger X { get; set; }

        /// <summary>
        /// Y coordinate
        /// </summary>
        public BigInteger Y { get; set; }

        /// <summary>
        /// Z coordinate
        /// </summary>
        public BigInteger Z { get; set; }

        /// <summary>
        /// Point (0,0,0) BigInteger
        /// </summary>
        public GAPoint_Big()
        {
            X = BigInteger.Zero;
            Y = BigInteger.Zero;
            Z = BigInteger.Zero;
        }

        /// <summary>
        /// Point with BigInteger numbers
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public GAPoint_Big(BigInteger x, BigInteger y, BigInteger z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Point with BigInteger numbers. int will convert to BigInteger
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public GAPoint_Big(int x, int y, int z)
        {
            X = (BigInteger)x;
            Y = (BigInteger)y;
            Z = (BigInteger)z;
        }

        /// <summary>
        /// Point with BigInteger numbers. double will convert to BigInteger
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="description_"></param>
        /// <param name="index_"></param>
        public GAPoint_Big(double x, double y, double z)
        {
            X = (BigInteger)x;
            Y = (BigInteger)y;
            Z = (BigInteger)z;
        }
    }
}
