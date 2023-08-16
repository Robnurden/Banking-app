namespace Banking_Application
{
    public class OptionsList
    {
        private static int _balance;

        public static void DisplayOptions()
        {
            Console.WriteLine("Welcome to RoBank!");
            while (true)
            {
                var option = SelectOption();
                switch (option)
                {
                    case 1:
                        _balance = Deposit.DepositOption(_balance);
                        DisplayBalance();
                        break;
                    case 2:
                        DisplayBalance();
                        _balance = Withdraw.WithdrawOption(_balance);
                        DisplayBalance();
                        break;
                    case 3:
                        DisplayBalance();
                        break;
                    case 4:
                        Exit();
                        return;
                }
            }
        }
        public static int SelectOption()
        {
            int option;
            var isValid = false;
            var validOptions = new List<int> { 1, 2, 3, 4 };
            do
            {
                Console.WriteLine("\nPlease select an option: " +
                                  "\n1. Deposit a credit balance " +
                                  "\n2. Withdraw a credit balance " +
                                  "\n3. Display my current balance" +
                                  "\n4. Exit");
                var selectedOption = Console.ReadLine();

                if (int.TryParse(selectedOption, out option) && validOptions.Contains(option))
                {
                    isValid = true;
                }
                else
                {
                    Console.WriteLine("\nInvalid input, please select an option of 1, 2, 3 or 4.\n");
                }

            } while (!isValid);

            Console.WriteLine($"\nYou have selected option {option}");

            return option;
        }

        public static void DisplayBalance()
        {
            Console.WriteLine($"\nYour current balance is {_balance}");
        }

        public static void Exit()
        {
            Environment.Exit(0);
        }
    }
}
