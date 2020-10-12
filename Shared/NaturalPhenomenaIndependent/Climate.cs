
namespace wasmSmokeMan.Shared.NaturalPhenomenaIndependent
{
    public class Climate
    {
        public Climate(double tempOutside,double tempInside,double windVelocity)
        {
            TempOutside = tempOutside;
            TempInside = tempInside;
            WindVelocity = windVelocity;
        }
        private double tempSupply;
        private double densityOutside;
        private double densityInside;
        private double densitySupply;
        private double specificGravityOutside;
        private double specificGravityInside ;

        public double TempOutside { get; set; }
        public double TempInside { get; set; }
        public double TempSupply
        {
            get
            {
                tempSupply = (TempOutside + TempInside) / 2;
                return tempSupply;
            }
        }
        public double WindVelocity { get; set; }
        public double DensityOutside
        {
            get
            {
                //Fluid air = new Fluid(FluidList.Air);
                //air.UpdatePT(Pressure.FromBars(1.013), Temperature.FromDegreesCelsius(TempOutside));
                //densityOutside = air.Density.Value;
                densityOutside = 353 / (273 + TempOutside);
                return densityOutside;
            }
        }
        public double DensityInside
        {
            get
            {
                //Fluid air = new Fluid(FluidList.Air);
                //air.UpdatePT(Pressure.FromBars(1.013), Temperature.FromDegreesCelsius(TempInside));
                //densityInside = air.Density.Value;
                densityInside = 353 / (273 + TempInside);
                return densityInside;
                
            }
        }
        public double DensitySupply
        {
            get
            {
                //Fluid air = new Fluid(FluidList.Air);
                //air.UpdatePT(Pressure.FromBars(1.013), Temperature.FromDegreesCelsius(TempSupply));
                //densitySupply = air.Density.Value;
                densitySupply = 353 / (273 + TempSupply);
                return densitySupply;
            }
        }

        public double SpecificGravityOutside { get => 3463 / (273 + TempOutside); }
        public double SpecificGravityInside { get => 3463 / (273 + TempInside); }
    }
}
