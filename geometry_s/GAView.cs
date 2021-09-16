using System;
using System.Collections.Generic;

namespace geometry_s
{
    /// <summary>
    /// view with axes, points, lines
    /// </summary>
    public class GAView
    {
        public double axis_x { get; set; }
        public double axis_y { get; set; }
        public double axis_z { get; set; }

        public GAViewPoint O_position_main { get; set; }
        public GAViewPoint O_position_isometric { get; set; }

        public List<GAViewPoint> points_view1_top { get; set; }
        public List<GAViewPoint> points_view2_front { get; set; }
        public List<GAViewPoint> points_view3_prof { get; set; }

        public List<GAViewPoint> points_view_isom { get; set; }
        public List<GAViewPoint> points_view_dimetric { get; set; }

        public List<GAViewLine> lines_view1_top { get; set; }
        public List<GAViewLine> lines_view2_front { get; set; }
        public List<GAViewLine> lines_view3_prof { get; set; }

        public List<GAViewLine> lines_connect_points_v1v2v3 { get; set; }
        public List<GAViewLine> lines_connect_points_v1v2 { get; set; }

        public List<GAViewLine> lines_view_isom { get; set; }
        public List<GAViewLine> lines_view_dimetric { get; set; }

        public List<GAViewLine> lines_axes_main { get; set; }
        public List<GAViewLine> lines_axes_main_2projection { get; set; }
        public List<GAViewLine> lines_axes_isom { get; set; }
        public List<GAViewLine> lines_axes_dimetric { get; set; }


