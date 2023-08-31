namespace Banking_Application;

public interface IDeposit
{
    decimal DepositOrchestrator(decimal balance);
    
    string? GetDepositAmount();

    bool ValidateDepositAmount(string? strAmount);

    void PrintInvalidInputMessage();
}