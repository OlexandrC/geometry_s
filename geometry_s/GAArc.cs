using System;
using System.Collections.Generic;

namespace geometry_s
{
    /// <summary>
    /// 3D Arc
    /// </summary>
    public class GAArc
    {
        /// <summary>
        /// arc center
        /// </summary>
        public GAPoint center { get; set; }

        /// <summary>
        /// arc start point
        /// </summary>
        public GAPoint start { get; set; }

        /// <summary>
        /// arc end point
        /// </summary>
        public GAPoint end { get; set; }

        /// <summary>
        /// arc radius
        /// </summary>
        public double radius { get; set; }

        /// <summary>
        /// arc plane
        /// </summary>
        public GAVector normal { get; set; }

        /// <summary>
        /// arc angle, DEG
        /// </summary>
        public double angle { get; }

        /// <summary>
        /// arc length
        /// </summary>
        public double length { get; }

        /// <summary>
        /// 3D arc //always counterclockwise, to change direction set plane with -Z.
        /// </summary>
        /// <param name="center_">arc center</param>
        /// <param name="start_">arc start point</param>
        /// <param name="angle_">arc sweep angle</param>
        /// <param name="normal_">arc normal vector (axis) (set -Z to change direction)</param>
        public GAArc(GAPoint center_, GAPoint start_, double angle_, GAVector normal_)
        {
            center = center_;
            start = start_;
            normal = normal_;
            angle = angle_;

            matrixTransform4x4_2 matrix = new matrixTransform4x4_2();
            matrix.setRotation(normal, angle);

            //точка смещенная, для матрицы трансформации
            //GAPoint pointMovedToCenter = new GAPoint(center - start_);
            GAPoint next_point = matrix.multiply(
                center - start_
                );

            end = matrix.multiply(center - start_) + center;

            radius = GAGeometry.distance(center, start);

            length = (angle / 360) * radius * 2.0 * Math.PI;

        }

        /// <summary>
        /// Возвращает точки делящие дугу на равные части
        /// </summary>
        /// <param name="amount">Сколько точек вернуть (1=середина дуги)</param>
        /// <returns></returns>
        public List<GAPoint> divide(int amount)
        {
            amount++;

            //точка смещенная, для матрицы трансформации
            GAPoint pointMovedToCenter = new GAPoint(center - start);

            List<GAPoint> points = new List<GAPoint>();

            double angle_step = angle / (double)amount;

            for (int p = 1; p < amount; p++) //начинаем с 1 так как в индексе 0 точка будет совпадать с началом/концом дуги
            {
                matrixTransform4x4_2 matrix = new matrixTransform4x4_2();
                matrix.setRotation(normal.X, normal.Y, normal.Z, angle_step * p);

                GAPoint next_point = matrix.multiply(
                    pointMovedToCenter
                    );

                GAPoint newP = next_point + center;

                points.Add(newP);
            }

            return points;
        }
    }
}
