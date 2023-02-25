using System.Net.Http;
using System.Threading.Tasks;

namespace BankingSystemTestProject.Services
{
    public class BankingSystemService
    {
        private static readonly HttpClient HttpClient = new ();
        private const string BaseUrl = "https://localhost:44384/BankingSystem";

        public static async Task<string> CreateUser(string userName)
        {
            var url = $"{BaseUrl}/CreateUser?userName={userName}";

            var task = await HttpClient.PostAsync(url, null);
            var response = task.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public static async Task<string> CreateAccount(string userName, int balance)
        {
            var url = $"{BaseUrl}/CreateAccount?userName={userName}&balance={balance}";

            var task = await HttpClient.PostAsync(url, null);
            var response = task.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public static async Task<string> DeleteAccount(string userName, int accountId)
        {
            var url = $"{BaseUrl}/DeleteAccount?userName={userName}&accountId={accountId}";

            var task = await HttpClient.DeleteAsync(url);
            var response = task.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public static async Task<string> DeleteUser(string userName)
        {
            var url = $"{BaseUrl}/DeleteUser?userName={userName}";

            var task = await HttpClient.DeleteAsync(url);
            var response = task.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public static async Task<string> DepositToAccount(string userName, int accountId, int deposit)
        {
            var url = $"{BaseUrl}/DepositToAccount?userName={userName}&accountId={accountId}&deposit={deposit}";

            var task = await HttpClient.PostAsync(url, null);
            var response = task.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public static async Task<string> WithdrawFromAccount(string userName, int accountId, int withdrawal)
        {
            var url = $"{BaseUrl}/WithdrawFromAccount?userName={userName}&accountId={accountId}&withdrawal={withdrawal}";

            var task = await HttpClient.PostAsync(url, null);
            var response = task.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
