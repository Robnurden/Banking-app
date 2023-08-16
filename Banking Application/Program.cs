using System.ComponentModel.Design;
using Console = System.Console;

namespace BankingApplication;
class Program
{
    private static int _balance;

    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to RoBank!");
        var isValid = true;

        while (isValid)
        {
            var option = Options();
            switch (option)
            {
                case 1:
                    Deposit();
                    break;
                case 2:
                    Withdraw();
                    break;
                case 3:
                    DisplayBalance();
                    break;
                case 4:
                    Exit();
                    isValid = false;
                    break;
            }
        }
    }

    public static int Options()
    {
        int option;
        var isValid = false;
        var validOptions = new List<int> { 1, 2, 3, 4 };     
        do
        {
            Console.WriteLine("Please select an option: " +
                              "\n1. Deposit a credit balance " +
                              "\n2. Withdraw a credit balance " +
                              "\n3. Display my current balance" +
                              "\n4. Exit");
            var selectedOption = Console.ReadLine();

            if (int.TryParse(selectedOption, out option) && validOptions.Contains(option))
            {
                isValid = true;
            }
            else
            {
                Console.WriteLine("Invalid input, please select an option of 1, 2, 3 or 4.");
            }

        } while (!isValid);

        Console.WriteLine($"You have selected option {option}");

        return option;
    }

    public static void Deposit()
    {
        int amount;
        var isValid = false;
        do
        {
            Console.WriteLine("How much would you like to deposit? ");
            var strAmount = Console.ReadLine();

            if (int.TryParse(strAmount, out amount) && amount is < 1000000 and > 0)
            {
                isValid = true;
            }
            else
            {
                Console.WriteLine("Invalid input, please enter a number between 1 and 1,000,000.");
            }

        } while (!isValid);

        _balance =+ amount;

        DisplayBalance();
    }

    public static void Withdraw()
    {
        int amount;
        var isValid = false;
        do
        {
            Console.WriteLine("How much would you like to withdraw? ");
            var strAmount = Console.ReadLine();

            if (int.TryParse(strAmount, out amount) && amount is > 0)
            {
                isValid = true;
            }
            else
            {
                Console.WriteLine("Invalid input, please enter a number greater than 1.");
            }

        } while (!isValid);

        _balance = -amount;

        DisplayBalance();
    }

    public static void DisplayBalance()
    {
        Console.WriteLine($"Your current balance is {_balance}");
    }

    public static void Exit()
    {
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();

        Environment.Exit(0);
    }
}