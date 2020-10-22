namespace wasmSmokeMan.Shared.SupplyStaircase.Functions
{
    public class F101
    {
        private readonly double massMaterial;
        private readonly double heatCombustionMaterial;
        private readonly double roomArea;
        private readonly double heatCombustionWood;

        public F101(double massMaterial,double heatCombustionMaterial,double roomArea)
        {
            this.massMaterial = massMaterial;
            this.heatCombustionMaterial = heatCombustionMaterial;
            this.roomArea = roomArea;
            this.heatCombustionWood = 13.8;
        }
        public double Comp()
        {
            return (massMaterial * heatCombustionMaterial) / ((roomArea) * heatCombustionWood);
        }
    }
}
