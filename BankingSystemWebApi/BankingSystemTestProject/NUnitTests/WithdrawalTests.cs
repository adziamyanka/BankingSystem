using System.Threading.Tasks;
using BankingSystemTestProject.Common;
using NUnit.Framework;

namespace BankingSystemTestProject.NUnitTests
{
    [TestFixture]
    public class WithdrawalTests : TestBase
    {
        private const int DefaultBalance = 1000;

        [SetUp]
        public async Task SetUpTestUserWithAccount()
        {
            await CreateUser(TestUserName);
        }

        [TearDown]
        public async Task TearDownTestUser()
        {
            await DeleteUser(TestUserName);
        }

        [Test]
        [TestCase(0)]
        [TestCase(100)]
        [TestCase(899)]
        public async Task WithdrawFromAccount(int withdrawal)
        {
            await CreateAccount(TestUserName, DefaultBalance);

            await WithdrawFromAccount(TestUserName, DefaultAccountId, withdrawal);

            ValidateSuccessfulResult();

            await GetUser(TestUserName);

            ValidateDefaultAccountBalance(DefaultBalance - withdrawal);
        }
      
        [Test]
        [TestCase(200, -1, ErrorMessages.NegativeWithdrawal)]
        [TestCase(1000, 900, ErrorMessages.WithdrawalLimit)]
        [TestCase(101, 2, ErrorMessages.BalanceMinLimit)]
        public async Task WithdrawUnexpectedAmount(int balance, int withdrawal, string errorMessage)
        {
            await CreateAccount(TestUserName, balance); 

            await WithdrawFromAccount(TestUserName, DefaultAccountId, withdrawal);

            ValidateError(errorMessage);
        }
    }
}