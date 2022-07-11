using FluentAssertions;
using Moq;
using Moq.AutoMock;
using StockOrders.Domain;
using StockOrders.Simulation;

namespace StockOrders.Tests.Simulation
{
    public class RandomDataGeneratorTests
    {
        [Fact(DisplayName = "IsTimeToCreateNewOrders Should Return True When Random IsLower Than 5")]
        public void IsTimeToCreateNewOrders_Should_Return_True_When_Random_IsLower_Than_5()
        {
            // Arrange
            Mock<IRandom> random = new Mock<IRandom>();
            random.Setup(r => r.Next(0, 100)).Returns(4);
            AutoMocker autoMocker = new AutoMocker();
            autoMocker.Use(random);
            var sut = autoMocker.CreateInstance<RandomDataGenerator>();

            // Act
            var result = sut.IsTimeToCreateNewOrders;

            // Assert
            result.Should().BeTrue();
        }

        [Fact(DisplayName = "IsTimeToCreateNewOrders Should Return False When Random IsGreater Than 4")]
        public void IsTimeToCreateNewOrders_Should_Return_False_When_Random_IsGreater_Than_4()
        {
            // Arrange
            Mock<IRandom> random = new Mock<IRandom>();
            random.Setup(r => r.Next(0, 100)).Returns(5);
            AutoMocker autoMocker = new AutoMocker();
            autoMocker.Use(random);
            var sut = autoMocker.CreateInstance<RandomDataGenerator>();

            // Act
            var result = sut.IsTimeToCreateNewOrders;

            // Assert
            result.Should().BeFalse();
        }

        [Fact(DisplayName = "CreateNewOrderData Should Return A Single Create Order Model Inside The Enumerator When Is Not High Load")]
        public void CreateNewOrderData_Should_Return_A_Single_Create_Order_Model_Inside_The_Enumerator_When_Is_Not_High_Load()
        {
            // Arrange
            Mock<IRandom> random = new Mock<IRandom>();
            random.Setup(r => r.Next(It.IsAny<int>(), It.IsAny<int>())).Returns(0);
            AutoMocker autoMocker = new AutoMocker();
            autoMocker.Use(random);
            var sut = autoMocker.CreateInstance<RandomDataGenerator>();

            // Act
            var result = sut.CreateNewOrderData(false);

            // Assert
            result.Should().HaveCount(1);
        }

        [Fact(DisplayName = "CreateNewOrderData Should Return 20 Create Order Model Inside The Enumerator When Is High Load")]
        public void CreateNewOrderData_Should_Return_20_Create_Order_Model_Inside_The_Enumerator_When_Is_High_Load()
        {
            // Arrange
            Mock<IRandom> random = new Mock<IRandom>();
            random.Setup(r => r.Next(It.IsAny<int>(), It.IsAny<int>())).Returns(0);
            AutoMocker autoMocker = new AutoMocker();
            autoMocker.Use(random);
            var sut = autoMocker.CreateInstance<RandomDataGenerator>();

            // Act
            var result = sut.CreateNewOrderData(true);

            // Assert
            result.Should().HaveCount(20);
        }

        [Fact(DisplayName = "UpdateOrders Should Return One Guid When Is Not High Load")]
        public void UpdateOrders_Should_Return_One_Guid_When_Is_Not_High_Load()
        {
            // Arrange
            Mock<IRandom> random = new Mock<IRandom>();
            random.Setup(r => r.Next(It.IsAny<int>(), It.IsAny<int>())).Returns(0);
            AutoMocker autoMocker = new AutoMocker();
            autoMocker.Use(random);
            var orders = new Dictionary<Guid, Order>();
            for (int i = 0; i < 20; i++)
            {
                var order = new Order(new CreateOrderModel()
                {
                    OrderDate = new DateTime(2022, 7, 10),
                    Account = 1,
                    Advisor = "Renato",
                    Asset = $"PETR{i}",
                    Quantity = 100,
                    Value = 10,
                    OrderType = OrderType.C,
                    Priority = Priority.None
                });
                orders.Add(order.Id, order);
            }
            var sut = autoMocker.CreateInstance<RandomDataGenerator>();

            // Act
            var result = sut.UpdateOrders(orders, false);

            // Assert
            result.Should().HaveCount(1);
            result.ElementAt(0).Should().Be(orders.ElementAt(10)
                .Value.Id);
        }

        [Fact(DisplayName = "UpdateOrders Should Return 20 Create Order Model Inside The Enumerator When Is High Load")]
        public void UpdateOrders_Should_Return_20_Create_Order_Model_Inside_The_Enumerator_When_Is_High_Load()
        {
            // Arrange
            Mock<IRandom> random = new Mock<IRandom>();
            random.Setup(r => r.Next(It.IsAny<int>(), It.IsAny<int>())).Returns(0);
            AutoMocker autoMocker = new AutoMocker();
            autoMocker.Use(random);
            var orders = new Dictionary<Guid, Order>();
            for (int i = 0; i < 20; i++)
            {
                var order = new Order(new CreateOrderModel()
                {
                    OrderDate = new DateTime(2022, 7, 10),
                    Account = 1,
                    Advisor = "Renato",
                    Asset = $"PETR{i}",
                    Quantity = 100,
                    Value = 10,
                    OrderType = OrderType.C,
                    Priority = Priority.None
                });
                orders.Add(order.Id, order);
            }
            var sut = autoMocker.CreateInstance<RandomDataGenerator>();

            // Act
            var result = sut.UpdateOrders(orders, true);

            // Assert
            result.Should().HaveCount(20);
            foreach (var r in result)
            {
                Assert.True(orders.ContainsKey(r));
            }
        }

        [Fact(DisplayName = "UpdateOrders Should Return Empty Collection When Input Order Is Empty And No High Load")]
        public void UpdateOrders_Should_Return_Empty_Collection_When_Input_Order_Is_Empty_And_No_High_Load()
        {
            // Arrange
            Mock<IRandom> random = new Mock<IRandom>();
            random.Setup(r => r.Next(It.IsAny<int>(), It.IsAny<int>())).Returns(0);
            AutoMocker autoMocker = new AutoMocker();
            autoMocker.Use(random);
            var orders = new Dictionary<Guid, Order>();
            var sut = autoMocker.CreateInstance<RandomDataGenerator>();

            // Act
            var result = sut.UpdateOrders(orders, false);

            // Assert
            result.Should().HaveCount(0);
        }

        [Fact(DisplayName = "UpdateOrders Should Return Empty Collection When Input Order Is Empty And High Load")]
        public void UpdateOrders_Should_Return_Empty_Collection_When_Input_Order_Is_Empty_And_High_Load()
        {
            // Arrange
            Mock<IRandom> random = new Mock<IRandom>();
            random.Setup(r => r.Next(It.IsAny<int>(), It.IsAny<int>())).Returns(0);
            AutoMocker autoMocker = new AutoMocker();
            autoMocker.Use(random);
            var orders = new Dictionary<Guid, Order>();
            var sut = autoMocker.CreateInstance<RandomDataGenerator>();

            // Act
            var result = sut.UpdateOrders(orders, true);

            // Assert
            result.Should().HaveCount(0);
        }

    }
}
