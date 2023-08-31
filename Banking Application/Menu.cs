﻿using System.Globalization;

namespace Banking_Application
{
    public class Menu
    {
        private readonly IDepositService _depositService;
        private readonly IWithdrawService _withdrawService;
        private static decimal _balance;
        private const string Greeting = "Welcome to RoBank";
        private const string DepositOption = "Deposit an amount";
        private const string WithdrawOption = "Withdraw an amount";
        private const string BalanceOption = "Display your current balance";

        public Menu(IDepositService depositService, IWithdrawService withdrawService)
        {
            _depositService = depositService;
            _withdrawService = withdrawService;
        }

        public void MainMenu()
        {
            Console.WriteLine($"{Greeting}");

            while (true)
            {
                var selectedOption = GetSelectedOption();
                MenuOrchestrator(selectedOption);
            }
        }

        public int GetSelectedOption()
        {
            string? selectedOption;
            bool isValid;
            do
            {
                selectedOption = ReadSelectedOption();
                isValid = ValidateOption(selectedOption);

                if (!isValid)
                {
                    PrintInvalidOptionMessage();
                }

            } while (!isValid);

            return int.Parse(selectedOption);
        }

        public void MenuOrchestrator(int selectedOption)
        {
            switch (selectedOption)
            {
                case 1:
                    _balance = _depositService.DepositOrchestrator(_balance);
                    break;
                case 2:
                    _balance = _withdrawService.WithdrawOrchestrator(_balance);
                    break;
                case 3:
                    DisplayBalance();
                    break;
                case 4:
                    Exit();
                    return;
            }
        }

        public string? ReadSelectedOption()
        {
            Console.WriteLine("\nPlease select an option: " +
                              $"\n1. {DepositOption} " +
                              $"\n2. {WithdrawOption} " +
                              $"\n3. {BalanceOption}" +
                              "\n4. Exit");

            return Console.ReadLine();
        }

        public bool ValidateOption(string? selectedOption)
        {
            var validOptions = new List<int> { 1, 2, 3, 4 };

            return int.TryParse(selectedOption, out var option) && validOptions.Contains(option);
        }

        private void PrintInvalidOptionMessage()
        {
            Console.WriteLine("\nInvalid input, please select an option of 1, 2, 3, or 4.\n");
        }

        public void DisplayBalance()
        {
            string currencyCode = "en-GB";

            CultureInfo cultureInfo = new CultureInfo(currencyCode);
            cultureInfo.NumberFormat.CurrencySymbol = GetCurrencySymbol(currencyCode);

            string formattedAmount = string.Format(cultureInfo, "{0:C}", _balance);

            Console.WriteLine($"\nYour current balance is {formattedAmount}");
        }

        public void Exit()
        {
            Console.WriteLine("\nThank you for using RoBank.");
            Environment.Exit(0);
        }

        public string GetCurrencySymbol(string currencyCode)
        {
            try
            {
                RegionInfo region = new RegionInfo(currencyCode);
                return region.CurrencySymbol;
            }
            catch (ArgumentException)
            {
                return currencyCode;
            }
        }
    }
}
