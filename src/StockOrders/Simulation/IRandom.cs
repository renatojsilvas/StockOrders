using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockOrders.Simulation
{
    public interface IRandom
    {
        int Next(int max);
        int Next(int min, int max);
        double NextDouble();

    }
}
