using System;
using System.Collections.Generic;
using System.Text;

namespace wasmSmokeMan.Shared.Functions
{
    public class F105
    {
        private readonly double openingRoomVal;
        private readonly double flowAirCompleteCombustion;
        private readonly double volumeRoom;

        public F105(double openingRoomVal,double flowAirCompleteCombustion,double volumeRoom)
        {
            this.openingRoomVal = openingRoomVal;
            this.flowAirCompleteCombustion = flowAirCompleteCombustion;
            this.volumeRoom = volumeRoom;
        }
        public double Comp()
        {
            return ((4500 * Math.Pow(openingRoomVal, 3)) / (1 + 500 * Math.Pow(openingRoomVal, 3))) + (Math.Pow(volumeRoom, (double)1 / 3)) / (6 * flowAirCompleteCombustion);
        }
    }
}
