using StockOrders.Domain;
using StockOrders.UI.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StockOrders.Simulation
{
    public class Mock : IStockMarket
    {
        private IDictionary<Guid, Order> _orders = new Dictionary<Guid, Order>();
        private readonly IRandomDataGenerator _dataGenerator;

        public Mock(IRandomDataGenerator dataGenerator)
        {
            RunMarket();
            _dataGenerator = dataGenerator;
        }

        /// <summary>
        /// Indicates that the market is open
        /// </summary>
        public bool IsOpen { get; private set; }

        /// <summary>
        /// List of the orders placed on the market
        /// </summary>
        public IList<Order> Orders => new List<Order>(_orders.Values);

        /// <summary>
        /// Open the market
        /// </summary>
        public void OpenMarket()
        {
            IsOpen = true;
        }

        /// <summary>
        /// Close the market
        /// </summary>
        public void CloseMarket()
        {
            IsOpen = false;
        }        

        /// <summary>
        /// Run the market simulation
        /// </summary>
        private void RunMarket()
        {
            var startTime = DateTime.Now;
            var isHighLoad = false;

            Task.Run(() =>
            {
                while (true)
                {
                    var timeEllapsed = DateTime.Now - startTime;
                    if (timeEllapsed.TotalSeconds > TimeSpan.FromSeconds(10).TotalSeconds)
                    {
                        isHighLoad = !isHighLoad;
                        startTime = DateTime.Now;
                    }

                    if (IsOpen)
                        RunSimulation(isHighLoad);

                    Thread.Sleep(TimeSpan.FromMilliseconds(10));
                }
            });
        }

        /// <summary>
        /// Create or Add Order
        /// </summary>        
        public void RunSimulation(bool isHighLoad)
        {
            if (_dataGenerator.IsTimeToCreateNewOrders)
                CreateOrder(isHighLoad);
            else
                UpdateOrders(isHighLoad);
        }

        /// <summary>
        /// Create Orders based on loading
        /// </summary>        

        public void CreateOrder(bool isHighLoad)
        {
            var model = _dataGenerator.CreateNewOrderData(isHighLoad);
            var orders = model.Select(m => new Order(m)).ToList();

            foreach (var order in orders)
            {
                _orders.Add(order.Id, order);
            }
        }

        /// <summary>
        /// Update Orders based on loading
        /// </summary>     
        public void UpdateOrders(bool isHighLoad)
        {
            var ordersId = _dataGenerator.UpdateOrders(_orders, isHighLoad);

            foreach (var orderId in ordersId)
            {
                var order = _orders[orderId];
                order.Execute(1);
                if (order.IsExecuted)
                    _orders.Remove(order.Id);
            }
        }
    }
}
