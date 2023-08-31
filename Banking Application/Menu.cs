namespace Banking_Application
{
    public class Menu
    {
        private static decimal _balance;

        public static void MainMenu()
        {
            DisplayGreeting();

            while (true)
            {
                var selectedOption = OptionSelect();
                MenuOrchestrator(selectedOption);
            }
        }

        public static void MenuOrchestrator(int selectedOption)
        {
            switch (selectedOption)
            {
                case 1:
                    _balance = Deposit.DepositOrchestrator(_balance);
                    break;
                case 2:
                    _balance = Withdraw.WithdrawOrchestrator(_balance);
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

        public static void DisplayGreeting()
        {
            Console.WriteLine("Welcome to RoBank!");
        }

        public static int OptionSelect()
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

        public static bool OptionValidation(string? selectedOption)
        {
            var isValid = ValidateOption(selectedOption);

            if (!isValid)
            {
                PrintInvalidOptionMessage();
            }

            return isValid;
        }

        public static string? OptionReader()
        {
            DisplayListOfOptions();
            var selectedOption = Console.ReadLine();

            return selectedOption;
        }

        public static void DisplayListOfOptions()
        {
            Console.WriteLine("\nPlease select an option: " +
                              "\n1. Deposit an amount " +
                              "\n2. Withdraw an amount " +
                              "\n3. Display my current balance" +
                              "\n4. Exit");
        }

        public static bool ValidateOption(string? selectedOption)
        {
            var validOptions = new List<int> { 1, 2, 3, 4 };

            return int.TryParse(selectedOption, out var option) && validOptions.Contains(option);
        }

        private static void PrintInvalidOptionMessage()
        {
            Console.WriteLine("\nInvalid input, please select an option of 1, 2, 3, or 4.\n");
        }

        public static void DisplayBalance()
        {
            Console.WriteLine($"\nYour current balance is {_balance}");
        }

        public static void Exit()
        {
            Console.WriteLine("\nThank you for using RoBank.");
            Environment.Exit(0);
        }
    }
}
