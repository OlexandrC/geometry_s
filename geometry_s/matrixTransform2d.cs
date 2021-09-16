namespace geometry_s
{
    //2019-08-12
    /// <summary>
    /// cell_11, cell_21 - for X transform
    /// cell_12, cell_22 - for Y transform
    /// cell_31 - X shear, cell_32 - Y shear
    /// cell_13, cell_23 - not using
    /// </summary>
    public class matrixTransform2d
    {

        /// <summary>
        /// cell 1,1 = x1
        /// </summary>
        public double cell_11 { get; set; }
        /// <summary>
        /// cell 1,2 = y1
        /// </summary>
        public double cell_12 { get; set; }
        /// <summary>
        /// cell 1,3 - not using
        /// </summary>
        public double cell_13 { get; set; }

        /// <summary>
        /// cell 2,1 = x2
        /// </summary>
        public double cell_21 { get; set; }
        /// <summary>
        /// cell 2,2 = y2
        /// </summary>
        public double cell_22 { get; set; }
        /// <summary>
        /// cell 2,3 - not using
        /// </summary>
        public double cell_23 { get; set; }

        /// <summary>
        /// cell 3,1 = x shear
        /// </summary>
        public double cell_31 { get; set; }
        /// <summary>
        /// cell 3,2 = y shear
        /// </summary>
        public double cell_32 { get; set; }
        /// <summary>
        /// cell 3,3 = scale
        /// </summary>
        public double cell_33 { get; set; }

        /// <summary>
        /// scale = cell 3,3
        /// </summary>
        public double scale { get { return cell_33; } set { scale = cell_33; } }

        /// <summary>
        /// default matrix [1,0,0, 0,1,0, 0,0,1]
        /// </summary>
        public matrixTransform2d()
        {
            cell_11 = 1; //x1
            cell_12 = 0; //y1
            cell_13 = 0;

            cell_21 = 0; //x2
            cell_22 = 1; //y2
            cell_23 = 0;

            cell_31 = 0;
            cell_32 = 0;
            cell_33 = 1; //scale

        }

        public void rotate_clockwise(double degrees)
        {
            cell_11 = GAGeometry.Cos(degrees);
            cell_12 = GAGeometry.Sin(degrees);
            cell_21 = -GAGeometry.Sin(degrees);
            cell_22 = GAGeometry.Cos(degrees);
        }
        public void rotate_counterclockwise(double degrees)
        {
            cell_11 = GAGeometry.Cos(degrees);
            cell_12 = -GAGeometry.Sin(degrees);
            cell_21 = GAGeometry.Sin(degrees);
            cell_22 = GAGeometry.Cos(degrees);
        }

        /// <summary>
        /// 2d point transformation
        /// </summary>
        /// <param name="viewPoint">2d point</param>
        /// <returns></returns>
        public GAViewPoint transformViewPoint(GAViewPoint viewPoint)
        {
            double[] pp = transform2dPoint(viewPoint.X, viewPoint.Y);
            return new GAViewPoint(pp[0], pp[1], viewPoint.description, viewPoint.index, viewPoint.description_t);
        }

        /// <summary>
        /// 2d point transformation
        /// </summary>
        /// <param name="point_X"></param>
        /// <param name="point_Y"></param>
        /// <returns></returns>
        public double[] transform2dPoint(double point_X, double point_Y)
        {
            double x = 0;
            double y = 0;

            x = cell_11 * point_X + cell_12 * point_Y;
            y = cell_21 * point_X + cell_22 * point_Y;

            x = cell_31 + x;
            y = cell_32 + y;

            x = x * scale;
            y = y * scale;
            return new double[] { x, y };
        }
    }
}
