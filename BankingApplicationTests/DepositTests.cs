using Banking_Application;
using FluentAssertions;

namespace BankingApplicationTests
{
    [TestFixture]
    public class DepositTests
    {
        [TestCase("1", true)]
        [TestCase("2", true)]
        [TestCase("0", false)]
        [TestCase("1000001", false)]
        [TestCase(" ", false)]
        [TestCase(null, false)]
        public void ValidateDepositAmount_ReturnsExpectedValue_WhenAmountSupplied(string? strAmount, bool expectedResult)
        {
            var result = Deposit.ValidateDepositAmount(strAmount);

            result.Should().Be(expectedResult);
        }
    }
}