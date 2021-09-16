namespace geometry_s
{
    /// <summary>
    /// 2d line
    /// </summary>
    public class GAViewLine
    {
        public GAViewPoint A { get; set; }
        public GAViewPoint B { get; set; }
        public double length { get; set; }

        public int description { get; set; }
        public int index { get; set; }

        public GAViewLine(GAViewLine line_, int description_ = 0, int index_ = 0)
        {
            A = new GAViewPoint(line_.A);
            B = new GAViewPoint(line_.B);
            length = GAGeometry.distance_points(A, B);

            description = description_;
            index = index_;
        }
        public GAViewLine(GAViewPoint A_, GAViewPoint B_, int description_ = 0, int index_ = 0)
        {
            A = A_;
            B = B_;
            length = GAGeometry.distance_points(A, B);

            description = description_;
            index = index_;
        }
        public GAViewLine(double Ax, double Ay, double Bx, double By, int description_ = 0, int index_ = 0)
        {
            A = new GAViewPoint();
            B = new GAViewPoint();
            A.X = Ax;
            A.Y = Ay;
            B.X = Bx;
            B.Y = By;
            length = GAGeometry.distance_points(A, B);
            description = description_;
            index = index_;
        }
        public GAViewLine(int Ax, int Ay, int Bx, int By, int description_ = 0, int index_ = 0)
        {
            A = new GAViewPoint(Ax, Ay);
            B = new GAViewPoint(Bx, By);
            length = GAGeometry.distance_points(A, B);
            description = description_;
            index = index_;
        }

    }
}
