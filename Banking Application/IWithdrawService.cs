namespace Banking_Application;

public interface IWithdrawService
{
    decimal WithdrawOrchestrator(decimal balance);
    bool BalanceCheck(decimal balance);
    bool ValidateWithdrawalAmount(string? strAmount, decimal balance);
    string? GetWithdrawalAmount();
    void PrintInvalidInputMessage(decimal balance);
}