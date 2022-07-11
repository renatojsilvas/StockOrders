using FluentAssertions;
using StockOrders.Domain;
using StockOrders.Simulation;
using StockOrders.UI.Abstractions;

namespace StockOrders.Tests.Simulation
{
    public class MockTests
    {
        [Fact(DisplayName = "Ctor Should Set Is Open To False")]
        public void Ctor_Should_Set_Is_Open_To_False()
        {
            // Arrange
            Moq.Mock<IRandomDataGenerator> randomData = new Moq.Mock<IRandomDataGenerator>();
            Moq.AutoMock.AutoMocker autoMocker = new Moq.AutoMock.AutoMocker();
            autoMocker.Use(randomData);
            var sut = autoMocker.CreateInstance<Mock>();

            // Act


            // Assert
            sut.IsOpen.Should().BeFalse();         
        }

        [Fact(DisplayName = "OpenMarket Should Set Is Open To True")]
        public void OpenMarket_Should_Set_Is_Open_To_True()
        {
            // Arrange
            Moq.Mock<IRandomDataGenerator> randomData = new Moq.Mock<IRandomDataGenerator>();
            Moq.AutoMock.AutoMocker autoMocker = new Moq.AutoMock.AutoMocker();
            autoMocker.Use(randomData);
            var sut = autoMocker.CreateInstance<Mock>();

            // Act
            sut.OpenMarket();

            // Assert
            sut.IsOpen.Should().BeTrue();
        }

        [Fact(DisplayName = "CloseMarket Should Set Is Open To False")]
        public void CloseMarket_Should_Set_Is_Open_To_False()
        {
            // Arrange
            Moq.Mock<IRandomDataGenerator> randomData = new Moq.Mock<IRandomDataGenerator>();
            Moq.AutoMock.AutoMocker autoMocker = new Moq.AutoMock.AutoMocker();
            autoMocker.Use(randomData);
            var sut = autoMocker.CreateInstance<Mock>();

            // Act
            sut.CloseMarket();

            // Assert
            sut.IsOpen.Should().BeFalse();
        }

        [Fact(DisplayName = "CreateOrder Should Create One Order When Is No High Load")]
        public void CreateOrder_Should_Create_One_Order_When_Is_No_High_Load()
        {
            // Arrange
            Moq.Mock<IRandomDataGenerator> randomData = new Moq.Mock<IRandomDataGenerator>();
            randomData.Setup(r => r.CreateNewOrderData(false)).Returns(
                new List<CreateOrderModel>()
                {
                   new CreateOrderModel()
                   {
                        OrderDate = new DateTime(2022, 7, 10),
                        Account = 1,
                        Advisor = "Renato",
                        Asset = "PETR4",
                        Quantity = 100,
                        Value = 10,
                        OrderType = OrderType.C,
                        Priority = Priority.None,
                   }
                });
            Moq.AutoMock.AutoMocker autoMocker = new Moq.AutoMock.AutoMocker();
            autoMocker.Use(randomData);
            var sut = autoMocker.CreateInstance<Mock>();

            // Act
            sut.CreateOrder(false);

            // Assert
            sut.Orders.Should().HaveCount(1);
        }

        [Fact(DisplayName = "CreateOrder Should Create 20 Orders When Is No High Load")]
        public void CreateOrder_Should_Create_20_Orders_When_Is_High_Load()
        {
            // Arrange
            Moq.Mock<IRandomDataGenerator> randomData = new Moq.Mock<IRandomDataGenerator>();

            List<CreateOrderModel> orders = new List<CreateOrderModel>();
            for (int i = 0; i < 20; i++)
            {
                orders.Add(new CreateOrderModel()
                {
                    OrderDate = new DateTime(2022, 7, 10),
                    Account = 1,
                    Advisor = "Renato",
                    Asset = "PETR4",
                    Quantity = 100,
                    Value = 10,
                    OrderType = OrderType.C,
                    Priority = Priority.None,
                });
            }
            randomData.Setup(r => r.CreateNewOrderData(true)).Returns(orders);
            Moq.AutoMock.AutoMocker autoMocker = new Moq.AutoMock.AutoMocker();
            autoMocker.Use(randomData);
            var sut = autoMocker.CreateInstance<Mock>();

            // Act
            sut.CreateOrder(true);

            // Assert
            sut.Orders.Should().HaveCount(20);
        }

        [Fact(DisplayName = "UpdateOrder Should Update One Order When Is No High Load")]
        public void UpdateOrder_Should_Update_One_Order_When_Is_No_High_Load()
        {
            // Arrange
            var guid = Guid.NewGuid();
            Moq.Mock<IRandomDataGenerator> randomData = new Moq.Mock<IRandomDataGenerator>();
            randomData.Setup(r => r.UpdateOrders(Moq.It.IsAny<Dictionary<Guid, Order>>(), false)).Returns(
                new List<Guid>()
                {
                   guid
                });
            randomData.Setup(r => r.CreateNewOrderData(false)).Returns(
                new List<CreateOrderModel>()
                {
                   new CreateOrderModel()
                   {
                        Id = guid,
                        OrderDate = new DateTime(2022, 7, 10),
                        Account = 1,
                        Advisor = "Renato",
                        Asset = "PETR4",
                        Quantity = 100,
                        Value = 10,
                        OrderType = OrderType.C,
                        Priority = Priority.None,
                   }
                });
            Moq.AutoMock.AutoMocker autoMocker = new Moq.AutoMock.AutoMocker();
            autoMocker.Use(randomData);
            var sut = autoMocker.CreateInstance<Mock>();
            sut.CreateOrder(false);

            // Act
            sut.UpdateOrders(false);

            // Assert
            sut.Orders.Should().HaveCount(1);
            sut.Orders[0].AvailableQuantity.Should().Be(99);
            sut.Orders[0].ExecutedQuantity.Should().Be(1);
        }

