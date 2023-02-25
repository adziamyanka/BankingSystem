using System.Threading.Tasks;
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
        [TestCase(0,  "An account cannot have less than $100.")]
        [TestCase(-1, "An account cannot have less than $100.")]
        [TestCase(99, "An account cannot have less than $100.")]
        [TestCase(10001, "A user cannot deposit more than $10000 in a single transaction.")]
        public async Task CreateAccountWithUnexpectedBalance(int balance, string errorMessage)
        {
            await CreateAccount(TestUserName, balance);

            ValidateError(errorMessage);
        }
    }
}
