using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course
{
    class TheStrongestGroup
    {
        //Массивы для хранения союзных групп
        ArrayList group_1 = new ArrayList();
        ArrayList group_2 = new ArrayList();
        ArrayList group_3 = new ArrayList();
        ArrayList group_4 = new ArrayList();
        //Массив для хранения состояния группы => жива/мертва
        int[] deadGroup = new int[4];

        public String TheStrongestGroups(int width, int height, int sizeEllips, Stopwatch timeDrow,
        int countParticle, ArrayList x, ArrayList y, int[,] particleCoordinates, int[] dx, int[] dy)
        {

            //Игровая модель взаимодействия 4 групп молекул
            //Случайным образом каждой из 4 групп добавляет по молекуле
            //При столкновении групп молекул считает количетсво молкул в каждой из них, и та в которой их оказалось больше выигрывает и поглощает вторую
            //Так как все группы в начальный момент времени находятся в одинаковых условиях, а процесс присоединения к ним молекул является случайным
            //Победа одной из этих групп является случайной
            //ЦЕЛЬ - моделирование взаимодействия молекулярных колоний на основе модели Идена

            //ПЕРЕПИСАТЬ ХОД РАБОТЫ ПРОГРАММЫ
            ///#0 размещаем в оперативной памяти массивы и инициализируем графическое окноW
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
            //Размещаем 4 точки на равных расстояних друг от друга
            int moving_x = width / 3;
            int moving_y = height / 3;
            drow.DrowSwitchColourseEllipse(moving_x, moving_y, sizeEllips, 1);
            drow.DrowSwitchColourseEllipse(moving_x+ moving_x, moving_y, sizeEllips, 2);
            drow.DrowSwitchColourseEllipse(moving_x, moving_y+ moving_y, sizeEllips, 3);
            drow.DrowSwitchColourseEllipse(moving_x+ moving_x, moving_y+ moving_y, sizeEllips, 4);
            particleCoordinates[moving_x, moving_y] = 1;
            particleCoordinates[moving_x + moving_x, moving_y] = 2;
            particleCoordinates[moving_x, moving_y + moving_y] = 3;
            particleCoordinates[moving_x + moving_x, moving_y + moving_y] = 4;
            //периметр 1 группы
            x = startPerimeter(moving_x, dx, x, 1);
            y = startPerimeter(moving_y, dy, y, 1);
            //периметр 2 группы
            x = startPerimeter(moving_x + moving_x, dx, x, 2);
            y = startPerimeter(moving_y, dy, y, 2);
            //периметр 3 группы
            x = startPerimeter(moving_x, dx, x, 3);
            y = startPerimeter(moving_y + moving_y, dy, y, 3);
            //периметр 4 группы
            x = startPerimeter(moving_x + moving_x, dx, x, 4);
            y = startPerimeter(moving_y + moving_y, dy, y, 4);
            //#3
            int rnd = random.Next(0, x.Count);
            int number_group = (int)x[rnd] % 10;
            int xp = (int)x[rnd] / 10;
            int yp = (int)y[rnd] / 10;
            //Массивы для хранения союзных групп
            //Сразу стартово добавляем в союзную группу и её саму
            group_1.Add(1);
            group_2.Add(2);
            group_3.Add(3);
            group_4.Add(4);
            //Количество частиц в группе хранится в массиве
            int[] countCellGroup = new int[4];
            //сразу добавим по стартовой ячейке в каждую
            for (int i = 0; i < 4; i++)
            {
                countCellGroup[i] = 1;
            }

            while ((xp - sizeEllips - 2 > 0) && (yp - sizeEllips - 2 > 0) && (yp + sizeEllips + 2 < height) && (xp + sizeEllips + 2 < width))
            {
                //#4
                countCellGroup[number_group - 1]++;
                particleCoordinates[xp, yp] = number_group;
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
                        x.Add(xn * 10 + number_group);
                        y.Add(yn * 10 + number_group);
                    }
                    //на периметре обнаружена клетка от другой группы
                    //и эта группа не входит в союзную группу
                    else if (cehckAlliedGroup(particleCoordinates[xn, yn] ,number_group))
                    {
                        //сравниваем эти группы
                        groupComparison(checkGroup(number_group), checkGroup(particleCoordinates[xn, yn]), countCellGroup, number_group, particleCoordinates[xn, yn]);
                    }
                }
                //#7
                drow.DrowSwitchColourseEllipse(xp, yp, sizeEllips, noumberLiveGroup(number_group));
                //#3
                rnd = random.Next(0, x.Count);
                number_group = (int)x[rnd] % 10;
                //
                xp = (int)x[rnd]/10;
                yp = (int)y[rnd]/10;
            }
            //Проверяем какая группа выиграла
            int victoryGroup = 0;
            for (int i = 0; i < 4; i++)
            {
                if (deadGroup[i] == 0) victoryGroup = i+1;
            }
            if (victoryGroup == 0) Console.WriteLine("ERROR vicrory");
            timeDrow.Stop();
            drow.Height = height;
            drow.Width = width;
            drow.Show();
            drow.Title = "ProbabilityPerSide " + timeDrow.ElapsedMilliseconds.ToString() + " мс" + " victory group " + colourVictory(victoryGroup);
            return timeDrow.ElapsedMilliseconds.ToString() + " мс";

        }

        //Только для красоты. Возвращает цвет выигревшей группы
        private String colourVictory(int groupVictory)
        {
            switch (groupVictory)
            {
                case 1:
                    return "Blue";
                case 2:
                    return "Red";
                case 3:
                    return "Black";
                case 4:
                    return "Green";
            }
            //Ошибка
            return "ERROR";
        }

        private ArrayList startPerimeter(int x_yp, int[] dx_y, ArrayList x_y, int group)
        {
            for (int i = 0; i < 4; i++)
            {
                x_y.Add((x_yp + dx_y[i]) * 10 + group);
            }
            return x_y;
        }
        //Номер живой группы
        private int noumberLiveGroup(int group)
        {
            ArrayList arrayGroup = checkGroup(group);
            //находим живую группу в списке союзников
            for (int i = 0; i < arrayGroup.Count; i++)
            {
                if (deadGroup[int.Parse(arrayGroup[i].ToString()) - 1] == 0) return int.Parse(arrayGroup[i].ToString());
            }
            //Ошибка
            Console.WriteLine("ERROR noumberLiveGroup");
            return 0;
        }
        //проверяем является ли группа союзной и её номер отличен нашего
        private Boolean cehckAlliedGroup(int group, int anotherGroup )
        {
            //првоеряем является ли она нашей союзной группой
            ArrayList allieaGroup = checkGroup(group);
            for (int i = 0; i < allieaGroup.Count; i++)
            {
                if (int.Parse(allieaGroup[i].ToString()) == anotherGroup) return false;
            }
            //Не является союзной группой
            return true;
        }

        //возвращаем массив союзных групп
        private ArrayList checkGroup(int group)
        {
            switch(group)
            {
                case (1):
                    return group_1;
                case (2):
                    return group_2;
                case (3):
                    return group_3;
                case (4):
                    return group_4;

            }
            //Ошибка
            Console.WriteLine("ERROR checkGroup");
            return group_1;
        }

        //Надо пройтись по всем нашим союзным группам и добавить к ним союзные группы встреченной группы
        private void addingGroups(ArrayList group, ArrayList another_group)
        {
            foreach (var item in group.ToArray())
            {
                foreach (var item_another in another_group)
                {
                    //возвращаем группу из списка собюзных и добавляем все элементы встреченной группы
                    checkGroup(int.Parse(item.ToString())).Add(int.Parse(item_another.ToString()));
                }

            }
        }

        //количество клеток у группах, номер 1 группы, номер 2 группы, массив для хранения умерших групп 
        private void groupComparison(ArrayList groupArray_1, ArrayList groupArray_2, int[] countCellGroup, int group_1, int group_2)
        {
            //случайное число
            Random random = new Random();
            int countCellX = 0;
            int countCellY = 0;
            //Проходим по всем союзным группам 1 группы и считаем совместное количество клеток
            for (int i = 0; i < groupArray_1.Count; i++)
            {
                countCellX = countCellX + countCellGroup[int.Parse(groupArray_1[i].ToString())-1];
            }
            
            //Проходим по всем союзным группам 2 группы и считаем совместное количество клеток
            for (int i = 0; i < groupArray_2.Count; i++)
            {
                countCellY = countCellY + countCellGroup[int.Parse(groupArray_2[i].ToString())-1];
            }
            //Проверяем в какой группе больше ячеек
            //Добавляем к одной групее собюзным групп другую группу союзных групп 
            addingGroups(checkGroup(group_1), checkGroup(group_2));
            addingGroups(checkGroup(group_2), checkGroup(group_1));
            //надо найти у проигравшей группы из всех союзников живую и пометить её как мёртвую
            if (countCellX > countCellY)
            {
                deadGroup[noumberLiveGroup(group_2) - 1] = 1;
            }
            else if(countCellX < countCellY)
            {
                deadGroup[noumberLiveGroup(group_1) - 1] = 1;
            }
            //countCellX = countCellY
            else
            {
                //выбираем победителя случайно
                if (random.Next(0, 1) == 0)
                {
                    deadGroup[noumberLiveGroup(group_2) - 1] = 1;
                }
                else
                {
                    deadGroup[noumberLiveGroup(group_1) - 1] = 1;
                }
            }
        }

    }
}
