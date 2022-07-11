using FluentAssertions;
using StockOrders.Simulation;

namespace StockOrders.Tests.Simulation
{
    public class RandomGeneratorTests
    {
        [Fact(DisplayName = "Next With One Parameter Should Return A Int Between 0 And The Parameter")]
        public void Next_With_One_Parameter_Should_Return_A_Int_Between_0_And_The_Parameter()
        {
            // Arrange
            var sut = new RandomGenerator();

            // Act
            var result = sut.Next(5);

            // Assert            
            result.Should().BeInRange(0, 4);
        }

        [Fact(DisplayName = "Next With Two Parameters Should Return A Int Between Min And The Max")]
        public void Next_With_Two_Parameters_Should_Return_A_Int_Between_Min_And_The_Max()
        {
            // Arrange
            var sut = new RandomGenerator();

            // Act
            var result = sut.Next(5, 10);

            // Assert            
            result.Should().BeInRange(5, 9);
        }

        [Fact(DisplayName = "NextDouble Should Return A Double Between 0 And 1")]
        public void NextDouble_Should_Return_A_Double_Between_0_And_1()
        {
            // Arrange
            var sut = new RandomGenerator();

            // Act
            var result = sut.NextDouble();

            // Assert            
            result.Should().BeInRange(0, 1);
        }
    }
}
