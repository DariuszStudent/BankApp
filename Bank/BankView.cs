using System;

namespace Bank
{
    public static class BankView
    {
        public static void Introduction()
        {
            Console.WriteLine("Witam w programie bankowym, w którym zarządzasz swoim kontem.");
            Console.WriteLine("Aby rozpocząć musisz się zalogować: ");
            Console.WriteLine("\nAdmin: login: admin ; password: 1234");
            Console.WriteLine("User1: login: babcia ; password: 1234");
            Console.WriteLine("User2: login: dziadek ; password: 1234\n");
        }

        public static void WrongPasswordLogin()
        {
            Console.WriteLine("Nie ma takiego użytkownika, spróbuj jeszcze raz.");
            Console.WriteLine("Wciśnij dowolny klawisz.");
            Console.ReadKey();
        }

        public static void MenuAdmin()
        {
            Console.WriteLine("1. Zablokuj konto użytkownika.");
            Console.WriteLine("2. Dodaj konto.");
            Console.WriteLine("3. Usuń konto.");
            Console.WriteLine("4. Podsumowanie: Ile w sumie jest pieniędzy w banku.");
            Console.WriteLine("5. Podsumowanie: Ile wynosi łączny kredyt.");
            Console.WriteLine("6. Otwórz historę kredytową klienta.");
            Console.WriteLine("7. Pobierz listę wszystkich dłużników.");
            Console.WriteLine("8. AboutUsersForDebugs");
            Console.WriteLine("9. Wyloguj się.");
        }

        public static void MenuUser()
        {
            Console.WriteLine("1. Wpłać pieniądze na swoje konto.");
            Console.WriteLine("2. Wypłać pieniądze ze swojego konta.");
            Console.WriteLine("3. Wygeneruj podsumowanie ile wpłaciłeś i wypłaciłeś od początku.");
            Console.WriteLine("4. Weź kredyt, jeśli nie masz jeszcze kredytu.");
            Console.WriteLine("5. Spłać kredyt, przelew z twojego konta.");
            Console.WriteLine("6. Zrób przelew na inne konto.");
            Console.WriteLine("7. AboutUsersForDebugs");
            Console.WriteLine("9. Wyloguj.");
        }

        public static void BlockedAccount(string login)
        {
            Console.WriteLine("Konto o loginie: " + login +
                    ". zostało zablokowane. Proszę skontaktować się z bankiem.");
            Console.ReadKey();
        }
    }
}
