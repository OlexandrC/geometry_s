using System.Collections.Generic;

namespace geometry_s
{
    /// <summary>
    /// поверхность в которой все точки это выпуклый контур
    /// </summary>
    public class GAFaceFlatConvex
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
        /// линия базовая для минимального по площади прямоугольника - размер L
        /// </summary>
        public GALine dim_L_line { get; }

        /// <summary>
        /// точка базовая для минимального по площади прямоугольника - размер L
        /// </summary>
        public GAPoint dim_L_point { get; }

        /// <summary>
        /// точка базовая для минимального по площади прямоугольника - размер W
        /// </summary>
        public GAPoint dim_W_point1 { get; }

        /// <summary>
        /// точка базовая для минимального по площади прямоугольника - размер W
        /// </summary>
        public GAPoint dim_W_point2 { get; }

        /// <summary>
        /// Area of minimal rectangle that bounds points
        /// </summary>
        public double AreaMinimal { get; }

        /// <summary>
        /// поверхность в которой все точки это выпуклый контур
        /// определяет точки для простановки размеров
        /// </summary>
        /// <param name="Points">the same plane points</param>
        public GAFaceFlatConvex(List<GAPoint> Points)
        {
            if (Points.Count < 3) { return; }
            points = Points;
            lines = new List<GALine>();
            center_point = new GAPoint();

            plane = new GAPlane(Points);

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

            // найти минимальную площадь
            // для всех линий
            for (int i = 0; i < lines.Count; i++)
            {
                GALine l_loop = lines[i];
                double max_dist_L = 0;
                GAPoint dim_L_point_loop = null;
                GAPoint dim_W_point1_loop = null;
                GAPoint dim_W_point2_loop = null;

                #region найдем наиболее удаленную точку
                for (int j = 0; j < points.Count; j++)
                {
                    GAPoint p = points[j];
                    double dist = GAGeometry.distance_line_point(l_loop, p);
                    if (dist > max_dist_L)
                    {
                        dim_L_point_loop = p;
                        max_dist_L = dist;
                    }

                    //if (GAGeometry.is_the_same_coordinates(p, l_loop.A) || GAGeometry.is_the_same_coordinates(p, l_loop.B))
                    //{
                    //    continue;
                    //}
                    //else
                    //{
                    //    double dist = GAGeometry.distance_line_point(l_loop, p);
                    //    if (dist > max_dist_L)
                    //    {
                    //        dim_L_point_loop = p;
                    //        max_dist_L = dist;
                    //    }
                    //}
                }
                #endregion

                #region найдем ширину перпендикулярно базовой линии, спроецируем точки на базовую линию для замера расстояния
                double max_dist_W = 0;
                for (int j = 0; j < points.Count; j++)
                {
                    GAPoint point1 = GAGeometry.PerpendicularToPoint(l_loop, points[j]);
                    for (int k = j + 1; k < points.Count; k++)
                    {
                        GAPoint point2 = GAGeometry.PerpendicularToPoint(l_loop, points[k]);
                        double dist = GAGeometry.distance_points(point1, point2);
                        if (dist > max_dist_W)
                        {
                            max_dist_W = dist;
                            dim_W_point1_loop = points[j];
                            dim_W_point2_loop = points[k];
                        }
                    }
                }
                #endregion
                double Area = max_dist_L * max_dist_W;

                #region если площадь наименьшая, то запоминаем точки для размеров
                if (Area < AreaMinimal || AreaMinimal <= 0)
                {
                    AreaMinimal = Area;
                    dim_L_line = l_loop;
                    dim_L_point = dim_L_point_loop;

                    dim_W_point1 = dim_W_point1_loop;
                    dim_W_point2 = dim_W_point2_loop;
                }
                #endregion
            }
        }

    }
}
