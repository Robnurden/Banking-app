namespace Banking_Application
{
    public class Withdraw
    {
        public static int WithdrawOption(int balance)
        {
            int amount;
            var isValid = false;
            do
            {
                Console.WriteLine("\nHow much would you like to withdraw? ");
                var strAmount = Console.ReadLine();

                if (int.TryParse(strAmount, out amount) && amount > 0 && amount <= balance)
                {
                    isValid = true;
                }
                else
                {
                    Console.WriteLine("\nInvalid input, please enter a number greater than 1 and less than your current balance.");
                }

            } while (!isValid);

            var newBalance = balance - amount;

            return newBalance;
        }
    }
}
