using System.Globalization;

namespace Banking_Application
{
    public class BalanceService : IBalanceService
    {
        private readonly ConsoleWrapper console;
        private const string Currency = "en-GB";

        public BalanceService(ConsoleWrapper console)
        {
            this.console = console;
        }

        public void DisplayBalance(decimal balance)
        {
            var cultureInfo = GetCultureInfo();
            
            var formattedAmount = string.Format(cultureInfo, "{0:C}", balance);

            console.WriteLine($"\nYour current balance is {formattedAmount}");
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
