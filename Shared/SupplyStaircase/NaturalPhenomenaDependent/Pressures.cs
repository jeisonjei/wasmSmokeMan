using System;
using wasmSmokeMan.Shared.SupplyStaircase.CompoundObjects;
using wasmSmokeMan.Shared.SupplyStaircase.NaturalPhenomenaIndependent;
using wasmSmokeMan.Shared.SupplyStaircase.SemanticObjects;

namespace wasmSmokeMan.Shared.SupplyStaircase.NaturalPhenomenaDependent
{
    public class Pressures
    {
        private double outsideDeltaPMax;
        public Pressures(Floors floors,Climate climate)
        {
            Floors = floors;
            Climate = climate;
        }
        public Pressures() { }

        public StairCase Stair { private get; set; }
        public Floors Floors { get; set; }
        public Climate Climate { private get; set; }

        public double OutsideDeltaPMax
        {
            get => 0.55 * Floors.HeightFromFirstToTopOfTheShaft * (Climate.SpecificGravityOutside - Climate.SpecificGravityInside) + 0.03 * (Climate.SpecificGravityOutside) * Math.Pow(2, 2);
            set
            {
                outsideDeltaPMax = value;

            }
        }
    }
}