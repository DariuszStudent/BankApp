using System.Collections.Generic;
using System.Linq;

namespace Bank.Core
{
    public class BankService
    {
        private List<Account> Accounts { get; set; }
        private List<DataAccount> DataAccounts { get; set; }

        public BankService()
        {
            Accounts = new List<Account>();
            DataAccounts = new List<DataAccount>();

            var admin = new Account(0, "admin", "admin", "admin", "1234", 0);
            admin.AdminFlag = true;
            Accounts.Add(admin);
            var user1 = new Account(1, "Bożena", "Babcia", "babcia", "1234", 0);
            Accounts.Add(user1);
            PayOnAccount(user1, 1000);
            PayOnAccount(user1, 500);
            var user2 = new Account(2, "Sławomir", "Dziadek", "dziadek", "1234", 0);
            Accounts.Add(user2);
            PayOnAccount(user2, 200);
            PayOnAccount(user2, 5000);
        }

        public Account Login(string login, string password)
        {
            var result = Accounts.FirstOrDefault(x => x.Login == login && x.Password == password);

            if (login != null) return result;
            else return null;
        }

        public bool CheckAdmin(Account account)
        {
            return Accounts.FirstOrDefault(x => x == account).AdminFlag;
        }

        // -------------------------------------------------------------------------------------------------ADMIN

        public bool BlockAccount(int id)
        {
            return Accounts.FirstOrDefault(x => x.Id == id).AccountBlockFlag = true;
        }


        public List<string> ShowAccounts()
        {
            var users = new List<string>();

            for (var i = 1; i < Accounts.Count; i++)
            {
                users.Add($"{Accounts[i].Id}. | {Accounts[i].FirstName} {Accounts[i].LastName} " +
                    $"| Stan konta: {Accounts[i].AccountBalance}zł. " +
                    $"| Stan kredytu: {Accounts[i].CreditBalance}zł.");
            }
            return users;
        }

        public void AddNewAccount(int id, string firstName, string lastName, string login,
            string password, decimal accountBalance)
        {
            var newUser = new Account(id, firstName, lastName, login, password, accountBalance);
            Accounts.Add(newUser);
            Accounts = Accounts.OrderBy(x => x.Id).ToList();
        }

        public bool CheckLogin(string login)
        {
            for (int i = 0; i < Accounts.Count; i++)
            {
                if (Accounts[i].Login == login && login.Length > 3) return false;
            }
            return true;
        }

        public List<string> CustomerCreditHistory(int idUser)
        {
            var listUsers = new List<string>();

            for (int i = 0; i < Accounts.Count; i++)
            {
                if (Accounts[i].Id == idUser)
                {
                    for (int j = 0; j < Accounts[i].DataAccount.Count; j++)
                    {
                        if (Accounts[i].DataAccount[j].BringCredit > 0)
                            listUsers.Add($"Dnia: {Helpers.DateInPolish(Accounts[i].DataAccount[j].Data)} |" +
                                $"Użytkownik: {Accounts[i].LastName}, wziął kredyt na: {Accounts[i].DataAccount[j].BringCredit} zł.");
                        if (Accounts[i].DataAccount[j].PayOffCredit > 0)
                            listUsers.Add($"Dnia: {Helpers.DateInPolish(Accounts[i].DataAccount[j].Data)} |" +
                                $"Użytkownik: {Accounts[i].LastName}, dał na kredyt: {Accounts[i].DataAccount[j].PayOffCredit} zł.");
                        else if (Accounts[i].DataAccount[j].RepaidLoan)
                                    listUsers.Add($"Dnia: {Helpers.DateInPolish(Accounts[i].DataAccount[j].Data)} |" +
                                        $"Użytkownik: {Accounts[i].LastName}, spłacił cały kredyt.");
                    }
                }
            }

            return listUsers;
        }

        public decimal TotalCredit()
        {
            var result = 0M;
            for (int i = 0; i < Accounts.Count; i++)
            {
                result += Accounts[i].CreditBalance;
            }
            return result;
        }

        public decimal AmountOfMoneyInTheBank()
        {
            var result = 0M;
            for (int i = 0; i < Accounts.Count; i++)
            {
                result += Accounts[i].AccountBalance;
            }
            return result;
        }

        public List<string> ListOfDebtors()
        {
            var users = new List<string>();

            for (var i = 1; i < Accounts.Count; i++)
            {
                if (Accounts[i].CreditBalance > 0) users.Add($"{Accounts[i].Id}. | {Accounts[i].FirstName} {Accounts[i].LastName} " +
                                                        $"| Stan konta: {Accounts[i].AccountBalance}zł. " +
                                                        $"| Stan kredytu: {Accounts[i].CreditBalance}zł.");
            }
            return users;
        }

