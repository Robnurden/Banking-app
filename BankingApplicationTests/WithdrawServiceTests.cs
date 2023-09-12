using Banking_Application;
using Moq;
using FluentAssertions;

namespace BankingApplicationTests
{
    [TestFixture]
    public class WithdrawServiceTests
    {
        private const decimal MinWithdrawal = 1;
        private const decimal BalanceThreshold = 0;

        [TestCase("50", 100, 50)]
        [TestCase("50.02", 100.01, 49.99)]
        public void WithdrawOrchestrator_ReturnsBalance_WhenValidValueIsEntered(string? strAmount, decimal balance, decimal expectedResult)
        {
            var consoleMock = new Mock<ConsoleWrapper>();
            consoleMock.Setup(c => c.ReadLine())
                .Returns(strAmount);

            var withdrawService = new WithdrawService(consoleMock.Object, BalanceThreshold);

            var result = withdrawService.WithdrawOrchestrator(balance);

            result.Should().Be(expectedResult);
        }

        [Test]
        public void WithdrawOrchestrator_PrintsInvalidInputMessage_WhenAnInvalidValueIsEntered()
        {
            const int balance = 100;
            var expectedString = $"\nInvalid input. Please enter an amount up to two decimal places, between {MinWithdrawal} and less than {balance + (decimal)0.01}.";
            var consoleMock = new Mock<ConsoleWrapper>();
            consoleMock.SetupSequence(c => c.ReadLine())
                .Returns(" ")
                .Returns("1");

            var withdrawService = new WithdrawService(consoleMock.Object, BalanceThreshold);
            withdrawService.WithdrawOrchestrator(balance);

            consoleMock.Verify(c => c.WriteLine(expectedString));
        }

        [Test]
        public void WithdrawOrchestrator_ReturnsOriginalBalance_WhenBalanceIsBelowAThreshold()
        {
            const int balance = 0;
            var expectedString = $"Current balance is {balance}. Please deposit funds before you can withdraw.";
            var consoleMock = new Mock<ConsoleWrapper>();

            var withdrawService = new WithdrawService(consoleMock.Object, BalanceThreshold);
            var result = withdrawService.WithdrawOrchestrator(balance);

            result.Should().Be(balance);
            consoleMock.Verify(c => c.WriteLine(expectedString));
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
            var withdrawService = new WithdrawService(new ConsoleWrapper(), BalanceThreshold);
            var result = withdrawService.ValidateWithdrawalAmount(strAmount, balance);

            result.Should().Be(expectedResult);
        }
    }
}
