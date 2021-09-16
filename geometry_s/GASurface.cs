using System.Collections.Generic;

namespace geometry_s
{
    /// <summary>
    /// класс описывает набор замкнутых отрезков лежащих в одной плоскости
    /// </summary>
    public class GASurface
    {
        List<GASurface> surf { get; set; }
        List<GALine> lines { get; set; }

        public GASurface(List<GALine> lines_)
        {
            lines = lines_;


            for (int i = 0; i < lines_.Count; i++)
            {


                GALine f_line = new GALine(lines_[i].A, lines_[i].B, i);

                for (int j = i + 1; j < lines_.Count; j++)
                {
                    //GALine f_line = new GALine(lines_[i].A, lines_[i].B, i);


                }
            }

        }

        //int next_line()
        //{

        //}

    }
}
