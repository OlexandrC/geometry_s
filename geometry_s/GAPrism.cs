using System.Collections.Generic;

namespace geometry_s
{
    public class GAPrism
    {
        public List<GALine> ground = new List<GALine>();
        public List<GALine> top = new List<GALine>();
        public List<GALine> ribs = new List<GALine>();

        public GAPoint centr_point = new GAPoint();

        public double Radius = 0;
        public double radius = 0;
        public double Height = 0;
        public List<GAPoint> points = new List<GAPoint>();

        public List<GAPoint> ground_points = new List<GAPoint>();
        public List<GAPoint> top_points = new List<GAPoint>();

        public GAPrism(GAPoint start_point_, double Radius_, int rib_count_, double height_, bool inside_radius = false, double a1 = 0, double a2 = 0, double a3 = 0)
        {
            if (rib_count_ < 3) { return; }
            centr_point = start_point_;

            double angle_summ = 180 * (rib_count_ - 2);
            double angle_gam = angle_summ / rib_count_;

            Height = height_;

            if (inside_radius)
            {
                radius = Radius_;
                Radius = radius / (GAGeometry.Sin(angle_gam / 2.0));

            }
            else
            {

                Radius = Radius_;
                radius = Radius * GAGeometry.Sin(angle_gam / 2.0);
            }

            for (int i = 0; i < rib_count_; i++)
            {
                GAPoint p = new GAPoint(
                    start_point_.X - Radius * GAGeometry.Cos((360.0 / rib_count_) * i),
                    start_point_.Y + Radius * GAGeometry.Sin((360.0 / rib_count_) * i),
                    0, 0, i);

                GAPoint p2 = new GAPoint(
                    start_point_.X - Radius * GAGeometry.Cos((360.0 / rib_count_) * i),
                    start_point_.Y + Radius * GAGeometry.Sin((360.0 / rib_count_) * i),
                    Height, 0, i);

                ground_points.Add(GAGeometry.transform_rotate(p, start_point_, a1, a2, a3));
                top_points.Add(GAGeometry.transform_rotate(p2, start_point_, a1, a2, a3));

            }

            //GAPoint top_point = new GAPoint(start_point_.X, start_point_.Y, start_point_.Z + Height, -1, -1);
            //GAPoint top_point_r = transform_rotate(top_point, start_point_, a1, a2, a3);

            ground = new List<GALine>();
            top = new List<GALine>();

            for (int i = 0; i < rib_count_; i++)
            {
                ribs.Add(new GALine(top_points[i], ground_points[i], i, (int)GAGeometry.word.ребро));

                if (i < rib_count_ - 1)
                {
                    ground.Add(new GALine(ground_points[i + 1], ground_points[i], i, (int)GAGeometry.word.основа));
                    top.Add(new GALine(top_points[i + 1], top_points[i], i, (int)GAGeometry.word.верх));
                }
            }

            ground.Add(new GALine(ground_points[ground_points.Count - 1], ground_points[0], ground_points.Count - 1, (int)GAGeometry.word.основа));
            top.Add(new GALine(top_points[top_points.Count - 1], top_points[0], top_points.Count - 1, (int)GAGeometry.word.верх));
        }

        /// <summary>
        /// список всех линий
        /// </summary>
        /// <returns></returns>
        public List<GALine> getAllLines()
        {
            List<GALine> all_lines = new List<GALine>();
            all_lines.AddRange(ground);
            all_lines.AddRange(top);
            all_lines.AddRange(ribs);
            return all_lines;
        }
    }
}
