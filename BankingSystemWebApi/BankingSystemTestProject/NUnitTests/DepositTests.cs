using System.Threading.Tasks;
using NUnit.Framework;

namespace BankingSystemTestProject.NUnitTests
{
    [TestFixture]
    public class DepositTests : TestBase
    {
        [SetUp]
        public async Task SetUpTestUserWithAccount()
        {
            await CreateUser(TestUserName);
            await CreateAccount(TestUserName, 300);
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
        }

        [Test]
        [TestCase(-1, "Deposit should be more than $0.")]
        [TestCase(10001, "A user cannot deposit more than $10000 in a single transaction.")]
        public async Task DepositUnexpectedAmount(int deposit, string errorMessage)
        {
            await DepositToAccount(TestUserName, DefaultAccountId, deposit);

            ValidateError(errorMessage);
        }
    }
}