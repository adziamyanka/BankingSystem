using System.Threading.Tasks;
using BankingSystemTestProject.Common;
using NUnit.Framework;

namespace BankingSystemTestProject.NUnitTests
{
    [TestFixture]
    public class WithdrawalTests : TestBase
    {
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
        [TestCase(10000)]
        public async Task WithdrawFromAccount(int withdrawal)
        {
            await CreateAccount(TestUserName, 1000);

            await DepositToAccount(TestUserName, DefaultAccountId, withdrawal);

            ValidateSuccessfulResult();
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