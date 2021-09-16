using System.Collections.Generic;

namespace geometry_s
{

    /// <summary>
    /// поверхность в пространстве состоящая из точек
    /// </summary>
    public class GAFaceFlatSimple
    {
        /// <summary>
        /// точки
        /// </summary>
        public List<GAPoint> points { get; }

        /// <summary>
        /// линии
        /// </summary>
        public List<GALine> lines { get; }

        /// <summary>
        /// плоскость поверхности
        /// </summary>
        public GAPlane plane { get; }

        /// <summary>
        /// центр точек, среднее значение всех координат
        /// </summary>
        public GAPoint center_point { get; }

        /// <summary>
        /// Поверхность в пространстве состоящая из точек.
        /// </summary>
        public GAFaceFlatSimple()
        {
            points = new List<GAPoint>();
            lines = new List<GALine>();
            center_point = new GAPoint();

            plane = null;

            center_point.X = 0;
            center_point.Y = 0;
            center_point.Z = 0;
        }

        /// <summary>
        /// Поверхность в пространстве состоящая из точек. Точки должны быть в одной плоскости
        /// </summary>
        /// <param name="Points"></param>
        public GAFaceFlatSimple(List<GAPoint> Points)
        {
            if (Points == null) { return; }
            if (Points.Count < 3) { return; }
            points = Points;
            lines = new List<GALine>();
            center_point = new GAPoint();

            GAPoint p1 = Points[0];
            GAPoint p2 = Points[1];
            GAPoint p3 = Points[2];

            plane = new GAPlane(p1, p2, p3);

            double max_min_X = 0, max_min_Y = 0, max_min_Z = 0;

            // добавляем линии и считаем сдреднюю точку одновременно
            for (int i = 0; i < Points.Count; i++)
            {
                GAPoint p = Points[i];
                max_min_X = max_min_X + p.X;
                max_min_Y = max_min_Y + p.Y;
                max_min_Z = max_min_Z + p.Z;
                if (i == 0) { continue; }
                lines.Add(new GALine(Points[i - 1], Points[i]));
            }
            if (Points.Count > 2) { lines.Add(new GALine(Points[Points.Count - 1], Points[0])); }

            center_point.X = max_min_X / Points.Count;
            center_point.Y = max_min_Y / Points.Count;
            center_point.Z = max_min_Z / Points.Count;

        }

    }
}
