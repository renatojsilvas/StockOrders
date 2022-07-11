using System;

namespace StockOrders.Simulation
{
    public class RandomGenerator : IRandom
    {
        private readonly Random _random = new();

        /// <summary>
        /// Wrapper of Random.Next
        /// </summary>        
        public int Next(int max)
        {
            return _random.Next(max);
        }

        /// <summary>
        /// Wrapper of Random.Next
        /// </summary>        
        public int Next(int min, int max)
        {
            return _random.Next(min, max);
        }

        /// <summary>
        /// Wrapper of Random.NextDouble
        /// </summary>        
        public double NextDouble()
        {
            return _random.NextDouble();
        }
    }
}
