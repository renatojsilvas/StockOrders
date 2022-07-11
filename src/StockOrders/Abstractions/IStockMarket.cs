using StockOrders.Domain;
using System;
using System.Collections.Generic;

namespace StockOrders.UI.Abstractions
{
    public interface IStockMarket
    {
        bool IsOpen { get; }        
        IList<Order> Orders { get; }              
        void OpenMarket();
        void CloseMarket();
    }
}