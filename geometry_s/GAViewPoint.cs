using System;

namespace geometry_s
{
    /// <summary>
    /// 2d point
    /// </summary>
    public class GAViewPoint
    {
        /// <summary>
        /// Координата X
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Координата Y
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// описание точки
        /// </summary>
        public string description_t { get; set; }

        /// <summary>
        /// номер точки
        /// </summary>
        public int description { get; set; }

        /// <summary>
        /// индекс точки
        /// </summary>
        public int index { get; set; }

        /// <summary>
        /// Точка по умолчанию 0, 0
        /// </summary>
        public GAViewPoint()
        {
            X = 0;
            Y = 0;
        }

        /// <summary>
        /// Точка
        /// </summary>
        /// <param name="x_">X</param>
        /// <param name="y_">Y</param>
        /// <param name="description_">номер</param>
        /// <param name="index_">индекс</param>
        /// <param name="description_t_">описание</param>
        public GAViewPoint(double x_, double y_, int description_ = 0, int index_ = 0, string description_t_ = "")
        {
            X = Math.Round(x_, 10);
            Y = Math.Round(y_, 10);

            description = description_;
            index = index_;
            description_t = description_t_;
        }

        /// <summary>
        /// Точка
        /// </summary>
        /// <param name="x_">X</param>
        /// <param name="y_">Y</param>
        /// <param name="description_">номер</param>
        /// <param name="index_">индекс</param>
        /// <param name="description_t_">описание</param>
        public GAViewPoint(int x_, int y_, int description_ = 0, int index_ = 0, string description_t_ = "")
        {
            X = x_;
            Y = y_;

            description = description_;
            index = index_;
            description_t = description_t_;
        }

        /// <summary>
        /// Точка
        /// </summary>
        /// <param name="x_">X</param>
        /// <param name="y_">Y</param>
        /// <param name="description_">номер</param>
        /// <param name="index_">индекс</param>
        /// <param name="description_t_">описание</param>
        public GAViewPoint(decimal x_, decimal y_, int description_ = 0, int index_ = 0, string description_t_ = "")
        {
            X = Math.Round(Convert.ToDouble(x_), 10);
            Y = Math.Round(Convert.ToDouble(y_), 10);

            description = description_;
            index = index_;
            description_t = description_t_;
        }

        /// <summary>
        /// точка XY
        /// </summary>
        /// <param name="point_">точка</param>
        /// <param name="index_">индекс, если -1 то берет из данной точки</param>
        /// <param name="description_">описание, если -1 то берет из данной точки</param>
        public GAViewPoint(GAViewPoint point_, int index_ = -1, int description_ = -1, string description_t_ = "")
        {
            X = point_.X;
            Y = point_.Y;

            if (index_ == -1)
            {
                index = point_.index;
            }
            else
            {
                index = index_;
            }

            if (description_ == -1)
            {
                description = point_.description;
            }
            else
            {
                description = description_;
            }

            description_t = description_t_;
        }

        /// <summary>
        /// Точка по тексту
        /// </summary>
        /// <param name="line">x=1,y=2</param>
        /// <param name="XY_separator"> , (x=1,y=2) (or your separator)</param>
        /// <param name="second_separator"> = (x=1,y=2) (or your separator)</param>
        public GAViewPoint(string line, string XY_separator, string second_separator = "")
        {

            // разделили по полам получили X Y
            string[] xy = line.Split(new string[] { XY_separator }, StringSplitOptions.None);

            //по умолчанию левая и правая сторона
            //X Y делим еще пополам если нада
            string x_text = xy[0];
            string y_text = xy[1];
            if (second_separator.Length > 0)
            {
                //если массивы по 2 элемента значит разделили правильно
                string[] xx = xy[0].Split(new string[] { second_separator }, StringSplitOptions.None);
                string[] yy = xy[1].Split(new string[] { second_separator }, StringSplitOptions.None);

                if (xx.Length == 2 && yy.Length == 2)
                {
                    x_text = xx[1];
                    y_text = yy[1];
                }
            }

            //на будущее, проверить все символы что в строчке и удалить текст (оставить одну(перву) точку или запятую)

            //for (int i=0; i < x_text.Length; i++)
            //{
            //    char l = x_text[i];
            //    if (!char.IsNumber(l))
            //    {
            //        if(l != ',' && l != '.')
            //        {
            //            x_text.Remove(i, 1);
            //        }
            //    }
            //}

            double x_val = 0;
            double y_val = 0;
            bool x_ok = double.TryParse(x_text, out x_val);
            bool y_ok = double.TryParse(y_text, out y_val);

            if (x_ok && y_ok)
            {
                X = x_val;
                Y = y_val;
            }
            else
            {
                X = double.NaN;
                Y = double.NaN;
            }
        }

        public static GAViewPoint operator -(GAViewPoint A, GAViewPoint B)
        {
            return new GAViewPoint(A.X - B.X, A.Y - B.Y);
        }

        public static GAViewPoint operator +(GAViewPoint A, GAViewPoint B)
        {
            return new GAViewPoint(A.X + B.X, A.Y + B.Y);
        }

        public static GAViewPoint operator *(GAViewPoint A, double a)
        {
            return new GAViewPoint(A.X * a, A.Y * a, A.description, A.index, A.description_t);
        }

        public static GAViewPoint operator /(GAViewPoint A, double a)
        {
            return new GAViewPoint(A.X / a, A.Y / a, A.description, A.index, A.description_t);
        }

    }
}
