using wasmSmokeMan.Shared.SupplyStaircase.NaturalPhenomenaIndependent;

namespace wasmSmokeMan.Shared.SupplyStaircase.SimpleObjects
{
    public class DoorInside
    {
        private Type doorType;
        private double width;
        private double height;
        private Climate climate;

        public DoorInside(double width, double height, Type type, Climate climate)
        {
            Width = width;
            Height = height;
            Climate = climate;
            Area = width * height;
            if (type == Type.Usual)
            {
                SmokeResistance = 5300 / climate.DensitySupply;
            }
            else if (type == Type.SmokeResistant)
            {
                SmokeResistance = 60000 / climate.DensitySupply;
            }
        }
        public DoorInside() { }
        public DoorInside(double width, double height, double smokeResistance)
        {
            Width = width;
            Height = height;
            SmokeResistance = smokeResistance;
            Area = width * height;
        }

        public double Width
        {
            get => width; set
            {
                width = value;
                Area = Width * Height;
            }
        }
        public double Height
        {
            get => height; set
            {
                height = value;
                Area = Width * Height;
            }
        }
        public Climate Climate
        {
            get => climate; set
            {
                climate = value;
                SetDoorType(doorType);
            }
        }
        public double Area { get; set; }
        public double SmokeResistance { get; set; }

        public Type GetDoorType()
        {

            return doorType;
        }

        public void SetDoorType(Type value)
        {

            doorType = value;
            if (value == Type.Usual)
            {
                SmokeResistance = 5300 / Climate.DensitySupply;
            }
            else if (value == Type.SmokeResistant)
            {
                SmokeResistance = 60000 / Climate.DensitySupply;
            }

        }

        public enum Type
        {
            Usual,
            SmokeResistant
        }

    }

}
