﻿namespace Banking_Application
{
    public interface IBalanceService
    {
        void DisplayBalance(decimal balance);

        string GetCurrencySymbol(string currencyCode);
    }
}
