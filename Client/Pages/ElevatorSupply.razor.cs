using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wasmSmokeMan.Client.Pages
{
    public partial class ElevatorSupply:ComponentBase
    {

        #region variables

        #endregion
        #region labels
        public string TempFN { get; set; } = "Температура";
        public string TempPC { get; set; } = "Температура наружного и внутреннего воздуха. При расчёте приточных противодымных систем температура наружного воздуха принимается для зимнего периода";
        public string TempRef { get; set; } = "СП 131.13330 Таблица 3.1 Колонка 5";
        #endregion
        void AssignShowResult()
        {

        }
    }
}
