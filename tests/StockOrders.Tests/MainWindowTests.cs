using FluentAssertions;
using StockOrders.UI;
using StockOrders.UI.Presentation;

namespace StockOrders.Tests.UI
{

    public class MainWindowTests
    {
        [StaFact(DisplayName = "Ctor Should Instantiate DataContext")]
        public void Ctor_Should_Instantiate_DataContext()
        {
            // Arrange
            MainWindow sut;

            // Act           
            sut = new MainWindow();

            // Assert
            sut.DataContext.Should().BeAssignableTo<StockHistoryViewModel>();
        }
    }
}
