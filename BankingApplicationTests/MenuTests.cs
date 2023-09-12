using Banking_Application;
using FluentAssertions;
using Moq;

namespace BankingApplicationTests
{
    [TestFixture]
    public class MenuTests
    {
        private Menu _menu;
        private Mock<IDepositService> _mockDepositService;
        private Mock<IWithdrawService> _mockWithdrawService;
        private Mock<IBalanceService> _mockBalanceService;
        private Mock<ConsoleWrapper> _mockConsoleWrapper;

        private const int DepositOption = 1;
        private const int WithdrawOption = 2;
        private const int BalanceOption = 3;
        private const decimal Balance = 0;

        [SetUp]
        public void Setup()
        {
            _mockDepositService = new Mock<IDepositService>();
            _mockWithdrawService = new Mock<IWithdrawService>();
            _mockBalanceService = new Mock<IBalanceService>();
            _mockConsoleWrapper = new Mock<ConsoleWrapper>();

            _menu = new Menu(
                _mockDepositService.Object,
                _mockWithdrawService.Object,
                _mockBalanceService.Object,
                _mockConsoleWrapper.Object);
        }

        [Test]
        public void OptionHandler_ShouldCallDepositService_WhenCorrespondingOptionChosen()
        {
            _menu.OptionHandler(DepositOption);

            _mockDepositService.Verify(ds => ds.DepositOrchestrator(Balance), Times.Once);
        }

        [Test]
        public void OptionHandler_ShouldCallWithdrawService_WhenCorrespondingOptionChosen()
        {
            _menu.OptionHandler(WithdrawOption);

            _mockWithdrawService.Verify(ws => ws.WithdrawOrchestrator(Balance), Times.Once);
        }

        [Test]
        public void OptionHandler_ShouldCallBalanceService_WhenCorrespondingOptionChosen()
        {
            _menu.OptionHandler(BalanceOption);

            _mockBalanceService.Verify(bs => bs.DisplayBalance(Balance), Times.Once);
        }

        [Test]
        public void GetSelectedOption_ShouldReturnAValue_WhenValidValueIsEntered()
        {
            _mockConsoleWrapper.Setup(c => c.ReadLine()).Returns("1");

            var result = _menu.GetSelectedOption();

            result.Should().Be(1);
        }

        [Test]
        public void GetSelectedOption_ShouldDisplayErrorMessage_WhenInvalidValueIsEntered()
        {
            var expectedMessage = "\nInvalid input, please select an option of 1, 2, 3, or 4.\n";
            _mockConsoleWrapper.SetupSequence(c => c.ReadLine())
                .Returns(" ")
                .Returns("1");

            _menu.GetSelectedOption();

            _mockConsoleWrapper.Verify(c => c.WriteLine(expectedMessage));
        }

        [TestCase("1", true)]
        [TestCase("2", true)]
        [TestCase("3", true)]
        [TestCase("4", true)]
        [TestCase("5", false)]
        [TestCase("0", false)]
        [TestCase(" ", false)]
        [TestCase(null, false)]
        [TestCase("hello", false)]
        public void ValidateOption_ShouldReturnExpectedResult_WhenValueIsEntered(string strOption, bool expectedResult)
        {
            var result = _menu.ValidateOption(strOption);

            result.Should().Be(expectedResult);
        }
    }
}
