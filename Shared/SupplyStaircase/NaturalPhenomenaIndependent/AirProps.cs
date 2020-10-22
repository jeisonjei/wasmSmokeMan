namespace wasmSmokeMan.Shared.SupplyStaircase.NaturalPhenomenaIndependent
{
    public class AirProps
    {
        private double density;
       public AirProps(double temperature)
        {
            Temperature = temperature;
        }

        public double Temperature { get; set; }
        public double Density
        {
            get
            {
                density = 353 / (Temperature + 273);
                return density;
            }
            set
            {
                density = value;
            }
        }
        
        
    }
}
