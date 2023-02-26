using System;
using System.Collections.Generic;
using System.Linq;
using BankingSystemWebApi.DataModels;

namespace BankingSystemWebApi
{
    public class UserManager : IUserManager
    {
        private Dictionary<string, User> _users = new ();
        private const int MinAccountLimit = 100;
        private const int MaxDepositLimit = 10000;
        private const double WithdrawLimit = 0.90;

        public List<User> GetAllUsers()
        {
            return _users.Select(x => x.Value).ToList();
        }

        public User GetUserByName(string userName)
        {
            if (!_users.TryGetValue(userName, out var user))
                throw new Exception($"User {userName} doesn't exist.");

            return user;
        }

        public User CreateUser(string userName)
        {
            if (_users.ContainsKey(userName))
                throw new Exception($"User {userName} already exists.");

            if (_users.TryAdd(userName,
                    new User()
                    {
                        UserName = userName
                    })) 
                return _users[userName];

            throw new Exception($"User {userName} wasn't created.");
        }

        public void CreateAccount(string userName, int balance)
        {
            if (!_users.ContainsKey(userName))
                throw new Exception($"User {userName} doesn't exist.");

            if (balance < MinAccountLimit)
                throw new Exception($"An account cannot have less than ${MinAccountLimit}.");
            if (balance > MaxDepositLimit)
                throw new Exception ($"A user cannot deposit more than ${MaxDepositLimit} in a single transaction.");
            
            _users[userName].Accounts ??= new List<Account>();
            _users[userName].Accounts.Add(new Account() {Id = _users[userName].Accounts.Count + 1, Balance = balance});
        }

        public void DeleteAccount(string userName, int accountId)
        {
            if (!_users.TryGetValue(userName, out var user))
                throw new Exception($"User {userName} doesn't exist.");

            if(user.Accounts == null)
                throw new Exception($"User {userName} doesn't have any accounts.");

            var index = user.Accounts.FindIndex(x => x.Id == accountId);
            if (index < 0)
                throw new Exception($"User {userName} doesn't have account with id {accountId}.");
           
            user.Accounts.RemoveAt(index);
        }

        public void DeleteUser(string userName)
        {
            if (!_users.ContainsKey(userName))
                throw new Exception($"User {userName} doesn't exist.");

            if(!_users.Remove(userName))
                throw new Exception($"User {userName} wasn't removed.");
        }

        public void DepositToAccount(string userName, int accountId, int deposit)
        {
            if (!_users.TryGetValue(userName, out var user))
                throw new Exception($"User {userName} doesn't exist.");

            var account = user.Accounts.FirstOrDefault(x => x.Id == accountId);
            if (account == null)
                throw new Exception($"Account with id {accountId} doesn't exist.");

            if (deposit < 0)
                throw new Exception("Deposit should be more than $0.");
            if (deposit > MaxDepositLimit)
                throw new Exception($"A user cannot deposit more than ${MaxDepositLimit} in a single transaction.");

            account.Balance += deposit;
        }

        public void WithdrawFromAccount(string userName, int accountId, int withdrawal)
        {
            if (!_users.TryGetValue(userName, out var user))
                throw new Exception($"User {userName} doesn't exist.");

            var account = user.Accounts.FirstOrDefault(x => x.Id == accountId);
            if (account == null)
                throw new Exception($"Account with id {accountId} doesn't exist.");

            if (withdrawal < 0)
                throw new Exception("Withdrawal amount should be more than $0.");
            if (withdrawal > account.Balance)
                throw new Exception("Account balance less than user wants to withdraw.");
            if (account.Balance - withdrawal < MinAccountLimit)
                throw new Exception($"An account cannot have less than ${MinAccountLimit}.");
            if (account.Balance * WithdrawLimit <= withdrawal)
                throw new Exception(
                    $"A user cannot withdraw more than {WithdrawLimit * 100}% of their total balance from an account in a single transaction.");

            account.Balance -= withdrawal;
        }
    }
}
