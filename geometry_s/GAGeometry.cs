using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace geometry_s
{
    /// <summary>
    /// объекты геометрических тел и некоторые их вычисления
    /// </summary>
    public static class GAGeometry
    {
        /// <summary>
        /// точность вычислений
        /// </summary>
        public static double TOLerance = 1e-10;

        /// <summary>
        /// номера для слов
        /// </summary>
        public enum word
        {
            /// <summary>
            /// empty
            /// </summary>
            пусто,

            /// <summary>
            /// base
            /// </summary>
            основа,

            /// <summary>
            /// top
            /// </summary>
            верх,

            /// <summary>
            /// rib
            /// </summary>
            ребро,

            /// <summary>
            /// intersection
            /// </summary>
            пересечение,

            /// <summary>
            /// cutted
            /// </summary>
            урезана,

            /// <summary>
            /// cutted rib
            /// </summary>
            ребро_урезанное,

            /// <summary>
            /// cutted base
            /// </summary>
            основа_урезанная,

            /// <summary>
            /// cutted top
            /// </summary>
            верх_урезанный,

            /// <summary>
            /// axis
            /// </summary>
            ось
        }

        /// <summary>
        /// проверяет одинаковость координат
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public static bool points_are_equal(GAPoint A, GAPoint B)
        {
            if (A == null && B == null) { return false; }
            if (A.X - B.X <= 1e-10 && A.Y - B.Y <= 1e-10 && A.Z - B.Z <= 1e-10) { return true; }
            else { return false; }
        }

        /// <summary>
        /// пересечение линий
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <param name="inside_only"></param>
        /// <returns></returns>
        public static GAPoint lines_intersection(GALine l1, GALine l2, bool inside_only)
        {

            double t1 = (l1.A.X * l2.A.Y - l1.A.Y * l2.A.X - l1.A.X * l2.B.Y + l1.A.Y * l2.B.X + l2.A.X * l2.B.Y - l2.A.Y * l2.B.X) / (l1.A.X * l2.A.Y - l1.A.Y * l2.A.X - l1.A.X * l2.B.Y + l1.A.Y * l2.B.X - l1.B.X * l2.A.Y + l2.A.X * l1.B.Y + l1.B.X * l2.B.Y - l1.B.Y * l2.B.X);

            //GAPoint p = new GAPoint(
            //    t1 * (l1.B.X - l1.A.X) - l1.A.X,
            //    t1 * (l1.B.Y - l1.A.Y) - l1.A.Y,
            //    t1 * (l1.B.Z - l1.A.Z) - l1.A.Z
            //    );

            GAPoint p = new GAPoint(
                l1.A.X - t1 * l1.A.X + t1 * l1.B.X,
                l1.A.Y - t1 * l1.A.Y + t1 * l1.B.Y,
                l1.A.Z - t1 * l1.A.Z + t1 * l1.B.Z
                );

            if (inside_only && is_point_inside_line(l1, p) && is_point_inside_line(l2, p))
            {
                return p;
            }
            else if (!inside_only && is_points_on_line(l1.A, l1.B, p) && is_points_on_line(l2.A, l2.B, p))
            {
                return p;
            }
            else
            {
                return null;
            }


        }    /////////////////!!!!!!!!!!!!!!!!!!!!

        /// <summary>
        /// пересечение линий
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <param name="inside_only"></param>
        /// <returns></returns>
        public static GAViewPoint lines_intersection(GAViewLine l1, GAViewLine l2, bool inside_only)
        {

            GAViewPoint p = new GAViewPoint(
(((((l2.A.X * (-1)) + l2.B.X) * ((l1.B.X * l1.A.Y * (-1)) + (l1.B.Y * l1.A.X))) + (((l2.B.X * l2.A.Y * (-1)) + (l2.B.Y * l2.A.X)) * (l1.A.X + (l1.B.X * (-1))))) / (((((l2.A.X * (-1)) + l2.B.X) * (l1.A.Y + (l1.B.Y * (-1))) * (-1)) + ((l2.A.Y + (l2.B.Y * (-1))) * ((l1.A.X * (-1)) + l1.B.X))))),

((((l2.A.Y + (l2.B.Y * (-1))) * ((l1.B.X * l1.A.Y * (-1)) + (l1.B.Y * l1.A.X)) * (-1)) + (((l2.B.X * l2.A.Y * (-1)) + (l2.B.Y * l2.A.X)) * (l1.A.Y + (l1.B.Y * (-1))))) / (((((l2.A.X * (-1)) + l2.B.X) * (l1.A.Y + (l1.B.Y * (-1))) * (-1)) + ((l2.A.Y + (l2.B.Y * (-1))) * ((l1.A.X * (-1)) + l1.B.X)))))
                );

            if (inside_only && is_point_inside_line(l1, p) && is_point_inside_line(l2, p))
            {
                return p;
            }
            else if (!inside_only && is_points_on_line(l1.A, l1.B, p) && is_points_on_line(l2.A, l2.B, p))
            {
                return p;
            }
            else
            {
                return null;
            }


        }

        /// <summary>
        /// сортирует точки по наименьшему удалению, для контура
        /// </summary>
        /// <param name="points_">точки</param>
        /// <param name="index_">true - усли есть точки с индексом -1, то они идут первыми</param>
        /// <returns></returns>
        public static void sort_by_distance(List<GAPoint> points_, bool index_ = true)
        {
            if (points_.Count <= 2) {  return; }
            List<GAPoint> sorted = new List<GAPoint>();


            if (points_[0].index == -1 && points_[1].index == -1)
            {
                if (index_)
                {
                    sorted.Add(points_[0]);
                    points_.RemoveAt(0);
                    sorted.Add(points_[0]);
                    points_.RemoveAt(0);
                }
            }
            else
            {
                sorted.Add(points_[0]);
                points_.RemoveAt(0);
            }
            
            
            do
            {
                double dist1 = distance_points(sorted[sorted.Count-1], points_[0]);
                int ind = 0;
                for (int i = 1; i < points_.Count; i++)
                {
                    //if (points_.Count > i+1 && points_[i].index == -1 && sorted[sorted.Count-1].index == -1)
                    //{
                    //    sorted.Add(points_[i]);
                    //    points_.RemoveAt(i);
                    //    ind = i + 1;
                    //    break;
                    //}
                    double dist2 = distance_points(sorted[sorted.Count-1], points_[i]);
                    if (dist2 < dist1) { ind = i; dist1 = dist2; }
                }
                sorted.Add(points_[ind]);
                points_.RemoveAt(ind);
            } while (points_.Count > 0);

            points_.AddRange(sorted);
        }

        /// <summary>
        /// сортирует точки по наименьшему удалению, для контура
        /// </summary>
        /// <param name="points_">точки</param>
        /// <param name="index_">true - усли есть точки с индексом -1, то они идут первыми</param>
        /// <returns></returns>
        public static void sort_by_distance(List<GAViewPoint> points_, bool index_ = true)
        {
            if (points_.Count <= 2) { return; }
            List<GAViewPoint> sorted = new List<GAViewPoint>();

            if (points_[0].index == -1 && points_[1].index == -1)
            {
                if (index_)
                {
                    sorted.Add(points_[0]);
                    points_.RemoveAt(0);
                    sorted.Add(points_[0]);
                    points_.RemoveAt(0);
                }
            }
            else
            {
                sorted.Add(points_[0]);
                points_.RemoveAt(0);
            }


            do
            {
                double dist1 = distance_points(sorted[sorted.Count - 1], points_[0]);
                int ind = 0;
                for (int i = 1; i < points_.Count; i++)
                {
                    double dist2 = distance_points(sorted[sorted.Count - 1], points_[i]);
                    if (dist2 < dist1) { ind = i; dist1 = dist2; }
                }
                sorted.Add(points_[ind]);
                points_.RemoveAt(ind);
            } while (points_.Count > 0);

            points_.AddRange(sorted);
        }

        /// <summary>
        /// сортирует по кунтуру, точки должны быть в одной плоскости
        /// </summary>
        /// <param name="points_"></param>
        /// <returns></returns>
        public static List<GAPoint> sort_by_contour(List<GAPoint> points_)
        {
            List<GALine> lines = new List<GALine>();
            for (int j = 0; j < points_.Count; j++)
            {
                for (int i = j + 1; i < points_.Count; i++)
                {
                    if (j == i) { continue; }
                    lines.Add(new GALine(points_[i], points_[j]));
                }
            }

            List<int> remove_ind = new List<int>();

            for (int j = 0; j < lines.Count; j++)
            {
                if (remove_ind.Contains(j)) { continue; }
                for (int i = j + 1; i < lines.Count; i++)
                {
                    if (remove_ind.Contains(i)) { continue; }

                    GAPoint p = lines_intersection(lines[j], lines[i], true);

                    if (p == null) { continue; }

                    if ((Math.Abs(p.X - lines[i].A.X) < TOLerance && Math.Abs(p.Y - lines[i].A.Y) < TOLerance && Math.Abs(p.Z - lines[i].A.Z) < TOLerance) ||
                        (Math.Abs(p.X - lines[i].B.X) < TOLerance && Math.Abs(p.Y - lines[i].B.Y) < TOLerance && Math.Abs(p.Z - lines[i].B.Z) < TOLerance) ||
                        (Math.Abs(p.X - lines[j].A.X) < TOLerance && Math.Abs(p.Y - lines[j].A.Y) < TOLerance && Math.Abs(p.Z - lines[j].A.Z) < TOLerance) ||
                        (Math.Abs(p.X - lines[j].B.X) < TOLerance && Math.Abs(p.Y - lines[j].B.Y) < TOLerance && Math.Abs(p.Z - lines[j].B.Z) < TOLerance))
                    { continue; }

                    if (!remove_ind.Contains(j)) { remove_ind.Add(j); }
                    if (!remove_ind.Contains(i)) { remove_ind.Add(i); }

                }
            }

            remove_ind.Sort();

            for (int i = remove_ind.Count - 1; i >= 0; i--)
            {
                lines.RemoveAt(remove_ind[i]);
            }

            List<GAPoint> points = new List<GAPoint>();
            while (lines.Count > 0)
            {
                if (points.Count == 0)
                {
                    points.Add(lines[0].A);
                    lines.RemoveAt(0);
                }

                foreach (GALine l in lines)
                {
                    if (Math.Abs(points[points.Count - 1].X - l.A.X) < TOLerance && Math.Abs(points[points.Count - 1].Y - l.A.Y) < TOLerance && Math.Abs(points[points.Count - 1].Z - l.A.Z) < TOLerance)
                    {
                        points.Add(l.B);
                        lines.Remove(l);
                        break;
                    }
                    else if(Math.Abs(points[points.Count - 1].X - l.B.X) < TOLerance && Math.Abs(points[points.Count - 1].Y - l.B.Y) < TOLerance && Math.Abs(points[points.Count - 1].Z - l.B.Z) < TOLerance)
                    {
                        points.Add(l.A);
                        lines.Remove(l);
                        break;
                    }else
                    {
                        return null;
                    }
                }
            }

            for (int i = points_.Count - 1; i >= 0; i--)
            {
                points_.RemoveAt(i);
            }

            foreach (GAPoint p in points)
            {
                points_.Add(p);
            }

            return points;
        }


        public static List<GAViewPoint> sort_by_contour(List<GAViewPoint> points_)
        {
            List<GAViewLine> lines = new List<GAViewLine>();
            for(int j=0; j < points_.Count; j++)
            {
                for (int i = j+1; i < points_.Count; i++)
                {
                    if (j == i) { continue; }
                    lines.Add(new GAViewLine(points_[i], points_[j]));
                }
            }

            List<int> remove_ind = new List<int>();

            for (int j = 0; j < lines.Count; j++)
            {
                if (remove_ind.Contains(j)) { continue; }
                for (int i = j+1; i < lines.Count ; i++)
                {
                    if (remove_ind.Contains(i)) { continue; }

                    GAViewPoint p = lines_intersection(lines[j], lines[i], true);

                    if ( p == null) { continue; }

                    if ((Math.Abs(p.X - lines[i].A.X) < TOLerance && Math.Abs(p.Y - lines[i].A.Y) < TOLerance) ||
                        (Math.Abs(p.X - lines[i].B.X) < TOLerance && Math.Abs(p.Y - lines[i].B.Y) < TOLerance) ||
                        (Math.Abs(p.X - lines[j].A.X) < TOLerance && Math.Abs(p.Y - lines[j].A.Y) < TOLerance) ||
                        (Math.Abs(p.X - lines[j].B.X) < TOLerance && Math.Abs(p.Y - lines[j].B.Y) < TOLerance))
                    { continue; }

                    if (!remove_ind.Contains(j)) { remove_ind.Add(j); }
                    remove_ind.Add(i);

                }
            }

            remove_ind.Sort();

            for(int i = remove_ind.Count-1; i >= 0 ; i--)
            {
                lines.RemoveAt(remove_ind[i]);
            }

            List<GAViewPoint> points = new List<GAViewPoint>();
            while(lines.Count > 0)
            {
                if (points.Count == 0)
                {
                    points.Add(lines[0].A);
                    lines.RemoveAt(0);
                }

                foreach(GAViewLine l in lines)
                {
                    if (Math.Abs(points[points.Count-1].X - l.A.X) < TOLerance && Math.Abs(points[points.Count - 1].Y - l.A.Y) < TOLerance)
                    {
                        points.Add(l.B);
                        lines.Remove(l);
                        break;
                    }else if (Math.Abs(points[points.Count - 1].X - l.B.X) < TOLerance && Math.Abs(points[points.Count - 1].Y - l.B.Y) < TOLerance)
                    {
                        points.Add(l.A);
                        lines.Remove(l);
                        break;
                    }
                }
            }

            for(int i = points_.Count - 1; i >= 0; i--)
            {
                points_.RemoveAt(i);
            }

            foreach(GAViewPoint p in points)
            {
                points_.Add(p);
            }

            return points;
        }

        /// <summary>
        /// центр окружности по трем точкам
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="C"></param>
        /// <returns></returns>
        public static GAViewPoint centrByPoint(GAViewPoint A, GAViewPoint B, GAViewPoint C)
        {
            double
            x12 = A.X - B.X,
                x23 = B.X - C.X,
                x31 = C.X - A.X,
                y12 = A.Y - B.Y,
                y23 = B.Y - C.Y,
                y31 = C.Y - A.Y,
                z1 = Math.Pow(A.X, 2) + Math.Pow(A.Y, 2),
                z2 = Math.Pow(B.X, 2) + Math.Pow(B.Y, 2),
                z3 = Math.Pow(C.X, 2) + Math.Pow(C.Y, 2),

                zx = y12 * z3 + y23 * z1 + y31 * z2,
                zy = x12 * z3 + x23 * z1 + x31 * z2,
                z = x12 * y31 - y12 * x31;
            return new GAViewPoint(-zx / (2 * z), zy / (2 * z));
        }

        /// <summary>
        /// зеркальное отражение по вертикали или по горизонтали
        /// </summary>
        /// <param name="points_">точки</param>
        /// <param name="v">0-Х, 1-Y</param>
        /// <returns></returns>
        public static void mirror(List<GAViewPoint> points_, int v = 0)
        {
            if (points_.Count == 0) { return; }

            double middle_x = 0;
            double middle_y = 0;
            switch(v)
            {
                case 0: middle_x = points_.Average(r => r.X); break;
                case 1: middle_y = points_.Average(r => r.Y); break;
            }

            foreach (GAViewPoint p in points_)
            {
                switch(v)
                {
                    case 0: p.X = middle_x + (middle_x - p.X); break;
                    case 1: p.Y = middle_y + (middle_y - p.Y); break;
                }
            }
        }

        #region math Calculations

        /// <summary>
        /// summ of true // сумма значений true
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static int CountTrue_SummOfTrue(params bool[] args)
        {
            return args.Count(t => t);
        }

        /// <summary>
        /// cosinus DEG
        /// </summary>
        /// <param name="alpha">DEG</param>
        /// <returns></returns>
        public static double Cos(double alpha)
        {
            return Math.Cos(alpha * (Math.PI / 180));
        }

        /// <summary>
        /// sinus DEG
        /// </summary>
        /// <param name="alpha">DEG</param>
        /// <returns></returns>
        public static double Sin(double alpha)
        {
            return Math.Sin(alpha * (Math.PI / 180));
        }

        /// <summary>
        /// tangens DEG
        /// </summary>
        /// <param name="alpha">DEG</param>
        /// <returns></returns>
        public static double Tan(double alpha)
        {
            return Math.Tan(alpha * (Math.PI / 180));
        }

        /// <summary>
        /// arcsinus DEG
        /// </summary>
        /// <param name="n">DEG</param>
        /// <returns>DEG angle</returns>
        public static double ASin(double n)
        {
            return get_degrees(Math.Asin(n));
        }

        /// <summary>
        /// arccos DEG
        /// </summary>
        /// <param name="n">DEG</param>
        /// <returns>DEG angle</returns>
        public static double ACos(double n)
        {
            return get_degrees(Math.Acos(n));
        }

        /// <summary>
        /// получить процент, точка на шкале в процентном соотношении
        /// </summary>
        /// <param name="start">начало шкалы</param>
        /// <param name="end">конец шкалы</param>
        /// <param name="number">точка на шкале</param>
        /// <returns>(0...1 если точка в шкале) точка на шкале в процентном соотношении</returns>
        public static double percent(double start, double end, double number)
        {
            return ((number - start) / (end - start));

            //return (((end - start) / 100) * (number - start));
        }

        /// <summary>
        /// получить число по проценту, точка на шкале в процентном соотношении
        /// </summary>
        /// <param name="start">начало шкалы</param>
        /// <param name="end">конец шкалы</param>
        /// <param name="percent_0_1">процент на шкале</param>
        /// <returns>(start...end если процент от 0 до 1 в шкале) точка на шкале в зависимости от указанного процента</returns>
        public static double percentPredict(double start, double end, double percent_0_1)
        {
            return (start + (end - start) * percent_0_1);

            //return (((end - start) / 100) * (number - start));
        }

        /// <summary>
        /// multiply every each item
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static double[] Multiply(this double[] arr, double val)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = arr[i] * val;
            }
            return arr;
        }

        /// <summary>
        /// добавляет 0 перед цифрой
        /// </summary>
        /// <param name="number"></param>
        /// <param name="zeros">Количество цифр</param>
        /// <returns></returns>
        public static string leadingZero(this int number, int zeros = 0)
        {
            string counter_str = "";
            int leading_zero = zeros - number.ToString().Length;
            if (leading_zero > 0)
            {
                for (int i = 0; i < leading_zero; i++) { counter_str = counter_str + "0"; }
            }
            counter_str = counter_str + number.ToString();
            return counter_str;
        }

        #endregion

        #region BigInteger calculation

        /// <summary>
        /// square root of BigInteger value, 
        /// </summary>
        /// <param name="n">number to find square root</param>
        /// <returns></returns>
        public static BigInteger Sqrt_Big(BigInteger n)
        {
            BigInteger step = BigInteger.Divide(n, 2);
            BigInteger root = BigInteger.Subtract(n, step);

            // While the square root is not found 
            Boolean found = false;
            while (!found)
            {

                BigInteger mult = BigInteger.Multiply(root, root);
                int a = BigInteger.Compare(mult, n);

                if (a == 0)
                {
                    // If n is a perfect square 
                    return root;
                }
                else if (a > 0)
                {
                    root = BigInteger.Subtract(root, step);
                    if (BigInteger.Compare(step, 1) == 0) { return root; }
                }
                else if (a < 0)
                {
                    root = BigInteger.Add(root, step);
                }

                if (BigInteger.Compare(step, 2) <= 0)
                {
                    step = 1;
                }
                else
                {
                    step = BigInteger.Divide(step, 2);
                }
            }

            return 0;
        }

        /// <summary>
        /// summ all numbers of array
        /// </summary>
        /// <param name="bigIntegerNumbers"></param>
        /// <returns></returns>
        public static BigInteger Summ_Big(params BigInteger[] bigIntegerNumbers)
        {
            BigInteger result = 0;
            foreach (BigInteger bi in bigIntegerNumbers)
            {
                result = BigInteger.Add(result, bi);
            }
            return result;
        }

        /// <summary>
        /// Multiply all numbers of array
        /// </summary>
        /// <param name="bigIntegerNumbers"></param>
        /// <returns></returns>
        public static BigInteger Multiply_Big(params BigInteger[] bigIntegerNumbers)
        {
            BigInteger result = 1;
            foreach (BigInteger bi in bigIntegerNumbers)
            {
                result = BigInteger.Multiply(result, bi);
            }
            return result;
        }

        #endregion

        #region get

        /// <summary>
        /// даем градусы, получаем радианы
        /// </summary>
        /// <param name="degrees">градусы</param>
        /// <returns>возвращает радианы</returns>
        public static double get_radian(double degrees)
        {

            return Math.Round(degrees * Math.PI / 180.0, 10);
        }

        /// <summary>
        /// даем радианы, получаем градусы
        /// </summary>
        /// <returns></returns>
        public static double get_degrees(double radian)
        {
            return radian * (180.0 / Math.PI);
        }

        /// <summary>
        /// угол между линиями на плоскости от 0 до 90
        /// </summary>
        /// <param name="l1">линия 1</param>
        /// <param name="l2">линия 2</param>
        /// <returns></returns>
        public static double get_angle(GAViewLine l1, GAViewLine l2)
        {
            //if (l1.A == null || l1.B == null || l2.A == null || l2.B == null) { return null; }
            double a1 = get_degrees( Math.Acos(Math.Abs(l1.A.X - l1.B.X) / l1.length));
            double a2 = get_degrees( Math.Acos(Math.Abs(l2.A.X - l2.B.X) / l2.length));
            
            return Math.Abs(a1 - a2);
        }

        /// <summary>
        /// угол между плоскостями
        /// </summary>
        /// <param name="p1">плоскость 1</param>
        /// <param name="p2">плоскость 2</param>
        /// <returns></returns>
        public static double get_angle(GAPlane p1, GAPlane p2)
        {
            double d = (p1.A * p2.A + p1.B * p2.B + p1.C * p2.C);
            double e1 = (double)Math.Sqrt(p1.A * p1.A + p1.B * p1.B + p1.C * p1.C);
            double e2 = (double)Math.Sqrt(p2.A * p2.A + p2.B * p2.B + p2.C * p2.C);
            d = d / (e1 * e2);
            if(d > 1) { d = 1; }
            double A = (180 / Math.PI) * (double)(Math.Acos(d));
            return A;
        }

        /// <summary>
        /// угол между двумя линиями
        /// </summary>
        /// <param name="line1"></param>
        /// <param name="line2"></param>
        /// <returns></returns>
        public static double get_angle(GALine line1, GALine line2)
        {
            return get_angle(new GAVector(line1.A, line1.B), new GAVector(line2.A, line2.B));
            //angle = arccos[(xa * xb + ya * yb + za * zb) / (√(xa2 + ya2 + za2) * √(xb2 + yb2 + zb2))]
        }

        /// <summary>
        /// угол между двумя векторами
        /// </summary>
        /// <param name="vector1"></param>
        /// <param name="vector2"></param>
        /// <returns></returns>
        public static double get_angle(GAVector vector1, GAVector vector2)
        {
            double angle = ACos(
                (vector1.X * vector2.X + vector1.Y * vector2.Y + vector1.Z * vector2.Z)
                /
                (
                Math.Pow(Math.Pow(vector1.X, 2) + Math.Pow(vector1.Y, 2) + Math.Pow(vector1.Z, 2), 0.5) *
                Math.Pow(Math.Pow(vector2.X, 2) + Math.Pow(vector2.Y, 2) + Math.Pow(vector2.Z, 2), 0.5)
                ));

            return angle;
        }

        /// <summary>
        /// ищет линию по индексу, если нету возвращает Null
        /// </summary>
        /// <param name="lines_"></param>
        /// <param name="index_"></param>
        /// <returns></returns>
        public static GALine get_lineByLineIndex(List<GALine> lines_, int index_)
        {
            foreach(GALine l in lines_)
            {
                if(l.index == index_) { return l; }
            }
            return null;
        }

        /// <summary>
        /// ищет линию по индексу, если нету возвращает Null
        /// </summary>
        /// <param name="lines_"></param>
        /// <param name="index_"></param>
        /// <returns></returns>
        public static GAViewLine get_lineByLineIndex(List<GAViewLine> lines_, int index_)
        {
            foreach (GAViewLine l in lines_)
            {
                if (l.index == index_) { return l; }
            }
            return null;
        }

        /// <summary>
        /// ищет точку по индексу, если нету возвращает Null
        /// </summary>
        /// <param name="points_"></param>
        /// <param name="index_"></param>
        /// <returns></returns>
        public static GAPoint get_PointByIndex(List<GAPoint> points_, int index_)
        {
            foreach (GAPoint p in points_)
            {
                if (p.index == index_) { return p; }
            }
            return null;
        }

        /// <summary>
        /// ищет точку по индексу, если нету возвращает Null
        /// </summary>
        /// <param name="points_"></param>
        /// <param name="index_"></param>
        /// <returns></returns>
        public static GAViewPoint get_PointByIndex(List<GAViewPoint> points_, int index_)
        {
            foreach (GAViewPoint p in points_)
            {
                if (p.index == index_) { return p; }
            }
            return null;
        }

        /// <summary>
        /// выбирает точки не повторяющиеся по координатам
        /// </summary>
        /// <param name="lines_">линии</param>
        /// <returns>не повторяющиеся точки линий</returns>
        public static List<GAViewPoint> get_PointsOffLines(List<GAViewLine> lines_)
        {
            List<GAViewPoint> points = new List<GAViewPoint>();
            for (int i = 0; i < lines_.Count; i++)
            {

                bool can_add_a = true;
                bool can_add_b = true;
                foreach (GAViewPoint pp in points)
                {
                    if (pp.X == lines_[i].A.X &&
                        pp.Y == lines_[i].A.Y)
                    {
                        can_add_a = false;
                    }

                    if (pp.X == lines_[i].B.X &&
                        pp.Y == lines_[i].B.Y)
                    {
                        can_add_b = false;
                    }

                    if (!can_add_a && !can_add_b) { break; }
                }
                if (can_add_a) { points.Add(lines_[i].A); }
                if (can_add_b) { points.Add(lines_[i].B); }
            }
            return points;
        }

        /// <summary>
        /// triangulate convex hull - алмаз из точек
        /// </summary>
        public static List<GATriangle> get_ConvexContour(List<GAPoint> gAPoints)
        {

            //2020-08-21 - что бы не херить входной List
            List<GAPoint> pointsThere = new List<GAPoint>();
            foreach(GAPoint p in gAPoints)
            {
                pointsThere.Add(p);
            }

            #region находим случайные четыре плоскости (треугольники тетраэдра) относительно которых разбиваем пространство и ищем наиболее удаленные точки

            GAPoint simplex0 = pointsThere[0];
            GAPoint simplex1 = null;
            GAPoint simplex2 = null;
            GAPoint simplex3 = null;

            for (int i = 1; i < pointsThere.Count; i++)
            {
                simplex1 = pointsThere[i];
                if (!is_the_same_coordinates(simplex0, simplex1))
                {
                    for (int j = i + 1; j < pointsThere.Count; j++)
                    {
                        simplex2 = pointsThere[j];
                        if (!is_points_on_line(simplex0, simplex1, simplex2))
                        {
                            GAPlane first_plane = new GAPlane(simplex0, simplex1, simplex2);
                            double dist_max = 0;
                            for (int d = j + 1; d < pointsThere.Count; d++)
                            {
                                double dist = distance_plane_point(pointsThere[d], first_plane);
                                if (dist > dist_max) { dist_max = dist; simplex3 = pointsThere[d]; }
                            }
                            if(dist_max < TOLerance) { dist_max = 0; return null; }
                        }
                        if (simplex3 != null) { break; }
                    }
                }
                if (simplex3 != null) { break; }
            }

            if (simplex2 == null) { return null; }
            if (simplex3 == null) { return null; }

            //четыре плоскости (треугольники тетраэдра) относительно которых разбиваем пространство и ищем наиболее удаленные точки
            GATetrahedron ga_tetrahedron = new GATetrahedron(simplex0, simplex1, simplex2, simplex3);
            #endregion

            //---------------продолжить 
            //C:\Users\User\Desktop\Dimension_test\models\216247-P_004.ipt

            //список треугольников (плоскостей)
            List<GATriangle> hull_faces = new List<GATriangle>();
            hull_faces.AddRange(ga_tetrahedron.triangles);

            //find furthest point, outside plane set 
            for (int i_ht = 0; i_ht < hull_faces.Count; i_ht++)
            {
                #region для каждой плоскости

                #region exclude point if exist, or if in triangle
                for (int i = 0; i < pointsThere.Count; i++)
                {
                    GAPoint p = pointsThere[i];
                    bool point_exists = false;
                    foreach (GATriangle tr_ex in hull_faces)
                    {
                        if (tr_ex.points.Contains(p)) { point_exists = true; break; }
                        if (is_point_inside_triangle_bool(p, tr_ex.points[0], tr_ex.points[1], tr_ex.points[2])) { point_exists = true; break; };
                    }
                    if (point_exists) { pointsThere.RemoveAt(i); i--; continue; }
                }
                #endregion

                // outside point set
                GATriangle tr = hull_faces[i_ht];
                List<GAPoint> outside_set = new List<GAPoint>();
                for (int i = 0; i < pointsThere.Count; i++)
                {
                    GAPoint p = pointsThere[i];

                    GALine line = new GALine(p, ga_tetrahedron.center_weight);
                    if (plane_line_intersection(tr.plane, line, true, 1e-5) != null || is_point_on_plane(p, tr.points[0], tr.points[1], tr.points[2])) { outside_set.Add(p); }
                }
                if (outside_set.Count == 0)
                {
                    //если нету точек за плоскостью (треугольником), то добавим плоскость (треугольник) в hull
                    if (!hull_faces.Contains(tr))
                    {
                        hull_faces.Add(tr);
                    }
                    //hull_faces_onStart.Add(tr);
                    continue;
                }

                GAPoint max_dist = get_MaxDistancePointFromPlane(tr.plane, outside_set);
                if (max_dist == null) { continue; }
                if (max_dist.index == 0) { continue; }

                //clear base pointsThere set
                pointsThere.RemoveAt(pointsThere.IndexOf(max_dist));

                GATetrahedron ttr = new GATetrahedron(tr.points[0], tr.points[1], tr.points[2], max_dist);
                for (int i = 0; i < pointsThere.Count; i++)
                {
                    if (is_point_inside_Tetrahedron(ttr, pointsThere[i]))
                    {
                        pointsThere.RemoveAt(i);
                        i--;
                    }
                }

                // determine invisible planes
                List<GATriangle> invisible_faces = new List<GATriangle>();
                for (int h_tr = 0; h_tr < hull_faces.Count; h_tr++)
                {
                    GATriangle ga_face = hull_faces[h_tr];
                    GALine line_to_center = new GALine(max_dist, ga_face.center_point);

                    int intersections = 0;

                    for (int h_tr2 = 0; h_tr2 < hull_faces.Count; h_tr2++)
                    {
                        if (hull_faces[h_tr2] == ga_face) { continue; }

                        GAPoint ga_intersect_point_0 = plane_line_intersection(hull_faces[h_tr2].plane, line_to_center, true);

                        if (ga_intersect_point_0 == null) { continue; }

                        bool max_dist_point_on_plane = is_point_on_plane(max_dist, ga_face.points[0], ga_face.points[1], ga_face.points[2], 1e-4);

                        bool inside_triangle = is_point_inside_triangle_bool(ga_intersect_point_0, hull_faces[h_tr2].points[0], hull_faces[h_tr2].points[1], hull_faces[h_tr2].points[2]);

                        if (!inside_triangle) { continue; }

                        if (max_dist_point_on_plane) { break; }

                        intersections++;

                        break;
                    }

                    if (intersections == 0) { invisible_faces.Add(ga_face); }
                }

                // find boundary of invisible planes
                List<GAPoint> invisible_boundary = new List<GAPoint>();
                List<GALine> invisible_boundary_lines = new List<GALine>();

                foreach (GATriangle tr_inv in invisible_faces)
                {
                    invisible_boundary_lines.AddRange(tr_inv.lines);
                    int index_ = hull_faces.IndexOf(tr_inv);
                    hull_faces.RemoveAt(index_);
                    if (index_ <= i_ht) { i_ht--; }
                }

                // delete lines inside boundary space (inside visible faces)
                for (int i = 0; i < invisible_boundary_lines.Count; i++)
                {
                    for (int j = i + 1; j < invisible_boundary_lines.Count; j++)
                    {
                        bool the_same0 = (is_the_same_coordinates(invisible_boundary_lines[i].A, invisible_boundary_lines[j].A)
                            && is_the_same_coordinates(invisible_boundary_lines[i].B, invisible_boundary_lines[j].B));
                        bool the_same1 = (is_the_same_coordinates(invisible_boundary_lines[i].A, invisible_boundary_lines[j].B)
                            && is_the_same_coordinates(invisible_boundary_lines[i].B, invisible_boundary_lines[j].A));
                        if (the_same0 || the_same1)
                        {
                            invisible_boundary_lines.RemoveAt(j);
                            invisible_boundary_lines.RemoveAt(i);
                            i--;
                            break;
                        }
                    }
                }

                // new planes from max to boundary of invisible
                foreach (GALine line_inv in invisible_boundary_lines)
                {
                    //GAFaceFlatSimple triangle_new = new GAFaceFlatSimple(new List<GAPoint> { line_inv.A, line_inv.B, max_dist });
                    GATriangle triangle_new = new GATriangle( line_inv.A, line_inv.B, max_dist );
                    hull_faces.Add(triangle_new);

                    // exclude point if exist
                    int index_ = pointsThere.IndexOf(line_inv.A);
                    if (index_ >= 0) { pointsThere.RemoveAt(pointsThere.IndexOf(line_inv.A)); }

                }

                #endregion //-------------для каждой плоскости
            }

            //foreach(GATriangle gtr in hull_faces_onStart) { hull_faces.Add(gtr); }

            return hull_faces;
        }

        /// <summary>
        /// make flat surfaces from triangles - все треугольники в плоскости, если треугольники в одной плоскости то объединяем их
        /// </summary>
        /// <param name="triangles"></param>
        /// <returns></returns>
        public static List<GAFaceFlatSimple> get_ConvexContourTrianglesToFaces (List<GATriangle> triangles)
        {
            // triangles to faces, reduce planes amount
            List<GAFaceFlatSimple> ga_faces = new List<GAFaceFlatSimple>();
            List<int> index_to_remove = new List<int>();

            for (int i = 0; i < triangles.Count; i++)
            {
                #region find triangles on the same plane
                if (index_to_remove.Contains(i)) { continue; }

                List<GAPoint> contour_points = new List<GAPoint>();
                List<GATriangle> ga_triangles_to_combine = new List<GATriangle>();
                GATriangle tr = triangles[i];
                bool the_same_plane = false;
                for (int j = i + 1; j < triangles.Count; j++)
                {
                    GATriangle tr2 = triangles[j];
                    int on_plane = 0;
                    if (is_point_on_plane(tr.points[0], tr.points[1], tr.points[2], tr2.points[0])) on_plane++;
                    if (is_point_on_plane(tr.points[0], tr.points[1], tr.points[2], tr2.points[1])) on_plane++;
                    if (is_point_on_plane(tr.points[0], tr.points[1], tr.points[2], tr2.points[2])) on_plane++;

                    if (on_plane == 3)
                    {
                        contour_points.AddRange(tr2.points);
                        ga_triangles_to_combine.Add(tr2);
                        index_to_remove.Add(j);
                        the_same_plane = true;
                    }
                }


                if (the_same_plane)
                {
                    contour_points.AddRange(tr.points);
                    //ga_triangles_to_combine.Add(tr);
                    index_to_remove.Add(i);

                }
                else
                {
                    continue;
                }
                #endregion

                // combine triangles to face (contour of triangles)
                #region  get all lines and remove coincident lines
                //List<GALine> ga_lines = new List<GALine>();
                //for (int i_tr = 0; i_tr < ga_triangles_to_combine.Count; i_tr++)
                //{
                //    ga_lines.AddRange(ga_triangles_to_combine[i_tr].lines);
                //}

                //for (int i_l1 = 0; i_l1 < ga_lines.Count; i_l1++)
                //{
                //    for (int i_l2 = i_l1 + 1; i_l2 < ga_lines.Count; i_l2++)
                //    {
                //        bool c1 = is_the_same_coordinates(ga_lines[i_l1].A, ga_lines[i_l2].A);
                //        bool c2 = is_the_same_coordinates(ga_lines[i_l1].B, ga_lines[i_l2].B);
                //        bool c3 = is_the_same_coordinates(ga_lines[i_l1].A, ga_lines[i_l2].B);
                //        bool c4 = is_the_same_coordinates(ga_lines[i_l1].B, ga_lines[i_l2].A);

                //        if ((c1 && c2) || (c3 && c4))
                //        {
                //            ga_lines.RemoveAt(i_l2);
                //            ga_lines.RemoveAt(i_l1);
                //            i_l1--;
                //            break;
                //        }
                //    }
                //}
                #endregion

                #region get points of lines and set it into right order //right course
                //List<GAPoint> ga_facePoints = new List<GAPoint>();
                //ga_facePoints.Add(ga_lines[0].A);
                //ga_facePoints.Add(ga_lines[0].B);
                //ga_lines.RemoveAt(0);
                //do
                //{
                //    for (int i_l1 = 0; i_l1 < ga_lines.Count; i_l1++)
                //    {
                //        GAPoint last_point = ga_facePoints[ga_facePoints.Count - 1];
                //        if (is_the_same_coordinates(ga_lines[i_l1].A, last_point))
                //        {
                //            if (!is_the_same_coordinates(ga_lines[i_l1].B, ga_facePoints[0]))
                //            {
                //                ga_facePoints.Add(ga_lines[i_l1].B);
                //            }
                //            ga_lines.RemoveAt(i_l1);
                //            i_l1--;
                //        }
                //        else if (is_the_same_coordinates(ga_lines[i_l1].B, last_point))
                //        {
                //            if (!is_the_same_coordinates(ga_lines[i_l1].A, ga_facePoints[0]))
                //            {
                //                ga_facePoints.Add(ga_lines[i_l1].A);
                //            }
                //            ga_lines.RemoveAt(i_l1);
                //            i_l1--;
                //        }
                //    }
                //} while (ga_lines.Count > 0);
                #endregion

                // create new face from pointsThere
                //GAFaceFlatSimple face_ = new GAFaceFlatSimple(ga_facePoints);
                GAFaceFlatSimple face_ = new GAFaceFlatSimple(get_ConvexContour2d(contour_points));
                ga_faces.Add(face_);
            }

            for(int i = 0; i < triangles.Count; i++)
            {
                if (index_to_remove.Contains(i)) { continue; }
                GAFaceFlatSimple fs = new GAFaceFlatSimple(triangles[i].points);
                ga_faces.Add(fs);
            }
            return ga_faces;
        }

        /// <summary>
        /// Возвращает точки плоского контура 3d - брутфорс метод
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static List<GAPoint> get_ConvexContour_old_TrianglesMethod_(List<GAPoint> points)
        {
            if (points.Count <= 3) { return null; }
            List<GAPoint> exclude = new List<GAPoint>();

            foreach (GAPoint p1 in points)
            {
                if (exclude.Contains(p1)) { continue; }
                foreach (GAPoint p2 in points)
                {
                    if (p2 == p1) { continue; }
                    if (exclude.Contains(p2)) { continue; }
                    foreach (GAPoint p3 in points)
                    {
                        if (p3 == p1) { continue; }
                        if (p3 == p2) { continue; }
                        if (exclude.Contains(p3)) { continue; }
                        foreach (GAPoint p4 in points)
                        {
                            if (p4 == p1) { continue; }
                            if (p4 == p2) { continue; }
                            if (p4 == p3) { continue; }
                            if (exclude.Contains(p4)) { continue; }

                            int inside = is_point_inside_triangle(p4, p1, p2, p3);
                            if(inside == 1 || inside == 2) { exclude.Add(p4); }
                        }
                    }
                }
            }

            List<GAPoint> contour = new List<GAPoint>();
            foreach(GAPoint pp in points)
            {
                if (exclude.Contains(pp)) { continue; }
                contour.Add(pp);
            }
            return contour;

        }

        /// <summary>
        /// Возвращает точки контура
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static List<GAViewPoint> get_ConvexContour2d(List<GAViewPoint> points)
        {
            //clear pointsThere if the same coordinate
            for(int i=0; i< points.Count; i++)
            {
                for (int j = i+1; j < points.Count; j++)
                {
                    if(is_the_same_coordinates(points[i], points[j], TOLerance))
                    {
                        points.RemoveAt(j);
                        j--;
                    }
                }
            }

            if (points.Count <= 3) { return null; }

            GAViewPoint pXMax = new GAViewPoint();
            GAViewPoint pXMin = new GAViewPoint();

            GAViewPoint[] maxMin = points.MaxX_MinX_MaxY_MinY();
            pXMax = maxMin[0];
            pXMin = maxMin[1];

            double polarAngleFirstLine = get_PolarAngle(pXMax, pXMin);

            List<GAViewPoint> S_top = new List<GAViewPoint>();
            List<GAViewPoint> S_down = new List<GAViewPoint>();

            // divide pointsThere in two subsets
            foreach (GAViewPoint p in points)
            {
                double polarAngle = get_PolarAngle(pXMax, p);
                if (polarAngle >= polarAngleFirstLine) { S_top.Add(p); }else{ S_down.Add(p); }
            }

            List<GAViewPoint> convex_hull = new List<GAViewPoint>();
            convex_hull.AddRange(findHull(S_top, pXMin, pXMax));
            convex_hull.AddRange(findHull(S_down, pXMin, pXMax));
            convex_hull.Add(pXMax);
            convex_hull.Add(pXMin);


            List<GAViewPoint> convexHullStack = new List<GAViewPoint>();

            //convexHullStack.Add(convex_hull.MinimumByY());

            // sort by direction
            double minY = convex_hull[0].Y;
            int min_y_ind = 0;
            for (int i = 0; i < convex_hull.Count; i++)
            {
                if (minY > convex_hull[i].Y) { minY = convex_hull[i].Y; min_y_ind = i; }
            }
            convexHullStack.Add(convex_hull[min_y_ind]);
            convex_hull.RemoveAt(min_y_ind);
            while (convex_hull.Count > 0)
            {
                GAViewPoint p = convexHullStack[convexHullStack.Count - 1];
                GAViewPoint next = null;
                double minAngle = 0;
                int min_index = 0;
                for (int i = 0; i < convex_hull.Count; i++)
                {
                    GAViewPoint pp = convex_hull[i];
                    if (next == null)
                    {
                        next = pp;
                        minAngle = get_PolarAngle(p, next);
                        min_index = i;
                    }
                    else
                    {
                        double angle_ = get_PolarAngle(p, pp);
                        if (angle_ < minAngle)
                        {
                            next = pp;
                            minAngle = get_PolarAngle(p, next);
                            min_index = i;
                        }
                        else if (Math.Abs(angle_ - minAngle) < TOLerance)
                        {
                            double dist_next = distance_points(p, next);
                            double dist_pp = distance_points(p, pp);
                            if (dist_pp < dist_next)
                            {
                                next = pp;
                                minAngle = get_PolarAngle(p, next);
                                min_index = i;
                            }
                        }
                    }
                }
                convexHullStack.Add(next);
                convex_hull.RemoveAt(min_index);
            }

            // clear point if on the same line
            for (int i = 2; i < convexHullStack.Count; i++)
            {
                GAViewLine ll = new GAViewLine(convexHullStack[i - 2], convexHullStack[i]);
                GAViewPoint p1 = new GAViewPoint(convexHullStack[i - 1]);

                if (is_the_same_coordinates(convexHullStack[i - 2], convexHullStack[i - 1])) { convexHullStack.RemoveAt(i-1); i--; continue; }

                if (is_point_inside_line(ll, p1))
                {
                    convexHullStack.RemoveAt(i - 1);
                    i--;
                }
            }
            
            if(convexHullStack.Count > 3)
            {
                GAViewLine llf = new GAViewLine(convexHullStack[convexHullStack.Count - 2], convexHullStack[0]);
                GAViewPoint p1f = new GAViewPoint(convexHullStack[convexHullStack.Count - 1]);
                if (is_the_same_coordinates(convexHullStack[convexHullStack.Count - 2], convexHullStack[convexHullStack.Count - 1]))
                { convexHullStack.RemoveAt(convexHullStack.Count - 1); }else 
                if (is_point_inside_line(llf, p1f)) {convexHullStack.RemoveAt(convexHullStack.Count - 1);}
            }


            return convexHullStack;
        }

        /// <summary>
        /// возвращает контур, точки должны лежать в одной плоскости
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static List<GAPoint> get_ConvexContour2d(List<GAPoint> points)
        {
            GAPlane base_planeXY = new GAPlane(new GAPoint(0, 0, 0), new GAPoint(1, 0, 0), new GAPoint(0, 1, 0));
            GAPlane base_planeXZ = new GAPlane(new GAPoint(0, 0, 0), new GAPoint(1, 0, 0), new GAPoint(0, 0, 1));
            GAPlane base_planeYZ = new GAPlane(new GAPoint(0, 0, 0), new GAPoint(0, 1, 0), new GAPoint(0, 0, 1));


            //make duplicate to easy calculation - own index
            List<GAPoint> input_points = new List<GAPoint>();
            //List<GAPoint> pointsThere = projected_points;
            //my_class.drawPoint3D(my_part, projected_points);
            for (int k = 0; k < points.Count; k++)
            {
                input_points.Add(new GAPoint(points[k].X, points[k].Y, points[k].Z, points[k].description, k, points[k].description_t));
            }

            //get model pointsThere
            // create plane of pointsThere for calculation
            GAPlane pp = null;
            int pont_index_to_plane_creation = 0;
            for (int i = 1; i < input_points.Count; i++)
            {
                if (is_the_same_coordinates(input_points[0], input_points[i])) { input_points.RemoveAt(i); } else { break; }
            }

            for (int i = 2; i < input_points.Count; i++)
            {
                if (is_the_same_coordinates(input_points[0], input_points[i])) { continue; }
                if (is_the_same_coordinates(input_points[1], input_points[i])) { continue; }
                if (is_points_on_line(input_points[0], input_points[1], input_points[i])) { continue; }
                pp = new GAPlane(input_points[0], input_points[1], input_points[i]);
                pont_index_to_plane_creation = i;
                //sketchs.Add(my_class.drawPoint3D(my_part, input_points[0]));
                //sketchs.Add(my_class.drawPoint3D(my_part, input_points[1]));
                //sketchs.Add(my_class.drawPoint3D(my_part, input_points[i]));
                break;
            }
            if (pp == null) { return null; }


            List<GAViewPoint> ga_points22_rot_cont_view = new List<GAViewPoint>();
            List<GAViewPoint> ga_points_22_rot_view = new List<GAViewPoint>();

            // check if plane is parrallel to XY base plane
            double first_angle = get_angle(pp, base_planeXY);
            if (Math.Abs(first_angle) < 1e-3 || Math.Abs(first_angle - 180) < 1e-3)
            {
                foreach (GAPoint p in input_points)
                {
                    ga_points_22_rot_view.Add(new GAViewPoint(p.X, p.Y, p.description, p.index, p.description_t));
                }
            }
            else
            if (Math.Abs(first_angle - 90) < 1e-3 || Math.Abs(first_angle - 270) < 1e-3)
            {
                List<GAPoint> points_rotated2 = new List<GAPoint>();
                double angle_to_check_ZY = get_angle(pp, base_planeYZ);
                double angle_to_check_XZ = get_angle(pp, base_planeXZ);
                if (Math.Abs(angle_to_check_ZY) < 1e-3 || Math.Abs(angle_to_check_ZY - 180) < 1e-3)
                {
                    points_rotated2 = transform_rotate(input_points, input_points[0], 0, 90, 0);
                }
                else
                {
                    points_rotated2 = transform_rotate(input_points, input_points[0], 0, 0, 90);
                }

                //Sketch3D s3d22222222222 = my_class.drawPoint3D(my_part, points_rotated2);
                //s3d22222222222.Delete();

                foreach (GAPoint p in points_rotated2)
                {
                    ga_points_22_rot_view.Add(new GAViewPoint(p.X, p.Y, p.description, p.index, p.description_t));
                }
            }
            else
            if (first_angle > 1e-3 && Math.Abs(first_angle - 180) > 1e-3)
            {
                // if plane is not parallel to some base plane, rotate plane to XY
                // find plane axis angle
                GALine lY = new GALine(new GAPoint(0, 0, 0), new GAPoint(0, 1, 0));
                GALine lZ = new GALine(new GAPoint(0, 0, 0), new GAPoint(0, 0, 1));

                GAPoint onY = plane_line_intersection(pp, lY, false);
                GAPoint onZ = plane_line_intersection(pp, lZ, false);

                lY = new GALine(new GAPoint(0, 0, 0), onY);
                lZ = new GALine(new GAPoint(0, 0, 0), onZ);

                double angleZaY = get_degrees(Math.Atan(lZ.length / lY.length));

                // first rotate
                List<GAPoint> points_rotated = new List<GAPoint>();

                points_rotated = transform_rotate(input_points, input_points[0], 0, 0, angleZaY);

                // check if rotation is correct
                pp = new GAPlane(points_rotated[0], points_rotated[1], points_rotated[pont_index_to_plane_creation]);
                double angle_to_check1 = get_angle(base_planeXZ, pp);
                double angle_to_check2 = get_angle(base_planeYZ, pp);
                if (Math.Abs(angle_to_check1 - 90) > TOLerance && Math.Abs(angle_to_check2 - 90) > TOLerance)
                {
                    points_rotated = transform_rotate(input_points, input_points[0], 0, 0, -angleZaY);
                    //pp = new GAPlane(points_rotated[0], points_rotated[1], points_rotated[pont_index_to_plane_creation]);
                    //angle_to_check1 = get_angle(base_planeXZ, pp);
                    //angle_to_check2 = get_angle(base_planeYZ, pp);
                }

                //sketchs.Add(my_class.drawPoint3D(my_part, points_rotated));

                //sketchs.Add(my_class.drawPoint3D(my_part, points_rotated));

                // find second angle
                pp = new GAPlane(points_rotated[0], points_rotated[1], points_rotated[pont_index_to_plane_creation]);

                GALine lX = new GALine(new GAPoint(0, 0, 0), new GAPoint(1, 0, 0));
                lZ = new GALine(new GAPoint(0, 0, 0), new GAPoint(0, 0, 1));

                GAPoint onX = plane_line_intersection(pp, lX, false);
                onZ = plane_line_intersection(pp, lZ, false);
                lZ = new GALine(new GAPoint(0, 0, 0), onZ);
                lX = new GALine(new GAPoint(0, 0, 0), onX);
                double angleZaX = get_degrees(Math.Atan(lZ.length / lX.length));

                // second rotate
                List<GAPoint> points_rotated2 = transform_rotate(points_rotated, points_rotated[0], 0, angleZaX, 0);
                //sketchs.Add(my_class.drawPoint3D(my_part, points_rotated2));

                //check if rotation is correct
                pp = new GAPlane(points_rotated2[0], points_rotated2[1], points_rotated2[pont_index_to_plane_creation]);
                angle_to_check1 = get_angle(base_planeXY, pp);

                if (Math.Abs(angle_to_check1) > TOLerance && Math.Abs(angle_to_check1 - 180) > TOLerance)
                {
                    points_rotated2 = transform_rotate(points_rotated, points_rotated[0], 0, -angleZaX, 0);
                    //pp = new GAPlane(points_rotated[0], points_rotated[1], points_rotated[pont_index_to_plane_creation]);
                    //angle_to_check1 = get_angle(base_planeXZ, pp);
                    //angle_to_check2 = get_angle(base_planeYZ, pp);

                    //sketchs.Add(my_class.drawPoint3D(my_part, points_rotated2));
                }

                //s3d_face.Delete();
                //foreach (Sketch3D s3d in sketchs) { s3d.Delete(); }
                //sketchs.Clear();
                //continue;

                // find convex contour
                // birng 3d pointsThere to 2d pointsThere
                ga_points_22_rot_view = new List<GAViewPoint>();
                foreach (GAPoint p in points_rotated2)
                {
                    ga_points_22_rot_view.Add(new GAViewPoint(p.X, p.Y, p.description, p.index, p.description_t));
                }

                //Sketch3D s3d22222222222 = my_class.drawPoint3D(my_part, points_rotated2);
                //s3d22222222222.Delete();
            }

            // find convex contour
            ga_points22_rot_cont_view = get_ConvexContour2d(ga_points_22_rot_view);
            if(ga_points22_rot_cont_view == null) { ga_points22_rot_cont_view = new List<GAViewPoint>(); }
            List<GAPoint> points_to_draw = new List<GAPoint>();
            foreach (GAViewPoint vp in ga_points22_rot_cont_view)
            {
                GAPoint ppp = new GAPoint(vp.X, vp.Y, 0);
                points_to_draw.Add(ppp);

            }
            //Sketch3D s3d_2435345 = my_class.drawPoint3D(my_part, points_to_draw);
            //s3d_2435345.Delete();

            //return convex contour
            List<GAPoint> convex_contour = new List<GAPoint>();
            for (int i = 0; i < ga_points22_rot_cont_view.Count; i++)
            {
                GAViewPoint vp = ga_points22_rot_cont_view[i];
                convex_contour.Add(points[vp.index]);
            }


            return convex_contour;
        }

        static List<GAViewPoint> findHull(List<GAViewPoint> points, GAViewPoint A_minX, GAViewPoint B_maxX)
        {
            if (points.Count == 0) { return points; }
            List<GAViewPoint> convex_hull = new List<GAViewPoint>();

            double max_dist = 0;
            GAViewPoint maxPoint = null;

            // point with maximal distance to create triangle
            foreach (GAViewPoint p in points)
            {
                if(maxPoint == null) { maxPoint = p; }
                double dist = distance_line_point(new GAViewLine(A_minX, B_maxX), p);

                if (dist > max_dist) { max_dist = dist; maxPoint = p; }
            }
            convex_hull.Add(maxPoint);
            List<GAViewPoint> s_left = new List<GAViewPoint>();
            List<GAViewPoint> s_right = new List<GAViewPoint>();

            //exclude pointsThere inside the triangle
            foreach (GAViewPoint p in points)
            {
                if (A_minX == p) { continue; }
                if (B_maxX == p) { continue; }
                if (maxPoint == p) { continue; }
                TOLerance = 1e-5;
                bool inside = is_point_inside_triangle(p, A_minX, B_maxX, maxPoint);
                if (inside) { continue; }
                else
                {

                    // if not in triangle, put point in two subsets
                    if (p.X >= maxPoint.X) { s_right.Add(p); } else { s_left.Add(p); }
                }
            }

            convex_hull.AddRange(findHull(s_left, A_minX, maxPoint));
            convex_hull.AddRange(findHull(s_right, maxPoint, B_maxX));

            return convex_hull;
        }

        /// <summary>
        /// полярная координата
        /// </summary>
        /// <param name="center"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static double get_PolarAngle(GAViewPoint center, GAViewPoint point)
        {
            // четверть 1
            if(point.X > center.X && point.Y > center.Y)
            {
                return Math.Atan((point.Y - center.Y) / (point.X - center.X));
            }

            // четверть 2
            if (point.X < center.X && point.Y > center.Y)
            {
                return  get_radian(180) + Math.Atan((point.Y - center.Y) / (point.X - center.X));
            }

            // четверть 3
            if (point.X < center.X && point.Y < center.Y)
            {
                return get_radian(180) + Math.Atan((point.Y - center.Y) / (point.X - center.X));
            }

            // четверть 4
            if (point.X > center.X && point.Y < center.Y)
            {
                return get_radian(360) + Math.Atan((point.Y - center.Y) / (point.X - center.X));
            }

            if(point.Y > center.Y && point.X == center.X)
            {
                return get_radian(90);
            }
            if (point.Y == center.Y && point.X < center.X)
            {
                return get_radian(180);
            }
            if (point.Y < center.Y && point.X == center.X)
            {
                return get_radian(270);
            }
            if (point.Y == center.Y && point.X > center.X)
            {
                return get_radian(0);
            }
            return 0;
        }

        /// <summary>
        /// find a point on maximal perpendicular distance from a plane
        /// </summary>
        /// <param name="plane"></param>
        /// <param name="points"></param>
        /// <returns>point</returns>
        public static GAPoint get_MaxDistancePointFromPlane(GAPlane plane, List<GAPoint> points)
        {
            GAPoint maxDistPoint = null;
            double distB = 0;
            foreach (GAPoint p in points)
            {
                double cur_dist = distance_plane_point(p, plane);
                if (cur_dist > distB) { distB = cur_dist; maxDistPoint = p; }
            }

            if(maxDistPoint == null)
            {
                return points[0];
            }
            else
            {
                return maxDistPoint;
            }
            
        }

        #endregion

        #region transform

        /// <summary>
        /// преобразование поворота
        /// </summary>
        /// <param name="gp">точкb для преобразования</param>
        /// <param name="center_point">центр относительно которого поворачиваем</param>
        /// <param name="angle1">угол X (вокруг оси Z)</param>
        /// <param name="angle2">угол Y (вокруг оси Y)</param>
        /// <param name="angle3">угол Z (вокруг оси X)</param>
        /// <returns></returns>
        public static List<GAPoint> transform_rotate(List<GAPoint> gp, GAPoint center_point, double angle1, double angle2, double angle3)
        {
           List<GAPoint> pl = new List<GAPoint>();
            
            foreach(GAPoint p in gp)
            {
                pl.Add(transform_rotate(p, center_point, angle1, angle2, angle3));
            }

            return pl;

        }

        /// <summary>
        /// преобразование поворота
        /// </summary>
        /// <param name="gp">точка для преобразования</param>
        /// <param name="center_point">центр относительно которого поворачиваем</param>
        /// <param name="angle1">угол X (вокруг оси Z)</param>
        /// <param name="angle2">угол Y (вокруг оси Y)</param>
        /// <param name="angle3">угол Z (вокруг оси X)</param>
        /// <returns></returns>
        public static GAPoint transform_rotate(GAPoint gp, GAPoint center_point, double angle1, double angle2, double angle3)
        {
            GAPoint pp = new GAPoint();
            pp.X = gp.X;
            pp.Y = gp.Y;
            pp.Z = gp.Z;
            //pp.description = gp.description;
            //pp.description_t = gp.description_t;
            //pp.index = gp.index;

            pp.X -= center_point.X;       // преобразование координат в систему координат с началом в базовой точке 
            pp.Y -= center_point.Y;
            pp.Z -= center_point.Z;

            GAPoint temp = new GAPoint(
                (double)(pp.X * Cos(angle1) + pp.Y * Sin(angle1)),
                (double)(pp.Y * Cos(angle1) - pp.X * Sin(angle1)),
                pp.Z
                );      // применяем матрицу поворота

            temp = new GAPoint(
            (double)(temp.X * Cos(angle2) + temp.Z * Sin(angle2)),
            temp.Y,
            (double)(temp.Z * Cos(angle2) - temp.X * Sin(angle2))
            );      // применяем матрицу поворота

            temp = new GAPoint(
                temp.X,
                (double)(temp.Y * Cos(angle3) + temp.Z * Sin(angle3)),
                (double)(temp.Z * Cos(angle3) - temp.Y * Sin(angle3))
                );      // применяем матрицу поворота

            temp.X += center_point.X;     // обратное преобразование координат
            temp.Y += center_point.Y;
            temp.Z += center_point.Z;
            temp.description_t = gp.description_t;
            temp.description = gp.description;
            temp.index = gp.index;

            return temp;


        }

        /// <summary>
        /// Преобразование поворота
        /// </summary>
        /// <param name="gp">точка для поворота</param>
        /// <param name="center_point">центр поворота</param>
        /// <param name="angle1">угол поворота</param>
        /// <returns></returns>
        public static GAViewPoint transform_rotate(GAViewPoint gp, GAViewPoint center_point, double angle1)
        {
            if (double.IsNaN(gp.X) || double.IsInfinity(gp.X)) { return gp; }
            if (double.IsNaN(gp.Y) || double.IsInfinity(gp.Y)) { return gp; }
            if (double.IsNaN(center_point.X) || double.IsInfinity(center_point.X)) { return gp; }
            if (double.IsNaN(center_point.Y) || double.IsInfinity(center_point.Y)) { return gp; }
            
            GAViewPoint pp = new GAViewPoint();
            pp.X = gp.X;
            pp.Y = gp.Y;

            pp.X -= center_point.X;       // преобразование координат в систему координат с началом в базовой точке 
            pp.Y -= center_point.Y;
            //gp.Z -= center_point.Z;

            GAViewPoint temp = new GAViewPoint(
                (double)(pp.X * Cos(angle1) + pp.Y * Sin(angle1)),
                (double)(pp.Y * Cos(angle1) - pp.X * Sin(angle1))
                //gp.Z
                );      // применяем матрицу поворота

            temp.X += center_point.X;     // обратное преобразование координат
            temp.Y += center_point.Y;
            //temp.Z += center_point.Z;
            temp.description = gp.description;
            temp.index = gp.index;
            temp.description_t = gp.description_t;

            return temp;
        }

        /// <summary>
        /// преобразование поворота
        /// </summary>
        /// <param name="gp">точки для преобразования</param>
        /// <param name="center_point">центр относительно которого поворачиваем</param>
        /// <param name="angle1">угол</param>
        /// <returns></returns>
        public static List<GAViewPoint> transform_rotate(List<GAViewPoint> gp, GAViewPoint center_point, double angle1)
        {
            List<GAViewPoint> pp = new List<GAViewPoint>();
            foreach(GAViewPoint p in gp)
            {
                pp.Add(transform_rotate(p, center_point, angle1));
            }
            return pp;
        }

        /// <summary>
        /// смещение точки
        /// </summary>
        /// <param name="gp"></param>
        /// <param name="dX">смещение по X</param>
        /// <param name="dY">смещение по Y</param>
        /// <returns></returns>
        public static GAViewPoint transform_move(GAViewPoint gp, double dX, double dY)
        {
            GAViewPoint pp = new GAViewPoint();
            pp.X = gp.X + dX;
            pp.Y = gp.Y + dY;
            pp.description = gp.description;
            pp.index = gp.index;
            return pp;
        }

        /// <summary>
        /// смещение точки
        /// </summary>
        /// <param name="gp"></param>
        /// <param name="dX">смещение по X</param>
        /// <param name="dY">смещение по Y</param>
        /// <returns></returns>
        public static List<GAViewPoint> transform_move(List<GAViewPoint> gp, double dX, double dY)
        {
            List<GAViewPoint> pp = new List<GAViewPoint>();
           
            foreach(GAViewPoint p in gp)
            {
                pp.Add(transform_move(p, dX, dY));
            }

            return pp;
        }

        /// <summary>
        /// смещение линии
        /// </summary>
        /// <param name="line_">линия</param>
        /// <param name="dX">смещение по X</param>
        /// <param name="dY">смещение по Y</param>
        /// <returns></returns>
        public static GAViewLine transform_move_line(GAViewLine line_, double dX, double dY)
        {
            GAViewLine pp = 
                new GAViewLine(
                    new GAViewPoint(transform_move(line_.A, dX, dY)), 
                    new GAViewPoint(transform_move(line_.B, dX, dY))
                    );
            return pp;
        }

        /// <summary>
        /// смещение списка линий
        /// </summary>
        /// <param name="lines_">линии</param>
        /// <param name="dX">смещение по X</param>
        /// <param name="dY">смещение по Y</param>
        /// <returns></returns>
        public static List<GAViewLine> transform_move_line(List<GAViewLine> lines_, double dX, double dY)
        {
            List<GAViewLine> ll = new List<GAViewLine>();
            foreach (GAViewLine l in lines_)
            {
                GAViewLine lll = new GAViewLine(transform_move_line(new GAViewLine(l), dX, dY), l.description, l.index);
                ll.Add(lll);
            }
            return ll;
        }

        /// <summary>
        /// Преобразование поворота
        /// </summary>
        /// <param name="line_">линия для поворота</param>
        /// <param name="center_point">центр поворота</param>
        /// <param name="angle1">угол поворота</param>
        /// <returns></returns>
        public static GAViewLine transform_rotate_line(GAViewLine line_, GAViewPoint center_point, double angle1)
        {
            GAViewLine ll = new GAViewLine(transform_rotate(new GAViewPoint(line_.A), center_point, angle1), transform_rotate(new GAViewPoint(line_.B), center_point, angle1));
            //line_.A = transform_rotate(line_.A, center_point, angle1);
            //line_.B = transform_rotate(line_.B, center_point, angle1);

            return ll;
        }

        /// <summary>
        /// Преобразование поворота
        /// </summary>
        /// <param name="lines_">список линий для поворота</param>
        /// <param name="center_point">центр поворота</param>
        /// <param name="angle1">угол поворота</param>
        /// <returns></returns>
        public static List<GAViewLine> transform_rotate_line(List<GAViewLine> lines_, GAViewPoint center_point, double angle1)
        {
            List<GAViewLine> ll = new List<GAViewLine>();
            for(int i = 0; i<lines_.Count; i++)
            {
                ll.Add(new GAViewLine( transform_rotate(lines_[i].A, center_point, angle1), transform_rotate(lines_[i].B, center_point, angle1)));
            }
            return ll;
        }

        /// <summary>
        /// Преобразование поворота
        /// </summary>
        /// <param name="lines_">список линий для поворота</param>
        /// <param name="center_point">центр поворота</param>
        /// <param name="angle1">угол поворота</param>
        /// <param name="angle2">угол поворота</param>
        /// <param name="angle3">угол поворота</param>
        /// <returns></returns>
        public static List<GALine> transform_rotate_line(List<GALine> lines_, GAPoint center_point, double angle1, double angle2, double angle3)
        {
            List<GALine> ll = new List<GALine>();
            for (int i = 0; i < lines_.Count; i++)
            {
                ll.Add(new GALine(transform_rotate(lines_[i].A, center_point, angle1, angle2, angle3), transform_rotate(lines_[i].B, center_point, angle1, angle2, angle3)));
            }
            return ll;
        }
        #endregion

        #region distance

        /// <summary>
        /// расстояние между точками в пространстве
        /// </summary>
        /// <param name="A">точка 1</param>
        /// <param name="B">точка 2</param>
        /// <returns></returns>
        public static double distance_points(GAPoint A, GAPoint B)
        {
            return Math.Pow(Math.Pow((A.X - B.X), 2) + Math.Pow((A.Y - B.Y), 2) + Math.Pow((A.Z - B.Z), 2), 0.5);
        }

        /// <summary>
        /// расстояние между точками на плоскости
        /// </summary>
        /// <param name="A">точка 1</param>
        /// <param name="B">точка 2</param>
        /// <returns></returns>
        public static double distance_points(GAViewPoint A, GAViewPoint B)
        {
            if (A == null) { return 0; }
            if (B == null) { return 0; }
            return Math.Pow(Math.Pow((A.X - B.X), 2) + Math.Pow((A.Y - B.Y), 2), 0.5);
        }

        /// <summary>
        /// расстояние от точки до плоскости
        /// </summary>
        /// <param name="A">точка</param>
        /// <param name="Plane">плоскость</param>
        /// <returns></returns>
        public static double distance_plane_point(GAPoint A, GAPlane Plane)
        {
            return Math.Abs(Plane.A * A.X + Plane.B * A.Y + Plane.C * A.Z + Plane.D) / (Math.Pow(Plane.A * Plane.A + Plane.B * Plane.B + Plane.C * Plane.C, 0.5));
        }

        /// <summary>
        /// расстояние от 2D точки до 2D линии
        /// </summary>
        /// <param name="L"></param>
        /// <param name="P"></param>
        /// <returns></returns>
        public static double distance_line_point(GAViewLine L, GAViewPoint P)
        {
            GAViewPoint l1 = L.A;
            GAViewPoint l2 = L.B;

            return Math.Abs((l2.X - l1.X) * (l1.Y - P.Y) - (l1.X - P.X) * (l2.Y - l1.Y)) /
                    Math.Sqrt(Math.Pow(l2.X - l1.X, 2) + Math.Pow(l2.Y - l1.Y, 2));
        }

        /// <summary>
        /// Длинна перпендикуляра опущенного на линию из точки (расстояние от точки до линии по перпендикуляру к линии)
        /// </summary>
        /// <param name="L">Линия</param>
        /// <param name="P">Точка</param>
        /// <returns>Расстояние</returns>
        public static double distance_line_point(GALine L, GAPoint P)
        {
            //Рассчитаем площадь по формуле Герона и от площади узнаем высоту
            double l_AB = L.length;
            double l_BC = distance_points(L.A, P);
            double l_AC = distance_points(L.B, P);

            double perim_2 = (l_AB + l_BC + l_AC)/2;

            double h = (Math.Pow(perim_2 * (perim_2 - l_AB) * (perim_2 - l_BC) * (perim_2 - l_AC), 0.5)) * 2 / l_AB;

            return h;
        }

        /// <summary>
        /// расстояние между 3D точками
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public static double distance(GAPoint A, GAPoint B)
        {
            return distance_points(A, B);
        }
        /// <summary>
        /// расстояние между 2D точками
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public static double distance(GAViewPoint A, GAViewPoint B)
        {
            return distance_points(A, B);
        }
        /// <summary>
        /// расстояние между точкой и плоскостью
        /// </summary>
        /// <param name="A"></param>
        /// <param name="Plane"></param>
        /// <returns></returns>
        public static double distance(GAPoint A, GAPlane Plane)
        {
            return distance_plane_point(A, Plane);
        }
        /// <summary>
        /// расстояние между 2D линией и 2D точкой
        /// </summary>
        /// <param name="L"></param>
        /// <param name="P"></param>
        /// <returns></returns>
        public static double distance(GAViewLine L, GAViewPoint P)
        {
            return distance_line_point(L, P);
        }
        /// <summary>
        /// расстояние между 3D точкой и 3D линией
        /// </summary>
        /// <param name="L"></param>
        /// <param name="P"></param>
        /// <returns></returns>
        public static double distance(GALine L, GAPoint P)
        {
            return distance_line_point(L, P);
        }
        ///// <summary>
        ///// расстояние между двумя параллельными плоскостями
        ///// </summary>
        ///// <param name="p1">плоскость 1</param>
        ///// <param name="p2">плоскость 2</param>
        ///// <returns>0 = плоскости не параллельны</returns>
        //public double distance(GAPlane p1, GAPlane p2)
        //{
        //    bool is_parallel = false;
        //    GAPoint v1 = new GAPoint(p1.A, p1.B, p1.C);
        //    GAPoint v2 = new GAPoint(p2.A, p2.B, p2.C);
        //    if (is_the_same_coordinates(v1, v2)) { is_parallel = true; }else
        //    if (is_points_on_line(new GAPoint(), v1, v2)) { is_parallel = true; }

        //    if (is_parallel)
        //    {
        //        double z1, d;
        //        z1 = -p1.D / p1.C;
        //        d = Math.Abs((p2.C * z1 + p2.D)) /
        //            (double)(Math.Sqrt(p2.A * p2.A + p2.B *
        //                              p2.B + p2.C * p2.C));
        //        return d;
        //    }
        //    else
        //    {
        //        return 0;
        //    }
        //}

        #endregion

        #region is

        /// <summary>
        /// лежат ли точки на одной плоскости
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="C"></param>
        /// <param name="D"></param>
        /// <returns></returns>
        public static bool is_point_on_plane(GAPoint A, GAPoint B, GAPoint C, GAPoint D, double tolerance = 1e-5)
        {
            double aa = (new matrix3x3(
                A.X - D.X, A.Y - D.Y, A.Z - D.Z,
                B.X - D.X, B.Y - D.Y, B.Z - D.Z,
                C.X - D.X, C.Y - D.Y, C.Z - D.Z
                 ).A) / 6.0;
            if (Math.Abs(aa) < tolerance) { return true; } else { return false; }

        }

        /// <summary>
        /// лежат ли точки на одной плоскости
        /// </summary>
        /// <param name="plane">plane</param>
        /// <param name="P">point</param>
        /// <param name="tolerance">tolerance</param>
        /// <returns></returns>
        public static bool is_point_on_plane(GAPlane plane, GAPoint P, double tolerance = 1e-5)
        {
            double dist = distance_plane_point(P, plane);
            return (dist < tolerance);
        }

        /// <summary>
        /// одинаковые координаты точек
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="tolerance">tolerance</param>
        /// <returns></returns>
        public static bool is_the_same_coordinates(GAPoint A, GAPoint B, double tolerance = 1e-5)
        {
            return (Math.Abs(A.X - B.X) < tolerance && Math.Abs(A.Y - B.Y) < tolerance && Math.Abs(A.Z - B.Z) < tolerance);
        }

        /// <summary>
        /// одинаковые координаты точек
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="tolerance">tolerance</param>
        /// <returns></returns>
        public static bool is_the_same_coordinates(GAViewPoint A, GAViewPoint B, double tolerance = 1e-5)
        {
            return (Math.Abs(A.X - B.X) < tolerance && Math.Abs(A.Y - B.Y) < tolerance);
        }

        /// <summary>
        /// являются ли вектора колинеальны // параллельны
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static bool is_colineral_vectors(GAVector v1, GAVector v2)
        {

            double kx = 0, ky = 0, kz = 0;
            if (v1.X < TOLerance && v2.X < TOLerance) { kx = 1; }
            if (v1.Y < TOLerance && v2.Y < TOLerance) { ky = 1; }
            if (v1.Z < TOLerance && v2.Z < TOLerance) { kz = 1; }

            if (v2.X != 0) { kx = v1.X / v2.X; }
            if (v2.Y != 0) { ky = v1.Y / v2.Y; }
            if (v2.Z != 0) { kz = v1.Z / v2.Z; }

            //if (v1.Z == 0 && v2.Z == 0 && v1.X / v2.X - v1.Y / v2.Y <= 1e-10) { return true; }
            //if (v1.Y == 0 && v2.Y == 0 && v1.X / v2.X - v1.Z / v2.Z <= 1e-10) { return true; }
            //if (v1.X == 0 && v2.X == 0 && v1.Y / v2.Y - v1.Z / v2.Z <= 1e-10) { return true; }
            //if (v1.X / v2.X - v1.Y / v2.Y <= 1e-10 &&
            //    v1.X / v2.X - v1.Z / v2.Z <= 1e-10) { return true; }

            if (kx - ky <= 1e-10 &&
            kx - kz <= 1e-10) { return true; }
            return false;
        }

        /// <summary>
        /// лижат ли точки на одной прямой
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="C"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static bool is_points_on_line(GAPoint A, GAPoint B, GAPoint C, double tolerance = 1e-5)
        {
            double dAB = distance_points(A, B);
            double dBC = distance_points(B, C);
            double dAC = distance_points(A, C);
            bool b0 = (Math.Abs(dAB - dBC - dAC) < TOLerance);
            bool b1 = (Math.Abs(dBC - dAB - dAC) < TOLerance);
            bool b2 = (Math.Abs(dAC - dBC - dAB) < TOLerance);

            return (b0 || b1 || b2);

        }

        /// <summary>
        /// лижат ли точки на одной прямой
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="C"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static bool is_points_on_line(GAViewPoint A, GAViewPoint B, GAViewPoint C, double tolerance = 1e-5)
        {
            double dAB = distance_points(A, B);
            double dBC = distance_points(B, C);
            double dAC = distance_points(A, C);
            bool b0 = (Math.Abs(dAB - dBC - dAC) < TOLerance);
            bool b1 = (Math.Abs(dBC - dAB - dAC) < TOLerance);
            bool b2 = (Math.Abs(dAC - dBC - dAB) < TOLerance);

            return (b0 || b1 || b2);
        }

        ///// <summary>
        ///// is 3D point on 3D arc - not ready
        ///// </summary>
        ///// <param name="point">3D point</param>
        ///// <param name="arc">3D arc</param>
        ///// <returns></returns>
        //public bool is_point_on_arc(GAPoint point, GAArc arc)
        //{
        //    if (!is_point_on_plane(arc.plane, point)) { return false; };                            //если в одной плоскости
        //    if (Math.Abs(distance(point, arc.center) - arc.radius) > TOLerance) { return false; }   //если радиус - ок
        //    //если угол ок
        //    return true;
        //}

        /// <summary>
        /// лижит ли точка в границах линии
        /// </summary>
        /// <param name="line"></param>
        /// <param name="C"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static bool is_point_inside_line(GALine line, GAPoint C, double tolerance = 1e-5)
        {
            GALine l1 = new GALine(line.A, C);
            GALine l2 = new GALine(line.B, C);

            if (Math.Abs((l1.length + l2.length) - line.length) < tolerance) { return true; }

            return false;
        }

        /// <summary>
        /// лижит ли точка в границах линии | -1=error, 0=false, 1=true, 2=point on one of triangle lines
        /// </summary>
        /// <param name="line"></param>
        /// <param name="C"></param>
        /// <param name="tolerance"></param>
        /// <returns>-1=error, 0=false, 1=true, 2=point on one of triangle lines</returns>
        public static bool is_point_inside_line(GAViewLine line, GAViewPoint C, double tolerance = 1e-5)
        {
            GAViewLine l1 = new GAViewLine(line.A, C);
            GAViewLine l2 = new GAViewLine(line.B, C);

            if (Math.Abs((l1.length + l2.length) - line.length) < tolerance) { return true; }

            return false;
        }

        //основной метод
        /// <summary>
        /// лежит ли точка в треугольнике | -1=error, 0=false, 1=true, 2=point on one of triangle lines
        /// </summary>
        /// <param name="P">point to check</param>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="C"></param>
        /// <returns>-1=error, 0=false, 1=true, 2=point on one of triangle lines</returns>
        public static int is_point_inside_triangle(GAPoint P, GAPoint A, GAPoint B, GAPoint C)
        {
            //основной метод

            GATriangle tr_main = new GATriangle(A, B, C);

            GATriangle tr_1 = new GATriangle(P, A, B);
            GATriangle tr_2 = new GATriangle(P, A, C);
            GATriangle tr_3 = new GATriangle(P, B, C);

            bool on_line_1 = is_point_inside_line(tr_main.lines[0], P);
            bool on_line_2 = is_point_inside_line(tr_main.lines[1], P);
            bool on_line_3 = is_point_inside_line(tr_main.lines[2], P);

            if(CountTrue_SummOfTrue(on_line_1, on_line_2, on_line_3) > 0) { return 2; }

            double summOfArea = Math.Round(Math.Abs(tr_1.Area + tr_2.Area + tr_3.Area), 10);

            if(summOfArea >= double.MaxValue)
            {
                throw new Exception("Area has too big value");
            }

            if (tr_main.Area >= double.MaxValue)
            {
                throw new Exception("Area has too big value");
            }

            //double tolPer = tr_main.Area * 0.0000001;
            //bool the_same_area = (((tr_1.Area + tr_2.Area + tr_3.Area) - tr_main.Area) < tolPer);
            
            double diff = Math.Abs(summOfArea - tr_main.Area);
            double diff2 = round_get_scale_by_significant_digits(diff, 4);
            bool the_same_area = (diff == 0);

            if (the_same_area) { return 1; }

            if (tr_main.Area < summOfArea) { return 0; }

            return -1;
        }

        /// <summary>
        /// лежит ли точка в треугольнике
        /// </summary>
        /// <param name="P">point to check</param>
        /// <param name="A">triangle point</param>
        /// <param name="B">triangle point</param>
        /// <param name="C">triangle point</param>
        /// <returns>-1=error, 0=false, 1=true, 2=point on one of triangle lines</returns>
        public static bool is_point_inside_triangle_bool(GAPoint P, GAPoint A, GAPoint B, GAPoint C)
        {
            int answr = is_point_inside_triangle(P, A, B, C);

            if (answr > 0) { return true; } else { return false; }
        }

        /// <summary>
        /// лежит ли точка в треугольнике| -1=error, 0=false, 1=true, 2=point on one of triangle lines
        /// </summary>
        /// <param name="P"></param>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="C"></param>
        /// <returns>-1=error, 0=false, 1=true, 2=point on one of triangle lines</returns>
        public static bool is_point_inside_triangle(GAViewPoint P, GAViewPoint A, GAViewPoint B, GAViewPoint C)
        {
            double a = (P.X - A.X) * (P.Y - B.Y) - (P.X - B.X) * (P.Y - A.Y),
                b = (P.X - B.X) * (P.Y - C.Y) - (P.X - C.X) * (P.Y - B.Y),
                c = (P.X - C.X) * (P.Y - A.Y) - (P.X - A.X) * (P.Y - C.Y);
            return ((a >= 0 && b >= 0 && c >= 0) ||
                (a <= 0 && b <= 0 && c <= 0));

            //return is_point_inside_triangle_bool(P.ToPoint(), A.ToPoint(), B.ToPoint(), C.ToPoint());
        }

        /// <summary>
        /// лежит ли точка в треугольнике, использовать только для вычисления экстемально больших чисел, нужно тестирование!!!
        /// </summary>
        /// <param name="P">point to check</param>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="C"></param>
        /// <returns>-1=error, 0=false, 1=true, 2=point on one of triangle lines</returns>
        public static bool is_point_inside_triangle_big(GAPoint_Big P, GAPoint_Big A, GAPoint_Big B, GAPoint_Big C)
        {
            GATriangle_Big tr_main = new GATriangle_Big(A, B, C);
            GATriangle_Big tr_1 = new GATriangle_Big(P, A, B);
            GATriangle_Big tr_2 = new GATriangle_Big(P, A, C);
            GATriangle_Big tr_3 = new GATriangle_Big(P, B, C);

            BigInteger AreaSumm = Summ_Big(tr_1.Area, tr_2.Area, tr_3.Area);

            BigInteger tolPer = BigInteger.Divide(AreaSumm, 100);

            BigInteger diff = BigInteger.Abs(BigInteger.Subtract(AreaSumm, tr_main.Area));

            if(diff < tolPer) { return true; } else { return false; }

        }

        /// <summary>
        /// is point inside triangle, the calculation uses BibInteger type
        /// </summary>
        /// <param name="P"></param>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="C"></param>
        /// <returns></returns>
        public static bool is_point_inside_triangle_big(GAPoint P, GAPoint A, GAPoint B, GAPoint C)
        {
            return is_point_inside_triangle_big( P.ToPointBig(), A.ToPointBig(), B.ToPointBig(), C.ToPointBig());
        }

        /// <summary>
        /// is point inside triangle, the calculation uses BibInteger type
        /// </summary>
        /// <param name="P"></param>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="C"></param>
        /// <returns></returns>
        public static bool is_point_inside_triangle_big(GAViewPoint P, GAViewPoint A, GAViewPoint B, GAViewPoint C)
        {
            return is_point_inside_triangle_big(P.ToPointBig(), A.ToPointBig(), B.ToPointBig(), C.ToPointBig());
        }


        /// <summary>
        /// лежит ли точка в тетраидре (пирамиде)
        /// </summary>
        /// <param name="T">tetrahedron</param>
        /// <param name="P">point</param>
        /// <returns></returns>
        public static bool is_point_inside_Tetrahedron(GATetrahedron T, GAPoint P)
        {
            GATetrahedron th1 = new GATetrahedron(T.A, T.B, T.C, P);
            GATetrahedron th2 = new GATetrahedron(T.A, T.B, T.D, P);
            GATetrahedron th3 = new GATetrahedron(T.A, T.D, T.C, P);
            GATetrahedron th4 = new GATetrahedron(T.B, T.D, T.C, P);

            double volume_sum = th1.volume + th2.volume + th3.volume + th4.volume;

            bool the_same_volume = (volume_sum - T.volume < TOLerance);
            
            if (the_same_volume && (T.volume < TOLerance || volume_sum < TOLerance))
            {
                int intr1 = is_point_inside_triangle(P, T.A, T.B, T.C);
                int intr2 = is_point_inside_triangle(P, T.A, T.B, T.D);
                int intr3 = is_point_inside_triangle(P, T.A, T.D, T.C);
                int intr4 = is_point_inside_triangle(P, T.B, T.D, T.C);

                if(intr1 <=1 || intr2 <= 1 || intr3 <= 1 || intr4 <= 1) { return false; }else { return true; }
            }

            return the_same_volume;

        }

        /// <summary>
        /// проверяет, являются ли проекции точек одинаковыми координатами
        /// </summary>
        /// <param name="plane"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static bool is_the_same_projection(GAPlane plane, GAPoint p1, GAPoint p2)
        {
            return is_the_same_coordinates(PerpendicularToPoint(plane, p1), PerpendicularToPoint(plane, p2), 1e-3);
        }

        #endregion

        #region min max

        /// <summary>
        /// minimal by X
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static GAPoint MinimumByX(List<GAPoint> list)
        {
            GAPoint minPoint = null;
            foreach (GAPoint vP in list)
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
        /// minimal by Y
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static GAPoint MinimumByY(List<GAPoint> list)
        {
            GAPoint minPoint = null;
            foreach (GAPoint vP in list)
            {
                if (minPoint == null) { minPoint = vP; continue; }
                if (vP.Y < minPoint.Y)
                {
                    minPoint = vP;
                }
            }
            return minPoint;
        }

        /// <summary>
        /// minimal by Z
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static GAPoint MinimumByZ(List<GAPoint> list)
        {
            GAPoint minPoint = null;
            foreach (GAPoint vP in list)
            {
                if (minPoint == null) { minPoint = vP; continue; }
                if (vP.Z < minPoint.Z)
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
        public static GAPoint MaximalByX(List<GAPoint> list)
        {
            GAPoint maxPoint = null;
            foreach (GAPoint vP in list)
            {
                if (maxPoint == null) { maxPoint = vP; continue; }
                if (vP.X > maxPoint.X)
                {
                    maxPoint = vP;
                }
            }
            return maxPoint;
        }

        /// <summary>
        /// maximal by Y
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static GAPoint MaximalByY(List<GAPoint> list)
        {
            GAPoint maxPoint = null;
            foreach (GAPoint vP in list)
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
        /// maximal by Z
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static GAPoint MaximalByZ(List<GAPoint> list)
        {
            GAPoint maxPoint = null;
            foreach (GAPoint vP in list)
            {
                if (maxPoint == null) { maxPoint = vP; continue; }
                if (vP.Z > maxPoint.Z)
                {
                    maxPoint = vP;
                }
            }
            return maxPoint;
        }

        #endregion

        #region perpendicular

        /// <summary>
        /// возвращает точку, перпендикуляр из заданной точки к линии (возвращенная точка лежит на линии)
        /// </summary>
        /// <param name="Line"></param>
        /// <param name="C"></param>
        /// <returns></returns>
        public static GAViewPoint PerpendicularToPoint(GAViewLine Line, GAViewPoint C)
        {
            GAViewPoint A = Line.A;
            GAViewPoint B = Line.B;
            double x1 = A.X, y1 = A.Y, x2 = B.X, y2 = B.Y, x3 = C.X, y3 = C.Y;
            double px = x2 - x1, py = y2 - y1, dAB = px * px + py * py;
            double u = ((x3 - x1) * px + (y3 - y1) * py) / dAB;
            double x = x1 + u * px, y = y1 + u * py;
            
            return new GAViewPoint(x, y); 
        }

        /// <summary>
        /// возвращает точку, перпендикуляр из заданной точки на плоскость (возвращенная точка лежит на плоскости)
        /// </summary>
        /// <param name="Plane">Plane</param>
        /// <param name="P">Point</param>
        /// <returns></returns>
        public static GAPoint PerpendicularToPoint(GAPlane Plane, GAPoint P)
        {
            double k = (-Plane.A * P.X - Plane.B * P.Y - Plane.C * P.Z - Plane.D) /
                (float)(Plane.A * Plane.A + Plane.B * Plane.B + Plane.C * Plane.C);
            double x2 = Plane.A * k + P.X;
            double y2 = Plane.B * k + P.Y;
            double z2 = Plane.C * k + P.Z;

            return new GAPoint(x2, y2, z2);
        }

        /// <summary>
        /// возвращает точку, перпендикуляр из заданной точки на пряму (возвращенная точка лежит на прямой)
        /// </summary>
        /// <param name="L">line</param>
        /// <param name="P">point</param>
        /// <returns></returns>
        public static GAPoint PerpendicularToPoint(GALine L, GAPoint P)
        {
            if (is_the_same_coordinates(P, L.A)) { return P; }
            if (is_the_same_coordinates(P, L.B)) { return P; }

            double l_AB = L.length;
            double l_BC = distance_points(L.A, P);
            double l_AC = distance_points(L.B, P);

            double h = distance_line_point(L, P);

            double max_l_AC_BC = 0;
            bool rev = false;
            if (l_BC > l_AC)
            {
                rev = true;
            }
            max_l_AC_BC = Math.Max(l_AC, l_BC);

            double angleA = ASin(h / max_l_AC_BC);
            double l_AH = max_l_AC_BC * Cos(angleA);
            double k = l_AH / l_AB;

            double Hx = 0;
            double Hy = 0;
            double Hz = 0;
            if (rev)
            {
                Hx = (L.B.X - L.A.X) * k + L.A.X;
                Hy = (L.B.Y - L.A.Y) * k + L.A.Y;
                Hz = (L.B.Z - L.A.Z) * k + L.A.Z;
            }
            else
            {
                Hx = (L.A.X - L.B.X) * k + L.B.X;
                Hy = (L.A.Y - L.B.Y) * k + L.B.Y;
                Hz = (L.A.Z - L.B.Z) * k + L.B.Z;
            }

            return new GAPoint(Hx, Hy, Hz);
        }

        /// <summary>
        /// возвращает кратчайшую линию между двумя другими линиями
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <param name="ShrinkToLine"> найденные точки сокращаются до границ указанных линий</param>
        /// <returns>
        /// если линии соосны возвращает линию (l1.A l1.A) - длинна 0
        /// если линии параллельны возвращает линию (l1.A, спроектированная точка )
        /// если линии пересекаются возвращает линию (точка пересечения, точка пересечения) - длинна 0
        /// </returns>
        public static GALine PerpendicularToTwoLines(GALine l1, GALine l2, bool ShrinkToLine = false)
        {
            #region если линии имеют общую точку, вернем эту точку

            if (is_the_same_coordinates(l1.A, l2.A))
            {
                return new GALine(l1.A, l1.A);
            }
            else
            if (is_the_same_coordinates(l1.A, l2.B))
            {
                return new GALine(l1.A, l1.A);
            }
            else
            if (is_the_same_coordinates(l1.B, l2.A))
            {
                return new GALine(l1.B, l1.B);
            }
            else
            if (is_the_same_coordinates(l1.B, l2.B))
            {
                return new GALine(l1.B, l1.B);
            }

            #endregion


            GAPoint vector_of_p1 = new GAPoint(l1.A.X - l1.B.X, l1.A.Y - l1.B.Y, l1.A.Z - l1.B.Z);

            GAPlane plane_perpendicular_to_l1 = new GAPlane(l1.A, new double[] { vector_of_p1.X, vector_of_p1.Y, vector_of_p1.Z });

            GALine l2_projection_on_perp_plane = new GALine(
                new GAPoint(PerpendicularToPoint(plane_perpendicular_to_l1, l2.A)), new GAPoint(PerpendicularToPoint(plane_perpendicular_to_l1, l2.B))
                );

            // если линия l2 спроектирована в точку 
            if (l2_projection_on_perp_plane.length < TOLerance)
            {
                #region линии параллельны или колинеальны

                double dist = distance(l1.A, l2_projection_on_perp_plane.A); 
                if(dist < TOLerance)
                {
                    //расстояние между точками равно нулю, значит линии соосны
                    // collinear
                    return new GALine(l1.A, l1.A);
                }else
                {
                    //расстояние между точками больше нуля, значит линии параллельны
                    // parallel
                    return new GALine(l1.A, l2_projection_on_perp_plane.A);
                }

                #endregion
            }

            // если линия P2 спроектирована линию, но проходит через точку первой линии, значит две линии в одной плоскости
            if (is_points_on_line(l2_projection_on_perp_plane.A, l2_projection_on_perp_plane.B, l1.A))
            {
                #region линии в одной плоскости

                //сгенерируем любую плоскость проходящую не через линию l2
                GAPlane g_plane = null;
                Random rnd = new Random();
                GAPoint random_point = new GAPoint(rnd.Next(0, 10), rnd.Next(0, 10), rnd.Next(0, 10));

                int ccc = 0;
                while (is_point_on_plane(l1.A, l1.B, l2.A, random_point))
                {
                    ccc++;
                    random_point = new GAPoint(rnd.Next(0, 10), rnd.Next(0, 10), rnd.Next(0, 10));
                    if (ccc > 1000) { return null; }
                }
                g_plane = new GAPlane(l1.A, l1.B, random_point);

                // найдем пересечение
                GAPoint g_point = plane_line_intersection(g_plane, l2, false);

                return new GALine(g_point, g_point);

                #endregion
            }

            //перпендикуляр от точки l1.A к спроектированную линию, точка пересечения это проекция одной из необходимых точек
            GAPoint tp1 = PerpendicularToPoint(l2_projection_on_perp_plane, l1.A);

            //линия проекции (связи) от tp1 по направлению к линии 2
            GALine l_tp1_l2 = new GALine(tp1, new GAPoint(tp1.X + vector_of_p1.X, tp1.Y + vector_of_p1.Y, tp1.Z + vector_of_p1.Z));

            #region пересечение линии связи и линии l2 - необходимая точка на линии 2

            GAPoint point2 = null;
            if (is_points_on_line(l_tp1_l2.A, l_tp1_l2.B, l2.A))
            {
                point2 = l2.A;
            }else
            {
                //сгенерируем любую плоскость проходящую не через линию l2
                GAPlane g_plane_2 = null;
                Random rnd2 = new Random();
                GAPoint random_point_2 = new GAPoint(rnd2.Next(0, 10), rnd2.Next(0, 10), rnd2.Next(0, 10));
                while (is_point_on_plane(l_tp1_l2.A, l_tp1_l2.B, l2.A, random_point_2))
                {
                    random_point_2 = new GAPoint(rnd2.Next(0, 10), rnd2.Next(0, 10), rnd2.Next(0, 10));
                }
                g_plane_2 = new GAPlane(l_tp1_l2.A, l_tp1_l2.B, random_point_2);

                // найдем пересечение
                point2 = plane_line_intersection(g_plane_2, l2, false);
            }
            
            #endregion

            //перпендикуляр от точки 2 на линию 1 - первая необходимая точка
            GAPoint point1 = PerpendicularToPoint(l1, point2);

            if (ShrinkToLine)
            {
                GAPoint i1Clamped = ClampPointToLine(point1, l1);
                GAPoint i2Clamped = ClampPointToLine(point2, l2);
                return new GALine(i1Clamped, i2Clamped);
            }
            else
            {
                return new GALine(point1, point2);
            }
        }
        /// <summary>
        /// help function for PerpendicularToTwoLines
        /// </summary>
        /// <param name="pointToClamp"></param>
        /// <param name="lineToClampTo"></param>
        /// <returns></returns>
        static GAPoint ClampPointToLine(GAPoint pointToClamp, GALine lineToClampTo)
        {
            GAPoint clampedPoint = new GAPoint();
            double minX, minY, minZ, maxX, maxY, maxZ;
            if (lineToClampTo.A.X <= lineToClampTo.B.X)
            {
                minX = lineToClampTo.A.X;
                maxX = lineToClampTo.B.X;
            }
            else
            {
                minX = lineToClampTo.B.X;
                maxX = lineToClampTo.A.X;
            }
            if (lineToClampTo.A.Y <= lineToClampTo.B.Y)
            {
                minY = lineToClampTo.A.Y;
                maxY = lineToClampTo.B.Y;
            }
            else
            {
                minY = lineToClampTo.B.Y;
                maxY = lineToClampTo.A.Y;
            }
            if (lineToClampTo.A.Z <= lineToClampTo.B.Z)
            {
                minZ = lineToClampTo.A.Z;
                maxZ = lineToClampTo.B.Z;
            }
            else
            {
                minZ = lineToClampTo.B.Z;
                maxZ = lineToClampTo.A.Z;
            }
            clampedPoint.X = (pointToClamp.X < minX) ? minX : (pointToClamp.X > maxX) ? maxX : pointToClamp.X;
            clampedPoint.Y = (pointToClamp.Y < minY) ? minY : (pointToClamp.Y > maxY) ? maxY : pointToClamp.Y;
            clampedPoint.Z = (pointToClamp.Z < minZ) ? minZ : (pointToClamp.Z > maxZ) ? maxZ : pointToClamp.Z;
            return clampedPoint;
        }

        #endregion

        #region plane intersection

        /// <summary>
        /// возвращает точку пересечения плоскости и линии
        /// </summary>
        /// <param name="plane_">плоскость</param>
        /// <param name="line_">линия</param>
        /// <param name="inside_line_only">true-точка лежит на плоскости и линии иначе возвращает Null, false-точка лежит на линии но не обязательно в нутри линии</param>
        /// <param name="tolerance">tolerance</param>
        /// <returns></returns>
        public static GAPoint plane_line_intersection(GAPlane plane_, GALine line_, bool inside_line_only = true, double tolerance = 1e-5)
        {
            //double t = (line_.A.X * plane_.A + line_.A.Y * plane_.B + line_.A.Z * plane_.C - plane_.D) / (plane_.A * (line_.B.X - line_.A.X) + plane_.B * (line_.B.Y - line_.A.Y) + plane_.C * (line_.B.Z - line_.A.Z));

            double t = (line_.A.X * plane_.A + line_.A.Y * plane_.B + line_.A.Z * plane_.C + plane_.D) / (plane_.A * (line_.A.X - line_.B.X) + plane_.B * (line_.A.Y - line_.B.Y) + plane_.C * (line_.A.Z - line_.B.Z));

            if (t == 0) { return null; }

            GAPoint p = new GAPoint(
                line_.A.X - t * line_.A.X + t * line_.B.X,
                line_.A.Y - t * line_.A.Y + t * line_.B.Y,
                line_.A.Z - t * line_.A.Z + t * line_.B.Z,
                line_.description, line_.index
                );

            if (inside_line_only)
            {
                if (is_point_inside_line(line_, p, tolerance)) { return p; } else { return null; }
            }

            return p;
        }

        /// <summary>
        /// возвращает список точек пересечения плоскости и линий, только тех что внутри линии
        /// </summary>
        /// <param name="plane_">plane</param>
        /// <param name="lines_">lines</param>
        /// <returns></returns>
        public static List<GAPoint> plane_line_intersection_points(GAPlane plane_, List<GALine> lines_)
        {
            List<GAPoint> points = new List<GAPoint>();
            foreach (GALine line in lines_)
            {
                GAPoint pp = plane_line_intersection(plane_, line);
                if (pp != null)
                {
                    points.Add(pp);
                }
            }
            return points;
        }

        /// <summary>
        /// возвращает список оставшихся линий
        /// </summary>
        /// <param name="plane_">plane</param>
        /// <param name="lines_">lines</param>
        /// <returns></returns>
        public static List<GALine> plane_line_intersection_lines(GAPlane plane_, List<GALine> lines_)
        {
            List<GALine> lines = new List<GALine>();

            GAPlane plane_0 = new GAPlane(plane_.A, plane_.B, plane_.C, 0);

            foreach (GALine line_ in lines_)
            {
                double t = (line_.A.X * plane_.A + line_.A.Y * plane_.B + line_.A.Z * plane_.C + plane_.D) / (plane_.A * (line_.A.X - line_.B.X) + plane_.B * (line_.A.Y - line_.B.Y) + plane_.C * (line_.A.Z - line_.B.Z));

                if (t == 0) { continue; }

                GAPoint p = new GAPoint(
                    line_.A.X - t * line_.A.X + t * line_.B.X,
                    line_.A.Y - t * line_.A.Y + t * line_.B.Y,
                    line_.A.Z - t * line_.A.Z + t * line_.B.Z
                    );

                if (!is_point_inside_line(line_, p)) { continue; }

                GALine l1 = new GALine(line_.A, p, line_.index, 0, line_.description);
                GALine l2 = new GALine(line_.B, p, line_.index, 0, line_.description);

                //GALine dist1 = new GALine(l1.center_point, new GAPoint());
                //GALine dist2 = new GALine(l2.center_point, new GAPoint());

                double dist1 = distance_plane_point(l1.center_point, plane_0);
                double dist2 = distance_plane_point(l2.center_point, plane_0);

                if (dist1 < dist2)
                {
                    lines.Add(l1);
                }
                else
                {
                    lines.Add(l2);
                }
            }

            return lines;
        }

        /// <summary>
        /// отсеченная шестиугольная призма
        /// </summary>
        /// <param name="plane_"></param>
        /// <param name="hex_prism">Hexagon prism</param>
        /// <returns></returns>
        public static GAHexagonPrismCorrect plane_hex_intersection(GAPlane plane_, GAHexagonPrismCorrect hex_prism)
        {
            GAPlane plane_center = new GAPlane(plane_.A, plane_.B, plane_.C, 0);
            List<GAPoint> points = new List<GAPoint>();
            List<GALine> lines = new List<GALine>();
            lines = hex_prism.lines;
            int cc = lines.Count;

            List<int> remove_indexes = new List<int>();

            points = plane_line_intersection_points(plane_, hex_prism.lines);

            List<GALine> ll = new List<GALine>();
            for (int i = 0; i < points.Count; i++)
            {
                for (int j = i + 1; j < points.Count; j++)
                {
                    // добавляем линию пересечения с плоскостью
                    GALine lll = new GALine(points[i], points[j]);

                    if (is_point_on_plane(lll.center_point, hex_prism.points[0], hex_prism.points[1], hex_prism.points[2]) ||
                        is_point_on_plane(lll.center_point, hex_prism.points[6], hex_prism.points[7], hex_prism.points[8]) ||

                        is_point_on_plane(lll.center_point, hex_prism.points[0], hex_prism.points[1], hex_prism.points[6]) ||
                        is_point_on_plane(lll.center_point, hex_prism.points[1], hex_prism.points[2], hex_prism.points[7]) ||
                        is_point_on_plane(lll.center_point, hex_prism.points[2], hex_prism.points[3], hex_prism.points[8]) ||
                        is_point_on_plane(lll.center_point, hex_prism.points[3], hex_prism.points[4], hex_prism.points[9]) ||
                        is_point_on_plane(lll.center_point, hex_prism.points[4], hex_prism.points[5], hex_prism.points[10]) ||
                        is_point_on_plane(lll.center_point, hex_prism.points[5], hex_prism.points[0], hex_prism.points[11])
                        )
                    {
                        ll.Add(new GALine(points[i], points[j], i, 0, (int)word.пересечение));
                    }

                }

                // делим основные линии пополам в точке пересечения с плоскостью
                for (int k = 0; k < cc; k++)
                {
                    GALine l54 = hex_prism.lines[k];
                    if (is_point_inside_line(l54, points[i]))
                    {
                        if (!remove_indexes.Contains(k)) { remove_indexes.Add(k); }
                        
                        int type = -1;
                        switch(l54.description)
                        {
                            case (int)word.ребро: type = (int)word.ребро_урезанное; break;
                            case (int)word.основа: type = (int)word.основа_урезанная; break;
                            case (int)word.верх: type = (int)word.верх_урезанный; break;
                        }

                        GALine l1 = new GALine(l54.A, points[i], l54.index, 0, type);
                        GALine l2 = new GALine(l54.B, points[i], l54.index, 0, type);

                        double dist12 = distance_plane_point(l1.center_point, plane_center);
                        double dist22 = distance_plane_point(l2.center_point, plane_center);

                        double dist1 = new GALine(l1.center_point, new GAPoint()).length;
                        double dist2 = new GALine(l2.center_point, new GAPoint()).length;

                        if (dist12 <= dist22)
                        {
                            ll.Add(l1);
                        }
                        else
                        {
                            ll.Add(l2);
                        }
                        //if (dist1 <= dist2)
                        //{
                        //    ll.Add(l1);
                        //}
                        //else
                        //{
                        //    ll.Add(l2);
                        //}

                    }
                }
            }

            for (int i = 0; i < hex_prism.lines.Count; i++)
            {
                if (remove_indexes.Contains(i)) { continue; }
                GALine lol = new GALine(new GAPoint(), hex_prism.lines[i].center_point);
                GAPoint p_pl = plane_line_intersection(plane_, lol, false);
                if (p_pl == null) { continue; }
                GALine lol_m = new GALine(new GAPoint(), p_pl);
                if (lol.length > lol_m.length)
                {
                    remove_indexes.Add(i);
                }
            }

            remove_indexes.Sort();

            for (int i = remove_indexes.Count; i > 0; i--) // удаляем основные линии которые пересекаются с плоскостью сечения + удаляем основные линии за плоскостью сечения
            {
                hex_prism.lines.RemoveAt(remove_indexes[i - 1]);
            }

            hex_prism.lines.AddRange(ll);

            return hex_prism;
        }

        /// <summary>
        /// отсеченная пятиугольная пирамида
        /// </summary>
        /// <param name="plane_">плоскость сечения</param>
        /// <param name="pentprism_cor_">пятиугольная правильная пирамида</param>
        /// <returns>отсеченная пятиугольная правильная пирамида</returns>
        public static GAPentagonPiramCorrect plane_pentpiram_intersection(GAPlane plane_, GAPentagonPiramCorrect pentprism_cor_)
        {
            GAPlane plane_center = new GAPlane(plane_.A, plane_.B, plane_.C, 0);

            List<int> remove_indexes = new List<int>();

            List<GAPoint> points = plane_line_intersection_points(plane_, pentprism_cor_.lines);

            List<GAPoint> pp16 = new List<GAPoint>(); // контур сечения
            List<GAPoint> pp00 = new List<GAPoint>(); // контур сечения
            List<GALine> ll16 = new List<GALine>(); // контур сечения
            List<GALine> ll00 = new List<GALine>(); // контур сечения

            List<GALine> ll = new List<GALine>(); // оставшиеся линии

            for (int i = 0; i < points.Count; i++)
            {
                #region добавляем точки контура пересечения

                
                if (points[i].index >= 16)
                {
                    pp16.Add(points[i]);
                }
                else
                {
                    pp00.Add(points[i]);
                }
                #endregion

                #region добавление основных линий в список для удаления

                // делим основные линии пополам в точке пересечения с плоскостью
                int cc = pentprism_cor_.lines.Count;
                for (int k = 0; k < cc; k++)
                {
                    GALine l54 = pentprism_cor_.lines[k];
                    if (is_point_inside_line(l54, points[i]))
                    {
                        if (!remove_indexes.Contains(k)) { remove_indexes.Add(k); }

                        int type = -1;
                        switch (l54.description)
                        {
                            case (int)word.ребро: type = (int)word.ребро_урезанное; break;
                            case (int)word.основа: type = (int)word.основа_урезанная; break;
                        }

                        GALine l1 = new GALine(l54.A, points[i], l54.index, 0, type);
                        GALine l2 = new GALine(l54.B, points[i], l54.index, 0, type);

                        double dist12 = distance_plane_point(l1.center_point, plane_center);
                        double dist22 = distance_plane_point(l2.center_point, plane_center);

                        double dist1 = new GALine(l1.center_point, new GAPoint()).length;
                        double dist2 = new GALine(l2.center_point, new GAPoint()).length;

                        if (dist12 <= dist22)
                        {
                            ll.Add(l1);
                        }
                        else
                        {
                            ll.Add(l2);
                        }
                    }
                }

                #endregion
            }

            if (pp00.Count == 2) // если пересечена основа
            {
                ll00.Add(new GALine(pp00[0], pp00[1], 0, 0, (int)word.пересечение));
            }

            if (pp16.Count > 2 && pp00.Count < 1) // если пересечены только ребра
            {
                for (int i = 0; i < pp16.Count - 1; i++)
                {
                    ll16.Add(new GALine(pp16[i], pp16[i + 1], pp16[i].index, 0, (int)word.пересечение));
                }
                ll16.Add(new GALine(pp16[pp16.Count-1], pp16[0], pp16[0].index, 0, (int)word.пересечение));
            }

            // если пересечены и основа и ребра
            if (pp16.Count > 2 && ll00.Count > 0)
            {
                do
                {
                    int ind = 0;
                    double dist = distance_points(ll00[ll00.Count - 1].B, pp16[0]);
                    for (int i = 1; i < pp16.Count ; i++)
                    {
                        double dist2 = distance_points(ll00[ll00.Count - 1].B, pp16[i]);
                        if (dist2 <= dist)
                        {
                            dist = dist2;
                            ind = i;
                        }
                    }

                    ll00.Add(new GALine(ll00[ll00.Count - 1].B, pp16[ind], pp16[ind].index, 0, (int)word. пересечение));
                    pp16.RemoveAt(ind);
                } while (pp16.Count > 0);

                ll00.Add(new GALine(ll00[ll00.Count - 1].B, ll00[0].A, ll00[0].index, 0, (int)word.пересечение));
            }


            for (int i = 0; i < pentprism_cor_.lines.Count; i++)
            {
                if (remove_indexes.Contains(i)) { continue; }
                GALine lol = new GALine(new GAPoint(), pentprism_cor_.lines[i].center_point); // линия от нач. координат до центра норм. линии
                GAPoint p_pl = plane_line_intersection(plane_, lol, false); // точка пересечения нов. линии и плоскости сечения
                if (p_pl == null) { continue; } // если пересечения нет, то линия находится до плоскости и не удаляется
                GALine lol_m = new GALine(new GAPoint(), p_pl); // для нахождения расстояния от нач. координат до точки пересечения с плоскостью
                if (lol.length > lol_m.length) // если от нач. координат - центр линии дальше чем точка пересечения этой линии - то линия удаляется
                {
                    remove_indexes.Add(i);
                }
            }

            remove_indexes.Sort();

            for (int i = remove_indexes.Count; i > 0; i--) // удаляем основные линии которые пересекаются с плоскостью сечения + удаляем основные линии за плоскостью сечения
            {
                pentprism_cor_.lines.RemoveAt(remove_indexes[i - 1]);
            }

            pentprism_cor_.lines.AddRange(ll);
            pentprism_cor_.lines.AddRange(ll00);
            pentprism_cor_.lines.AddRange(ll16);

            pentprism_cor_.points.AddRange(points);
            return pentprism_cor_;

        }

        public static GACilinder plane_cilindr_intersection(GAPlane plane_, GACilinder cilindr_cor_)
        {
            GACilinder c = (GACilinder)cilindr_cor_.Clone();

            c.ribs = new List<GALine>();
            c.top_points = new List<GAPoint>();
            c.base_points = new List<GAPoint>();
            c.top_lines = new List<GALine>();
            c.base_lines = new List<GALine>();

            c.section_lines = new List<GALine>();
            c.section_points = new List<GAPoint>();
            
            // точки на ребрах
            List<GAPoint> cilindr_intersection_points = plane_line_intersection_points(plane_, cilindr_cor_.ribs);
            

            // ребра от основы к точкам
            List<GALine> cilinder_cuted_ribs = plane_line_intersection_lines(plane_, cilindr_cor_.ribs);
            c.ribs.AddRange(cilinder_cuted_ribs);

            // точки пересечения с верхом
            List<GAPoint> cilindr_intersection_points_top = plane_line_intersection_points(plane_, cilindr_cor_.top_lines);
            c.top_points.AddRange(cilindr_intersection_points_top);

            //точки пересечения с основой
            List<GAPoint> cilindr_intersection_points_base = plane_line_intersection_points(plane_, cilindr_cor_.base_lines);
            c.base_points.AddRange(cilindr_intersection_points_base);
            

            #region точки верха и низа до плоскости, остальные скрыть

            List<GAPoint> cilindr_cuted_top_points = new List<GAPoint>();
            List<GAPoint> cilindr_cuted_base_points = new List<GAPoint>();

            List<GALine> cilindr_cuted_top_lines = new List<GALine>(); // микро линии  верха
            List<GALine> cilindr_cuted_base_lines = new List<GALine>();// микро линии низа

            double cilindr_plane_distance = distance_plane_point(new GAPoint(), plane_); // расстояние от О до плоскости сечения
            GAPlane cilindr_zero_plane = new GAPlane(plane_.A, plane_.B, plane_.C, 0); // плоскость // к сечению через О
            foreach (GAPoint p in cilindr_cor_.base_points)
            {
                double cilindr_dist1 = distance_plane_point(p, cilindr_zero_plane); //расстояние от точки до плоскости через О
                if (cilindr_dist1 <= cilindr_plane_distance)
                {
                    cilindr_cuted_base_points.Add(p);

                    if (cilindr_cuted_base_points.Count > 1)
                    {
                        cilindr_cuted_base_lines.Add(new GALine(cilindr_cuted_base_points[cilindr_cuted_base_points.Count - 2], cilindr_cuted_base_points[cilindr_cuted_base_points.Count - 1]));
                    }
                }

            }
            if (cilindr_cuted_base_points.Count > 1)
            {
                cilindr_cuted_base_lines.Add(new GALine(cilindr_cuted_base_points[cilindr_cuted_base_points.Count - 1], cilindr_cuted_base_points[0]));
            }

            foreach (GAPoint p in cilindr_cor_.top_points)
            {
                double cilindr_dist1 = distance_plane_point(p, cilindr_zero_plane); //расстояние от точки до плоскости через О
                if (cilindr_dist1 <= cilindr_plane_distance)
                {
                    cilindr_cuted_top_points.Add(p);

                    if (cilindr_cuted_top_points.Count > 1)
                    {
                        cilindr_cuted_top_lines.Add(new GALine(cilindr_cuted_top_points[cilindr_cuted_top_points.Count - 2], cilindr_cuted_top_points[cilindr_cuted_top_points.Count - 1]));
                    }
                }

            }
            if (cilindr_cuted_top_points.Count > 1)
            {
                cilindr_cuted_top_lines.Add(new GALine(cilindr_cuted_top_points[cilindr_cuted_top_points.Count - 1], cilindr_cuted_top_points[0]));
            }

            #endregion

            c.base_points.AddRange(cilindr_cuted_base_points);
            c.top_points.AddRange(cilindr_cuted_top_points);

            c.base_lines.AddRange(cilindr_cuted_base_lines);
            c.top_lines.AddRange(cilindr_cuted_top_lines);

            #region сечение

            List<GAPoint> cilindr_section_points_temp = new List<GAPoint>();
            List<GAPoint> cilindr_section_points_spline1 = new List<GAPoint>();
            List<GAPoint> cilindr_section_points_spline2 = new List<GAPoint>();

            List<GALine> cilindr_section_lines_spline1 = new List<GALine>();
            List<GALine> cilindr_section_lines_spline2 = new List<GALine>();

            cilindr_section_points_temp.AddRange(cilindr_intersection_points);
            cilindr_section_points_temp.AddRange(cilindr_intersection_points_top);
            cilindr_section_points_temp.AddRange(cilindr_intersection_points_base);

            //GAPoint cilindr_temp_point = cilindr_section_points_temp[0];

            //for(int i=0; i < cilindr_section_points_temp.Count; i++)
            {
                //double cilindr_dist_tr = 0;
                //if (cilindr_intersection_points_top.Count == 0 && cilindr_intersection_points_base.Count == 0)
                //{
                int ind = 0;
                bool line1 = true;
                for (int j = 0; j < cilindr_intersection_points.Count; j++)
                {
                    ind = cilindr_intersection_points[j].index;
                    switch (line1)
                    {
                        case true:
                            if (cilindr_section_points_spline1.Count == 0) { cilindr_section_points_spline1.Add(cilindr_intersection_points[j]); continue; }
                            if (cilindr_section_points_spline1[j - 1].index + 1 == ind) { cilindr_section_points_spline1.Add(cilindr_intersection_points[j]); continue; }
                            else
                            {
                                line1 = false;
                                j--;
                                continue;
                            }
                            break;
                        case false:
                            if (cilindr_section_points_spline2.Count == 0) { cilindr_section_points_spline2.Add(cilindr_intersection_points[j]); continue; }
                            //if (cilindr_section_points_spline2[j - 1].index + 1 == ind) {cilindr_section_points_spline1.Add(cilindr_intersection_points[j]);}
                            cilindr_section_points_spline2.Add(cilindr_intersection_points[j]);
                            break;
                    }
                }
                //}
            }

            if (cilindr_intersection_points_top.Count == 0 && cilindr_intersection_points_base.Count == 0)
            {
                c.section_points.AddRange(cilindr_section_points_spline1);
                c.section_points.AddRange(cilindr_section_points_spline2);
                sort_by_distance(c.section_points);
            }
            
            if (cilindr_intersection_points_top.Count == 0 && cilindr_intersection_points_base.Count > 1)
            {
                c.section_points.Add(cilindr_intersection_points_base[0]);
                c.section_points.AddRange(cilindr_section_points_spline1);
                c.section_points.AddRange(cilindr_section_points_spline2);
                sort_by_distance(c.section_points);
                c.section_points.Add(cilindr_intersection_points_base[1]);
            }
            if (cilindr_intersection_points_top.Count > 1 && cilindr_intersection_points_base.Count == 0)
            {
                c.section_points.Add(cilindr_intersection_points_top[0]);
                c.section_points.AddRange(cilindr_section_points_spline1);
                c.section_points.AddRange(cilindr_section_points_spline2);
                sort_by_distance(c.section_points);
                c.section_points.Add(cilindr_intersection_points_top[1]);
            }
            if (cilindr_intersection_points_top.Count > 1 && cilindr_intersection_points_base.Count > 1)
            {
                double dist_spline1 = 0;
                double dist_spline2 = 0;
                double dist_spline3 = 0;
                double dist_spline4 = 0;
                int ind_s1 = 1;
                int ind_s2 = 1;

                if (cilindr_intersection_points_top.Count == 1) { cilindr_intersection_points_top.Add(cilindr_intersection_points_top[0]); }
                foreach (GAPoint p in cilindr_section_points_spline1)
                {
                    if (cilindr_intersection_points_top.Count > 1)
                    {
                        dist_spline1 += Math.Round(distance_points(cilindr_intersection_points_top[0], p), 5);
                        dist_spline2 += Math.Round(distance_points(cilindr_intersection_points_top[1], p), 5);
                    }

                    if (cilindr_intersection_points_base.Count > 1)
                    {
                        dist_spline3 += Math.Round(distance_points(cilindr_intersection_points_base[0], p), 5);
                        dist_spline4 += Math.Round(distance_points(cilindr_intersection_points_base[1], p), 5);
                    }

                }
                if (dist_spline1 < dist_spline2)
                {
                    ind_s1 = 0;
                }
                if (dist_spline3 < dist_spline4)
                {
                    ind_s2 = 0;
                }

                List<GAPoint> res = new List<GAPoint>();
                if (ind_s1 == 0)
                {
                    if (cilindr_intersection_points_top.Count > 1)
                    {
                        c.section_points.Add(cilindr_intersection_points_top[0]);
                        res.Add(cilindr_intersection_points_top[1]);
                    }
                    c.section_points.AddRange(cilindr_section_points_spline1);
                    res.AddRange(cilindr_section_points_spline2);

                }
                else
                {
                    if (cilindr_intersection_points_top.Count > 1)
                    {
                        c.section_points.Add(cilindr_intersection_points_top[1]);
                        res.Add(cilindr_intersection_points_top[0]);
                    }
                    c.section_points.AddRange(cilindr_section_points_spline1);
                    res.AddRange(cilindr_section_points_spline2);
                }
                if (ind_s2 == 0)
                {
                    if (cilindr_intersection_points_base.Count > 1)
                    {
                        c.section_points.Add(cilindr_intersection_points_base[0]);
                        res.Add(cilindr_intersection_points_base[1]);
                    }

                    sort_by_distance(c.section_points);
                    sort_by_distance(res);
                }
                else
                {
                    if (cilindr_intersection_points_base.Count > 1)
                    {
                        c.section_points.Add(cilindr_intersection_points_base[1]);
                        res.Add(cilindr_intersection_points_base[0]);
                    }
                    sort_by_distance(c.section_points);
                    sort_by_distance(res);
                }

                res.Reverse();
                c.section_points.AddRange(res);
            }

            for(int i =0; i<c.section_points.Count; i++)
            {
                c.section_points[i].index = i + 1;
                c.section_points[i].description = (int)word.пересечение;
            }

            for (int j = 0; j < c.section_points.Count - 1; j++)
            {
                c.section_lines.Add(new GALine(c.section_points[j], c.section_points[j + 1], j+1, 0, (int)word.пересечение));
            }
            if (c.section_points.Count > 1) { c.section_lines.Add(new GALine(c.section_points[c.section_points.Count-1], c.section_points[0])); }

            #endregion

            return c;
        }

        /// <summary>
        /// plane cone intersection - returns sectioned cone
        /// </summary>
        /// <param name="plane_"></param>
        /// <param name="cone_cor_"></param>
        /// <returns></returns>
        public static GACone plane_cone_intersection(GAPlane plane_, GACone cone_cor_)
        {
            GAPlane plane_0 = new GAPlane(plane_.A, plane_.B, plane_.C, 0);
            GACone c = (GACone)cone_cor_.Clone();

            c.ribs = new List<GALine>();
            c.base_points_help = new List<GAPoint>();
            c.base_lines_help = new List<GALine>();//

            c.ribs = new List<GALine>();//
            c.ribs_help = new List<GALine>();//

            c.section_points = new List<GAPoint>();//
            c.section_points_help = new List<GAPoint>();//
            c.section_lines = new List<GALine>();
            c.section_lines_help = new List<GALine>();

            // точки пересечения
            c.section_points.AddRange(plane_line_intersection_points(plane_, cone_cor_.base_lines_help));
            if (c.section_points.Count == 2)
            {
                c.section_points[0].index = -1;
                c.section_points[1].index = -1;
            }
            c.section_points.AddRange(plane_line_intersection_points(plane_, cone_cor_.ribs));
            sort_by_distance(c.section_points);

            for (int i = 0; i < c.section_points.Count; i++)
            {
                c.section_points[i].index = i + 1;
            }

            // точки пересечения - help
            c.section_points_help.AddRange(plane_line_intersection_points(plane_, cone_cor_.base_lines_help));
            if (c.section_points_help.Count == 2)
            {
                c.section_points_help[0].index = -1;
                c.section_points_help[1].index = -1;
            }
            c.section_points_help.AddRange(plane_line_intersection_points(plane_, cone_cor_.ribs_help));
            sort_by_distance(c.section_points_help);

            for (int i = 0; i < c.section_points_help.Count; i++)
            {
                c.section_points_help[i].index = i + 1;
            }


            // ребра от основы к точкам
            c.ribs.AddRange(plane_line_intersection_lines(plane_, cone_cor_.ribs));
            c.ribs_help.AddRange(plane_line_intersection_lines(plane_, cone_cor_.ribs_help));

            double dist = distance_plane_point(new GAPoint(), plane_);
            c.base_lines_help.AddRange(plane_line_intersection_lines(plane_, cone_cor_.base_lines_help)); // пересечение с основой
            for(int i=0; i < cone_cor_.base_lines_help.Count; i++)
            {
                double dist2 = distance_plane_point(cone_cor_.base_lines_help[i].center_point, plane_0);
                if (dist2 <= dist)
                {
                    c.base_lines_help.Add(cone_cor_.base_lines_help[i]);
                }
            }
            
            c.base_points_help.AddRange(plane_line_intersection_points(plane_, cone_cor_.base_lines_help));
            foreach (GAPoint p in cone_cor_.base_points_help)
            {
                double dist2 = distance_plane_point(p, plane_0);
                if (dist2 <= dist)
                {
                    c.base_points_help.Add(p);
                }
            }
            sort_by_distance(c.base_points_help);
            return c;
        }

        /// <summary>
        /// plane piramida intersection - return sectioned piramida
        /// </summary>
        /// <param name="plane_"></param>
        /// <param name="piramida_"></param>
        /// <returns></returns>
        public static GAPiramida plane_piramida_intersection(GAPlane plane_, GAPiramida piramida_)
        {
            GAPlane plane_center = new GAPlane(plane_.A, plane_.B, plane_.C, 0);

            List<int> remove_indexes_ribs = new List<int>();
            List<int> remove_indexes_ground = new List<int>();

            List<GAPoint> points_ground = plane_line_intersection_points(plane_, piramida_.ground);
            List<GAPoint> points_ribs = plane_line_intersection_points(plane_, piramida_.ribs);
            List<GAPoint> points_section = new List<GAPoint>();

            List<int> indexes = new List<int>();
            points_section.AddRange(points_ribs);

            while(points_ground.Count > 0)
            {
                indexes = new List<int>();
                for (int i = 0; i < 103; i++)
                {
                    indexes.Add(i);
                }

                List<int> to_remove = new List<int>();
                for (int j=0; j < points_section.Count; j++)
                {
                    if (indexes.Contains(points_section[j].index))
                    {
                        to_remove.Add(points_section[j].index); continue;
                    }
                }
                to_remove.Sort();
                for(int k = to_remove.Count-1;k>=0; k--)
                {
                    indexes.RemoveAt(to_remove[k]); continue;
                }

                points_section.Add(points_ground[0]);
                points_section[points_section.Count - 1].index = indexes[0];
                points_ground.RemoveAt(0);
                

            }

            //points_section.AddRange(points_ground);

            //for(int i = 0; i< points_section.Count; i++)
            //{
            //    points_section[i].index = i;
            //}
            sort_by_contour(points_section);

            List<GALine> ll_ribs = new List<GALine>(); // оставшиеся линии
            List<GALine> ll_ground = new List<GALine>(); // оставшиеся линии

            for (int i = 0; i < points_section.Count; i++)
            {
                #region добавление основных линий в список для удаления

                // делим основные линии пополам в точке пересечения с плоскостью
                int cc = piramida_.ribs.Count;
                for (int k = 0; k < cc; k++)
                {
                    GALine l54 = piramida_.ribs[k];
                    if (is_point_inside_line(l54, points_section[i]))
                    {
                        if (!remove_indexes_ribs.Contains(k)) { remove_indexes_ribs.Add(k); }

                        int type = -1;
                        switch (l54.description)
                        {
                            case (int)word.ребро: type = (int)word.ребро_урезанное; break;
                            case (int)word.основа: type = (int)word.основа_урезанная; break;
                        }

                        GALine l1 = new GALine(l54.A, points_section[i], l54.index, 0, type);
                        GALine l2 = new GALine(l54.B, points_section[i], l54.index, 0, type);

                        double dist12 = distance_plane_point(l1.center_point, plane_center);
                        double dist22 = distance_plane_point(l2.center_point, plane_center);

                        double dist1 = new GALine(l1.center_point, new GAPoint()).length;
                        double dist2 = new GALine(l2.center_point, new GAPoint()).length;

                        if (dist12 <= dist22)
                        {
                            ll_ribs.Add(l1);
                        }
                        else
                        {
                            ll_ribs.Add(l2);
                        }
                    }
                }


                int ccg = piramida_.ground.Count;
                for (int k = 0; k < ccg; k++)
                {
                    GALine l54 = piramida_.ground[k];
                    if (is_point_inside_line(l54, points_section[i]))
                    {
                        if (!remove_indexes_ground.Contains(k)) { remove_indexes_ground.Add(k); }

                        int type = -1;
                        switch (l54.description)
                        {
                            case (int)word.ребро: type = (int)word.ребро_урезанное; break;
                            case (int)word.основа: type = (int)word.основа_урезанная; break;
                        }

                        GALine l1 = new GALine(l54.A, points_section[i], l54.index, 0, type);
                        GALine l2 = new GALine(l54.B, points_section[i], l54.index, 0, type);

                        double dist12 = distance_plane_point(l1.center_point, plane_center);
                        double dist22 = distance_plane_point(l2.center_point, plane_center);

                        double dist1 = new GALine(l1.center_point, new GAPoint()).length;
                        double dist2 = new GALine(l2.center_point, new GAPoint()).length;

                        if (dist12 <= dist22)
                        {
                            ll_ground.Add(l1);
                        }
                        else
                        {
                            ll_ground.Add(l2);
                        }
                    }
                }
                #endregion
            }


            for (int i = 0; i < piramida_.ribs.Count; i++)
            {
                if (remove_indexes_ribs.Contains(i)) { continue; }
                GALine lol = new GALine(new GAPoint(), piramida_.ribs[i].center_point); // линия от нач. координат до центра норм. линии
                GAPoint p_pl = plane_line_intersection(plane_, lol, false); // точка пересечения нов. линии и плоскости сечения
                if (p_pl == null) { continue; } // если пересечения нет, то линия находится до плоскости и не удаляется
                GALine lol_m = new GALine(new GAPoint(), p_pl); // для нахождения расстояния от нач. координат до точки пересечения с плоскостью
                if (lol.length > lol_m.length) // если от нач. координат - центр линии дальше чем точка пересечения этой линии - то линия удаляется
                {
                    remove_indexes_ribs.Add(i);
                }
            }
            for (int i = 0; i < piramida_.ground.Count; i++)
            {
                if (remove_indexes_ground.Contains(i)) { continue; }
                GALine lol = new GALine(new GAPoint(), piramida_.ground[i].center_point); // линия от нач. координат до центра норм. линии
                GAPoint p_pl = plane_line_intersection(plane_, lol, false); // точка пересечения нов. линии и плоскости сечения
                if (p_pl == null) { continue; } // если пересечения нет, то линия находится до плоскости и не удаляется
                GALine lol_m = new GALine(new GAPoint(), p_pl); // для нахождения расстояния от нач. координат до точки пересечения с плоскостью
                if (lol.length > lol_m.length) // если от нач. координат - центр линии дальше чем точка пересечения этой линии - то линия удаляется
                {
                    remove_indexes_ground.Add(i);
                }
            }

            remove_indexes_ribs.Sort();
            remove_indexes_ground.Sort();

            for (int i = remove_indexes_ribs.Count; i > 0; i--) // удаляем основные линии которые пересекаются с плоскостью сечения + удаляем основные линии за плоскостью сечения
            {
                piramida_.ribs.RemoveAt(remove_indexes_ribs[i - 1]);
            }
            for (int i = remove_indexes_ground.Count; i > 0; i--) // удаляем основные линии которые пересекаются с плоскостью сечения + удаляем основные линии за плоскостью сечения
            {
                piramida_.ground.RemoveAt(remove_indexes_ground[i - 1]);
            }

            piramida_.ribs.AddRange(ll_ribs);
            piramida_.ground.AddRange(ll_ground);
            piramida_.points.AddRange(points_section);
            return piramida_;

        }

        /// <summary>
        /// plane prism intersection - returns sectioned prism
        /// </summary>
        /// <param name="plane_"></param>
        /// <param name="prism_"></param>
        /// <returns></returns>
        public static GAPrism plane_prism_intersection(GAPlane plane_, GAPrism prism_)
        {
            GAPlane plane_center = new GAPlane(plane_.A, plane_.B, plane_.C, 0);

            List<int> remove_indexes_ribs = new List<int>();
            List<int> remove_indexes_ground = new List<int>();
            List<int> remove_indexes_top = new List<int>();

            List<GAPoint> points_ground = plane_line_intersection_points(plane_, prism_.ground);
            List<GAPoint> points_top = plane_line_intersection_points(plane_, prism_.top);
            List<GAPoint> points_ribs = plane_line_intersection_points(plane_, prism_.ribs);
            List<GAPoint> points_section = new List<GAPoint>();

            List<int> indexes = new List<int>();
            points_section.AddRange(points_ribs);

            while (points_ground.Count > 0)
            {
                indexes = new List<int>();
                for (int i = 0; i < 103; i++)
                {
                    indexes.Add(i);
                }

                List<int> to_remove = new List<int>();
                for (int j = 0; j < points_section.Count; j++)
                {
                    if (indexes.Contains(points_section[j].index))
                    {
                        to_remove.Add(points_section[j].index); continue;
                    }
                }
                to_remove.Sort();
                for (int k = to_remove.Count - 1; k >= 0; k--)
                {
                    indexes.RemoveAt(to_remove[k]); continue;
                }

                points_section.Add(points_ground[0]);
                points_section[points_section.Count - 1].index = indexes[0];
                points_ground.RemoveAt(0);
            }

            while (points_top.Count > 0)
            {
                indexes = new List<int>();
                for (int i = 0; i < 103; i++)
                {
                    indexes.Add(i);
                }

                List<int> to_remove = new List<int>();
                for (int j = 0; j < points_section.Count; j++)
                {
                    if (indexes.Contains(points_section[j].index))
                    {
                        to_remove.Add(points_section[j].index); continue;
                    }
                }
                to_remove.Sort();
                for (int k = to_remove.Count - 1; k >= 0; k--)
                {
                    indexes.RemoveAt(to_remove[k]); continue;
                }

                points_section.Add(points_top[0]);
                points_section[points_section.Count - 1].index = indexes[0];
                points_top.RemoveAt(0);
            }

            sort_by_contour(points_section);

            List<GALine> ll_ribs = new List<GALine>(); // оставшиеся линии
            List<GALine> ll_ground = new List<GALine>(); // оставшиеся линии
            List<GALine> ll_top = new List<GALine>(); // оставшиеся линии

            for (int i = 0; i < points_section.Count; i++)
            {
                #region добавление основных линий в список для удаления

                // делим основные линии пополам в точке пересечения с плоскостью
                int cc = prism_.ribs.Count;
                for (int k = 0; k < cc; k++)
                {
                    GALine l54 = prism_.ribs[k];
                    if (is_point_inside_line(l54, points_section[i]))
                    {
                        if (!remove_indexes_ribs.Contains(k)) { remove_indexes_ribs.Add(k); }

                        int type = -1;
                        switch (l54.description)
                        {
                            case (int)word.ребро: type = (int)word.ребро_урезанное; break;
                            case (int)word.основа: type = (int)word.основа_урезанная; break;
                        }

                        GALine l1 = new GALine(l54.A, points_section[i], l54.index, 0, type);
                        GALine l2 = new GALine(l54.B, points_section[i], l54.index, 0, type);

                        double dist12 = distance_plane_point(l1.center_point, plane_center);
                        double dist22 = distance_plane_point(l2.center_point, plane_center);

                        double dist1 = new GALine(l1.center_point, new GAPoint()).length;
                        double dist2 = new GALine(l2.center_point, new GAPoint()).length;

                        if (dist12 <= dist22)
                        {
                            ll_ribs.Add(l1);
                        }
                        else
                        {
                            ll_ribs.Add(l2);
                        }
                    }
                }


                int ccg = prism_.ground.Count;
                for (int k = 0; k < ccg; k++)
                {
                    GALine l54 = prism_.ground[k];
                    if (is_point_inside_line(l54, points_section[i]))
                    {
                        if (!remove_indexes_ground.Contains(k)) { remove_indexes_ground.Add(k); }

                        int type = -1;
                        switch (l54.description)
                        {
                            case (int)word.ребро: type = (int)word.ребро_урезанное; break;
                            case (int)word.основа: type = (int)word.основа_урезанная; break;
                        }

                        GALine l1 = new GALine(l54.A, points_section[i], l54.index, 0, type);
                        GALine l2 = new GALine(l54.B, points_section[i], l54.index, 0, type);

                        double dist12 = distance_plane_point(l1.center_point, plane_center);
                        double dist22 = distance_plane_point(l2.center_point, plane_center);

                        double dist1 = new GALine(l1.center_point, new GAPoint()).length;
                        double dist2 = new GALine(l2.center_point, new GAPoint()).length;

                        if (dist12 <= dist22)
                        {
                            ll_ground.Add(l1);
                        }
                        else
                        {
                            ll_ground.Add(l2);
                        }
                    }
                }

                int cct = prism_.top.Count;
                for (int k = 0; k < cct; k++)
                {
                    GALine l54 = prism_.top[k];
                    if (is_point_inside_line(l54, points_section[i]))
                    {
                        if (!remove_indexes_top.Contains(k)) { remove_indexes_top.Add(k); }

                        int type = -1;
                        switch (l54.description)
                        {
                            case (int)word.ребро: type = (int)word.ребро_урезанное; break;
                            case (int)word.основа: type = (int)word.основа_урезанная; break;
                        }

                        GALine l1 = new GALine(l54.A, points_section[i], l54.index, 0, type);
                        GALine l2 = new GALine(l54.B, points_section[i], l54.index, 0, type);

                        double dist12 = distance_plane_point(l1.center_point, plane_center);
                        double dist22 = distance_plane_point(l2.center_point, plane_center);

                        //double dist1 = new GALine(l1.center_point, new GAPoint()).length;
                        //double dist2 = new GALine(l2.center_point, new GAPoint()).length;

                        if (dist12 <= dist22)
                        {
                            ll_top.Add(l1);
                        }
                        else
                        {
                            ll_top.Add(l2);
                        }
                    }
                }
                #endregion
            }


            for (int i = 0; i < prism_.ribs.Count; i++)
            {
                if (remove_indexes_ribs.Contains(i)) { continue; }
                GALine lol = new GALine(new GAPoint(), prism_.ribs[i].center_point); // линия от нач. координат до центра норм. линии
                GAPoint p_pl = plane_line_intersection(plane_, lol, false); // точка пересечения нов. линии и плоскости сечения
                if (p_pl == null) { continue; } // если пересечения нет, то линия находится до плоскости и не удаляется
                GALine lol_m = new GALine(new GAPoint(), p_pl); // для нахождения расстояния от нач. координат до точки пересечения с плоскостью
                if (lol.length > lol_m.length) // если от нач. координат - центр линии дальше чем точка пересечения этой линии - то линия удаляется
                {
                    remove_indexes_ribs.Add(i);
                }
            }
            for (int i = 0; i < prism_.ground.Count; i++)
            {
                if (remove_indexes_ground.Contains(i)) { continue; }
                GALine lol = new GALine(new GAPoint(), prism_.ground[i].center_point); // линия от нач. координат до центра норм. линии
                GAPoint p_pl = plane_line_intersection(plane_, lol, false); // точка пересечения нов. линии и плоскости сечения
                if (p_pl == null) { continue; } // если пересечения нет, то линия находится до плоскости и не удаляется
                GALine lol_m = new GALine(new GAPoint(), p_pl); // для нахождения расстояния от нач. координат до точки пересечения с плоскостью
                if (lol.length > lol_m.length) // если от нач. координат - центр линии дальше чем точка пересечения этой линии - то линия удаляется
                {
                    remove_indexes_ground.Add(i);
                }
            }

            for (int i = 0; i < prism_.top.Count; i++)
            {
                if (remove_indexes_top.Contains(i)) { continue; }
                GALine lol = new GALine(new GAPoint(), prism_.top[i].center_point); // линия от нач. координат до центра норм. линии
                GAPoint p_pl = plane_line_intersection(plane_, lol, false); // точка пересечения нов. линии и плоскости сечения
                if (p_pl == null) { continue; } // если пересечения нет, то линия находится до плоскости и не удаляется
                GALine lol_m = new GALine(new GAPoint(), p_pl); // для нахождения расстояния от нач. координат до точки пересечения с плоскостью
                if (lol.length > lol_m.length) // если от нач. координат - центр линии дальше чем точка пересечения этой линии - то линия удаляется
                {
                    remove_indexes_top.Add(i);
                }
            }

            remove_indexes_ribs.Sort();
            remove_indexes_ground.Sort();
            remove_indexes_top.Sort();

            for (int i = remove_indexes_ribs.Count; i > 0; i--) // удаляем основные линии которые пересекаются с плоскостью сечения + удаляем основные линии за плоскостью сечения
            {
                prism_.ribs.RemoveAt(remove_indexes_ribs[i - 1]);
            }
            for (int i = remove_indexes_ground.Count; i > 0; i--) // удаляем основные линии которые пересекаются с плоскостью сечения + удаляем основные линии за плоскостью сечения
            {
                prism_.ground.RemoveAt(remove_indexes_ground[i - 1]);
            }
            for (int i = remove_indexes_top.Count; i > 0; i--) // удаляем основные линии которые пересекаются с плоскостью сечения + удаляем основные линии за плоскостью сечения
            {
                prism_.top.RemoveAt(remove_indexes_top[i - 1]);
            }

            prism_.ribs.AddRange(ll_ribs);
            prism_.ground.AddRange(ll_ground);
            prism_.top.AddRange(ll_top);
            prism_.points.AddRange(points_section);
            return prism_;

        }

        #endregion

        #region clases

        /// <summary>
        /// примыкающие линии
        /// </summary>
        /// <param name="current">заданная линия</param>
        /// <param name="lines_">список линий для поиска </param>
        /// <returns>Возвращает список линий совпадающих(по точкам) к заданной линии</returns>
        public static List<GALine> surf_next_lines(GALine current, List<GALine> lines_)
        {
            List<GALine> ll = new List<GALine>();
            GALine cur_line = null;
            foreach(GALine l in lines_)
            {
                if (l.index == current.index) {continue; }
                if (distance_points(l.A , current.A) <= 1e-5 || distance_points( l.B, current.A ) <= 1e-5 ||
                    distance_points( l.B, current.A) <=1e-5 || distance_points(l.B, current.A) <= 1e-5)
                {
                    ll.Add(l);
                }
            }

            return ll;
        }

        //public List<GASurface> sufraces (List<GALine> lines_)
        //{
        //    List<GASurface> surf = new List<GASurface>();

        //    GALine f_l = new GALine(lines_[0].A, lines_[0].B, 0);

        //    surf_next_lines(f_l, lines_);


        //    return surf;

        //}

        #endregion

        #region drawings functions

        /// <summary>
        /// координаты видов в зависимости от их количества на чертеже
        /// </summary>
        /// <param name="max">всего видов</param>
        /// <param name="current">текущий вид по счету</param>
        /// <param name="sheet_width">ширина листа</param>
        /// <param name="sheet_height">высота листа</param>
        /// <returns>возвращает координату вида</returns>
        public static double[] getViewPosition(int max, int current, double sheet_width, double sheet_height)
        {
            double[] pos_ = new double[2] { 0, 0 };
            int x = 0; int y = 1;

            if (max == 1 && current == 1)
            {
                pos_[x] = sheet_width / 2.0;
                pos_[y] = sheet_height / 2.0;
            }

            if (max == 2)
            {
                pos_[y] = sheet_height / 2;

                if (current == 1) { pos_[x] = sheet_width / 4; }
                if (current == 2) { pos_[x] = (sheet_width / 4) * 3; }
            }

            if (max == 3)
            {
                pos_[y] = sheet_height / 2;

                if (current == 1) { pos_[x] = sheet_width / 6; }
                if (current == 2) { pos_[x] = (sheet_width / 6) * 3; }
                if (current == 3) { pos_[x] = (sheet_width / 6) * 5; }
            }

            if (max == 4)
            {
                if (current == 1) { pos_[x] = (sheet_width / 4) * 1; pos_[y] = (sheet_height / 4) * 3; }
                if (current == 2) { pos_[x] = (sheet_width / 4) * 3; pos_[y] = (sheet_height / 4) * 3; }
                if (current == 3) { pos_[x] = (sheet_width / 4) * 1; pos_[y] = (sheet_height / 4) * 1; }
                if (current == 4) { pos_[x] = (sheet_width / 4) * 3; pos_[y] = (sheet_height / 4) * 1; }
            }

            if (max == 5)
            {
                if (current == 1) { pos_[x] = (sheet_width / 6) * 1; pos_[y] = (sheet_height / 4) * 3; }
                if (current == 2) { pos_[x] = (sheet_width / 6) * 3; pos_[y] = (sheet_height / 4) * 3; }
                if (current == 3) { pos_[x] = (sheet_width / 6) * 5; pos_[y] = (sheet_height / 4) * 3; }
                if (current == 4) { pos_[x] = (sheet_width / 4) * 1; pos_[y] = (sheet_height / 4) * 1; }
                if (current == 5) { pos_[x] = (sheet_width / 4) * 3; pos_[y] = (sheet_height / 4) * 1; }
            }

            if (max == 6)
            {
                if (current == 1) { pos_[x] = (sheet_width / 6) * 1; pos_[y] = (sheet_height / 4) * 3; }
                if (current == 2) { pos_[x] = (sheet_width / 6) * 3; pos_[y] = (sheet_height / 4) * 3; }
                if (current == 3) { pos_[x] = (sheet_width / 6) * 5; pos_[y] = (sheet_height / 4) * 3; }
                if (current == 4) { pos_[x] = (sheet_width / 6) * 1; pos_[y] = (sheet_height / 4) * 1; }
                if (current == 5) { pos_[x] = (sheet_width / 6) * 3; pos_[y] = (sheet_height / 4) * 1; }
                if (current == 6) { pos_[x] = (sheet_width / 6) * 5; pos_[y] = (sheet_height / 4) * 1; }
            }

            if (max == 7)
            {
                if (current == 1) { pos_[x] = (sheet_width / 8) * 1; pos_[y] = (sheet_height / 4) * 3; }
                if (current == 2) { pos_[x] = (sheet_width / 8) * 3; pos_[y] = (sheet_height / 4) * 3; }
                if (current == 3) { pos_[x] = (sheet_width / 8) * 5; pos_[y] = (sheet_height / 4) * 3; }
                if (current == 4) { pos_[x] = (sheet_width / 8) * 7; pos_[y] = (sheet_height / 4) * 3; }
                if (current == 5) { pos_[x] = (sheet_width / 6) * 1; pos_[y] = (sheet_height / 4) * 1; }
                if (current == 6) { pos_[x] = (sheet_width / 6) * 3; pos_[y] = (sheet_height / 4) * 1; }
                if (current == 7) { pos_[x] = (sheet_width / 6) * 5; pos_[y] = (sheet_height / 4) * 1; }
            }

            if (max == 8)
            {
                if (current == 1) { pos_[x] = (sheet_width / 8) * 1; pos_[y] = (sheet_height / 4) * 3; }
                if (current == 2) { pos_[x] = (sheet_width / 8) * 3; pos_[y] = (sheet_height / 4) * 3; }
                if (current == 3) { pos_[x] = (sheet_width / 8) * 5; pos_[y] = (sheet_height / 4) * 3; }
                if (current == 4) { pos_[x] = (sheet_width / 8) * 7; pos_[y] = (sheet_height / 4) * 3; }
                if (current == 5) { pos_[x] = (sheet_width / 8) * 1; pos_[y] = (sheet_height / 4) * 1; }
                if (current == 6) { pos_[x] = (sheet_width / 8) * 3; pos_[y] = (sheet_height / 4) * 1; }
                if (current == 7) { pos_[x] = (sheet_width / 8) * 5; pos_[y] = (sheet_height / 4) * 1; }
                if (current == 8) { pos_[x] = (sheet_width / 8) * 7; pos_[y] = (sheet_height / 4) * 1; }
            }

            if (max == 9)
            {
                if (current == 1) { pos_[x] = (sheet_width / 6) * 1; pos_[y] = (sheet_height / 6) * 5; }
                if (current == 2) { pos_[x] = (sheet_width / 6) * 3; pos_[y] = (sheet_height / 6) * 5; }
                if (current == 3) { pos_[x] = (sheet_width / 6) * 5; pos_[y] = (sheet_height / 6) * 5; }

                if (current == 4) { pos_[x] = (sheet_width / 6) * 1; pos_[y] = (sheet_height / 6) * 3; }
                if (current == 5) { pos_[x] = (sheet_width / 6) * 3; pos_[y] = (sheet_height / 6) * 3; }
                if (current == 6) { pos_[x] = (sheet_width / 6) * 5; pos_[y] = (sheet_height / 6) * 3; }

                if (current == 7) { pos_[x] = (sheet_width / 6) * 1; pos_[y] = (sheet_height / 6) * 1; }
                if (current == 8) { pos_[x] = (sheet_width / 6) * 3; pos_[y] = (sheet_height / 6) * 1; }
                if (current == 9) { pos_[x] = (sheet_width / 6) * 5; pos_[y] = (sheet_height / 5) * 1; }
            }

            if (max > 9 && max <= 12)
            {
                if (current <= 4) { pos_[x] = (sheet_width / 8) * (current * 2 - 1); pos_[y] = (sheet_height / 6) * 5; }
                if (current > 4 && current <= 8) { pos_[x] = (sheet_width / 8) * ((current - 4) * 2 - 1); pos_[y] = (sheet_height / 6) * 3; }
                if (current > 8 && current <= 12) { pos_[x] = (sheet_width / 8) * ((current - 8) * 2 - 1); pos_[y] = (sheet_height / 6) * 1; }
            }

            if (max > 12 && max <= 16)
            {
                if (current <= 4) { pos_[x] = (sheet_width / 8) * (current * 2 - 1); pos_[y] = (sheet_height / 8) * 7; }
                if (current > 4 && current <= 8) { pos_[x] = (sheet_width / 8) * ((current - 4) * 2 - 1); pos_[y] = (sheet_height / 8) * 5; }
                if (current > 8 && current <= 12) { pos_[x] = (sheet_width / 8) * ((current - 8) * 2 - 1); pos_[y] = (sheet_height / 8) * 3; }
                if (current > 12 && current <= 16) { pos_[x] = (sheet_width / 8) * ((current - 12) * 2 - 1); pos_[y] = (sheet_height / 8) * 1; }
            }

            int r = 0;
            if (max > 16 && max <= 20)
            {
                r = 5;
            }

            if (max > 20 && max <= 24)
            {
                r = 6;
            }

            if (max > 24 && max <= 28)
            {
                r = 7;
            }

            if (max > 28 && max <= 32)
            {
                r = 8;
            }
            if (max > 32) {
                throw new Exception("Maximal elements is 32");
            }
            if (current <= r) { pos_[x] = (sheet_width / (r * 2)) * (current * 2 - 1); pos_[y] = (sheet_height / 8) * 7; }
            if (current > r && current <= r * 2) { pos_[x] = (sheet_width / (r * 2)) * ((current - r) * 2 - 1); pos_[y] = (sheet_height / 8) * 5; }
            if (current > r * 2 && current <= r * 3) { pos_[x] = (sheet_width / (r * 2)) * ((current - r * 2) * 2 - 1); pos_[y] = (sheet_height / 8) * 3; }
            if (current > r * 3 && current <= r * 4) { pos_[x] = (sheet_width / (r * 2)) * ((current - r * 3) * 2 - 1); pos_[y] = (sheet_height / 8) * 1; }

            return pos_;
        }

        /// <summary>
        /// дать масштаб, получить масштаб приближенный к ГОСТу
        /// </summary>
        /// <param name="scale"></param>
        /// <param name="up">в большую сторону или в меньшую</param>
        /// <returns></returns>
        public static double getNormalScale(double scale, bool up = false)
        {
            double[] scale_range = new double[] { 100.0, 50.0, 40.0, 20.0, 10.0, 5.0, 4.0, 2.5, 2.0,
                1.0,
                1.0 / 2.0, 1.0 / 2.5, 1.0 / 4.0, 1.0 / 5.0, 1.0 / 10.0, 1.0 / 15.0, 1.0 / 20.0, 1.0 / 25.0, 1.0 / 40.0, 1.0 / 50.0, 1.0 / 75.0, 1.0 / 100.0, 1.0 / 200.0, 1.0 / 400.0, 1.0 / 500.0, 1.0 / 800.0, 1.0 / 1000.0, 1.0 / 2000.0, 1.0 / 5000.0, 1.0 / 10000.0, 1.0 / 20000.0, 1.0 / 25000.0, 1.0 / 50000.0 };

            if (scale >= scale_range[0]) { return scale_range[0]; }
            else
            if (scale <= scale_range[scale_range.Length - 1]) { return scale_range[scale_range.Length - 1]; }

            double new_scale = scale / scale_range[0];
            bool first = (new_scale > 1) ? true : false;
            for (int i = 0; i < scale_range.Length; i++)
            {
                new_scale = scale / scale_range[i];
                if(new_scale == 1) { return scale; }
                if (first != (new_scale > 1))
                {
                    if (up) { new_scale = scale_range[((i == 0) ? 1 : i) - 1]; }
                    else
                    {
                        new_scale = scale_range[i];
                    }
                    return new_scale;
                }
            }

            return scale;
        }

        ///// <summary>
        ///// дать масштаб, получить масштаб приближенный к ГОСТу - от MAS
        ///// </summary>
        ///// <param name="scaleDouble"></param>
        ///// <returns></returns>
        //public double normazileScale(double scaleDouble)
        //{
        //    #region ряд масштабов
        //    double[] scale_range = new double[]
        //    {
        //            100.0, 50.0, 40.0, 20.0, 15.0, 10.0, 5.0, 4.0, 2.5, 2.0,
        //            1.0,
        //            1.0/2.0,
        //            1.0/2.5,
        //            1.0/4.0,
        //            1.0/5.0,
        //            1.0/10.0,
        //            1.0/15.0,
        //            1.0/20.0,
        //            1.0/25.0,
        //            1.0/40.0,
        //            1.0/50.0,
        //            1.0/75.0,
        //            1.0/100.0,
        //            1.0/200.0,
        //            1.0/400.0,
        //            1.0/500.0,
        //            1.0/800.0,
        //            1.0/1000.0,
        //            1.0/2000.0,
        //            1.0/5000.0,
        //            1.0/10000.0,
        //            1.0/20000.0,
        //            1.0/25000.0,
        //            1.0/50000.0

        //    };

        //    double[] scale_range_1 = new double[] {100.0, 50.0, 40.0, 20.0, 15.0, 10.0, 5.0, 4.0, 2.5, 2.0,
        //            1.0,
        //            1.0,
        //            1.0,
        //            1.0,
        //            1.0,
        //            1.0,
        //            1.0,
        //            1.0,
        //            1.0,
        //            1.0,
        //            1.0,
        //            1.0,
        //            1.0,
        //            1.0,
        //            1.0,
        //            1.0,
        //            1.0,
        //            1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 };

        //    double[] scale_range_2 = new double[] {1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0,
        //            1.0,
        //            2.0,
        //            2.5,
        //            4.0,
        //            5.0,
        //            10.0,
        //            15.0,
        //            20.0,
        //            25.0,
        //            40.0,
        //            50.0,
        //            75.0,
        //            100.0,
        //            200.0,
        //            400.0,
        //            500.0,
        //            800.0,
        //            1000.0,
        //            2000.0,
        //            5000.0,
        //            10000.0,
        //            20000.0,
        //            25000.0,
        //            50000.0 };
        //    #endregion

        //    double sc1 = 1; double sc2 = 1;
        //    for (int ii = 0; ii < scale_range.Length; ii++)
        //    {
        //        double sc = scale_range[ii];
        //        if (sc <= scaleDouble) { sc1 = scale_range_1[ii]; sc2 = scale_range_2[ii]; break; }
        //    }

        //    return sc1 / sc2;
        //}
        #endregion

        #region special round

        /// <summary> 
        /// Определение точности округления по количеству значащих цифр
        /// </summary>
        /// <param name="d">число</param>
        /// <param name="digits">количество значимых чисел</param>
        /// <returns></returns>
        public static double round_get_scale_by_significant_digits(double d, int digits)
        {
            if (d == 0)
                return 0;

            double scale = Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(d))) + 1);
            return scale / Math.Pow(10, digits);
        }

        /// <summary>
        /// Округление с необходимой кратностью и направлением
        /// </summary>
        /// <param name="numeral">Цифра для округления</param>
        /// <param name="accuracy">Точность (кратность)</param>
        /// <param name="down_middle_up">-1 = вниз, 0 = до ближайшего, 1 = вверх</param>
        /// <returns></returns>
        public static double round(double numeral, double accuracy, int down_middle_up = 0)
        {
            //double aaa = Math.Round(numeral % accuracy);

            double tmp = Math.Round((numeral % accuracy), 10);
            accuracy = Math.Abs(accuracy);
            if (Math.Round(numeral / accuracy, 10) == Math.Round(numeral / accuracy, 0)) { tmp = 0; }

            if (down_middle_up == 1)
            {
                if (tmp != 0) numeral += numeral > 0 ? (accuracy - tmp) : -tmp;
                numeral = Math.Round(numeral, 10);
            }
            if (down_middle_up == -1)
            {
                if (tmp != 0) numeral += numeral < 0 ? -(accuracy + tmp) : -tmp;
                numeral = Math.Round(numeral, 10);
            }

            if (down_middle_up == 0)
            {
                double num1 = numeral;
                double num2 = numeral;

                if (tmp != 0) num1 += num1 > 0 ? (accuracy - tmp) : -tmp;
                num1 = Math.Round(num1, 10);
                if (tmp != 0) num2 += num2 < 0 ? -(accuracy + tmp) : -tmp;
                num2 = Math.Round(num2, 10);
                numeral = Math.Round((num1 - numeral), 10) > Math.Round((numeral - num2), 10) ? num2 : num1;

            }
            return numeral;
        }

        #endregion

        #region filters

        /// <summary>
        /// Median filter. 
        /// Explanation:
        /// x = [2 80 6 3] 
        /// y[1] = median[2 2 80] = 2
        /// y[2] = median[2 80 6] = median[2 6 80] = 6
        /// y[3] = median[80 6 3] = median[3 6 80] = 6
        /// y[4] = median[6 3 3] = median[3 3 6] = 3
        /// Значения отсчётов внутри окна фильтра сортируются в порядке возрастания (убывания); и значение, находящееся в середине упорядоченного списка, поступает на выход фильтра. В случае чётного числа отсчётов в окне выходное значение фильтра равно среднему значению двух отсчётов в середине упорядоченного списка.
        /// </summary>
        /// <param name="values">Values to sort</param>
        /// <param name="window">Filter window, if odd number returns middle value, if even number returns average of two middle values</param>
        /// <param name="ByCircle">true = first additionat items will be the last items of range, false = first additional items will be duplicated</param>
        /// <returns>filtered array</returns>
        public static List<double> filter_Median(List<double> values, int window, bool ByCircle = false)
        {
            List<double> values2 = new List<double>();

            foreach(double d in values)
            {
                values2.Add(d);
            }

            //сколько элементов добавлять
            int addEl = 0;
            if(window%2 == 0)
            {
                addEl = window / 2;
            }
            else
            {
                addEl = Convert.ToInt32(Math.Floor((double)window / 2d));
            }

            //добавляем элементы по краям
            if(ByCircle)
            {
                //первые добавочные элементы это последние элементы
                for(int i = 0; i < addEl; i++)
                {
                    values2.Insert(0, values[values.Count - 1 - i]); //вставка конечных элементов в начало
                    values2.Insert(values2.Count, values[i]); //вставка первых элементов в конец
                }
            }
            else
            {
                //первые добавочные элементы это копии первого/последнего входного элемента
                for (int i = 0; i < addEl; i++)
                {
                    values2.Insert(0, values[0]);//вставка в начало //копирование начала
                    values2.Insert(values2.Count, values[values.Count - 1]);//вставка в конец //копирование конца
                }
            }

            List<double> filtered = new List<double>();

            for(int i = 0; i < values.Count; i++)
            {
                double median = filter_getMedian(values2.GetRange(i, window));
                filtered.Add(median);
            }

            return filtered;
        }

        /// <summary>
        /// Sorts range and returns the middle parameter (middle index of array, not middle as math function)
        /// </summary>
        /// <param name="values"></param>
        /// <param name="window">Filter window, if odd number returns middle value, if even number returns average of two middle values</param>
        /// <returns>Returns value with middle index of array</returns>
        public static double filter_getMedian(List<double> values)
        {
            if(values == null)
            {
                throw new ArgumentException("values can't be NULL");
            }

            if (values.Count < 0)
            {
                throw new ArgumentException("Values amount must be more than 0");
            }

            List<double> sortedValues = new List<double>();
            double returnValue = double.NaN;

            foreach(double v in values)
            {
                sortedValues.Add(v);
            }

            sortedValues.Sort();

            if(values.Count % 2 == 0)
            {
                int index1 = values.Count / 2;
                int index2 = index1--;

                returnValue = (sortedValues[index1] + sortedValues[index2]) / 2d;
            }
            else
            {
                int index = Convert.ToInt32(Math.Floor((double)values.Count / 2d));
                returnValue = sortedValues[index];
            }

            if (double.IsNaN(returnValue))
            {
                throw new ArithmeticException("returned value is null, wrong median calculation");
            }
            if (double.IsInfinity(returnValue))
            {
                throw new ArithmeticException("returned value is infinity, wrong median calculation");
            }

            return returnValue;
        }

        #endregion


    }
}
