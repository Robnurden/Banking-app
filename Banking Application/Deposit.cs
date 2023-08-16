namespace Banking_Application
{
    public class Deposit
    {
        public static int DepositOption(int balance)
        {
            const int maximumDeposit = 1000000;
            int amount;
            var isValid = false;
            do
            {
                Console.WriteLine("\nHow much would you like to deposit? ");
                var strAmount = Console.ReadLine();

                if (int.TryParse(strAmount, out amount) && amount is < maximumDeposit and > 0)
                {
                    isValid = true;
                }
                else
                {
                    Console.WriteLine("\nInvalid input, please enter a number between 1 and 1,000,000.");
                }

            } while (!isValid);

            var newBalance = balance + amount;

            return newBalance;
        }
    }
}
