namespace Banking_Application
{
    public class Deposit
    {
        public static int DepositOption(int balance)
        {
            int amount;
            var isValid = false;
            do
            {
                Console.WriteLine("How much would you like to deposit? ");
                var strAmount = Console.ReadLine();

                if (int.TryParse(strAmount, out amount) && amount is < 1000000 and > 0)
                {
                    isValid = true;
                }
                else
                {
                    Console.WriteLine("Invalid input, please enter a number between 1 and 1,000,000.");
                }

            } while (!isValid);

            var newBalance = balance + amount;

            return newBalance;
        }
    }
}
