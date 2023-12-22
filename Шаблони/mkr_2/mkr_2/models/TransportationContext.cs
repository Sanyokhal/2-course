using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mkr_2.models
{
    internal class TransportationContext
    {
        private ITransport _strategy;

        public TransportationContext(ITransport strategy)
        {
            _strategy = strategy;
        }

        public void SetStrategy(ITransport strategy)
        {
            _strategy = strategy;
        }

        public double CalculateCost(double distance)
        {
            return _strategy.CalculateCost(distance);
        }
    }
}
