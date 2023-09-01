namespace Banking_Application
{
    public class ConsoleWrapper
    {
        public virtual string ReadLine()
        {
            return Console.ReadLine();
        }

        public virtual void WriteLine(string value)
        {
            Console.WriteLine(value);
        }
    }
}
