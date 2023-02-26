using System.Threading.Tasks;
using BankingSystemTestProject.Common;
using NUnit.Framework;

namespace BankingSystemTestProject.NUnitTests
{
    [TestFixture]
    public class CreateAccountTests : TestBase
    {
        [SetUp]
        public async Task SetUpTestUser()
        {
            await CreateUser(TestUserName);
        }

        [TearDown]
        public async Task TearDownTestUser()
        {
            await DeleteUser(TestUserName);
        }

        [Test]
        [TestCase(100)]
        [TestCase(200)]
        [TestCase(10000)]
        public async Task CreateAccount(int balance)
        {
            await CreateAccount(TestUserName, balance);

            ValidateSuccessfulResult();

            await GetUser(TestUserName);

            ValidateDefaultAccountBalance(balance);

        }

        [TestCase(100, 200)]
        public async Task CreateTwoAccounts(int balance1, int balance2)
        {
            await CreateAccount(TestUserName, balance1);

            ValidateSuccessfulResult();

            await CreateAccount(TestUserName, balance2);

            ValidateSuccessfulResult();

            await GetUser(TestUserName);

            ValidateAllUserAccounts(balance1, balance2);
        }

        [Test]
        [TestCase(0, ErrorMessages.BalanceMinLimit)]
        [TestCase(-1, ErrorMessages.BalanceMinLimit)]
        [TestCase(99, ErrorMessages.BalanceMinLimit)]
        [TestCase(10001, ErrorMessages.SingleTransaction)]
        public async Task CreateAccountWithUnexpectedBalance(int balance, string errorMessage)
        {
            await CreateAccount(TestUserName, balance);

            ValidateError(errorMessage);
        }

        [Test]
        public async Task DeleteAccount()
        {
            await CreateAccount(TestUserName, 200);

            ValidateSuccessfulResult();

            await DeleteAccount(TestUserName, DefaultAccountId);

            ValidateSuccessfulResult();

            await GetUser(TestUserName);

            ValidateUserHasNoAccounts();
        }

        [Test]
        public async Task DeleteNonExistentAccount()
        {
            await DeleteAccount(TestUserName, DefaultAccountId);

            ValidateError($"User {TestUserName.ToLower()} doesn't have any accounts.");
        }
    }
}
