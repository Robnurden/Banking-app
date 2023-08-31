﻿using static System.Net.Mime.MediaTypeNames;
using System.Text.RegularExpressions;

namespace Banking_Application
{
    public class Deposit : IDeposit
    {
        private const decimal MaxDeposit = 1000000;
        private const decimal MinDeposit = 1;

        public decimal DepositOrchestrator(decimal balance)
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
                    amount = Math.Round(decimal.Parse(strAmount), 2, MidpointRounding.AwayFromZero);
                }

            } while (!isValid);

            return Math.Round(balance + amount, 2, MidpointRounding.AwayFromZero);
        }

        public string? GetDepositAmount()
        {
            Console.WriteLine("\nHow much would you like to deposit?");
            return Console.ReadLine();
        }

        public bool ValidateDepositAmount(string? strAmount)
        {
            string pattern = @"^\-?[0-9]+(?:\.[0-9]{1,2})?$";
            var isValidDecimalString = Regex.IsMatch(strAmount, pattern);

            if (isValidDecimalString)
            {
                return decimal.TryParse(strAmount, out var amount) && amount is < MaxDeposit and > 0;
            }

            return false;
        }

        public void PrintInvalidInputMessage()
        {
            Console.WriteLine($"\nInvalid input. Please enter an amount up to two decimal places, between {MinDeposit} and {MaxDeposit}.");
        }
    }
}
