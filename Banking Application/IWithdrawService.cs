namespace Banking_Application;

public interface IWithdrawService
{
    decimal WithdrawOrchestrator(decimal balance);
}