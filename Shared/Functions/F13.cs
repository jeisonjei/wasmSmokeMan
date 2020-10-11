using System;
using System.Collections.Generic;
using System.Text;

namespace wasmSmokeMan.Shared.Functions
{
    public class F13
    {
        private readonly double tempRoom;
        private readonly double fireLoadRoomArea;

        public F13(double tempRoomCelsuis,double fireLoadRoomArea)
        {
            this.tempRoom = tempRoomCelsuis+273;
            this.fireLoadRoomArea = fireLoadRoomArea;
        }
        public double Comp()
        {
            return tempRoom + 940 * Math.Exp(0.0047 * fireLoadRoomArea - 0.141);
        }
    }
}
