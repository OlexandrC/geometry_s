using System.Collections.Generic;

namespace geometry_s
{
    /// <summary>
    /// Матрица трансформации 4х4
    /// (Sx 0 0 Tx)
    /// (0 Sy 0 Ty)
    /// (0 0 Sz Tz)
    /// (0 0 0 W )
    /// Sx-масштаб по Х
    /// Sy-масштаб по Y
    /// Sz-масштаб по Z
    /// Tx-сдвиг по X
    /// Ty-сдвиг по Y
    /// Tz-сдвиг по Z
    /// W=1, если W=0 то это направление так как он не может быть сдвинут
    /// </summary>
    public class matrixTransform4x4_2
    {
        /// <summary>
        /// ряд 0 колонка 0
        /// </summary>
        public double row_0_cell_0 { get; set; }

        /// <summary>
        /// ряд 0 колонка 1
        /// </summary>
        public double row_0_cell_1 { get; set; }

        /// <summary>
        /// ряд 0 колонка 2
        /// </summary>
        public double row_0_cell_2 { get; set; }

        /// <summary>
        /// ряд 0 колонка 3
        /// </summary>
        public double row_0_cell_3 { get; set; }

        /// <summary>
        /// ряд 1 колонка 0
        /// </summary>
        public double row_1_cell_0 { get; set; }

        /// <summary>
        /// ряд 1 колонка 1
        /// </summary>
        public double row_1_cell_1 { get; set; }

        /// <summary>
        /// ряд 1 колонка 2
        /// </summary>
        public double row_1_cell_2 { get; set; }

        /// <summary>
        /// ряд 1 колонка 3
        /// </summary>
        public double row_1_cell_3 { get; set; }

        /// <summary>
        /// ряд 2 колонка 
        /// </summary>
        public double row_2_cell_0 { get; set; }

        /// <summary>
        /// ряд 2 колонка 1
        /// </summary>
        public double row_2_cell_1 { get; set; }

        /// <summary>
        /// ряд 2 колонка 2
        /// </summary>
        public double row_2_cell_2 { get; set; }

        /// <summary>
        /// ряд 2 колонка 3
        /// </summary>
        public double row_2_cell_3 { get; set; }

        /// <summary>
        /// ряд 3 колонка 0
        /// </summary>
        public double row_3_cell_0 { get; set; }

        /// <summary>
        /// ряд 3 колонка 1
        /// </summary>
        public double row_3_cell_1 { get; set; }

        /// <summary>
        /// ряд 3 колонка 2
        /// </summary>
        public double row_3_cell_2 { get; set; }

        /// <summary>
        /// ряд 3 колонка 3
        /// </summary>
        public double row_3_cell_3 { get; set; }

        /// <summary>
        /// (не проверено!!!) установить масштаб
        /// row_0_cell_0 = scaleX;
        /// row_1_cell_1 = scaleY;
        /// row_2_cell_2 = scaleZ;
        /// </summary>
        /// <param name="scaleX">row_0_cell_0 = scaleX</param>
        /// <param name="scaleY">row_1_cell_1 = scaleY</param>
        /// <param name="scaleZ">row_2_cell_2 = scaleZ</param>
        public void setScale(double scaleX, double scaleY, double scaleZ)
        {
            row_0_cell_0 = scaleX;
            row_1_cell_1 = scaleY;
            row_2_cell_2 = scaleZ;
        }

        /// <summary>
        ///(не проверено!!!)  установить масштаб
        /// row_0_cell_0 = scale;
        /// row_1_cell_1 = scale;
        /// row_2_cell_2 = scale;
        /// </summary>
        /// <param name="scale">row_0_cell_0 = row_1_cell_1 = row_2_cell_2 = scale </param>
        public void setScale(double scale)
        {
            setScale(scale, scale, scale);
        }

        /// <summary>
        /// (не проверено!!!) установить сдвиг
        /// row_0_cell_3 = moveX
        /// row_1_cell_3 = moveY
        /// row_2_cell_3 = moveZ
        /// </summary>
        /// <param name="moveX">row_0_cell_3 = moveX </param>
        /// <param name="moveY">row_1_cell_3 = moveY </param>
        /// <param name="moveZ">row_2_cell_3 = moveZ </param>
        public void setMove(double moveX, double moveY, double moveZ)
        {
            row_0_cell_3 = moveX;
            row_1_cell_3 = moveY;
            row_2_cell_3 = moveZ;
        }

