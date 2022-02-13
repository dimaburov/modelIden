using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course
{
    class PercolationModel
    {
        public String PercolationModels(int width, int height, int sizeEllips, Stopwatch timeDrow,
        int countParticle, ArrayList x, ArrayList y, int[,] particleCoordinates, int[] dx, int[] dy, float f)
        {
            ///Перкаляционная модель Идена
            ///#0 размещаем в оперативной памяти массивы и инициализируем графическое окно
            ///#1 размещаем в центре решётки зерно роста кластера
            ///#2 выбираем случайным образом ячейку периметра кластера
            ///#3 выполняем проверку на достижение переходим к пункту 8
            ///#4 присоединяем к кластеру новую ячейку, если она не залокированна или не занята
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
            int[,] tesArray = new int[width, height];
            //вероятность блокировки узла
            //float f = float.Parse(probabilityBlocking.Text);
            //#1
            drow.DrowEllipse(width / 2, height / 2, sizeEllips, true);
            particleCoordinates[width / 2, height / 2] = 1;
            int xp = width / 2; int yp = height / 2;
            for (int i = 0; i < 4; i++)
            {
                x.Add(xp + dx[i]);
                y.Add(yp + dy[i]);
            }
            //#2
            int rnd = random.Next(0, x.Count);
            xp = (int)x[rnd];
            yp = (int)y[rnd];
            //#3
            while ((xp - sizeEllips - 2 > 0) && (yp - sizeEllips - 2 > 0) && (yp + sizeEllips + 2 < height) && (xp + sizeEllips + 2 < width))
            {
                //проверка вероятности
                double rnd_f = random.Next(0, 10) / 10.0;
                if (rnd_f - f <= 0)
                {
                    particleCoordinates[xp, yp] = 1;
                    x.RemoveAt(rnd);
                    y.RemoveAt(rnd);
                }
                else
                {
                    particleCoordinates[xp, yp] = 1;
                    countParticle++;
                    x.RemoveAt(rnd);
                    y.RemoveAt(rnd);
                    //#6
                    for (int i = 0; i < 4; i++)
                    {
                        int xn = xp + dx[i];
                        int yn = yp + dy[i];
                        if (particleCoordinates[xn, yn] != 1)
                        {
                            x.Add(xn);
                            y.Add(yn);
                        }
                    }
                    //#7
                    drow.DrowEllipse(xp, yp, sizeEllips, true);
                }
                //#3
                //проверка на отсутствие свободных узлов
                if (x.Count == 0)
                {
                    timeDrow.Stop();
                    drow.Show();
                    drow.Title = "PercolationModel " + timeDrow.ElapsedMilliseconds.ToString() + " мс";
                    return timeDrow.ElapsedMilliseconds.ToString() + " мс";
                }
                rnd = random.Next(0, x.Count);
                xp = (int)x[rnd];
                yp = (int)y[rnd];
            }
            timeDrow.Stop();
            drow.Height = height;
            drow.Width = width;
            drow.Show();
            drow.Title = "PercolationModel " + timeDrow.ElapsedMilliseconds.ToString() + " мс";
            return timeDrow.ElapsedMilliseconds.ToString() + " мс";
        }
    }
}
