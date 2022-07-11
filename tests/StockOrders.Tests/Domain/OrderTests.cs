using FluentAssertions;
using StockOrders.Domain;

namespace StockOrders.Tests.Domain
{
    public class OrderTests
    {
        [Fact(DisplayName = "Ctor Should Initialize An Order From Create Order Model")]
        public void Ctor_Should_Initialize_An_Order_From_Create_Order_Model()
        {
            // Arrange
            var createOrderModel = new CreateOrderModel()
            {
                OrderDate = new DateTime(2022, 7, 10),
                Account = 1,
                Advisor = "Renato",
                Asset = "PETR4",
                Quantity = 100,
                Value = 10,
                OrderType = OrderType.C,
                Priority = Priority.None
            };

            // Act
            var order = new Order(createOrderModel);

            // Assert
            order.OrderDate.Should().Be(new DateTime(2022, 7, 10));
            order.Account.Should().Be(1);
            order.Advisor.Should().Be("Renato");
            order.Asset.Should().Be("PETR4");
            order.Quantity.Should().Be(100);
            order.Value.Should().Be(10);
            order.OrderType.Should().Be(OrderType.C);
            order.Priority.Should().Be(Priority.None);
            order.ApparentQuantity.Should().Be(0);
            order.CancelledQuantity.Should().Be(0);
            order.ExecutedQuantity.Should().Be(0);
            order.ExecutedQuantity.Should().Be(0);
            order.ValueTrigger.Should().Be(null);
            order.Target.Should().Be(null);
            order.TargetTrigger.Should().Be(null);
            order.Reduction.Should().Be(null);            
            order.AvailableQuantity.Should().Be(100);
            order.IsExecuted.Should().BeFalse();
        }

        [Fact(DisplayName = "Execute Should Increment Executed Quantity By One")]
        public void Execute_Should_Increment_Executed_Quantity_By_One()
        {
            // Arrange
            var createOrderModel = new CreateOrderModel()
            {
                OrderDate = new DateTime(2022, 7, 10),
                Account = 1,
                Advisor = "Renato",
                Asset = "PETR4",
                Quantity = 100,
                Value = 10,
                OrderType = OrderType.C,
                Priority = Priority.None
            };
            var order = new Order(createOrderModel);

            // Act
            order.Execute(50);

            // Assert           
            order.ExecutedQuantity.Should().Be(50);
            order.AvailableQuantity.Should().Be(50);
            order.IsExecuted.Should().BeFalse();
        }

        [Fact(DisplayName = "Execute Should Finalize An Order When Executed Quantity Is Greater Than Quantity")]
        public void Execute_Should_Finalize_An_Order_When_Executed_Quantity_Is_Greater_Than_Quantity()
        {
            // Arrange
            var createOrderModel = new CreateOrderModel()
            {
                OrderDate = new DateTime(2022, 7, 10),
                Account = 1,
                Advisor = "Renato",
                Asset = "PETR4",
                Quantity = 100,
                Value = 10,
                OrderType = OrderType.C,
                Priority = Priority.None
            };
            var order = new Order(createOrderModel);

            // Act
            order.Execute(101);

            // Assert           
            order.ExecutedQuantity.Should().Be(100);
            order.AvailableQuantity.Should().Be(0);
            order.IsExecuted.Should().BeTrue();
        }
    }
}
