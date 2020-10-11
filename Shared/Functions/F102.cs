using System;
using System.Collections.Generic;
using System.Text;

namespace wasmSmokeMan.Shared.Functions
{
    public class F102
    {
        private readonly double massMaterial;
        private readonly double heatCombustionMaterial;
        private readonly double heatCombustionWood;
        private readonly double surfaceArea;
        private readonly double openingArea;

        public F102(double massMaterial,double heatCombustionMaterial,double surfaceArea,double openingArea)
        {
            this.massMaterial = massMaterial;
            this.heatCombustionMaterial = heatCombustionMaterial;
            this.heatCombustionWood = 13.8;
            this.surfaceArea = surfaceArea;
            this.openingArea = openingArea;
        }
        public double Comp()
        {
            return (massMaterial * heatCombustionMaterial) / ((surfaceArea - openingArea) * heatCombustionWood);
        }
    }
}