        /// <summary>
        /// вычисляет виды
        /// </summary>
        /// <param name="O_position"> начало координат основных видов</param>
        /// <param name="isom_position">начало координат изометрического вида</param>
        /// <param name="offset_v1">смещение вида 1, вид сверху</param>
        /// <param name="offset_v2">смещение вида 2, вид фронт</param>
        /// <param name="offset_v3">смещение вида 3, вид профиль</param>
        /// <param name="lines">линии для вида</param>
        /// <param name="points">точки для вида, кружочки</param>
        public GAView(GAViewPoint O_position, GAViewPoint isom_position, GAViewPoint dimetric_position, GAViewPoint offset_v1, GAViewPoint offset_v2, GAViewPoint offset_v3, List<GALine> lines, List<GAPoint> points)
        {
            //double line_max_x = 0, line_max_y = 0, line_max_z = 0;

            O_position_main = O_position;
            O_position_isometric = isom_position;

            points_view1_top = new List<GAViewPoint>();
            points_view2_front = new List<GAViewPoint>();
            points_view3_prof = new List<GAViewPoint>();

            points_view_isom = new List<GAViewPoint>();
            points_view_dimetric = new List<GAViewPoint>();

            lines_connect_points_v1v2v3 = new List<GAViewLine>();
            lines_connect_points_v1v2 = new List<GAViewLine>();

            lines_view1_top = new List<GAViewLine>();
            lines_view2_front = new List<GAViewLine>();
            lines_view3_prof = new List<GAViewLine>();

            lines_view_isom = new List<GAViewLine>();
            lines_view_dimetric = new List<GAViewLine>();

            lines_axes_main_2projection = new List<GAViewLine>();
            lines_axes_main = new List<GAViewLine>();
            lines_axes_isom = new List<GAViewLine>();
            lines_axes_dimetric = new List<GAViewLine>();

            foreach (GAPoint p in points)
            {
                #region MyRegion

                points_view1_top.Add(new GAViewPoint(O_position.X - p.X, O_position.Y - p.Y, p.description, p.index, p.description_t));
                points_view2_front.Add(new GAViewPoint(O_position.X - p.X, O_position.Y + p.Z, p.description, p.index, p.description_t));
                points_view3_prof.Add(new GAViewPoint(O_position.X + p.Y, O_position.Y + p.Z, p.description, p.index, p.description_t));

                lines_connect_points_v1v2v3.Add(new GAViewLine(points_view1_top[points_view1_top.Count - 1], points_view2_front[points_view2_front.Count - 1], -1, p.index));
                lines_connect_points_v1v2v3.Add(new GAViewLine(points_view3_prof[points_view3_prof.Count - 1], points_view2_front[points_view2_front.Count - 1], -1, p.index));

                lines_connect_points_v1v2.Add(new GAViewLine(points_view1_top[points_view1_top.Count - 1], points_view2_front[points_view2_front.Count - 1], -1, p.index));

                points_view_isom.Add(get_isometric_point(p, isom_position));
                points_view_dimetric.Add(get_dimetric_point(p, dimetric_position));

                #endregion
            }

            foreach (GALine l in lines)
            {
                axis_x = Math.Max(l.max_X, axis_x);
                axis_y = Math.Max(l.max_Y, axis_y);
                axis_z = Math.Max(l.max_Z, axis_z);

                lines_view1_top.Add(new GAViewLine(
                    new GAViewPoint(O_position.X - l.A.X, O_position.Y - l.A.Y),
                    new GAViewPoint(O_position.X - l.B.X, O_position.Y - l.B.Y), l.description, l.index
                    ));

                lines_view2_front.Add(new GAViewLine(
                    new GAViewPoint(O_position.X - l.A.X, O_position.Y + l.A.Z),
                    new GAViewPoint(O_position.X - l.B.X, O_position.Y + l.B.Z), l.description, l.index
                    ));

                lines_view3_prof.Add(new GAViewLine(
                    new GAViewPoint(O_position.X + l.A.Y, O_position.Y + l.A.Z),
                    new GAViewPoint(O_position.X + l.B.Y, O_position.Y + l.B.Z), l.description, l.index
                    ));

                lines_view_isom.Add(get_isometric_line(l, isom_position));

                lines_view_dimetric.Add(get_dimetric_line(l, dimetric_position));

            }

            axis_x = axis_x + 5;
            axis_y = axis_y + 5;
            axis_z = axis_z + 5;


            lines_axes_main.Add(new GAViewLine(O_position, new GAViewPoint(O_position.X - axis_x, O_position.Y)));
            lines_axes_main.Add(new GAViewLine(O_position, new GAViewPoint(O_position.X, O_position.Y + axis_z)));
            lines_axes_main.Add(new GAViewLine(O_position, new GAViewPoint(O_position.X, O_position.Y - axis_y)));
            lines_axes_main.Add(new GAViewLine(O_position, new GAViewPoint(O_position.X + axis_y, O_position.Y)));

            lines_axes_main_2projection.Add(new GAViewLine(O_position, new GAViewPoint(O_position.X - axis_x, O_position.Y)));
            lines_axes_main_2projection.Add(new GAViewLine(O_position, new GAViewPoint(O_position.X, O_position.Y + axis_z)));
            lines_axes_main_2projection.Add(new GAViewLine(O_position, new GAViewPoint(O_position.X, O_position.Y - axis_y)));

            // isometric axis
            lines_axes_isom.Add(new GAViewLine(isom_position, get_isometric_point(new GAPoint(axis_x, 0, 0), isom_position)));
            lines_axes_isom.Add(new GAViewLine(isom_position, get_isometric_point(new GAPoint(0, axis_y, 0), isom_position)));
            lines_axes_isom.Add(new GAViewLine(isom_position, get_isometric_point(new GAPoint(0, 0, axis_z), isom_position)));

            // dimetric axis
            lines_axes_dimetric.Add(new GAViewLine(dimetric_position, get_dimetric_point(new GAPoint(axis_x, 0, 0), dimetric_position)));
            lines_axes_dimetric.Add(new GAViewLine(dimetric_position, get_dimetric_point(new GAPoint(0, axis_y, 0), dimetric_position)));
            lines_axes_dimetric.Add(new GAViewLine(dimetric_position, get_dimetric_point(new GAPoint(0, 0, axis_z), dimetric_position)));

        }

