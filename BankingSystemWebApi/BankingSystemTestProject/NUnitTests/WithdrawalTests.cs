using System.Threading.Tasks;
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
        [TestCase(200, -1, "Withdrawal amount should be more than $0.")]
        [TestCase(1000, 900, "A user cannot withdraw more than 90% of their total balance from an account in a single transaction.")]
        [TestCase(101, 2, "An account cannot have less than $100 at any time in an account.")]
        public async Task WithdrawUnexpectedAmount(int balance, int withdrawal, string errorMessage)
        {
            await CreateAccount(TestUserName, balance); 

            await WithdrawFromAccount(TestUserName, DefaultAccountId, withdrawal);

            ValidateError(errorMessage);
        }
    }
}