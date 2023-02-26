using System.Threading.Tasks;
using BankingSystemTestProject.Common;
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
        [TestCase(-1, ErrorMessages.NegativeDeposit)]
        [TestCase(10001, ErrorMessages.SingleTransaction)]
        public async Task DepositUnexpectedAmount(int deposit, string errorMessage)
        {
            await DepositToAccount(TestUserName, DefaultAccountId, deposit);

            ValidateError(errorMessage);
        }
    }
}