        /// <summary>
        /// вычисляет виды
        /// </summary>
        /// <param name="O_position"> начало координат основных видов</param>
        /// <param name="isom_position">начало координат изометрического вида</param>
        /// <param name="offset_v1">смещение вида 1, вид сверху</param>
        /// <param name="offset_v2">смещение вида 2, вид фронт</param>
        /// <param name="offset_v3">смещение вида 3, вид профиль</param>
        /// <param name="lines">линии для вида</param>
        /// <param name="points">точки для вида, кружочки</param>
        public GAView(GAViewPoint O_position, GAViewPoint isom_position, GAViewPoint dimetric_position, GAViewPoint offset_v1, GAViewPoint offset_v2, GAViewPoint offset_v3, GALine line, GAPoint point)
        {
            O_position_main = O_position;
            O_position_isometric = isom_position;

            points_view1_top = new List<GAViewPoint>();
            points_view2_front = new List<GAViewPoint>();
            points_view3_prof = new List<GAViewPoint>();

            points_view_isom = new List<GAViewPoint>();
            points_view_dimetric = new List<GAViewPoint>();

            lines_connect_points_v1v2v3 = new List<GAViewLine>();
            lines_connect_points_v1v2 = new List<GAViewLine>();

            lines_view1_top = new List<GAViewLine>();
            lines_view2_front = new List<GAViewLine>();
            lines_view3_prof = new List<GAViewLine>();

            lines_view_isom = new List<GAViewLine>();
            lines_view_dimetric = new List<GAViewLine>();

            lines_axes_main = new List<GAViewLine>();
            lines_axes_main_2projection = new List<GAViewLine>();
            lines_axes_isom = new List<GAViewLine>();
            lines_axes_dimetric = new List<GAViewLine>();



            points_view1_top.Add(new GAViewPoint(O_position.X - point.X, O_position.Y - point.Y, point.description, point.index, point.description_t));
            points_view2_front.Add(new GAViewPoint(O_position.X - point.X, O_position.Y + point.Z, point.description, point.index, point.description_t));
            points_view3_prof.Add(new GAViewPoint(O_position.X + point.Y, O_position.Y + point.Z, point.description, point.index, point.description_t));

            points_view_isom.Add(get_isometric_point(point, isom_position));
            points_view_dimetric.Add(get_dimetric_point(point, dimetric_position));



            lines_connect_points_v1v2v3.Add(new GAViewLine(points_view1_top[points_view1_top.Count - 1], points_view2_front[points_view2_front.Count - 1], -1, point.index));
            lines_connect_points_v1v2v3.Add(new GAViewLine(points_view3_prof[points_view3_prof.Count - 1], points_view2_front[points_view2_front.Count - 1], -1, point.index));

            lines_connect_points_v1v2.Add(new GAViewLine(points_view1_top[points_view1_top.Count - 1], points_view2_front[points_view2_front.Count - 1], -1, point.index));

            axis_x = Math.Max(line.max_X, axis_x);
            axis_y = Math.Max(line.max_Y, axis_y);
            axis_z = Math.Max(line.max_Z, axis_z);

            lines_view1_top.Add(new GAViewLine(
                new GAViewPoint(O_position.X - line.A.X, O_position.Y - line.A.Y),
                new GAViewPoint(O_position.X - line.B.X, O_position.Y - line.B.Y)
                ));

            lines_view2_front.Add(new GAViewLine(
                new GAViewPoint(O_position.X - line.A.X, O_position.Y + line.A.Z),
                new GAViewPoint(O_position.X - line.B.X, O_position.Y + line.B.Z)
                ));

            lines_view3_prof.Add(new GAViewLine(
                new GAViewPoint(O_position.X + line.A.Y, O_position.Y + line.A.Z),
                new GAViewPoint(O_position.X + line.B.Y, O_position.Y + line.B.Z)
                ));

            lines_view_isom.Add(get_isometric_line(line, isom_position));

            lines_view_dimetric.Add(get_dimetric_line(line, dimetric_position));



            axis_x = axis_x + 5;
            axis_y = axis_y + 5;
            axis_z = axis_z + 5;


            lines_axes_main.Add(new GAViewLine(O_position, new GAViewPoint(O_position.X - axis_x, O_position.Y)));
            lines_axes_main.Add(new GAViewLine(O_position, new GAViewPoint(O_position.X, O_position.Y + axis_z)));
            lines_axes_main.Add(new GAViewLine(O_position, new GAViewPoint(O_position.X, O_position.Y - axis_y)));
            lines_axes_main.Add(new GAViewLine(O_position, new GAViewPoint(O_position.X + axis_y, O_position.Y)));

            lines_axes_main_2projection.Add(new GAViewLine(O_position, new GAViewPoint(O_position.X - axis_x, O_position.Y)));
            lines_axes_main_2projection.Add(new GAViewLine(O_position, new GAViewPoint(O_position.X, O_position.Y + axis_z)));
            lines_axes_main_2projection.Add(new GAViewLine(O_position, new GAViewPoint(O_position.X, O_position.Y - axis_y)));

            // isometric axis
            lines_axes_isom.Add(new GAViewLine(isom_position, get_isometric_point(new GAPoint(axis_x, 0, 0), isom_position)));
            lines_axes_isom.Add(new GAViewLine(isom_position, get_isometric_point(new GAPoint(0, axis_y, 0), isom_position)));
            lines_axes_isom.Add(new GAViewLine(isom_position, get_isometric_point(new GAPoint(0, 0, axis_z), isom_position)));

            // dimetric axis
            lines_axes_dimetric.Add(new GAViewLine(dimetric_position, get_dimetric_point(new GAPoint(axis_x, 0, 0), dimetric_position)));
            lines_axes_dimetric.Add(new GAViewLine(dimetric_position, get_dimetric_point(new GAPoint(0, axis_y, 0), dimetric_position)));
            lines_axes_dimetric.Add(new GAViewLine(dimetric_position, get_dimetric_point(new GAPoint(0, 0, axis_z), dimetric_position)));

        }

