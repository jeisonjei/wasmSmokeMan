using System;
using System.Collections.Generic;
using System.Linq;
using UnitsNet;
using wasmSmokeMan.Shared.CompoundObjects;
using wasmSmokeMan.Shared.NaturalPhenomenaDependent;
using wasmSmokeMan.Shared.NaturalPhenomenaIndependent;
using wasmSmokeMan.Shared.SemanticObjects;
using wasmSmokeMan.Shared.SimpleObjects;

namespace wasmSmokeMan.Shared.SemanticObjects
{
    //пока что нет возможности задания отметки надземного этажа (например если ЛК начинается на 2-ом этаже и отметка этого этажа отлична от 0). При необходимости добавить
    public class Floors
    {
        public Floors() { }
        public Floors(int first, int qu, double exhaustShaftTopOffsetFromRoof)
        {
            Qu = qu;
            NotSpecified = Qu;
            First = first;
            if (First > 0)
            {
                Last = Qu + First - 1;
            }
            else if (First < 0)
            {
                Last = Qu + First;
            }
            else if (First == 0)
            {
                throw new ArithmeticException($"Индекс первого этажа не может быть {0}. Первый этаж может быть или больше нуля : 1,2,3 итд, или меньше нуля -1,-2,-3 итд");
            }
            ExhaustShaftTopOffsetFromRoof = exhaustShaftTopOffsetFromRoof;
            //добавление метода в событие
            LevelsComplete += CompLevels;
        }
        //перегрузка конструктора с высотой вытяжной шахты над уровнем кровли по умолчанию
        public Floors(int first, int qu)
        {
            Qu = qu;
            NotSpecified = Qu;
            First = first;
            if (First > 0)
            {
                Last = Qu + First - 1;
            }
            else if (First < 0)
            {
                Last = Qu + First;
            }
            else if (First == 0)
            {
                throw new ArithmeticException($"Индекс первого этажа не может быть {0}. Первый этаж может быть или больше нуля : 1,2,3 итд, или меньше нуля -1,-2,-3 итд");
            }
            //добавление метода в событие
            LevelsComplete += CompLevels;
        }
        //событие для выполнения когда все этажи добавлены
        public delegate void Void();
        public event Void LevelsComplete;

        private int qu;
        public int Qu
        {
            get => this.qu;
            set
            {
                Levels.Clear();
                this.qu = value;
                NotSpecified = this.qu;
                if (First > 0)
                {
                    Last = qu + First - 1;
                }
                else if (First < 0)
                {
                    Last = qu + First;
                }
                
            }
        }
        public int NotSpecified { get; set; }
        private int first;
        public int First
        {
            get => this.first;
            set
            {
                Levels.Clear();
                NotSpecified = this.qu;
                this.first = value;
                if (First > 0)
                {
                    Last = qu + First - 1;
                }
                else if (First < 0)
                {
                    Last = qu + First;
                }

            }
        }

        public int Last { get; set; }
        public SortedList<int, (double height, double level)> Levels { get; set; } = new SortedList<int, (double height, double level)>();
        public double BuildingHeightOverall { get; set; }
        private double heightFromFirstToTopOfTheShaft;

        public double BuildingHeightBelowZero { get; set; }
        public double BuildingHeightAboveZero { get; set; }
        public double FirstFloorLevel { get; set; }
        public double LastFloorLevel { get; set; }
        public double RoofFloorLevel { get; set; }
        public double ExhaustShaftTopOffsetFromRoof { get; set; } = 2;
        public double HeightFromFirstToTopOfTheShaft { get => RoofFloorLevel + ExhaustShaftTopOffsetFromRoof; internal set => heightFromFirstToTopOfTheShaft = value; }

