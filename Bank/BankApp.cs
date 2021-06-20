using Bank.Core;
using System;

namespace Bank
{
    public class BankApp
    {
        BankService bankService = new BankService();

        public Account Login()
        {
            Console.WriteLine("Podaj login: ");
            var login = Console.ReadLine();
            Console.WriteLine("Podaj hasło: ");
            var password = Console.ReadLine();

            return bankService.Login(login, password);
        }

        public bool CheckAdmin(Account account)
        {
            return bankService.CheckAdmin(account);
        }

        public void ShowAllAccounts()
        {
            var accounts = bankService.ShowAccounts();

            foreach (var item in accounts)
            {
                Console.WriteLine(item);
            }
        }

        public void BlockAccount()
        {
            Console.WriteLine("Wybierz użytkownika, którego chcesz zablokować/odblokować.");
            Console.WriteLine("Wpisz 0 aby zakończyć proces.");
            var id = Helpers.JustInts();
            if (id == 0) return;
            var result = bankService.BlockAccount(id);
            if (result) Console.WriteLine("Użytkownik o ID: " + id + 
                ". został zablokowany.");
            else Console.WriteLine("Operacja nie udana.");
            Console.ReadKey();
        }
        
        public void AddNewAccount()
        {
            var id = bankService.CheckIdAcc();
            Console.WriteLine("Twoje Id: " + id);

            Console.WriteLine("Podaj imię: ");
            var firstName = Console.ReadLine();

            Console.WriteLine("Podaj Nazwisko: ");
            var lastName = Console.ReadLine();

            Console.WriteLine("Podaj Login: ");
            var login = Console.ReadLine();
            var isLogin = bankService.CheckLogin(login);
            while (!isLogin)
            {
                Console.WriteLine("Login musi być unikalny.");
                Console.WriteLine("Podaj jeszcze raz:");
                login = Console.ReadLine();
                isLogin = bankService.CheckLogin(login);
            }

            Console.WriteLine("Podaj Hasło: ");
            var password = Console.ReadLine();

            Console.WriteLine("Wpłata na konto: ");
            var accountBalance = Helpers.JustDecimal();

            bankService.AddNewAccount(id, firstName, lastName, login, password, accountBalance);
        }

        public void AboutUsers()
        {
            var listString = bankService.AboutUsers();
            foreach (var userInfo in listString)
            {
                Console.WriteLine(userInfo);
            }
        }

        public void RemoveAccount()
        {
            Console.WriteLine("Podaj id użytkownika którego chcesz usunąć: ");
            Console.WriteLine("Wpisz 0 aby zakończyć proces.");
            var id = Helpers.JustInts();
            if (id == 0) return;
            var result = bankService.RemoveAccount(id);
            if (result)
            {
                Console.WriteLine("Udało się usunąć użytkownika.");
            }
            else Console.WriteLine("Nie udało się usunąć użytkownika.");
            Console.ReadKey();
        }

        public void PayOnInfo(Account account)
        {
            var payOnOff = bankService.PayOnOffInfo(account);
            Console.WriteLine("Razem wpłaciłeś na konto: " + payOnOff.Item1 + "zł.");
            Console.WriteLine("Razem wypłaciłeś z konta: " + payOnOff.Item2 + "zł.");
        }

        public void PayOffAccount(Account account)
        {
            Console.WriteLine("Stan Twojego konta: " + account.AccountBalance + "zł.");
            Console.WriteLine("Jaką kwotę chcesz wypłacić ze swojego konta?");
            var payOff = Helpers.JustDecimal();
            if (payOff > account.AccountBalance)
            {
                Console.WriteLine("Nie możesz tyle wypłacić. Może czas na kredyt?");
                return;
            }
            bankService.PayOffAccount(account, payOff);
            Console.WriteLine("Stan Twojego konta po operacji: " + account.AccountBalance + "zł.");
        }

        public void TransferToOtherAccount(Account account)
        {
            Console.WriteLine("Stan Twojego konta: " + account.AccountBalance + "zł.");
            Console.WriteLine("Wybierz konto na które chcesz przelać pieniądze, wprowadź Id: ");
            Console.WriteLine("Wpisz 0 jeśli chcesz zakończyć proces.");
            var userToTransfer = Helpers.JustInts();
            if (userToTransfer == 0) return;
            Console.WriteLine("Podaj kwotę którą chcesz przelać: ");
            var result = Helpers.JustDecimal();
            if (result > account.AccountBalance)
            {
                Console.WriteLine("Kwota przekracza środki na Twoim koncie.");
                return;
            }
            if (bankService.TransferToOtherAccount(account, userToTransfer, result)) 
                Console.WriteLine("Transfer zakończony sukcesem.");
            else Console.WriteLine("Przykro mi, coś poszło nie tak.");

        }

        public void PayOffCredit(Account account)
        {
            if (!account.CreditFlag)
            {
                Console.WriteLine("Nie posiadasz kredytu.");
                return;
            }
            Console.WriteLine("Środki na koncie: " + account.AccountBalance + "zł.");
            Console.WriteLine("Wartość kredytu: " + account.CreditBalance + "zł.");
            Console.WriteLine("Wpisz jaką sumę chcesz przeznaczyć na spłatę: ");
            var result = Helpers.JustDecimal();
            if (result > account.AccountBalance) Console.WriteLine("Kwota przekracza środki na Twoim koncie.");
            if (result > account.CreditBalance) Console.WriteLine("Kwota jest za duża.");
            else bankService.PayOffCredit(account, result);
            if (account.CreditBalance > 0) Console.WriteLine("Zostało do spłaty: " + account.CreditBalance + "zł.");
            else Console.WriteLine("Kredyt został spłacony, dziękujemy.");
        }

        public void BringCredit(Account account)
        {
            if (account.CreditFlag)
            {
                Console.WriteLine("Przykro nam, już masz kredyt na swoim koncie.");
                Console.WriteLine("Na kwotę: " + account.CreditBalance + "zł.");
                return;
            }
            Console.WriteLine("Na jaką kwotę chcesz wziąć kredyt?");
            Console.WriteLine("Jeśli chcesz zakończyć proces, wpisz 0");
            var credit = Helpers.JustDecimal();
            if (credit == 0) return;

            var result = bankService.BringCredit(account, credit);

            if (result) Console.WriteLine("Kredyt został przyznany na kwotę: " + credit + "zł.");
            else Console.WriteLine("Przykro nam, kredyt nie został przyznany.");
        }

        public void PayOnAccount(Account account)
        {
            Console.WriteLine("Stan Twojego konta: " + account.AccountBalance + "zł.");
            Console.WriteLine("Jaką kwotę chcesz wpłacić na swoje konto?");
            var payOn = Helpers.JustDecimal();
            bankService.PayOnAccount(account, payOn);
            Console.WriteLine("Stan Twojego konta po operacji: " + account.AccountBalance + "zł.");
        }
    }
}
