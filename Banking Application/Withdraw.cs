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
                Console.WriteLine("How much would you like to withdraw? ");
                var strAmount = Console.ReadLine();

                if (int.TryParse(strAmount, out amount) && amount is > 0)
                {
                    isValid = true;
                }
                else
                {
                    Console.WriteLine("Invalid input, please enter a number greater than 1.");
                }

            } while (!isValid);

            var newBalance = balance - amount;

            return newBalance;
        }
    }
}
