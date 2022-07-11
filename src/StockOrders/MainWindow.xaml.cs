using StockOrders.Simulation;
using StockOrders.UI.Presentation;
using System.Windows;

namespace StockOrders.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>    
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new StockHistoryViewModel(new Mock(new RandomDataGenerator(new RandomGenerator())));
        }
    }
}
