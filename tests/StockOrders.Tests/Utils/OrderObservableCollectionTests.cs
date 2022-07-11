using FluentAssertions;
using StockOrders.Domain;
using StockOrders.Utils;

namespace StockOrders.Tests.Utils
{
    public class OrderObservableCollectionTests
    {
        [Fact(DisplayName = "AddOrders Should Add One Order When It Have A List With One Order")]
        public void AddOrders_Should_Add_One_Order_When_It_Have_A_List_With_One_Order()
        {
            // Arrange
            var numberOfCollectionChanges = 0;
            var sut = new OrderObservableCollection();
            sut.CollectionChanged += (sender, args) =>
            {
                numberOfCollectionChanges++;
            };
            var singleOrder = new List<Order>()
            {
                new Order(new CreateOrderModel()
                {
                    OrderDate = new DateTime(2022, 7, 10),
                    Account = 1,
                    Advisor = "Renato",
                    Asset = "PETR4",
                    Quantity = 100,
                    Value = 10,
                    OrderType = OrderType.C,
                    Priority = Priority.None
                })
            };

            // Act
            sut.AddOrders(singleOrder);

            // Assert
            sut.Should().HaveCount(1);
            numberOfCollectionChanges.Should().Be(1);
        }

        [Fact(DisplayName = "AddOrders Should Add Two Order When It Have A List With Two Order")]
        public void AddOrders_Should_Add_Two_Order_When_It_Have_A_List_With_Two_Order()
        {
            // Arrange
            var numberOfCollectionChanges = 0;
            var sut = new OrderObservableCollection();
            sut.CollectionChanged += (sender, args) =>
            {
                numberOfCollectionChanges++;
            };
            var singleOrder = new List<Order>()
            {
                new Order(new CreateOrderModel()
                {
                    OrderDate = new DateTime(2022, 7, 10),
                    Account = 1,
                    Advisor = "Renato",
                    Asset = "PETR4",
                    Quantity = 100,
                    Value = 10,
                    OrderType = OrderType.C,
                    Priority = Priority.None
                }),
                new Order(new CreateOrderModel()
                {
                    OrderDate = new DateTime(2022, 7, 10),
                    Account = 1,
                    Advisor = "Renato",
                    Asset = "PETR4",
                    Quantity = 100,
                    Value = 10,
                    OrderType = OrderType.C,
                    Priority = Priority.None
                })
            };

            // Act
            sut.AddOrders(singleOrder);

            // Assert
            sut.Should().HaveCount(2);
            numberOfCollectionChanges.Should().Be(1);
        }
    }
}