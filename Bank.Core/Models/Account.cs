using System.Collections.Generic;

namespace Bank.Core
{
    public class Account
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public decimal AccountBalance { get; set; }
        public decimal CreditBalance { get; set; }
        public bool AccountBlockFlag { get; set; }
        public bool CreditFlag { get; set; }
        public bool AdminFlag { get; set; }
        public List<DataAccount> DataAccount { get; set; }

        public Account(int id, string firstName, string lastName, string login, string password, decimal accountBalance,
            decimal CreditBalance = 0, bool accountBlock = false ,bool creditFlag = false, bool adminFlag = false)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Login = login;
            Password = password;
            AccountBalance = accountBalance;
            DataAccount = new List<DataAccount>();
        }

        public override string ToString()
        {
            return Id.ToString() + ";" + FirstName + ";" + LastName + ";" + Login + ";" + Password + ";" 
                + AccountBalance.ToString() + ";" + CreditBalance.ToString() + ";" + 
                AccountBlockFlag.ToString() + ";" + CreditFlag.ToString() + ";" + AdminFlag.ToString();
        }
    }
}
