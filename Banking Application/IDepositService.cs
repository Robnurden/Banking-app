namespace Banking_Application;

public interface IDepositService
{
    decimal DepositOrchestrator(decimal balance);

    bool ValidateDepositAmount(string? strAmount);
}