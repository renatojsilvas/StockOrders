using StockOrders.Domain;
using StockOrders.UI.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockOrders.Simulation
{
    public class RandomDataGenerator : IRandomDataGenerator
    {
        private static readonly string[] ADVISORS = { "Renato", "José", "Viviane", "Mariana", "Marcela", "Victor", "Marcia" };
        private static readonly string[] ASSETS = { "PETR4", "VALE5", "MGLU4", "ETER3", "WEGE3", "ITUB4", "POMO4" };
        private const int MINIMUM_ACCOUNT_NUMBER = 1000000;
        private const int MAXIMUM_ACCOUNT_NUMBER = 4000000;
        private const int MINIMUM_QUANTITY_NUMBER = 1;
        private const int MAXIMUM_QUANTITY_NUMBER = 100;
        private const decimal OFFSET_VALUE = 1m;
        private const double SPAN_VALUE = 100;
        private const int CREATE_NEW_ORDER_PROBABILITY = 5;
        private static readonly int NUMBER_OF_PRIORITIES = Enum.GetNames(typeof(Priority)).Length;
        private static readonly int NUMBER_OF_ORDERTYPE = Enum.GetNames(typeof(OrderType)).Length;
        private const int NUMBER_OF_ORDERS_ON_HIGH_LOAD = 20;

        private readonly IRandom _random;

        public RandomDataGenerator(IRandom random)
        {
            _random = random;
        }

        /// <summary>
        /// Indicated whether creates or updates
        /// </summary>
        public bool IsTimeToCreateNewOrders => _random.Next(0, 100) < CREATE_NEW_ORDER_PROBABILITY;

        /// <summary>
        /// Creates a new list os orders
        /// </summary>        
        public IEnumerable<CreateOrderModel> CreateNewOrderData(bool isHighLoad)
        {
            return isHighLoad ? CreateMultipleOrders() : CreateSingleOrder();
        }

        private IEnumerable<CreateOrderModel> CreateMultipleOrders()
        {
            return Enumerable.Range(1, NUMBER_OF_ORDERS_ON_HIGH_LOAD)
                             .SelectMany(_ => CreateSingleOrder())
                             .ToList();
        }

        private IEnumerable<CreateOrderModel> CreateSingleOrder()
        {
            return new List<CreateOrderModel>(){
                new CreateOrderModel()
            {
                OrderDate = DateTime.Now,
                Advisor = ADVISORS[_random.Next(ADVISORS.Length)],
                Account = _random.Next(MINIMUM_ACCOUNT_NUMBER, MAXIMUM_ACCOUNT_NUMBER),
                Asset = ASSETS[_random.Next(ADVISORS.Length)],
                Quantity = _random.Next(MINIMUM_QUANTITY_NUMBER, MAXIMUM_QUANTITY_NUMBER),
                Value = (decimal)(_random.NextDouble() * SPAN_VALUE) + OFFSET_VALUE,
                OrderType = (OrderType)_random.Next(NUMBER_OF_ORDERTYPE),
                Priority = (Priority)_random.Next(NUMBER_OF_PRIORITIES)
            }};
        }

        /// <summary>
        /// Updates a existent list os orders
        /// </summary>     
        public IEnumerable<Guid> UpdateOrders(IDictionary<Guid, Order> orders, bool isHighLoad)
        {
            return isHighLoad ? UpdateMultipleOrders(orders) : UpdateSingleOrder(orders);
        }

        private IEnumerable<Guid> UpdateMultipleOrders(IDictionary<Guid, Order> orders)
        {
            return Enumerable.Range(1, NUMBER_OF_ORDERS_ON_HIGH_LOAD)
                             .SelectMany(_ => UpdateSingleOrder(orders));

        }

        private IEnumerable<Guid> UpdateSingleOrder(IDictionary<Guid, Order> orders)
        {
            if (orders.Count < 1)
                return new List<Guid>();

            var newestOrders = orders.Values.OrderByDescending(o => o.OrderDate)
                                            .TakeLast(10);
            return new List<Guid>() { newestOrders.ElementAt(_random.Next(newestOrders.Count())).Id };
        }
    }
}
