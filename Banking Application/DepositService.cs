using System.Text.RegularExpressions;

namespace Banking_Application
{
    public class DepositService : IDepositService
    {
        private readonly ConsoleWrapper console;
        private const decimal MaxDeposit = 1000000;
        private const decimal MinDeposit = 1;
        private const string CurrencyRegex = @"^\-?[0-9]+(?:\.[0-9]{1,2})?$";

        public DepositService(ConsoleWrapper console)
        {
            this.console = console;
        }

        public decimal DepositOrchestrator(decimal balance)
        {
            bool isValid;
            decimal roundedAmount = 0;
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
                    var amount = decimal.Parse(strAmount);

                    roundedAmount = CalculateAmountWithRounding(balance + amount);
                }

            } while (!isValid);

            return roundedAmount;
        }

        public bool ValidateDepositAmount(string? strAmount)
        {
            var isValidDecimalString = strAmount != null && Regex.IsMatch(strAmount, CurrencyRegex);

            if (isValidDecimalString)
            {
                return decimal.TryParse(strAmount, out var amount) && amount is < MaxDeposit and > 0;
            }

            return false;
        }

        private string? GetDepositAmount()
        {
            console.WriteLine("\nHow much would you like to deposit?");
            return console.ReadLine();
        }

        private void PrintInvalidInputMessage()
        {
            console.WriteLine($"\nInvalid input. Please enter an amount up to two decimal places, between {MinDeposit} and {MaxDeposit}.");
        }

        private decimal CalculateAmountWithRounding(decimal amount)
        {
            return Math.Round(amount, 2, MidpointRounding.AwayFromZero);
        }
    }
}
