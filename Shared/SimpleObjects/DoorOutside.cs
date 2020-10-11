using wasmSmokeMan.Shared.CompoundObjects;
using wasmSmokeMan.Shared.NaturalPhenomenaDependent;
using wasmSmokeMan.Shared.NaturalPhenomenaIndependent;
using wasmSmokeMan.Shared.SemanticObjects;
using wasmSmokeMan.Shared.SimpleObjects;

namespace wasmSmokeMan.Shared.SimpleObjects
{
    public class DoorOutside
    {
        private double width;
        private double height;

        public DoorOutside(double width, double height)
        {
            Width = width;
            Height = height;
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
        public double Area { get; set; }
    }

}
