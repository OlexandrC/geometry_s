namespace geometry_s
{

    /// <summary>
    /// Матрица трансформации 4х4 (устаревший класс)
    /// </summary>
    public class matrixTransform4x4
    {
        double r10 { get; set; }
        double r11 { get; set; }
        double r12 { get; set; }
        double r13 { get; set; }

        double r20 { get; set; }
        double r21 { get; set; }
        double r22 { get; set; }
        double r23 { get; set; }

        double r30 { get; set; }
        double r31 { get; set; }
        double r32 { get; set; }
        double r33 { get; set; }

        double r40 { get; set; }
        double r41 { get; set; }
        double r42 { get; set; }
        double r43 { get; set; }

        /// <summary>
        ///  Матрица трансформации 4х4 
        /// </summary>
        public matrixTransform4x4()
        {
            r10 = 0;
            r11 = 0;
            r12 = 0;
            r13 = 0;

            r20 = 0;
            r21 = 0;
            r22 = 0;
            r23 = 0;

            r30 = 0;
            r31 = 0;
            r32 = 0;
            r33 = 0;

            r40 = 0;
            r41 = 0;
            r42 = 0;
            r43 = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arrayOf16_rowsByRows">Данные по рядам, первых 4 значения массива это первый ряд и так далее</param>
        public matrixTransform4x4(double[] arrayOf16_rowsByRows)
        {
            r10 = arrayOf16_rowsByRows[0];
            r11 = arrayOf16_rowsByRows[1];
            r12 = arrayOf16_rowsByRows[2];
            r13 = arrayOf16_rowsByRows[3];

            r20 = arrayOf16_rowsByRows[4];
            r21 = arrayOf16_rowsByRows[5];
            r22 = arrayOf16_rowsByRows[6];
            r23 = arrayOf16_rowsByRows[7];

            r30 = arrayOf16_rowsByRows[8];
            r31 = arrayOf16_rowsByRows[9];
            r32 = arrayOf16_rowsByRows[10];
            r33 = arrayOf16_rowsByRows[11];

            r40 = arrayOf16_rowsByRows[12];
            r41 = arrayOf16_rowsByRows[13];
            r42 = arrayOf16_rowsByRows[14];
            r43 = arrayOf16_rowsByRows[15];
        }
    }
}