        /// <summary>
        /// (не проверено!!!) вращение вокруг оси X
        /// </summary>
        /// <param name="angleX">угол в градусах (DEG)</param>
        public void setRotationX(double angleX)
        {
            row_1_cell_1 = GAGeometry.Cos(angleX);
            row_1_cell_2 = -GAGeometry.Sin(angleX);

            row_2_cell_1 = GAGeometry.Sin(angleX);
            row_2_cell_2 = GAGeometry.Cos(angleX);
        }

        /// <summary>
        /// (не проверено!!!) вращение вокруг оси Y
        /// </summary>
        /// <param name="angleY">угол в градусах (DEG)</param>
        public void setRotationY(double angleY)
        {
            row_0_cell_0 = GAGeometry.Cos(angleY);
            row_0_cell_2 = GAGeometry.Sin(angleY);

            row_2_cell_0 = -GAGeometry.Sin(angleY);
            row_2_cell_2 = GAGeometry.Cos(angleY);
        }

        /// <summary>
        /// (не проверено!!!) вращение вокруг оси Z
        /// </summary>
        /// <param name="angleZ">угол в градусах (DEG)</param>
        public void setRotationZ(double angleZ)
        {
            row_0_cell_0 = GAGeometry.Cos(angleZ);
            row_0_cell_1 = -GAGeometry.Sin(angleZ);

            row_1_cell_0 = GAGeometry.Sin(angleZ);
            row_1_cell_1 = GAGeometry.Cos(angleZ);
        }

        /// <summary>
        /// Вращение вокруг оси. (Rx, Ry, Rz) - вектор оси
        /// </summary>
        /// <param name="Rx">X вектора</param>
        /// <param name="Ry">Y вектора</param>
        /// <param name="Rz">Z вектора</param>
        /// <param name="A">угол поворота в градусах (DEG)</param>
        public void setRotation(double Rx, double Ry, double Rz, double A)
        {
            double cos = GAGeometry.Cos(A);
            double sin = GAGeometry.Sin(A);

            row_0_cell_0 = cos + Rx * Rx * (1 - cos);
            row_0_cell_1 = Rx * Ry * (1 - cos) - Rz * sin;
            row_0_cell_2 = Rx * Rz * (1 - cos) + Ry * sin;

            row_1_cell_0 = Ry * Rx * (1 - cos) + Rz * sin;
            row_1_cell_1 = cos + Ry * Ry * (1 - cos);
            row_1_cell_2 = Ry * Rz * (1 - cos) - Rx * sin;

            row_2_cell_0 = Rz * Rx * (1 - cos) - Ry * sin;
            row_2_cell_1 = Rz * Ry * (1 - cos) + Rx * sin;
            row_2_cell_2 = cos + Rz * Rz * (1 - cos);
        }

        /// <summary>
        /// Вращение вокруг вектора оси
        /// </summary>
        /// <param name="V">Вектор</param>
        /// <param name="angleDEG">Угол в градусах</param>
        public void setRotation(GAVector V, double angleDEG)
        {
            setRotation(V.X, V.Y, V.Z, angleDEG);
        }

        /// <summary>
        ///  Матрица трансформации 4х4, единицы по диагонали (ряд ячейка (00, 11, 22, 33))
        /// </summary>
        public matrixTransform4x4_2()
        {
            row_0_cell_0 = 1;
            row_0_cell_1 = 0;
            row_0_cell_2 = 0;
            row_0_cell_3 = 0;

            row_1_cell_0 = 0;
            row_1_cell_1 = 1;
            row_1_cell_2 = 0;
            row_1_cell_3 = 0;

            row_2_cell_0 = 0;
            row_2_cell_1 = 0;
            row_2_cell_2 = 1;
            row_2_cell_3 = 0;

            row_3_cell_0 = 0;
            row_3_cell_1 = 0;
            row_3_cell_2 = 0;
            row_3_cell_3 = 1;
        }

