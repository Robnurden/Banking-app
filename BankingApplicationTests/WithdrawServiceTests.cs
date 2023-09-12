using Banking_Application;
using Moq;
using FluentAssertions;

namespace BankingApplicationTests
{
    [TestFixture]
    public class WithdrawServiceTests
    {
        private const decimal MinWithdrawal = 1;

        [TestCase("50", 100, 50)]
        [TestCase("50.02", 100.01, 49.99)]
        public void WithdrawOrchestrator_ReturnsBalance_WhenValidWithdrawValueIsEntered(string? strAmount, decimal balance, decimal expectedResult)
        {
            var consoleMock = new Mock<ConsoleWrapper>();
            consoleMock.Setup(c => c.ReadLine())
                .Returns(strAmount);

            var withdrawService = new WithdrawService(consoleMock.Object);

            var result = withdrawService.WithdrawOrchestrator(balance);

            result.Should().Be(expectedResult);
        }

        [Test]
        public void WithdrawOrchestrator_PrintsInvalidInputMessage_WhenAnInvalidWithdrawValueIsEntered()
        {
            const int balance = 100;
            var expectedString = $"\nInvalid input. Please enter an amount up to two decimal places, between {MinWithdrawal} and less than {balance + (decimal)0.01}.";
            var consoleMock = new Mock<ConsoleWrapper>();
            consoleMock.SetupSequence(c => c.ReadLine())
                .Returns(" ")
                .Returns("1");

            var withdrawService = new WithdrawService(consoleMock.Object);
            withdrawService.WithdrawOrchestrator(balance);

            consoleMock.Verify(c =>
                c.WriteLine(expectedString));
        }
    }
}