        //этот метод выполняется только тогда, когда все этажи добавлены, то есть переменная NotSpecified==0
        public void CompLevels()
        {
            //общая высота всех этажей
            BuildingHeightOverall = Math.Round(Levels.Sum(lev => lev.Value.height), 5);
            //высота подземных этажей (этажа с индексом 0 быть не может - в методе AddRange() в этом случае добавления не происходит
            BuildingHeightBelowZero = Levels.Where(lev => lev.Key < 0).Sum(lev => lev.Value.height);
            //высота надземных этажей
            BuildingHeightAboveZero = Levels.Where(lev => lev.Key > 0).Sum(lev => lev.Value.height);
            //пока подразумевается, что в здании есть надземные этажи, для случай, когда все этажи подземные, пока не обрабатывается
            FirstFloorLevel = BuildingHeightAboveZero - BuildingHeightOverall;
            LastFloorLevel = BuildingHeightAboveZero - Levels.Last().Value.height;
            RoofFloorLevel = BuildingHeightAboveZero;
            double _ = FirstFloorLevel;
            for (int i = Levels.First().Key; i <= Levels.Last().Key; i++)
            {
                if (i == 0)
                {
                    continue;
                }

                Levels[i] = (Levels[i].height, _);
                _ += Levels[i].height;
            }

        }

        public void AddRange((int, int) range, double height)
        {
            if (range.Item2 < range.Item1)
            {
                throw new ArithmeticException("Неправильно указан диапазон: этажи указываются в порядке возрастания");
            }
            if (range.Item1 < First)
            {
                throw new ArithmeticException($"Первый этаж диапазона {range.Item1} не может быть ниже первого этажа здания {First}. Измените диапазон или измените первый этаж здания");
            }
            if (range.Item2 > Last)
            {
                throw new ArithmeticException($"Последний этаж диапазона {range.Item2} не может быть выше последнего этажа здания {Last}. Измените диапазон или измените количество здания");
            }
            for (int i = range.Item1; i <= range.Item2; i++)
            {
                if (i == 0)
                {
                    continue;
                }
                Levels.Add(i, (height, 0));
            }
            if (range.Item1 < 0)
            {
                //если начало диапазона этажей < 0, то в диапазон попадает 0, поэтому выражение другое
                NotSpecified = NotSpecified - (range.Item2 - range.Item1);
            }
            else
            {
                NotSpecified = NotSpecified - (range.Item2 + 1 - range.Item1);
            }
            if (NotSpecified == 0)
            {
                LevelsComplete();
            }
        }

        public void AddSingle(int level, double height)
        {
            if (NotSpecified <= 0)
            {
                throw new ArithmeticException("Указаны все этажи");
            }
            if (level == 0)
            {
                throw new ArithmeticException($"В списке этажей не может быть этажа с индексом {level}. Этажи могут иметь индексы больше нуля (1,2,3...) или меньше нуля (-1,-2,-3...)");
            }
            if (level < First)
            {
                throw new ArithmeticException($"Добавляемый этаж {level} не может быть ниже первого этажа здания {First}. Измените добавляемый этаж или измените первый этаж здания");
            }
            if (level > Last)
            {
                throw new ArithmeticException($"Добавляемый этаж {level} не может быть выше последнего этажа здания {Last}. Измените добавляемый этаж или измените количество этажей здания");
            }

            Levels.Add(level, (height, 0));
            NotSpecified--;
            if (NotSpecified == 0)
            {
                LevelsComplete();
            }
        }
        public void RemoveRange((int, int) range)
        {
            for (int i = range.Item1; i <= range.Item2; i++)
            {
                Levels.Remove(i);
                NotSpecified++;
            }
            List<int> keys = new List<int>(Levels.Keys);
            foreach (var key in keys)
            {
                Levels[key] = (Levels[key].height, 0);
            }

        }
        public void RemoveSingle(int level)
        {
            Levels.Remove(level);
            NotSpecified++;
            List<int> keys = new List<int>(Levels.Keys);
            foreach (var key in keys)
            {
                Levels[key] = (Levels[key].height, 0);
            }
        }
    }

}
