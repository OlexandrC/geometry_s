using System;
using System.Collections.Generic;

namespace geometry_s
{
    /// <summary>
    /// тетраэдр
    /// </summary>
    public class GATetrahedron
    {
        /// <summary>
        /// точка (вершина)
        /// </summary>
        public GAPoint A;

        /// <summary>
        /// точка (вершина)
        /// </summary>
        public GAPoint B;

        /// <summary>
        /// точка (вершина)
        /// </summary>
        public GAPoint C;

        /// <summary>
        /// точка (вершина)
        /// </summary>
        public GAPoint D;

        /// <summary>
        /// центр тяжести
        /// </summary>
        public GAPoint center_weight;

        /// <summary>
        /// объем
        /// </summary>
        public double volume;

        public List<GATriangle> triangles { get; }

        /// <summary>
        /// тетраэдр
        /// </summary>
        /// <param name="a">точка</param>
        /// <param name="b">точка</param>
        /// <param name="c">точка</param>
        /// <param name="d">точка</param>
        public GATetrahedron(GAPoint a, GAPoint b, GAPoint c, GAPoint d)
        {
            GAPoint[] points = new GAPoint[] { a, b, c, d };
            A = a;
            B = b;
            C = c;
            D = d;

            triangles = new List<GATriangle>();
            triangles.Add(new GATriangle(a, b, c));
            triangles.Add(new GATriangle(a, b, d));
            triangles.Add(new GATriangle(a, d, c));
            triangles.Add(new GATriangle(d, b, c));

            center_weight = new GAPoint();
            center_weight.X = (A.X + B.X + C.X + D.X) / 4;
            center_weight.Y = (A.Y + B.Y + C.Y + D.Y) / 4;
            center_weight.Z = (A.Z + B.Z + C.Z + D.Z) / 4;

            double l1 = GAGeometry.distance_points(a, b);
            double l2 = GAGeometry.distance_points(a, c);
            double l3 = GAGeometry.distance_points(a, d);
            double l4 = GAGeometry.distance_points(b, c);
            double l5 = GAGeometry.distance_points(c, d);
            double l6 = GAGeometry.distance_points(b, d);

            volume = Math.Pow(
                (
                l1 * l1 * l5 * l5 * (l2 * l2 + l3 * l3 + l4 * l4 + l6 * l6 - l1 * l1 - l5 * l5) +
                l2 * l2 * l6 * l6 * (l1 * l1 + l3 * l3 + l4 * l4 + l5 * l5 - l2 * l2 - l6 * l6) +
                l3 * l3 * l4 * l4 * (l1 * l1 + l2 * l2 + l5 * l5 + l6 * l6 - l3 * l3 - l4 * l4) -
                l1 * l1 * l2 * l2 * l4 * l4 -
                l2 * l2 * l3 * l3 * l5 * l5 -
                l1 * l1 * l3 * l3 * l6 * l6 -
                l4 * l4 * l5 * l5 * l6 * l6
                ) / 144
                , 0.5);
        }

    }
}
