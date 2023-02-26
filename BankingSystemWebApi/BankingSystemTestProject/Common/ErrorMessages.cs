
namespace BankingSystemTestProject.Common
{
    public static class ErrorMessages
    {
        public const string SingleTransaction = "A user cannot deposit more than $10000 in a single transaction.";
        public const string BalanceMinLimit = "An account cannot have less than $100.";
        public const string NegativeDeposit = "Deposit should be more than $0.";
        public const string NegativeWithdrawal = "Withdrawal amount should be more than $0.";
        public const string WithdrawalLimit = "A user cannot withdraw more than 90% of their total balance from an account in a single transaction.";
    }
}
