using System.Globalization;

namespace Banking_Application
{
    public interface IBalanceService
    {
        void DisplayBalance(decimal balance);

        CultureInfo GetCultureInfo();
    }
}
