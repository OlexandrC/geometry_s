using System;

namespace geometry_s
{
    /// <summary>
    /// 3d line
    /// </summary>
    public class GALine
    {
        public GAPoint A { get; set; }
        public GAPoint B { get; set; }

        public GAPoint center_point { get; set; }

        public double length { get; }
        public double max_X { get; }
        public double max_Y { get; }
        public double max_Z { get; }

        public int index { get; set; }
        public int line_type { get; set; }
        public int description { get; set; }

        /// <summary>
        /// направляющий вектор
        /// </summary>
        public GAVector NP { get; set; }

        /// <summary>
        /// линия 3D
        /// </summary>
        /// <param name="a">начальная точка</param>
        /// <param name="b">конечная точка</param>
        /// <param name="index_">индекс линии (вспомогательный параметр)</param>
        /// <param name="line_type_">тип линии (вспомогательный параметр)</param>
        /// <param name="description_">описание (вспомогательный параметр)</param>
        public GALine(GAPoint a, GAPoint b, int index_ = -1, int line_type_ = 0, int description_ = (int)geometry_s.GAGeometry.word.пусто)
        {
            A = a; B = b;
            length = Math.Round(Math.Pow(Math.Pow((A.X - B.X), 2) + Math.Pow((A.Y - B.Y), 2) + Math.Pow((A.Z - B.Z), 2), 0.5), 10);

            max_X = Math.Max(A.X, B.X);
            max_Y = Math.Max(B.Y, B.Y);
            max_Z = Math.Max(A.Z, B.Z);

            NP = new GAVector(b.X - a.X, b.Y - a.Y, b.Z - a.Z);

            center_point = new GAPoint(
                (Math.Max(a.X, b.X) + Math.Min(a.X, b.X)) / 2.0,
                (Math.Max(a.Y, b.Y) + Math.Min(a.Y, b.Y)) / 2.0,
                (Math.Max(a.Z, b.Z) + Math.Min(a.Z, b.Z)) / 2.0);

            index = index_;
            line_type = line_type_;
            description = description_;
        }

        /// <summary>
        /// точка на линии на указанном расстоянии от точки А в направлении точки В
        /// </summary>
        /// <param name="distance">расстояние от точки А до точки на линии в направлении точки В</param>
        /// <returns></returns>
        public GAPoint getPointOnLine(double distance)
        {
            return new GAPoint
            (
                ((B.X - A.X) / length) * (distance) + A.X,
                ((B.Y - A.Y) / length) * (distance) + A.Y,
                ((B.Z - A.Z) / length) * (distance) + A.Z
            );
        }
    }
}
