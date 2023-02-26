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
    }
}
