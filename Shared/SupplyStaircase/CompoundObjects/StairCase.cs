using wasmSmokeMan.Shared.SupplyStaircase.SemanticObjects;
using wasmSmokeMan.Shared.SupplyStaircase.SimpleObjects;

namespace wasmSmokeMan.Shared.SupplyStaircase.CompoundObjects
{
    public class StairCase
    {
        public StairCase(double area, Portal portalType, Floors floors, DoorOutside doorOutside, int quDoorOutside, DoorInside doorInside,Window window)
        {
            Area = area;
            PortalType = portalType;
            Floors = floors;
            DoorOutside = doorOutside;
            QuDoorOutside = quDoorOutside;
            DoorInside = doorInside;
            Window = window;
        }
        public StairCase() { }

        private double ksiR;

        public double Area { get; set; }
        public Floors Floors { get; set; }
        public DoorOutside DoorOutside { get; set; }
        public DoorInside DoorInside { get; set; }
        public Window Window { get; }
        public int QuDoorOutside { get; set; }
        public Portal PortalType { get; set; }
        public double KsiR
        {
            get
            {
                if (PortalType == Portal.Straight)
                {
                    ksiR = 0;
                }
                else if (PortalType == Portal.Angular)
                {
                    ksiR = 0.99;
                }
                else if (PortalType == Portal.ZShape)
                {
                    ksiR = 2.9;
                }
                return ksiR;
            }
        }

        

        public enum Portal
        {
            Straight,
            Angular,
            ZShape
        }
    }

}
