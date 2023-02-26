using System.Threading.Tasks;
using BankingSystemTestProject.Common;
using NUnit.Framework;

namespace BankingSystemTestProject.NUnitTests
{
    [TestFixture]
    public class DepositTests : TestBase
    {
        private const int DefaultBalance = 300;

        [SetUp]
        public async Task SetUpTestUserWithAccount()
        {
            await CreateUser(TestUserName);
            await CreateAccount(TestUserName, DefaultBalance);
        }

        [TearDown]
        public async Task TearDownTestUser()
        {
            await DeleteUser(TestUserName);
        }

        [Test]
        [TestCase(0)]
        [TestCase(100)]
        [TestCase(10000)]
        public async Task DepositToAccount(int deposit)
        {
            await DepositToAccount(TestUserName, DefaultAccountId, deposit);

            ValidateSuccessfulResult();

            await GetUser(TestUserName);

            ValidateDefaultAccountBalance(DefaultBalance + deposit);
        }
     
        [Test]
        [TestCase(-1, ErrorMessages.NegativeDeposit)]
        [TestCase(10001, ErrorMessages.SingleTransaction)]
        public async Task DepositUnexpectedAmount(int deposit, string errorMessage)
        {
            await DepositToAccount(TestUserName, DefaultAccountId, deposit);

            ValidateError(errorMessage);
        }
    }
}