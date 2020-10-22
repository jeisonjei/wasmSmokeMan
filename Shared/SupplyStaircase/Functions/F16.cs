using System;

namespace wasmSmokeMan.Shared.SupplyStaircase.Functions
{
    public class F16
    {
        private readonly double tempRoom;
        private readonly double tempSmokeFromRoom;
        private readonly double areaCorridor;
        private readonly double lengthCorridor;
        private readonly double hsm;

        public F16(double tempRoomCelsuis,double tempSmokeFromRoom,double heightCorridor,double areaCorridor,double lengthCorridor)
        {
            this.tempRoom = tempRoomCelsuis+273;
            this.tempSmokeFromRoom = tempSmokeFromRoom;
            this.areaCorridor = areaCorridor;
            this.lengthCorridor = lengthCorridor;
            this.hsm = 0.55 * heightCorridor;
        }
        public double Comp()
        {
            return tempRoom + (1.22 * (tempSmokeFromRoom - tempRoom) * (2 * hsm + (areaCorridor / lengthCorridor))) / lengthCorridor * (1 - Math.Exp((-0.58 * lengthCorridor) / (2 * hsm + (areaCorridor / lengthCorridor))));
        }
    }
}
