using Banking_Application;
using FluentAssertions;
using Moq;

namespace BankingApplicationTests
{
    [TestFixture]
    public class DepositServiceTests
    {
        private DepositService _depositService;
        private Mock<ConsoleWrapper> _consoleWrapper;

        private const int MinDeposit = 1;
        private const int MaxDeposit = 1000000;

        [SetUp]
        public void Setup()
        {
            _consoleWrapper = new Mock<ConsoleWrapper>();
            _depositService = new DepositService(_consoleWrapper.Object);
        }

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
            var result = _depositService.ValidateDepositAmount(strAmount);

            result.Should().Be(expectedResult);
        }

        [TestCase("100", 100, 200)]
        [TestCase("100.01", 100.01, 200.02)]
        public void DepositOrchestrator_ReturnsValueAddedToBalance_WhenAValidDepositIsMade(string? strAmount, decimal balance, decimal expectedResult)
        {
            _consoleWrapper.Setup(c => c.ReadLine())
                .Returns(strAmount);

            var result = _depositService.DepositOrchestrator(balance);

            result.Should().Be(expectedResult);
        }

        [Test]
        public void DepositOrchestrator_PrintsInvalidInputMessage_WhenAnInvalidDepositIsEntered()
        {
            var expectedString = $"\nInvalid input. Please enter an amount up to two decimal places, between {MinDeposit} and {MaxDeposit}.";

            _consoleWrapper.SetupSequence(c => c.ReadLine())
                .Returns(" ")
                .Returns("1");

            _depositService.DepositOrchestrator(100);

            _consoleWrapper.Verify(c =>
                c.WriteLine(expectedString));
        }
    }
}