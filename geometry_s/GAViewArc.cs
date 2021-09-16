namespace geometry_s
{
    /// <summary>
    /// 2d arc
    /// </summary>
    public class GAViewArc
    {
        public GAViewPoint center { get; set; }
        public double radius { get; set; }
        public double angle1_deg { get; set; }
        public double angle2_deg { get; set; }

        public int description { get; set; }
        public int index { get; set; }

        /// <summary>
        /// дуга
        /// </summary>
        /// <param name="center_">центр</param>
        /// <param name="radius_">радиус</param>
        /// <param name="angle1_deg_">начальный угол</param>
        /// <param name="angle2_deg_">конечный угол</param>
        public GAViewArc(GAViewPoint center_, double radius_, double angle1_deg_, double angle2_deg_, int description_ = 0, int index_ = 0)
        {
            center = center_;
            radius = radius_;
            angle1_deg = angle1_deg_;
            angle2_deg = angle2_deg_;
            description = description_;
            index = index_;
        }
    }
}
