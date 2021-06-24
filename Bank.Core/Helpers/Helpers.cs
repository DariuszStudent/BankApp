using System;
using System.Globalization;

namespace Bank
{
    public static class Helpers
    {
        public static int JustInts()

        {
            var userNumber = default(int);
            while (!int.TryParse(Console.ReadLine(), out userNumber))
            {
                Console.WriteLine("Musisz podać liczbę większą od 0!");
            }
            if (userNumber < 0) userNumber *= -1;
            return userNumber;
        }

        public static decimal JustDecimal()
        {
            var userNumber = default(decimal);
            while (!decimal.TryParse(Console.ReadLine(), out userNumber) && userNumber > 0)
            {
                Console.WriteLine("Musisz podać liczbę większą od zera!");
            }
            if (userNumber < 0) userNumber *= -1;
            return userNumber;
        }

        public static string DateInPolish(DateTime dateTime)
        {   
            CultureInfo polish = new CultureInfo("pl-PL");
            return dateTime.ToString("d MMMM yyyy", polish);
        }
    }
}
