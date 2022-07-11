using StockOrders.Domain;
using StockOrders.Utils;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace StockOrders.UI.Abstractions
{
    public interface IViewModel
    {
        OrderObservableCollection OpenOrders { get; }     
    }
}