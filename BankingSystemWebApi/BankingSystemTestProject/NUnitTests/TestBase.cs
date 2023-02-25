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
    }
}
