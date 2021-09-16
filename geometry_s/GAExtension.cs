using System.Collections.Generic;
using System.Numerics;

namespace geometry_s
{
    public static class GAExtension
    {
        #region Object To Object

        /// <summary>
        /// makes 3d point from 2d point where (2d.X = 3d.X, 2d.Y = 3d.Y, 3d.Z = 0)
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static GAPoint ToPoint(this GAViewPoint point)
        {
            return new GAPoint(point.X, point.Y, 0);
        }

        public static GAPoint_Big ToPointBig(this GAPoint P)
        {
            return new GAPoint_Big(P.X, P.Y, P.Z);
        }

        /// <summary>
        /// Z=0
        /// </summary>
        /// <param name="P"></param>
        /// <returns></returns>
        public static GAPoint_Big ToPointBig(this GAViewPoint P)
        {
            return new GAPoint_Big(P.X, P.Y, 0);
        }

        /// <summary>
        /// manimal by X
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static GAViewPoint MinimumByX(this List<GAViewPoint> list)
        {
            GAViewPoint minPoint = null;
            foreach (GAViewPoint vP in list)
            {
                if (minPoint == null) { minPoint = vP; continue; }
                if (vP.X < minPoint.X)
                {
                    minPoint = vP;
                }
            }
            return minPoint;
        }

        /// <summary>
        /// maximal by X
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static GAViewPoint MaximalByX(this List<GAViewPoint> list)
        {
            GAViewPoint minPoint = null;
            foreach (GAViewPoint vP in list)
            {
                if (minPoint == null) { minPoint = vP; continue; }
                if (vP.X > minPoint.X)
                {
                    minPoint = vP;
                }
            }
            return minPoint;
        }

        /// <summary>
        /// minimal by Y
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static GAViewPoint MinimumByY(this List<GAViewPoint> list)
        {
            GAViewPoint minPoint = null;
            foreach (GAViewPoint vP in list)
            {
                if (minPoint == null) { minPoint = vP; continue; }
                if (vP.Y == minPoint.Y && vP.X < minPoint.X)
                {
                    minPoint = vP;
                }
                if (vP.Y < minPoint.Y)
                {
                    minPoint = vP;
                }

            }
            return minPoint;
        }

        /// <summary>
        /// maximal by Y
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static GAViewPoint MaximalByY(this List<GAViewPoint> list)
        {
            GAViewPoint maxPoint = null;
            foreach (GAViewPoint vP in list)
            {
                if (maxPoint == null) { maxPoint = vP; continue; }
                if (vP.Y > maxPoint.Y)
                {
                    maxPoint = vP;
                }
            }
            return maxPoint;
        }

        /// <summary>
        /// 4 points, maximal and minimal by X and by Y
        /// </summary>
        /// <param name="list"></param>
        /// <returns>массив точек</returns>
        public static GAViewPoint[] MaxX_MinX_MaxY_MinY(this List<GAViewPoint> list)
        {
            GAViewPoint maxPointX = list[0];
            GAViewPoint minPointX = list[0];
            GAViewPoint maxPointY = list[0];
            GAViewPoint minPointY = list[0];
            foreach (GAViewPoint vP in list)
            {
                if (vP.X > maxPointX.X)
                {
                    maxPointX = vP;
                }
                if (vP.X < minPointX.X)
                {
                    minPointX = vP;
                }
                if (vP.Y > maxPointY.Y)
                {
                    maxPointY = vP;
                }
                if (vP.Y < minPointY.Y)
                {
                    minPointY = vP;
                }
            }

            return new GAViewPoint[] { maxPointX, minPointX, maxPointY, minPointY };

        }

        public static List<GAFaceFlatSimple> toFaceFlatSimple(this List<GATriangle> triangles)
        {
            List<GAFaceFlatSimple> l_ffc = new List<GAFaceFlatSimple>();
            foreach (GATriangle tr in triangles)
            {
                l_ffc.Add(tr.toFaceFlatSimple());
            }
            return l_ffc;
        }

        #endregion
    }
}
