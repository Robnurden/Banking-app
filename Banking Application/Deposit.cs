namespace Banking_Application
{
    public class Deposit
    {
        private const int MaxDeposit = 1000000;
        private const int MinDeposit = 1;

        public static int DepositOrchestrator(int balance)
        {
            bool isValid;
            var intAmount = 0;
            do
            {
                var strAmount = GetDepositAmount();

                isValid = ValidateDepositAmount(strAmount);

                if (!isValid)
                {
                    PrintInvalidInputMessage();
                }
                else
                {
                    intAmount = int.Parse(strAmount);
                }

            } while (!isValid);

            return balance + intAmount;
        }

        public static string? GetDepositAmount()
        {
            Console.WriteLine("\nHow much would you like to deposit?");
            return Console.ReadLine();
        }

        public static bool ValidateDepositAmount(string? strAmount)
        {
            return int.TryParse(strAmount, out var amount) && amount is < MaxDeposit and > 0;
        }

        public static void PrintInvalidInputMessage()
        {
            Console.WriteLine($"\nInvalid input, please enter a number between {MinDeposit} and {MaxDeposit}.");
        }
    }
}