        /// <summary>
        /// Матрица трансформации 4х4, Данные по рядам первых 4 значения массива это первый ряд и так далее
        /// </summary>
        /// <param name="arrayOf16_rowsByRows">Данные по рядам, первых 4 значения массива это первый ряд и так далее</param>
        public matrixTransform4x4_2(double[] arrayOf16_rowsByRows)
        {
            row_0_cell_0 = arrayOf16_rowsByRows[0];
            row_0_cell_1 = arrayOf16_rowsByRows[1];
            row_0_cell_2 = arrayOf16_rowsByRows[2];
            row_0_cell_3 = arrayOf16_rowsByRows[3];

            row_1_cell_0 = arrayOf16_rowsByRows[4];
            row_1_cell_1 = arrayOf16_rowsByRows[5];
            row_1_cell_2 = arrayOf16_rowsByRows[6];
            row_1_cell_3 = arrayOf16_rowsByRows[7];

            row_2_cell_0 = arrayOf16_rowsByRows[8];
            row_2_cell_1 = arrayOf16_rowsByRows[9];
            row_2_cell_2 = arrayOf16_rowsByRows[10];
            row_2_cell_3 = arrayOf16_rowsByRows[11];

            row_3_cell_0 = arrayOf16_rowsByRows[12];
            row_3_cell_1 = arrayOf16_rowsByRows[13];
            row_3_cell_2 = arrayOf16_rowsByRows[14];
            row_3_cell_3 = arrayOf16_rowsByRows[15];
        }

        /// <summary>
        /// (не проверено на массивах больше 4-х!!!) Умножение на матрицу. Массив должен быть кратным четырем.
        /// (00 01 02 03) * (array[0]) = (00*array[0] + 01*array[1] + 02*array[2] + 03*array[3])
        /// (10 11 12 13)   (array[1])   (10*array[0] + 11*array[1] + 12*array[2] + 13*array[3])
        /// (20 21 22 23)   (array[2])   (20*array[0] + 21*array[1] + 22*array[2] + 23*array[3])
        /// (30 31 32 33)   (array[3])   (30*array[0] + 31*array[1] + 32*array[2] + 33*array[3])
        /// </summary>
        /// <param name="array">Массив должен быть кратен четырем</param>
        /// <returns></returns>
        public List<double> multiply(double[] array)
        {
            //2020-07-27
            int step = array.Length / 4;
            if (step != (((double)array.Length) / 4.0)) { return null; } //массив не кратен четырем

            List<double> result = new List<double>();

            for (int i = 0; i < step; i++)
            {
                result.Add(
                row_0_cell_0 * array[0 + 4 * i] +
                row_0_cell_1 * array[1 + 4 * i] +
                row_0_cell_2 * array[2 + 4 * i] +
                row_0_cell_3 * array[3 + 4 * i]
                );

                result.Add(
                row_1_cell_0 * array[0 + 4 * i] +
                row_1_cell_1 * array[1 + 4 * i] +
                row_1_cell_2 * array[2 + 4 * i] +
                row_1_cell_3 * array[3 + 4 * i]
                );

                result.Add(
                row_2_cell_0 * array[0 + 4 * i] +
                row_2_cell_1 * array[1 + 4 * i] +
                row_2_cell_2 * array[2 + 4 * i] +
                row_2_cell_3 * array[3 + 4 * i]
                );

                result.Add(
                row_3_cell_0 * array[0 + 4 * i] +
                row_3_cell_1 * array[1 + 4 * i] +
                row_3_cell_2 * array[2 + 4 * i] +
                row_3_cell_3 * array[3 + 4 * i]
                );
            }
            return result;
        }

        /// <summary>
        /// Умножение точки на матрицу трансформации
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public GAPoint multiply(GAPoint point)
        {
            List<double> var = multiply(new double[] { point.X, point.Y, point.Z, 1 });
            return new GAPoint(var[0], var[1], var[2]);
        }

        /// <summary>
        /// (не проверено на массивах больше 4-х!!!)  Умножение на матрицу. Массив должен быть кратным четырем.
        /// (00 01 02 03) * (V0) = (00*V0+01*V1+02*V2+03*V3)
        /// (10 11 12 13)   (V1)   (10*V0+11*V1+12*V2+13*V3)
        /// (20 21 22 23)   (V2)   (20*V0+21*V1+22*V2+23*V3)
        /// (30 31 32 33)   (V3)   (30*V0+31*V1+32*V2+33*V3)
        /// </summary>
        /// <param name="array">Массив должен быть кратен четырем</param>
        /// <returns></returns>
        List<double> multiply(List<double> array)
        {
            return multiply(array.ToArray());
        }

    }
}
