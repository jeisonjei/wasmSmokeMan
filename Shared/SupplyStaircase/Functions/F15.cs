namespace wasmSmokeMan.Shared.SupplyStaircase.Functions
{
    public class F15
    {
        private readonly double tempRoomMax;

        public F15(double tempRoomMax)
        {
            this.tempRoomMax = tempRoomMax;
        }
        public double Comp()
        {
            return 0.8 * tempRoomMax;
        }
    }
}
