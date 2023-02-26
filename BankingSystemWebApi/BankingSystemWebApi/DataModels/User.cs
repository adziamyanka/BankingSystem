using System.Collections.Generic;

namespace BankingSystemWebApi.DataModels
{
    public class User
    {
        public string Name { get; set; }
        public List<Account> Accounts { get; set; }
    }
}
