using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using wasmSmokeMan.Shared.CompoundObjects;
using wasmSmokeMan.Shared.NaturalPhenomenaDependent;
using wasmSmokeMan.Shared.NaturalPhenomenaIndependent;
using wasmSmokeMan.Shared.SemanticObjects;
using wasmSmokeMan.Shared.SimpleObjects;
namespace wasmSmokeMan.Shared.Functions
{
    public class MethodsSupplyStair
    {
        //пока расчёт для лестничной клетки, с первого этажа которой нет выхода в холл или коридор, а есть выход на улицу
        public MethodsSupplyStair(StairCase stairCase, Climate climate)
        {
            Stair = stairCase;
            Climate = climate;
            CompPwind();
            CompPs2_23();
            CompGsa_24();
        }
        public double Kww { get; set; } = 0.8;
        public double Kw0 { get; set; } = -0.6;
        public double g { get; set; } = 9.81;

        public StairCase Stair { get; set; }

        public Climate Climate { get; set; }
        //дополнительная формула - ветровой напор в лестничной клетке
        public double Pwind { get; set; }
        //формула 23
        public double Ps2_23 { get; set; }
        //формула 24
        public double Gsa_24 { get; set; }
        public double Lv { get; set; }

        public List<EachFloorResult> EachFloorResults { get; set; } = new List<EachFloorResult>();
        public void CompCollect()
        {
            double p = Ps2_23;
            double Gsd, Gsw, v;
            double Gsum = Gsa_24;
            //пока рассматривается только случай, когда первый этаж ЛК носит индекс 1
            foreach (var lev in Stair.Floors.Levels.Where(lev => lev.Key > (Stair.Floors.Levels.First().Key + 1)))
            {
                v = Gsum / (Climate.DensitySupply * Stair.Area);
                p = p + 0.5 * 60 * Climate.DensitySupply * Math.Pow(v, 2);
                Gsd = Comp29Comp30(p, lev.Value.level);
                Stair.Window.CompLeakage(p, lev.Value.level);
                Gsw = Stair.Window.Leakage;
                Gsum = Gsum + Gsd + Gsw;
                EachFloorResults.Add(new EachFloorResult { LevelKey = lev.Key, LevelValue = lev.Value.level, V = v, P = p, Gsd = Gsd, Gsw = Gsw, Gsum = Gsum });
            }
            Lv = EachFloorResults.Last().Gsum * 3600 / Climate.DensityOutside;
            var l = EachFloorResults.OrderByDescending(res => res.LevelKey);
        }
        public class EachFloorResult
        {
            public int LevelKey { get; set; }
            public double LevelValue { get; set; }
            public double V { get; set; }
            public double P { get; set; }
            public double Gsd { get; set; }
            public double Gsw { get; set; }
            public double Gsum { get; set; }
        }
        public void CompPwind()
        {
            Pwind = 0.25 * (Kww - Kw0) * Climate.DensityOutside * Math.Pow(Climate.WindVelocity, 2);
        }
        public void CompPs2_23()
        {
            Ps2_23 = 20 - g * (Stair.Floors.Levels.First().Value.height + 0.5 * Stair.DoorInside.Height) * (Climate.DensitySupply - Climate.DensityInside) + Pwind;
        }
        public void CompGsa_24()
        {
            double ksiD = 2.44;
            double Z = 1;
            Gsa_24 = Math.Pow(((2 * Climate.DensitySupply / ((((Stair.QuDoorOutside * ksiD) + (Stair.KsiR) + 1) / (Math.Pow(Stair.DoorOutside.Area, 2))) + ((60 * Z) / (Math.Pow(Stair.Area, 2))))) * (Pwind + 20 - (g * (Stair.Floors.Levels.First().Value.height + 0.5 * Stair.DoorInside.Height) * (Climate.DensitySupply - Climate.DensityInside)) + (0.5 * g * Stair.DoorOutside.Height * (Climate.DensityOutside - Climate.DensitySupply)))), 0.5);
        }
        public double Comp25(
            double flowSmokeMain,
            double quStaircaseSameCorridor
            )
        {
            return flowSmokeMain / quStaircaseSameCorridor;
        }

        public double Comp26(
                double pressurePreviousFloor,
                double velocityAirPreviousFloor
            )
        {
            double ksiS = 60;
            return pressurePreviousFloor + 0.5 * ksiS * Climate.DensitySupply * Math.Pow(velocityAirPreviousFloor, 2);
        }
        public double Comp27(
                double floowAirPreviousFloor,
                double leakageCurrentFloorWindow,
                double leakageCurrentFloorDoor
            )
        {
            return floowAirPreviousFloor + leakageCurrentFloorWindow + leakageCurrentFloorDoor;
        }
        public double Comp28(
                double flowAirCurrentFloor
            )
        {
            return flowAirCurrentFloor / (Climate.DensitySupply * Stair.Area);
        }
        public double Comp29Comp30(
                double pressureCurrentFloor,
                double floorLevelCurrent
            )
        {
            double g = 9.81;
            return (Stair.DoorInside.Area / Math.Pow(Stair.DoorInside.SmokeResistance, 0.5)) * Math.Pow((pressureCurrentFloor + g * (floorLevelCurrent + 0.5 * Stair.DoorInside.Height) * (Climate.DensitySupply - Climate.DensityInside) - Pwind), 0.5);

        }
        public double Comp31(
                double windowArea,
                double airResistanceNorm,
                double pressureCurrentFloorStaircase,
                double floorLevelCurrent,
                double buildingHeightFromFirstToTopOfTheShaft
            )
        {
            double specificGravityOutside = 3463 / (273 + (-25));
            double specificGravityInside = 3463 / (273 + 20);
            double deltaP = 0.55 * buildingHeightFromFirstToTopOfTheShaft * (specificGravityOutside - specificGravityInside) + 0.03 * (specificGravityInside) * Math.Pow(2, 2);
            double windowAirResistanceNorm = (1 / airResistanceNorm) * Math.Pow(deltaP / 10, (double)(2 / 3));
            double g = 9.81;
            return (windowArea / (windowAirResistanceNorm * 3600)) * Math.Pow(((pressureCurrentFloorStaircase + g * (floorLevelCurrent + 0.5 * (2.1)) * (specificGravityOutside - specificGravityInside))), 0.67);
        }
    }


}

