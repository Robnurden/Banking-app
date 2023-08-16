using System.ComponentModel.Design;

namespace BankingApplication;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to RoBank!");
        var option = Options();
        Console.ReadLine();
    }

    public static int Options()
    {
        int option;
        var isValid = false;
        var validOptions = new List<int> { 1, 2, 3 };     
        do
        {
            Console.WriteLine("Please select an option: " +
                              "\n1. Deposit a credit balance " +
                              "\n2. Withdraw a credit balance " +
                              "\n3. Display my current balance");
            var selectedOption = Console.ReadLine();

            if (int.TryParse(selectedOption, out option) && validOptions.Contains(option))
            {
                isValid = true;
            }
            else
            {
                Console.WriteLine("Invalid input, please select an option of 1, 2 or 3.");
            }

        } while (!isValid);

        Console.WriteLine($"You have selected option {option}");

        return option;
    }
}