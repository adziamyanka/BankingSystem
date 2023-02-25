using System.Collections.Generic;

namespace BankingSystemTestProject.DataModels
{
    public class User
    {
        public string UserName { get; set; }
        public List<Account> Accounts { get; set; }
    }
}
