using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mkr_2.models
{
    internal class ScooterStrategy:ITransport
    {
        public double CalculateCost(double distance)
        {
            double km_per_minute = 0.6;
            double costPerMinute = 3;
            return (distance/km_per_minute) * costPerMinute;
        }
    }
}
