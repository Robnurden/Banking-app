namespace Banking_Application
{
    public class Deposit
    {
        private const decimal MaxDeposit = 1000000;
        private const decimal MinDeposit = 1;

        public static decimal DepositOrchestrator(decimal balance)
        {
            bool isValid;
            decimal amount = 0;
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
                    amount = decimal.Parse(strAmount);
                }

            } while (!isValid);

            return balance + amount;
        }

        public static string? GetDepositAmount()
        {
            Console.WriteLine("\nHow much would you like to deposit?");
            return Console.ReadLine();
        }

        public static bool ValidateDepositAmount(string? strAmount)
        {
            return decimal.TryParse(strAmount, out var amount) && amount is < MaxDeposit and > 0;
        }

        public static void PrintInvalidInputMessage()
        {
            Console.WriteLine($"\nInvalid input, please enter a number between {MinDeposit} and {MaxDeposit}.");
        }
    }
}
