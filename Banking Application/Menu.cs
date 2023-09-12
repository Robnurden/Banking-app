namespace Banking_Application
{
    public class Menu
    {
        private readonly IDepositService _depositService;
        private readonly IWithdrawService _withdrawService;
        private readonly IBalanceService _balanceService;
        private readonly ConsoleWrapper _console;
        private static decimal _balance;
        private const string Greeting = "Welcome to RoBank";
        private const string DepositOption = "Deposit an amount";
        private const string WithdrawOption = "Withdraw an amount";
        private const string BalanceOption = "Display your current balance";

        public Menu(IDepositService depositService, IWithdrawService withdrawService, IBalanceService balanceService, ConsoleWrapper console)
        {
            _depositService = depositService;
            _withdrawService = withdrawService;
            _balanceService = balanceService;
            _console = console;
        }

        public void MainMenu()
        {
            _console.WriteLine($"{Greeting}");

            while (true)
            {
                var selectedOption = GetSelectedOption();
                OptionHandler(selectedOption);
            }
        }

        public int GetSelectedOption()
        {
            string? selectedOption;
            bool isValid;
            do
            {
                selectedOption = ReadSelectedOption();
                isValid = ValidateOption(selectedOption);

                if (!isValid)
                {
                    PrintInvalidOptionMessage();
                }

            } while (!isValid);

            return int.Parse(selectedOption);
        }

        public void OptionHandler(int selectedOption)
        {
            switch (selectedOption)
            {
                case 1:
                    _balance = _depositService.DepositOrchestrator(_balance);
                    break;
                case 2:
                    _balance = _withdrawService.WithdrawOrchestrator(_balance);
                    break;
                case 3:
                    _balanceService.DisplayBalance(_balance);
                    break;
                case 4:
                    Exit();
                    return;
            }
        }

        public string? ReadSelectedOption()
        {
            _console.WriteLine("\nPlease select an option: " +
                              $"\n1. {DepositOption} " +
                              $"\n2. {WithdrawOption} " +
                              $"\n3. {BalanceOption}" +
                              "\n4. Exit");

            return _console.ReadLine();
        }

        public bool ValidateOption(string? selectedOption)
        {
            var validOptions = new List<int> { 1, 2, 3, 4 };

            return int.TryParse(selectedOption, out var option) && validOptions.Contains(option);
        }

        private void PrintInvalidOptionMessage()
        {
            _console.WriteLine("\nInvalid input, please select an option of 1, 2, 3, or 4.\n");
        }

        public void Exit()
        {
            _console.WriteLine("\nThank you for using RoBank.");
            Environment.Exit(0);
        }
    }
}