        [Fact(DisplayName = "UpdateOrder Should Update 20 Orders When Is High Load")]
        public void UpdateOrder_Should_Update_20_Orders_When_Is_High_Load()
        {
            // Arrange
            List<Guid> guids = new List<Guid>();
            for (int i = 0; i < 20; i++)
            {
                guids.Add(Guid.NewGuid());
            }
            List<CreateOrderModel> models = new List<CreateOrderModel>();
            for (int i = 0; i < 20; i++)
            {
                models.Add(new CreateOrderModel()
                {
                    Id = guids[i],
                    OrderDate = new DateTime(2022, 7, 10),
                    Account = 1,
                    Advisor = "Renato",
                    Asset = "PETR4",
                    Quantity = 100,
                    Value = 10,
                    OrderType = OrderType.C,
                    Priority = Priority.None,
                });
            }
            Moq.Mock<IRandomDataGenerator> randomData = new Moq.Mock<IRandomDataGenerator>();
            randomData.Setup(r => r.UpdateOrders(Moq.It.IsAny<Dictionary<Guid, Order>>(), true)).Returns(
                new List<Guid>(guids));
            randomData.Setup(r => r.CreateNewOrderData(true)).Returns(
                new List<CreateOrderModel>(models));
            Moq.AutoMock.AutoMocker autoMocker = new Moq.AutoMock.AutoMocker();
            autoMocker.Use(randomData);
            var sut = autoMocker.CreateInstance<Mock>();
            sut.CreateOrder(true);

            // Act
            sut.UpdateOrders(true);

            // Assert
            sut.Orders.Should().HaveCount(20);
            for (int i = 0; i < 20; i++)
            {
                sut.Orders[i].AvailableQuantity.Should().Be(99);
                sut.Orders[i].ExecutedQuantity.Should().Be(1);
            }
        }

        [Fact(DisplayName = "RunSimulation Should Create One Order When Is No High Load")]
        public void RunSimulation_Should_Create_One_Order_When_Is_No_High_Load()
        {
            // Arrange
            Moq.Mock<IRandomDataGenerator> randomData = new Moq.Mock<IRandomDataGenerator>();
            randomData.Setup(r => r.CreateNewOrderData(false)).Returns(
                new List<CreateOrderModel>()
                {
                   new CreateOrderModel()
                   {
                        OrderDate = new DateTime(2022, 7, 10),
                        Account = 1,
                        Advisor = "Renato",
                        Asset = "PETR4",
                        Quantity = 100,
                        Value = 10,
                        OrderType = OrderType.C,
                        Priority = Priority.None,
                   }
                });
            randomData.Setup(r => r.IsTimeToCreateNewOrders).Returns(true);
            Moq.AutoMock.AutoMocker autoMocker = new Moq.AutoMock.AutoMocker();
            autoMocker.Use(randomData);
            var sut = autoMocker.CreateInstance<Mock>();

            // Act
            sut.RunSimulation(false);

            // Assert
            sut.Orders.Should().HaveCount(1);
        }

        [Fact(DisplayName = "RunSimulation Should Create 20 Orders When Is No High Load")]
        public void RunSimulation_Should_Update_One_Order_When_Is_No_High_Load()
        {
            // Arrange
            var guid = Guid.NewGuid();
            Moq.Mock<IRandomDataGenerator> randomData = new Moq.Mock<IRandomDataGenerator>();
            randomData.Setup(r => r.UpdateOrders(Moq.It.IsAny<Dictionary<Guid, Order>>(), false)).Returns(
                new List<Guid>()
                {
                   guid
                });
            randomData.Setup(r => r.CreateNewOrderData(false)).Returns(
                new List<CreateOrderModel>()
                {
                   new CreateOrderModel()
                   {
                        Id = guid,
                        OrderDate = new DateTime(2022, 7, 10),
                        Account = 1,
                        Advisor = "Renato",
                        Asset = "PETR4",
                        Quantity = 100,
                        Value = 10,
                        OrderType = OrderType.C,
                        Priority = Priority.None,
                   }
                });
            randomData.Setup(r => r.IsTimeToCreateNewOrders).Returns(false);
            Moq.AutoMock.AutoMocker autoMocker = new Moq.AutoMock.AutoMocker();
            autoMocker.Use(randomData);
            var sut = autoMocker.CreateInstance<Mock>();
            sut.CreateOrder(false);

            // Act
            sut.RunSimulation(false);

            // Assert
            sut.Orders.Should().HaveCount(1);
            sut.Orders[0].AvailableQuantity.Should().Be(99);
            sut.Orders[0].ExecutedQuantity.Should().Be(1);
        }
    }
}
