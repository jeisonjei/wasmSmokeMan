namespace wasmSmokeMan.Shared.SupplyStaircase.SimpleObjects
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
