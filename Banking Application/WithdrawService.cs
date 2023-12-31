﻿using System.Text.RegularExpressions;

namespace Banking_Application
{
    public class WithdrawService : IWithdrawService
    {
        private readonly ConsoleWrapper _console;
        private const decimal MinWithdrawal = 1;
        private readonly decimal _balanceThreshold;

        public WithdrawService(ConsoleWrapper console, decimal balanceThreshold)
        {
            _console = console;
            _balanceThreshold = balanceThreshold;
        }

        public decimal WithdrawOrchestrator(decimal balance)
        {
            if (!BalanceCheck(balance))
            {
                return balance;
            }

            decimal roundedAmount = 0;
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
                    var amount = decimal.Parse(strAmount);

                    roundedAmount = CalculateAmountWithRounding(amount);
                }

            } while (!isValid);

            return CalculateAmountWithRounding(balance - roundedAmount);
        }

        private bool BalanceCheck(decimal balance)
        {
            if (balance > _balanceThreshold)
            {
                return true;
            }

            _console.WriteLine($"Current balance is {balance}. Please deposit funds before you can withdraw.");

            return false;
        }

        public bool ValidateWithdrawalAmount(string? strAmount, decimal balance)
        {
            string pattern = @"^\-?[0-9]+(?:\.[0-9]{1,2})?$";
            var isValidDecimalString = strAmount != null && Regex.IsMatch(strAmount, pattern);

            if (isValidDecimalString)
            {
                return decimal.TryParse(strAmount, out var amount) && amount > 0 && amount <= balance;
            }

            return false;
        }

        private string? GetWithdrawalAmount()
        {
            _console.WriteLine("\nHow much would you like to withdraw? ");
            var strAmount = _console.ReadLine();
            return strAmount;
        }

        private void PrintInvalidInputMessage(decimal balance)
        {
            _console.WriteLine($"\nInvalid input. Please enter an amount up to two decimal places, between {MinWithdrawal} and less than {balance + (decimal)0.01}.");
        }

        private decimal CalculateAmountWithRounding(decimal amount)
        {
            return Math.Round(amount, 2, MidpointRounding.AwayFromZero);
        }
    }
}
