
using FluentAssertions;
using StockOrders.Domain;
using StockOrders.UI.Abstractions;
using StockOrders.UI.Presentation;

namespace StockOrders.Tests.ViewModels
{
    public class StockHistoryViewModelTests
    {
        [Fact(DisplayName = "Ctor Should Open Market")]
        public void Ctor_Should_Open_Market()
        {
            // Arrange
            var stockMarket = new Moq.Mock<IStockMarket>();
            Moq.AutoMock.AutoMocker autoMocker = new Moq.AutoMock.AutoMocker();
            autoMocker.Use(stockMarket);
            var sut = autoMocker.CreateInstance<StockHistoryViewModel>();

            // Act                       

            // Assert
            stockMarket.Verify(x => x.OpenMarket(), Moq.Times.Once);
        }


        [Fact(DisplayName = "UpdateUI Should Update OpenOrders And Quantities")]
        public void UpdateUI_Should_Update_OpenOrders_And_Quantities()
        {
            // Arrange
            var stockMarket = new Moq.Mock<IStockMarket>();
            List<Order> orders = new List<Order>();
            for (int i = 0; i < 20; i++)
            {
                var model = new CreateOrderModel()
                {
                    OrderDate = new DateTime(2022, 7, 10),
                    Account = 1,
                    Advisor = "Renato",
                    Asset = "PETR4",
                    Quantity = 100,
                    Value = 10,
                    OrderType = OrderType.C,
                    Priority = Priority.None,
                };
                var order = new Order(model);
                orders.Add(order);
            }
            orders[0].Execute(10);
            orders[5].Execute(10);
            orders[10].Execute(10);
            orders[15].Execute(10);
            orders[19].Execute(10);
            orders[1].Execute(100);
            orders[6].Execute(100);
            orders[11].Execute(100);
            orders[16].Execute(100);
            orders[18].Execute(100);
            stockMarket.Setup(x => x.Orders).Returns(orders);
            Moq.AutoMock.AutoMocker autoMocker = new Moq.AutoMock.AutoMocker();
            autoMocker.Use(stockMarket);
            var sut = autoMocker.CreateInstance<StockHistoryViewModel>();

            // Act                       
            sut.UpdateUI();

            // Assert
            sut.OpenOrders.Should().HaveCount(20);
            sut.AvailableQuantity.Should().Be($"Total Disponível: 1450");
            sut.TotalQuantity.Should().Be($"Total Quantidade: 2000");
        }

        [Fact(DisplayName = "UpdateUI Should Fire Property Changed Event Twice")]
        public void UpdateUI_Should_Fire_Property_Changed_Event_Twice()
        {
            // Arrange
            var stockMarket = new Moq.Mock<IStockMarket>();
            List<Order> orders = new List<Order>();
            for (int i = 0; i < 20; i++)
            {
                var model = new CreateOrderModel()
                {
                    OrderDate = new DateTime(2022, 7, 10),
                    Account = 1,
                    Advisor = "Renato",
                    Asset = "PETR4",
                    Quantity = 100,
                    Value = 10,
                    OrderType = OrderType.C,
                    Priority = Priority.None,
                };
                var order = new Order(model);
                orders.Add(order);
            }
            orders[0].Execute(10);
            orders[5].Execute(10);
            orders[10].Execute(10);
            orders[15].Execute(10);
            orders[19].Execute(10);
            orders[1].Execute(100);
            orders[6].Execute(100);
            orders[11].Execute(100);
            orders[16].Execute(100);
            orders[18].Execute(100);
            stockMarket.Setup(x => x.Orders).Returns(orders);
            Moq.AutoMock.AutoMocker autoMocker = new Moq.AutoMock.AutoMocker();
            autoMocker.Use(stockMarket);
            var sut = autoMocker.CreateInstance<StockHistoryViewModel>();
            int numberOfPropertyChangedEvents = 0;
            sut.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == "AvailableQuantity")
                    numberOfPropertyChangedEvents++;
                if (e.PropertyName == "TotalQuantity")
                    numberOfPropertyChangedEvents++;
            };

            // Act                       
            sut.UpdateUI();

            // Assert
            numberOfPropertyChangedEvents.Should().Be(2);
        }
    }
}
