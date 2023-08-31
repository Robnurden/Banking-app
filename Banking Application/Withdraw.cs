namespace Banking_Application
{
    public class Withdraw
    {
        private const decimal MinWithdrawal = 1;

        public decimal WithdrawOrchestrator(decimal balance)
        {
            if (!BalanceCheck(balance))
            {
                return balance;
            }

            decimal amount;
            bool isValid;
            do
            {
                var strAmount = GetWithdrawalAmount();

                isValid = ValidateWithdrawalAmount(strAmount, out amount, balance);
                
                if (!isValid)
                {
                    PrintInvalidInputMessage(balance);
                }
                else
                {
                    amount = decimal.Parse(strAmount);
                }

            } while (!isValid);

            return balance - amount;
        }

        public bool BalanceCheck(decimal balance)
        {
            if (balance > 0)
            {
                return true;
            }

            Console.WriteLine($"Current balance is {balance}. Please deposit funds before you can withdraw.");

            return false;
        }

        public bool ValidateWithdrawalAmount(string? strAmount, out decimal amount, decimal balance)
        {
            return decimal.TryParse(strAmount, out amount) && amount > 0 && amount <= balance;
        }

        public string? GetWithdrawalAmount()
        {
            Console.WriteLine("\nHow much would you like to withdraw? ");
            var strAmount = Console.ReadLine();
            return strAmount;
        }

        public void PrintInvalidInputMessage(decimal balance)
        {
            Console.WriteLine($"\nInvalid input, please enter a value between {MinWithdrawal} and less than {balance}.");
        }
    }
}
