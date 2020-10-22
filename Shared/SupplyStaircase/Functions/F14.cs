using System;

namespace wasmSmokeMan.Shared.SupplyStaircase.Functions
{
    public class F14
    {
        private readonly double tempRoom;
        private readonly double fireLoadRoomSurface;

        public F14(double tempRoomCelsuis,double fireLoadRoomSurface)
        {
            this.tempRoom = tempRoomCelsuis+273;
            this.fireLoadRoomSurface = fireLoadRoomSurface;
        }
        public double Comp()
        {
            return tempRoom + 224 * Math.Pow(fireLoadRoomSurface, 0.528);
        }
    }
}
