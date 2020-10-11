using System;
using System.Collections.Generic;
using System.Text;

namespace wasmSmokeMan.Shared.Functions
{
    //формула 17 - массовый расход удаляемого дыма из коридора
    public class F17
    {
        private readonly double doorWidth;
        private readonly double doorHeight;
        private readonly double ksm;

        public F17(double doorWidth,double doorHeight,double ksm)
        {
            this.doorWidth = doorWidth;
            this.doorHeight = doorHeight;
            this.ksm = ksm;
        }

        public double Comp()
        {
            return ksm * ((doorWidth * doorHeight) * (Math.Pow(doorHeight, 0.5)));
        }
    }
}
