using System.Collections.Generic;
using System.Linq;
using System.Numerics;


namespace geometry_s
{
    /// <summary>
    /// 3d triangle with BigInteger calculations
    /// </summary>
    public class GATriangle_Big
    {
        /// <summary>
        /// Точки треугольника
        /// </summary>
        public List<GAPoint_Big> points { get; }

        /// <summary>
        /// Линии треугольника
        /// </summary>
        public List<GALine_Big> lines { get; set; }

        /// <summary>
        /// Perimetr
        /// </summary>
        public BigInteger P { get; }

        /// <summary>
        /// Area (площадь по формуле Герона)
        /// </summary>
        public BigInteger Area { get; }

        /// <summary>
        /// Triangle with BigInteger calculations
        /// </summary>
        /// <param name="A_">Point</param>
        /// <param name="B_">Point</param>
        /// <param name="C_">Point</param>
        public GATriangle_Big(GAPoint_Big A_, GAPoint_Big B_, GAPoint_Big C_)
        {
            lines = new List<GALine_Big>();
            lines.Add(new GALine_Big(A_, B_));
            lines.Add(new GALine_Big(B_, C_));
            lines.Add(new GALine_Big(C_, A_));

            points = new List<GAPoint_Big>();
            points.Add(A_);
            points.Add(B_);
            points.Add(C_);

            P = GAGeometry.Summ_Big(lines.Select(a => a.length).ToArray<BigInteger>());
            BigInteger P_Divided_2 = BigInteger.Divide(P, (BigInteger)2);

            BigInteger b0 = BigInteger.Subtract(P_Divided_2, lines[0].length);
            BigInteger b1 = BigInteger.Subtract(P_Divided_2, lines[1].length);
            BigInteger b2 = BigInteger.Subtract(P_Divided_2, lines[2].length);

            Area = GAGeometry.Sqrt_Big(GAGeometry.Multiply_Big(P_Divided_2, b0, b1, b2));

        }

    }
}
