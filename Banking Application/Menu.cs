using System.Globalization;

namespace Banking_Application
{
    public class Menu
    {
        private readonly IDepositService _depositService;
        private readonly IWithdrawService _withdrawService;

        private static decimal _balance;

        public Menu(IDepositService depositService, IWithdrawService withdrawService)
        {
            _depositService = depositService;
            _withdrawService = withdrawService;
        }

        public void MainMenu()
        {
            DisplayGreeting();

            while (true)
            {
                var selectedOption = OptionSelect();
                MenuOrchestrator(selectedOption);
            }
        }

        public void MenuOrchestrator(int selectedOption)
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
                    DisplayBalance();
                    break;
                case 4:
                    Exit();
                    return;
                default:
                    throw new ArgumentException("Invalid option");
            }
        }

        public void DisplayGreeting()
        {
            Console.WriteLine("Welcome to RoBank!");
        }

        public int OptionSelect()
        {
            string? selectedOption;
            bool isValid;
            do
            {
                selectedOption = OptionReader();
                isValid = OptionValidation(selectedOption);

            } while (!isValid);

            return int.Parse(selectedOption);
        }

        public bool OptionValidation(string? selectedOption)
        {
            var isValid = ValidateOption(selectedOption);

            if (!isValid)
            {
                PrintInvalidOptionMessage();
            }

            return isValid;
        }

        public string? OptionReader()
        {
            DisplayListOfOptions();
            var selectedOption = Console.ReadLine();

            return selectedOption;
        }

        public void DisplayListOfOptions()
        {
            Console.WriteLine("\nPlease select an option: " +
                              "\n1. Deposit an amount " +
                              "\n2. Withdraw an amount " +
                              "\n3. Display my current balance" +
                              "\n4. Exit");
        }

        public bool ValidateOption(string? selectedOption)
        {
            var validOptions = new List<int> { 1, 2, 3, 4 };

            return int.TryParse(selectedOption, out var option) && validOptions.Contains(option);
        }

        private void PrintInvalidOptionMessage()
        {
            Console.WriteLine("\nInvalid input, please select an option of 1, 2, 3, or 4.\n");
        }

        public void DisplayBalance()
        {
            string currencyCode = "en-GB";

            CultureInfo cultureInfo = new CultureInfo(currencyCode);
            cultureInfo.NumberFormat.CurrencySymbol = GetCurrencySymbol(currencyCode);

            string formattedAmount = string.Format(cultureInfo, "{0:C}", _balance);

            Console.WriteLine($"\nYour current balance is {formattedAmount}");
        }

        public void Exit()
        {
            Console.WriteLine("\nThank you for using RoBank.");
            Environment.Exit(0);
        }

        public string GetCurrencySymbol(string currencyCode)
        {
            try
            {
                RegionInfo region = new RegionInfo(currencyCode);
                return region.CurrencySymbol;
            }
            catch (ArgumentException)
            {
                return currencyCode;
            }
        }
    }
}
