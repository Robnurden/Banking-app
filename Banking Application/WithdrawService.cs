using System.Text.RegularExpressions;

namespace Banking_Application
{
    public class WithdrawService : IWithdrawService
    {
        private const decimal MinWithdrawal = 1;

        public decimal WithdrawOrchestrator(decimal balance)
        {
            if (!BalanceCheck(balance))
            {
                return balance;
            }

            decimal amount = 0;
            bool isValid;
            do
            {
                var strAmount = GetWithdrawalAmount();

                isValid = ValidateWithdrawalAmount(strAmount, balance);
                
                if (!isValid)
                {
                    PrintInvalidInputMessage(balance);
                }
                else
                {
                    amount = Math.Round(decimal.Parse(strAmount), 2, MidpointRounding.AwayFromZero);
                }

            } while (!isValid);

            return Math.Round(balance - amount, 2, MidpointRounding.AwayFromZero);
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

        public bool ValidateWithdrawalAmount(string? strAmount, decimal balance)
        {
            string pattern = @"^\-?[0-9]+(?:\.[0-9]{1,2})?$";
            var isValidDecimalString = Regex.IsMatch(strAmount, pattern);

            if (isValidDecimalString)
            {
                return decimal.TryParse(strAmount, out var amount) && amount > 0 && amount <= balance;
            }

            return false;
        }

        public string? GetWithdrawalAmount()
        {
            Console.WriteLine("\nHow much would you like to withdraw? ");
            var strAmount = Console.ReadLine();
            return strAmount;
        }

        public void PrintInvalidInputMessage(decimal balance)
        {
            Console.WriteLine($"\nInvalid input. Please enter an amount up to two decimal places, between {MinWithdrawal} and less than {balance + (decimal)0.01}.");
        }
    }
}
