using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course
{
    //ТЕСТОВЫЙ КЛАСС
    class ProbabilityPerSide
    {
        public String ProbabilityPerSides(int width, int height, int sizeEllips, Stopwatch timeDrow, int countColour,
            Boolean colour, int countParticle, ArrayList x, ArrayList y, int[,] particleCoordinates, int[] dx, int[] dy, double top,
        double right, double bottom, double left)
        {
            //вероятностный рост по сторонам
            //в результате можно настраивать приоритетность одной из сторон роста
            //Вводится вероятность на каждую из 4 сторон
            //В координате точки периметра будет храниться вероятность попадания этой точки в узел при её выпаднии

            ///#0 размещаем в оперативной памяти массивы и инициализируем графическое окно
            ///#1 размещаем в центре решётки зерно роста кластера
            ///#2 выполняем проверку на достижение переходим к пункту 8
            ///#3 выбираем случайным образом ячейку периметра кластера
            ///#4 присоединяем к кластеру новую ячейку
            ///#5 исключаем выбранную ячейку из списка периметра кластера
            ///#6 вычисляем новывй периметр кластера
            ///#7 рисуем частицу на экране и переходим к пункту 2 
            ///#8 заканчиваем работу программы


            //случайное число
            Random random = new Random();
            //начинает подсчёт потраченного времени
            timeDrow.Start();
            //класс рисования
            WindowDrow drow = new WindowDrow();
            //#1
            drow.DrowEllipse(width / 2, height / 2, sizeEllips, true);
            particleCoordinates[width / 2, height / 2] = 1;
            int xp = width / 2; int yp = height / 2;
            for (int i = 0; i < 4; i++)
            {
                x.Add((xp + dx[i]) * 10 + side(dx[i], dy[i], sizeEllips, top, right, bottom, left));
                y.Add(yp + dy[i]);
            }
            //#3
            int rnd = random.Next(0, x.Count);
            xp = (int)x[rnd] / 10;
            yp = (int)y[rnd];

            while ((xp - sizeEllips - 2 > 0) && (yp - sizeEllips - 2 > 0) && (yp + sizeEllips + 2 < height) && (xp + sizeEllips + 2 < width))
            {
                //#4
                countParticle++;
                particleCoordinates[xp, yp] = 1;
                //#5
                x.RemoveAt(rnd);
                y.RemoveAt(rnd);
                //#6
                //Определяем периметр кластера самым обычным способом
                //В будущем будет отльной функцией
                for (int i = 0; i < 4; i++)
                {
                    int xn = xp + dx[i];
                    int yn = yp + dy[i];
                    if (particleCoordinates[xn, yn] == 0)
                    {
                        //в xn хранится вероятность попадения точки в узел 
                        x.Add(xn * 10 + side(dx[i], dy[i], sizeEllips,  top, right, bottom, left));
                        y.Add(yn);
                    }
                }
                //#7
                if (countParticle % countColour == 0)
                {
                    //Gray
                    if (colour)
                    {
                        colour = false;
                    }
                    //Black
                    else
                    {
                        colour = true;
                    }

                }
                drow.DrowEllipse(xp, yp, sizeEllips, colour);
                //#3
                //проверка взятия узла
                xp = 0; yp = 0;
                ArrayList copy_x = Copy(x);
                ArrayList copy_y = Copy(y);
                while (xp == 0 && yp == 0 && copy_x.Count!=0)
                {
                    rnd = random.Next(0, copy_x.Count);
                    //пробное изменение
                    double f_rnd = random.Next(0, 10) / 10.0;
                    if (f_rnd - ((int)copy_x[rnd] % 10) / 10.0 <= 0 && (((int)copy_x[rnd] % 10) / 10.0) != 0)
                    {
                        xp = (int)copy_x[rnd] / 10;
                        yp = (int)copy_y[rnd];
                    }
                    else
                    {
                        copy_x.RemoveAt(rnd);
                        copy_y.RemoveAt(rnd);
                    }
                }
            }
            timeDrow.Stop();
            drow.Height = height;
            drow.Width = width;
            drow.Show();
            drow.Title = "ProbabilityPerSide " + timeDrow.ElapsedMilliseconds.ToString() + " мс";
            return timeDrow.ElapsedMilliseconds.ToString() + " мс";
        }

        private int side(int dx, int dy, int sizeEllips, double top, double right, double bottom, double left)
        {
            if (dx == 1 * sizeEllips && dy == 0) return int.Parse((right * 10).ToString());
            if (dx == 0 && dy == -1 * sizeEllips) return int.Parse((top * 10).ToString());
            if (dx == -1 * sizeEllips && dy == 0) return int.Parse((left * 10).ToString());
            if (dx == 0 && dy == 1 * sizeEllips) return int.Parse((bottom * 10).ToString());
            return 0;
        }

        private ArrayList Copy(ArrayList array)
        {
            ArrayList array_copy = new ArrayList();
            for (int i = 0; i < array.Count; i++)
            {
                array_copy.Add(array[i]);
            }
            return array_copy;
        }
    }
}
