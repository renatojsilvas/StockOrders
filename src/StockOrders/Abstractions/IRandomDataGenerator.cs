using StockOrders.Domain;
using System;
using System.Collections.Generic;

namespace StockOrders.UI.Abstractions
{
    public interface IRandomDataGenerator
    {
        IEnumerable<CreateOrderModel> CreateNewOrderData(bool isHighLoad);
        IEnumerable<Guid> UpdateOrders(IDictionary<Guid, Order> orders, bool isHighLoad);        
        bool IsTimeToCreateNewOrders { get; }
    }
}
