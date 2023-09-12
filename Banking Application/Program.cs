namespace Banking_Application;
class Program
{
    static void Main(string[] args)
    {
        var menu = new Menu(
            new DepositService(new ConsoleWrapper()),
            new WithdrawService(new ConsoleWrapper(), 0),
            new BalanceService(new ConsoleWrapper()),
            new ConsoleWrapper()
            );
        menu.MainMenu();
    }
}