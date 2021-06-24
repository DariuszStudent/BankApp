using System;

namespace Bank
{
    class Program
    {
        static void Main(string[] args)
        {
            var bankApp = new BankApp();

            var exit = false;

            while (!exit)
            {
                Console.Clear();
                BankView.Introduction();
                var account = bankApp.Login();
                if (account!=null && account.AccountBlockFlag) BankView.BlockedAccount(account.Login);
                else if (account != null) exit = true;
                else BankView.WrongPasswordLogin();

                while (exit)
                {
                    Console.Clear();

                    if (bankApp.CheckAdmin(account))
                    {
                        BankView.MenuAdmin();
                        switch (Console.ReadLine())
                        {
                            case "1":
                                bankApp.ShowAllAccounts();
                                bankApp.BlockAccount();
                                Console.ReadKey();
                                break;
                            case "2":
                                bankApp.AddNewAccount();
                                break;
                            case "3":
                                bankApp.ShowAllAccounts();
                                bankApp.RemoveAccount();
                                Console.ReadKey();
                                break;
                            case "4":
                                bankApp.AmountOfMoneyInTheBank();
                                Console.ReadKey();
                                break;
                            case "5":
                                bankApp.TotalCredit();
                                Console.ReadKey();
                                break;
                            case "6":
                                bankApp.CustomerCreditHistory();
                                Console.ReadKey();
                                break;
                            case "7":
                                bankApp.ListOfDebtors();
                                Console.ReadKey();
                                break;
                            case "8":
                                bankApp.AboutUsers();
                                Console.ReadKey();
                                break;
                            case "9":
                                exit = false;
                                break;
                            default:
                                Console.WriteLine("Wciśnij 1-7 lub 9 żeby wyjść.");
                                Console.WriteLine("Wciśniej dowlny klawisz.");
                                Console.ReadKey();
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Witamy użytkownika: {account.FirstName} {account.LastName}");
                        Console.WriteLine($"Stan Twojego konta to: {account.AccountBalance}");
                        Console.WriteLine();
                        BankView.MenuUser();
                        switch (Console.ReadLine())
                        {
                            case "1":
                                bankApp.PayOnAccount(account);
                                Console.ReadKey();
                                break;
                            case "2":
                                bankApp.PayOffAccount(account);
                                Console.ReadKey();
                                break;
                            case "3":
                                bankApp.PayOnInfo(account);
                                Console.ReadKey();
                                break;
                            case "4":
                                bankApp.BringCredit(account);
                                Console.ReadKey();
                                break;
                            case "5":
                                bankApp.PayOffCredit(account);
                                Console.ReadKey();
                                break;
                            case "6":
                                bankApp.ShowAllAccounts();
                                bankApp.TransferToOtherAccount(account);
                                Console.ReadKey();
                                break;
                            case "7":
                                bankApp.AboutUsers();
                                Console.ReadKey();
                                break;
                            case "9":
                                Console.WriteLine("Wylogowałeś się, dziękujemy za skorzystanie z naszej aplikacji");
                                Console.ReadKey();
                                exit = false;
                                break;
                            default:
                                break;
                        }
                    }
                }

            }
        }
    }
}
