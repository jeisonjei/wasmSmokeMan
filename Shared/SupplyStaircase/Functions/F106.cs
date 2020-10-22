using System;
using System.Collections.Generic;
using System.Linq;

namespace wasmSmokeMan.Shared.SupplyStaircase.Functions
{
    public class F106
    {
        private readonly List<(double, double)> openingAreaAndHeight;
        private readonly double volume;

        public F106(List<(double,double)> openingAreaAndHeight,double volume)
        {
            if (openingAreaAndHeight is null)
            {
                throw new ArgumentNullException(nameof(openingAreaAndHeight));
            }

            this.openingAreaAndHeight = openingAreaAndHeight;
            this.volume = volume;
        }
        public double Comp()
        {
            return (openingAreaAndHeight.Sum(x => x.Item1 * (Math.Pow(x.Item2, 0.5)))) / (Math.Pow(volume, (double)2 / 3));
        }
    }
}
