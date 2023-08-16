namespace Banking_Application
{
    public class OptionsMenu
    {
        public static int Options()
        {
            int option;
            var isValid = false;
            var validOptions = new List<int> { 1, 2, 3, 4 };
            do
            {
                Console.WriteLine("\nPlease select an option: " +
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
                    Console.WriteLine("\nInvalid input, please select an option of 1, 2, 3 or 4.\n");
                }

            } while (!isValid);

            Console.WriteLine($"\nYou have selected option {option}\n");

            return option;
        }
    }
}
