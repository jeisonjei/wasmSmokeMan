using System;
using System.Collections.Generic;
using System.Text;

namespace wasmSmokeMan.Shared.Functions
{
    public class F200
    {
        private readonly double roomArea;
        private readonly double roomHeight;

        public F200(double roomArea,double roomHeight)
        {
            this.roomArea = roomArea;
            this.roomHeight = roomHeight;
        }
        public double Comp()
        {
            return roomArea* roomHeight;
        }
    }
}
