namespace wasmSmokeMan.Shared.SupplyStaircase.Functions
{
    public class F107
    {
        private readonly double heatCombustionMaterial;

        public F107(double heatCombustionMaterial)
        {
            this.heatCombustionMaterial = heatCombustionMaterial;
        }
        public double Comp()
        {
            return 0.263 * (heatCombustionMaterial);
        }
    }
}
