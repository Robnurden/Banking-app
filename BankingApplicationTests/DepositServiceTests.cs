using Banking_Application;
using FluentAssertions;
using Moq;
using System.Diagnostics.Metrics;
using System.Numerics;

namespace BankingApplicationTests
{
    [TestFixture]
    public class DepositServiceTests
    {
        private const int MinDeposit = 1;
        private const int MaxDeposit = 1000000;

        [TestCase("1", true)]
        [TestCase("2", true)]
        [TestCase("2.1", true)]
        [TestCase("2.12", true)]
        [TestCase("2.121", false)]
        [TestCase("0", false)]
        [TestCase("1000001", false)]
        [TestCase(" ", false)]
        [TestCase(null, false)]
        public void ValidateDepositAmount_ReturnsExpectedValue_WhenAmountSupplied(string? strAmount, bool expectedResult)
        {
            var deposit = new DepositService(new ConsoleWrapper());
            var result = deposit.ValidateDepositAmount(strAmount);

            result.Should().Be(expectedResult);
        }

        [TestCase("100", 100, 200)]
        [TestCase("100.01", 100.01, 200.02)]
        public void DepositOrchestrator_ReturnsValueAddedToBalance_WhenAValidDepositIsMade(string? strAmount, decimal balance, decimal expectedResult)
        {
            var consoleMock = new Mock<ConsoleWrapper>();
            consoleMock.Setup(c => c.ReadLine())
                .Returns(strAmount);

            var deposit = new DepositService(consoleMock.Object);

            var result = deposit.DepositOrchestrator(balance);

            result.Should().Be(expectedResult);
        }

        [Test]
        public void DepositOrchestrator_PrintsInvalidInputMessage_WhenAnInvalidDepositIsEntered()
        {
            var expectedString = "\nInvalid input. Please enter an amount up to two decimal places, between 1 and 1000000.";
            var consoleMock = new Mock<ConsoleWrapper>();
            consoleMock.SetupSequence(c => c.ReadLine())
                .Returns(" ")
                .Returns("1");

            var deposit = new DepositService(consoleMock.Object);
            deposit.DepositOrchestrator(100);

            consoleMock.Verify(c =>
                c.WriteLine(expectedString));
        }
    }
}