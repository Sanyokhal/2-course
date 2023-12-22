using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mkr_2.models
{
    internal class TaxiStrategy:ITransport
    {
        public double CalculateCost(double distance)
        { 
            double costPerKilometer = 20;
            return distance * costPerKilometer;
        }
    }
}
