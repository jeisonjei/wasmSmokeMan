using System;

namespace wasmSmokeMan.Shared.SupplyStaircase.Functions
{
    public class F103
    {
        private readonly double roomVolume;

        public F103(double roomVolume)
        {
            this.roomVolume = roomVolume;
        }
        public double Comp()
        {
            double val = (double)2 / 3;
            return 6 * Math.Pow(roomVolume, val);
        }
    }
}