        /// <summary>
        /// возвращает XY для вида чертежа из XYZ
        /// </summary>
        /// <param name="A">точка</param>
        /// <param name="isom_position">O-точка изометрического вида</param>
        /// <returns></returns>
        public GAViewPoint get_isometric_point(GAPoint A, GAViewPoint isom_position)
        {
            return new GAViewPoint(isom_position.X - A.X * 0.82 * GAGeometry.Cos(30) + A.Y * 0.82 * GAGeometry.Cos(30),
                    isom_position.Y + A.Z * 0.82 - A.X * 0.82 * GAGeometry.Sin(30) - A.Y * 0.82 * GAGeometry.Sin(30)
                    , A.description, A.index);
        }

        /// <summary>
        /// возвращает XY для вида чертежа из XYZ
        /// </summary>
        /// <param name="line">линия A(XYZ), B(XYZ)</param>
        /// <param name="isom_position">O-точка изометрического вида</param>
        /// <returns>А(XY), B(XY) вида чертежа</returns>
        public GAViewLine get_isometric_line(GALine line, GAViewPoint isom_position)
        {
            return
                new GAViewLine(get_isometric_point(line.A, isom_position), get_isometric_point(line.B, isom_position), line.description, line.index);
        }

        /// <summary>
        /// возвращает XY для вида чертежа из XYZ
        /// </summary>
        /// <param name="A">точка</param>
        /// <param name="dimetric_position">O-точка диметрического вида</param>
        /// <returns></returns>
        public GAViewPoint get_dimetric_point(GAPoint A, GAViewPoint dimetric_position)
        {

            return new GAViewPoint(dimetric_position.X - A.X * 0.94 * GAGeometry.Cos(7.1) + A.Y * 0.47 * GAGeometry.Cos(41.25),
                    dimetric_position.Y + A.Z * 0.94 - A.X * 0.94 * GAGeometry.Sin(7.1) - A.Y * 0.47 * GAGeometry.Sin(41.25)
                    , A.description, A.index);
        }

        /// <summary>
        /// возвращает XY для вида чертежа из XYZ
        /// </summary>
        /// <param name="line">линия A(XYZ), B(XYZ)</param>
        /// <param name="dimetric_position">O-точка диметрического вида</param>
        /// <returns>А(XY), B(XY) вида чертежа</returns>
        public GAViewLine get_dimetric_line(GALine line, GAViewPoint dimetric_position)
        {
            return
                new GAViewLine(get_dimetric_point(line.A, dimetric_position), get_dimetric_point(line.B, dimetric_position)
                , line.description, line.index);
        }

    }
}
