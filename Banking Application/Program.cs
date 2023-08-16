using Console = System.Console;

namespace Banking_Application;
class Program
{
    private static int _balance;

    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to RoBank!");
        while (true)
        {
            var option = OptionsMenu.Options();
            switch (option)
            {
                case 1:
                    _balance = Deposit.DepositOption(_balance);
                    DisplayBalance();
                    break;
                case 2:
                    _balance = Withdraw.WithdrawOption(_balance);
                    DisplayBalance();
                    break;
                case 3:
                    DisplayBalance();
                    break;
                case 4:
                    Exit();
                    return;
            }
        }
    }

    public static void DisplayBalance()
    {
        Console.WriteLine($"\nYour current balance is {_balance}");
    }

    public static void Exit()
    {
        Environment.Exit(0);
    }
}