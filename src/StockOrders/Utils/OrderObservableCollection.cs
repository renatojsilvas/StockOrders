using StockOrders.Domain;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace StockOrders.Utils
{
    public class OrderObservableCollection : ObservableCollection<Order>
    {
        private bool _suppressNotification;

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (!_suppressNotification)
                base.OnCollectionChanged(e);
        }

        /// <summary>
        /// Add a range of orders to a ObservableCollection
        /// </summary>        
        public void AddOrders(IEnumerable<Order> orders)
        {
            _suppressNotification = true;

            foreach (var order in orders)
            {
                Add(order);
            }

            _suppressNotification = false;

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
}
