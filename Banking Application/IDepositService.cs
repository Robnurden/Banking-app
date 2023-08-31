namespace Banking_Application;

public interface IDepositService
{
    decimal DepositOrchestrator(decimal balance);
    
    string? GetDepositAmount();

    bool ValidateDepositAmount(string? strAmount);

    void PrintInvalidInputMessage();
}