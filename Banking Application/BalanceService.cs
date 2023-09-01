using System.Globalization;

namespace Banking_Application
{
    public class BalanceService : IBalanceService
    {
        private const string Currency = "en-GB";

        public void DisplayBalance(decimal balance)
        {
            var cultureInfo = GetCultureInfo();
            
            var formattedAmount = string.Format(cultureInfo, "{0:C}", balance);

            Console.WriteLine($"\nYour current balance is {formattedAmount}");
        }

        public CultureInfo GetCultureInfo()
        {
            var region = new RegionInfo(Currency);

            var cultureInfo = new CultureInfo(Currency)
            {
                NumberFormat =
                {
                    CurrencySymbol = region.CurrencySymbol
                }
            };

            return cultureInfo;
        }
    }
}
