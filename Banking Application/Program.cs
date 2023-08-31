namespace Banking_Application;
class Program
{
    static void Main(string[] args)
    {
        var menu = new Menu(new DepositService(), new WithdrawService());
        menu.MainMenu();
    }
}