        public bool RemoveAccount(int id)
        {
            var result = false;
            for (int i = 1; i < Accounts.Count; i++)
            {
                if (id == Accounts[i].Id)
                {
                    Accounts.Remove(Accounts[i]);
                    Accounts = Accounts.OrderBy(x => x.Id).ToList();
                    return result = true;
                }
            }
            return result;
        }

        public List<string> AboutUsers()
        {
            var listString = new List<string>();
            listString.Add("ID ; FirstName ; LastName ; Login ; Password ; " +
                "Money ; Credit ; isAccBlock ; isCredit ; isAdmin");

            for (int i = 0; i < Accounts.Count; i++)
            {
                listString.Add(Accounts[i].ToString());
            }

            return listString;
        }

        // ------------------------------------------------------------------------------------------------USERS

        public void PayOnAccount(Account account, decimal payOn)
        {
            var dataAccount = new DataAccount(CheckIdData(), payOn, 0, 0, 0);

            for (int i = 0; i < Accounts.Count; i++)
            {
                if (Accounts[i].Id == account.Id)
                {
                    Accounts[i].AccountBalance += payOn;
                    Accounts[i].DataAccount.Add(dataAccount);
                }
            }
        }

        public (decimal, decimal) PayOnOffInfo(Account account)
        {
            var payOn = 0M;
            var payOff = 0M;
            var ok = false;
            for (int i = 0; i < Accounts.Count; i++)
            {
                if (Accounts[i].Id == account.Id)
                {
                    for (int j = 0; j < Accounts[i].DataAccount.Count; j++)
                    {
                        payOn += Accounts[i].DataAccount[j].PayOnAccount;
                        payOff += Accounts[i].DataAccount[j].PayOffAccount;
                        ok = true;
                    }
                    if (ok) return (payOn, payOff);
                }
            }
            return (payOn, payOff);
        }

        public void PayOffAccount(Account account, decimal payOff)
        {
            var dataAccount = new DataAccount(CheckIdData(), 0, payOff, 0, 0);

            for (int i = 0; i < Accounts.Count; i++)
            {
                if (Accounts[i].Id == account.Id)
                {
                    Accounts[i].AccountBalance -= payOff;
                    Accounts[i].DataAccount.Add(dataAccount);
                }
            }
        }

        /*public void SaveNewAccount(int id, string firstName, string lastName, string login, string password,
            decimal accountBalance, decimal creditBalance, bool accountBlockFlag, bool creditFlag, bool adminFlag)
        {
            var account = new Account(id, firstName, lastName, login, password, accountBalance,
                accountBlockFlag, creditFlag, adminFlag, creditBalance);
        }*/

        public int CheckIdAcc()
        {
            var result = 1;
            for (int i = 1; i < Accounts.Count; i++)
            {
                if (result == Accounts[i].Id) result++;
                else return result;
            }
            return result;
        }

        public int CheckIdData()
        {
            var result = 1;
            for (int i = 1; i < DataAccounts.Count; i++)
            {
                if (result == DataAccounts[i].Id) result++;
                else return result;
            }
            return result;
        }

        public bool BringCredit(Account account, decimal credit)
        {
            for (int i = 0; i < Accounts.Count; i++)
            {
                if (Accounts[i].Id == account.Id)
                {
                    Accounts[i].CreditFlag = true;
                    Accounts[i].CreditBalance = credit;
                    Accounts[i].AccountBalance += credit;
                    Accounts[i].DataAccount.Add(new DataAccount(CheckIdData(), 0, 0, credit, 0));
                    return true;
                }
            }
            return false;
        }

        public bool TransferToOtherAccount(Account account, int userToTransfer, decimal payOn)
        {
            var transferOk = false;
            if (userToTransfer >= Accounts.Count) return false;

            for (int i = 0; i < Accounts.Count; i++)
            {
                if (Accounts[i].Id == userToTransfer)
                {                    
                    Accounts[i].AccountBalance += payOn;
                    transferOk = true;
                }
                if (transferOk)
                {
                    Accounts.FirstOrDefault(x => x.Id == account.Id).AccountBalance -= payOn;
                    return transferOk = true;
                }
            }
            return transferOk;
        }

        public void PayOffCredit(Account account, decimal payOn)
        {
            for (int i = 0; i < Accounts.Count; i++)
            {
                if (Accounts[i].Id == account.Id)
                {
                    Accounts[i].CreditBalance -= payOn;
                    Accounts[i].AccountBalance -= payOn;
                    Accounts[i].DataAccount.Add(new DataAccount(CheckIdData(), 0, 0, 0, payOn));
                    if (Accounts[i].CreditBalance == 0)
                    {
                        Accounts[i].CreditFlag = false;
                        Accounts[i].DataAccount.Add(new DataAccount(CheckIdData(), 0, 0, 0, 0, true));
                    }
                }
                
            }
        }
    }
}
