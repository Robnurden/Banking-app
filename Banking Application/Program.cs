namespace Banking_Application;
class Program
{
    static void Main(string[] args)
    {
        var menu = new Menu(new DepositServiceService(), new WithdrawService());
        menu.MainMenu();
    }
}