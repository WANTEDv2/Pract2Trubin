using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pract2Trub
{
    public class WeightPoints
    {
        public Points koefSignalPoints;
        public Points koefFunctionPoints;

        public WeightPoints(Points koefSignalPoints, Points koefFunctionPoints) {
            this.koefSignalPoints = koefSignalPoints;
            this.koefFunctionPoints = koefFunctionPoints;
        }
    }
}
