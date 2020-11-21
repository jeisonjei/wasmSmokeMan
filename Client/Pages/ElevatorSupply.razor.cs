using MatBlazor;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wasmSmokeMan.Shared.SupplyElevator;

namespace wasmSmokeMan.Client.Pages
{
    public partial class ElevatorSupply:ComponentBase
    {
        #region injections
        [Inject] IMatToaster Toaster { get; set; }
        [Inject] Climate Climate { get; set; }
        [Inject] Floors Floors { get; set; }
        #endregion
        #region variables
        bool TableFloorsShow { get; set; } = false;
        #endregion
        #region labels
        public string PopTitle { get; set; } = "Информация";
        public string TempFN { get; set; } = "Температура";
        public string TempPC { get; set; } = "Температура наружного и внутреннего воздуха. При расчёте приточных противодымных систем температура наружного воздуха принимается для зимнего периода";
        public string TempRef { get; set; } = "СП 131.13330 Таблица 3.1 Колонка 5";
        public string WindVelocityFN { get; set; } = "Скорость ветра";
        public string WindVelocityPC { get; set; } = "Максимальная из скоростей ветра по румбам за январь для района строительства";
        public string WindVelocityPR { get; set; } = "СП 131.13330 Таблица 3.1 Колонка 19";
        public string ChipsFloorsFN { get; set; } = "Этажи";
        public string ChipsFloorsPC { get; set; } = "В это поле добавляются этажи здания. Индекс этажа и высота вводятся в следующем формате:</br><code>[индекс];[высота]</code> - для добавления одного этажа, например</br><code>1;4.5</code> означает добавить 1-ый этаж, высотой 4.5 метра, или</br><code>[индекс первого этажа диапазона]-[индекс последнего этажа диапазона];[высота]</code> - для добавления диапазона этажей, например</br><code>2-6;3.15</code> означает добавить этажи со 2-го по 6-ой включительно высотой 3.15 метра. После ввода значений нужно нажать <code>Enter</code>, для удаления используется <code>Backspace</code> Отметки рассчитываются после добавления всех этажей";
        #endregion
        #region assigngs
        #region result
        void AssignShowResult()
        {

        }
        #endregion
        #region climate
        void ATempOutside(string tempOutside)
        {
            Climate.TempOutside = tempOutside.ToDouble();
        }
        void ATempInside(string tempInside)
        {
            Climate.TempInside = tempInside.ToDouble();
        }
        void AWindVelocity(string windVelocity)
        {
            Climate.WindVelocity = windVelocity.ToDouble();
        }
        #endregion
        #region floors
        void AFirstFloorIndex(string firstFloorIndex)
        {
            //очистить поле ввода высот этажей при изменении индекса первого этажа
            ChipsFloors.Clear();
            counter = ChipsFloors.Count();
            Floors.First = firstFloorIndex.ToInt();
        }
        void AQuFloors(string quFloors)
        {
            //очистить поле ввода высот этажей при изменении количества этажей.
            ChipsFloors.Clear();
            counter = ChipsFloors.Count();
            Floors.Qu = quFloors.ToInt();
        }
        List<string> ChipsFloors { get; set; } = new List<string>();
        int counter = 0;
        Stack<string> ranges = new Stack<string>();
        public void AFloorLevels()
        {
            counter++;
            try
            {
                if (ChipsFloors.Count() < counter)
                {
                    counter = ChipsFloors.Count();
                    Console.WriteLine($"counter : {counter}");
                    Console.WriteLine($"chipses : {ChipsFloors.Count()}");
                    if (ranges.First().Contains("-"))
                    {
                        Floors.RemoveRange((Convert.ToInt32(ranges.First().Split("-")[0]), Convert.ToInt32(ranges.First().Split("-")[1])));
                    }
                    else
                    {
                        Floors.RemoveSingle(Convert.ToInt32(ranges.First()));
                    }
                    ranges.Pop();
                    foreach (var item in ranges)
                    {
                        Console.WriteLine($"ranges: {item}");
                    }
                    return;
                }
                Console.WriteLine($"counter : {counter}");
                Console.WriteLine($"chipses : {ChipsFloors.Count()}");
                var lch = ChipsFloors.Last();
                int rangeStart, rangeEnd, single;
                double height;
                if (ChipsFloors.Last().Contains("-"))
                {
                    rangeStart = Convert.ToInt32(lch.Split("-")[0]);
                    rangeEnd = Convert.ToInt32(lch.Split(";")[0].Split("-")[1]);
                    height = Convert.ToDouble(lch.Split(";")[1]);
                    Floors.AddRange((rangeStart, rangeEnd), height);
                    ranges.Push(lch.Split(";")[0]);
                }
                else
                {
                    single = Convert.ToInt32(lch.Split(";")[0]);
                    height = Convert.ToDouble(lch.Split(";")[1]);
                    Floors.AddSingle(single, height);
                    ranges.Push(lch.Split(";")[0]);
                }
                foreach (var item in ranges)
                {
                    Console.WriteLine($"ranges: {item}");
                }
            }
            catch (Exception e)
            {

                Console.WriteLine($"{e.Message}");
                Toaster.Add(e.Message, MatToastType.Danger);
            }
        }

        #endregion
        #endregion
        #region helpers
        void TableFloorsShowToggle()
        {
            TableFloorsShow = true;
        }
        void TableFloorsHideToggle()
        {
            TableFloorsShow = false;
        }
        #endregion
    }
}
