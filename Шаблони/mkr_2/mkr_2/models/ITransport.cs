using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mkr_2.models
{
    internal interface ITransport
    {
        double CalculateCost(double distance);
    }
}
