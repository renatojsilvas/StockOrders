using StockOrders.UI.Abstractions;
using StockOrders.Utils;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Threading;

namespace StockOrders.UI.Presentation
{
    public class StockHistoryViewModel : IViewModel, INotifyPropertyChanged
    {
        private const int UI_UPATE_RATE_IN_MS = 50;
        private readonly object _lock = new object();
        private readonly IStockMarket _market;

        public event PropertyChangedEventHandler? PropertyChanged;
        public StockHistoryViewModel(IStockMarket market)
        {
            var updateUITimer = new DispatcherTimer();
            updateUITimer.Interval = TimeSpan.FromMilliseconds(UI_UPATE_RATE_IN_MS);
            updateUITimer.Tick += (sender, e) =>
            {
                UpdateUI();
            };
            updateUITimer.Start();

            OpenOrders = new OrderObservableCollection();
            BindingOperations.EnableCollectionSynchronization(OpenOrders, _lock);

            _market = market;
            _market.OpenMarket();
        }

        /// <summary>
        /// Update the user interface
        /// </summary>
        public void UpdateUI()
        {
            var orders = _market.Orders;

            OpenOrders.Clear();
            OpenOrders.AddOrders(orders);

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AvailableQuantity)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TotalQuantity)));
        }

        /// <summary>
        /// List of Open Orders
        /// </summary>
        public OrderObservableCollection OpenOrders { get; private set; }

        /// <summary>
        /// Sum of Available Quantities
        /// </summary>
        public string AvailableQuantity => $"Total Disponível: {OpenOrders.Sum(x => x.AvailableQuantity)}";

        /// <summary>
        /// Sum of Total Quantities
        /// </summary>
        public string TotalQuantity => $"Total Quantidade: {OpenOrders.Sum(x => x.Quantity)}";
    }
}
