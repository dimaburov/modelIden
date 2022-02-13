using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course
{
    class StandartModel
    {
        public String StandartModels(int width, int height, int sizeEllips, Stopwatch timeDrow, int countColour,
            Boolean colour, int countParticle, ArrayList x, ArrayList y, int[,] particleCoordinates, int[] dx, int[] dy)
        {
            ///Стандартная модель Идена
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
                x.Add(xp + dx[i]);
                y.Add(yp + dy[i]);
            }
            //#2
            //#3
            int rnd = random.Next(0, x.Count);
            xp = (int)x[rnd];
            yp = (int)y[rnd];
            //while (xp < width || yp < height)
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
                //#3
                rnd = random.Next(0, x.Count);
                xp = (int)x[rnd];
                yp = (int)y[rnd];
            }
            timeDrow.Stop();
            drow.Height = height;
            drow.Width = width;
            drow.Show();
            drow.Title = "StandartModel " + timeDrow.ElapsedMilliseconds.ToString() + " мс";
            return timeDrow.ElapsedMilliseconds.ToString() + " мс";
            //Очистка памяти пробник
            //GC.Collect();
            //GC.WaitForPendingFinalizers();
        }
    }
}
