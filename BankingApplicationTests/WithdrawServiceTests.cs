using Banking_Application;
using Moq;
using FluentAssertions;

namespace BankingApplicationTests
{
    [TestFixture]
    public class WithdrawServiceTests
    {
        private WithdrawService _withdrawService;
        private Mock<ConsoleWrapper> _consoleWrapper;

        private const decimal MinWithdrawal = 1;
        private const decimal BalanceThreshold = 0;

        [SetUp]
        public void Setup()
        {
            _consoleWrapper = new Mock<ConsoleWrapper>();
            _withdrawService = new WithdrawService(_consoleWrapper.Object, BalanceThreshold);
        }

        [TestCase("50", 100, 50)]
        [TestCase("50.02", 100.01, 49.99)]
        public void WithdrawOrchestrator_ReturnsBalance_WhenValidValueIsEntered(string? strAmount, decimal balance, decimal expectedResult)
        {
            _consoleWrapper.Setup(c => c.ReadLine())
                .Returns(strAmount);

            var result = _withdrawService.WithdrawOrchestrator(balance);

            result.Should().Be(expectedResult);
        }

        [Test]
        public void WithdrawOrchestrator_PrintsInvalidInputMessage_WhenAnInvalidValueIsEntered()
        {
            const int balance = 100;
            var expectedString = $"\nInvalid input. Please enter an amount up to two decimal places, between {MinWithdrawal} and less than {balance + (decimal)0.01}.";

            _consoleWrapper.SetupSequence(c => c.ReadLine())
                .Returns(" ")
                .Returns("1");

            _withdrawService.WithdrawOrchestrator(balance);

            _consoleWrapper.Verify(c => c.WriteLine(expectedString));
        }

        [Test]
        public void WithdrawOrchestrator_ReturnsOriginalBalance_WhenBalanceIsBelowAThreshold()
        {
            const int balance = 0;
            var expectedString = $"Current balance is {balance}. Please deposit funds before you can withdraw.";

            var result = _withdrawService.WithdrawOrchestrator(balance);

            result.Should().Be(balance);
            _consoleWrapper.Verify(c => c.WriteLine(expectedString));
        }

        [TestCase("1", 2, true)]
        [TestCase("1", 1, true)]
        [TestCase("2", 1, false)]
        [TestCase("1.1", 2, true)]
        [TestCase("1.11", 2, true)]
        [TestCase("1.111", 2, false)]
        [TestCase(" ", 100, false)]
        [TestCase(null, 100, false)]
        [TestCase("hello", 100, false)]
        public void ValidateWithdrawalAmount_ShouldReturnExpectedValue(string? strAmount, decimal balance, bool expectedResult)
        {
            var result = _withdrawService.ValidateWithdrawalAmount(strAmount, balance);

            result.Should().Be(expectedResult);
        }
    }
}
