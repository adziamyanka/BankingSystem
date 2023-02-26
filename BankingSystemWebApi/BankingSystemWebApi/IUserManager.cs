using System.Collections.Generic;
using BankingSystemWebApi.DataModels;

namespace BankingSystemWebApi
{
    public interface IUserManager
    {
        List<User> GetAllUsers();
        User GetUserByName(string userName);
        User CreateUser(string userName);
        void CreateAccount(string userName, int balance);
        void DeleteAccount(string userName, int accountId);
        void DeleteUser(string userName);
        void DepositToAccount(string userName, int accountId, int deposit);
        void WithdrawFromAccount(string userName, int accountId, int withdrawal);
    }
}
