using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using wasmSmokeMan.Shared.CompoundObjects;
using wasmSmokeMan.Shared.NaturalPhenomenaDependent;
using wasmSmokeMan.Shared.NaturalPhenomenaIndependent;
using wasmSmokeMan.Shared.SemanticObjects;
using wasmSmokeMan.Shared.SimpleObjects;

namespace wasmSmokeMan.Shared.NaturalPhenomenaIndependent
{
    public class AirProps
    {
        private double density;
       public AirProps(double temperature)
        {
            Temperature = temperature;
        }

        public double Temperature { get; set; }
        public double Density
        {
            get
            {
                density = 353 / (Temperature + 273);
                return density;
            }
            set
            {
                density = value;
            }
        }
        
        
    }
}
