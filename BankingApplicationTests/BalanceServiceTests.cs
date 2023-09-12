using Banking_Application;
using Moq;

namespace BankingApplicationTests
{
    [TestFixture]
    public class BalanceServiceTests
    {
        [Test]
        public void DisplayBalance_OutputsBalanceToConsole_WithCultureSymbol()
        {
            const decimal balance = (decimal)100.19;
            var expectedResult = $"\nYour current balance is £{balance}";
            var consoleMock = new Mock<ConsoleWrapper>();

            var balanceService = new BalanceService(consoleMock.Object);

            balanceService.DisplayBalance(balance);

            consoleMock.Verify(c => c.WriteLine(expectedResult));
        }
    }
}
