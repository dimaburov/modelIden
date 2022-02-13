using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course
{
    class NoiseModel
    {
        //Модель Идена с подавлением шума
        public String NoiseModels(int width, int height, int sizeEllips, Stopwatch timeDrow, int countColour,
        Boolean colour, int countParticle, ArrayList x, ArrayList y, int[,] particleCoordinates, int[] dx, int[] dy, float countVisits)
        {
            ///модель  Идена с подавлением шума
            ///#0 размещаем в оперативной памяти массивы и инициализируем графическое окно
            ///#1 размещаем в центре решётки зерно роста кластера
            ///#2 выбираем случайным образом ячейку периметра кластера
            ///#3 выполняем проверку на достижение переходим к пункту 8
            ///#4 присоединяем к кластеру новую ячейку, если её уже выбирали m раз
            ///#4*узел занимается с некоторой вероятностью f = [m] - m после [m] визитво и с вероятностью 1- f
            ///#4**после [m] + 1 визитов
            ///#5 исключаем выбранную ячейку из списка периметра кластера
            ///#5* подсчёт степени анизотропии кластера
            ///#6 вычисляем новывй периметр кластера
            ///#7 рисуем частицу на экране и переходим к пункту 2 
            ///#8 заканчиваем работу программы


            //случайное число
            Random random = new Random();
            //класс рисования
            WindowDrow drow = new WindowDrow();
            //начинает подсчёт потраченного времени
            timeDrow.Start();
            int[,] tesArray = new int[width, height];
            //вероятность для пункта 4* 
            int m_int = 0;
            double f = 0;
            if ((countVisits % 1) != 0)
            {
                f = 1 - (countVisits % 1);
                m_int = int.Parse((countVisits + f).ToString());
            }
            else
            {
                f = 1;
                m_int = int.Parse(countVisits.ToString());
            }
            //#1
            drow.DrowEllipse(width / 2, height / 2, sizeEllips, true);
            particleCoordinates[width / 2, height / 2] = 0;
            tesArray[width / 2, height / 2] = 1;
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
            //while (xp < width || yp < height)
            //#3
            while ((xp - sizeEllips - 2 > 0) && (yp - sizeEllips - 2 > 0) && (yp + sizeEllips + 2 < height) && (xp + sizeEllips + 2 < width))
            {
                //#4
                particleCoordinates[xp, yp]++;
                //if (particleCoordinates[xp, yp] == m_int) tesArray[xp, yp] = 1;
                //#5
                //проверка вероятности
                double rnd_f = random.Next(0, 10) / 10.0;
                if (particleCoordinates[xp, yp] == m_int)
                {
                    //попало в вероятность
                    if (rnd_f - f <= 0)
                    {
                        tesArray[xp, yp] = 1;
                    }
                }
                else if (particleCoordinates[xp, yp] >= m_int)
                {
                    //попало в вероятность
                    if (rnd_f - (1 - f) <= 0)
                    {
                        tesArray[xp, yp] = 1;
                    }
                }
                if (tesArray[xp, yp] == 1)
                {
                    countParticle++;
                    x.RemoveAt(rnd);
                    y.RemoveAt(rnd);
                    // x.Remove(xp);
                    //y.Remove(yp);
                    //#6
                    for (int i = 0; i < 4; i++)
                    {
                        int xn = xp + dx[i];
                        int yn = yp + dy[i];
                        if (tesArray[xn, yn] != 1)
                        {
                            x.Add(xn);
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
                }
                //#3
                rnd = random.Next(0, x.Count);
                xp = (int)x[rnd];
                yp = (int)y[rnd];
            }
            timeDrow.Stop();
            drow.Height = height;
            drow.Width = width;
            drow.Show();
            drow.Title = "Noise Model " + timeDrow.ElapsedMilliseconds.ToString() + " мс";
            return timeDrow.ElapsedMilliseconds.ToString() + " мс";
            //очистка памяти пробник
            //GC.Collect();
            //GC.WaitForPendingFinalizers();
        }
    }
}
