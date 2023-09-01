using System.Globalization;

namespace Banking_Application
{
    public class BalanceService : IBalanceService
    {
        public void DisplayBalance(decimal balance)
        {
            string currencyCode = "en-GB";

            CultureInfo cultureInfo = new CultureInfo(currencyCode);
            cultureInfo.NumberFormat.CurrencySymbol = GetCurrencySymbol(currencyCode);

            string formattedAmount = string.Format(cultureInfo, "{0:C}", balance);

            Console.WriteLine($"\nYour current balance is {formattedAmount}");
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
