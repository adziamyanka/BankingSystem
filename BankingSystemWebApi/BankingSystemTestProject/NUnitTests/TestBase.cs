using System.Linq;
using System.Threading.Tasks;
using BankingSystemTestProject.Common;
using BankingSystemTestProject.DataModels;
using BankingSystemTestProject.Services;
using Newtonsoft.Json;
using NUnit.Framework;

namespace BankingSystemTestProject.NUnitTests
{
    public class TestBase
    {
        public const string TestUserName = "TestUser";
        public const int DefaultAccountId = 1;
        public Response Response { get; set; }
        public User User { get; set; }

        public async Task GetUser(string userName)
        {
            var response = await BankingSystemService.GetUser(userName);
            User = JsonConvert.DeserializeObject<User>(response);
        }
        
        public async Task CreateUser(string userName)
        {
            var response = await BankingSystemService.CreateUser(userName);
            var user = JsonConvert.DeserializeObject<User>(response);

            Assert.IsNotNull(user, "User wasn't created");
        }

        public async Task CreateAccount(string userName, int balance)
        {
            var response = await BankingSystemService.CreateAccount(userName, balance);
            Response = JsonConvert.DeserializeObject<Response>(response);
        }

        public async Task DeleteAccount(string userName, int accountId)
        {
            var response = await BankingSystemService.DeleteAccount(userName, accountId);
            Response = JsonConvert.DeserializeObject<Response>(response);
        }

        public async Task DeleteUser(string userName)
        {
            var response = await BankingSystemService.DeleteUser(userName);
            Response = JsonConvert.DeserializeObject<Response>(response);

            ValidateSuccessfulResult();
        }

        public async Task DepositToAccount(string userName, int accountId, int deposit)
        {
            var response = await BankingSystemService.DepositToAccount(userName, accountId, deposit);
            Response = JsonConvert.DeserializeObject<Response>(response);
        }

        public async Task WithdrawFromAccount(string userName, int accountId, int deposit)
        {
            var response = await BankingSystemService.WithdrawFromAccount(userName, accountId, deposit);
            Response = JsonConvert.DeserializeObject<Response>(response);
        }

        public void ValidateSuccessfulResult()
        {
            Assert.IsNotNull(Response, "There is no result");
            Assert.AreEqual(ResponseStatus.Ok, Response.Status, $"Status of the response is not {ResponseStatus.Ok}. Error message {Response.Message}");
        }

        public void ValidateError(string errorMessage)
        {
            Assert.IsNotNull(Response, "There is no result");
            Assert.AreEqual(ResponseStatus.Error, Response.Status, $"Status of the response is not {ResponseStatus.Error}");
            Assert.AreEqual(errorMessage, Response.Message, $"Error message is not as expected. Expected {errorMessage}, but was {Response.Message}");
        }

        public void ValidateDefaultAccountBalance(int balance)
        {
            Assert.IsNotNull(User, "There is no user");
            Assert.IsNotNull(User.Accounts, $"User {User.Name} has no accounts");

            var defaultAccount = User.Accounts.FirstOrDefault(x => x.Id == DefaultAccountId);
            Assert.IsNotNull(defaultAccount, $"There is no account with id {DefaultAccountId}");

            Assert.AreEqual(balance, defaultAccount.Balance, $"Balance is not as expected: {defaultAccount.Balance}, expected {balance}");
        }

        public void ValidateAllUserAccounts(params int[] balances)
        {
            Assert.IsNotNull(User, "There is no user");
            Assert.IsNotNull(User.Accounts, $"User {User.Name} has no accounts");
            Assert.AreEqual(balances.Length, User.Accounts.Count, $"Amounts of accounts are not equal. Expected {balances.Length}, but was {User.Accounts.Count}");

            for (int id = 1; id <= balances.Length; id++)
            {
                var account = User.Accounts.FirstOrDefault(x => x.Id == id);
                Assert.IsNotNull(account, $"There is no account with id {id}");

                Assert.AreEqual(balances[id-1], account.Balance, $"Balance is not as expected: {account.Balance}, expected {balances[id - 1]}");
            }
        }

        public void ValidateUserHasNoAccounts()
        {
            Assert.IsNotNull(User, "There is no user");
            Assert.IsEmpty(User.Accounts, $"User {User.Name} has an account/s");
        }
    }
}
