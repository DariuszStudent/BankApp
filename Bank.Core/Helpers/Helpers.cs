using System;

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
            return userNumber;
        }

        public static decimal JustDecimal()
        {
            var userNumber = default(decimal);
            while (!decimal.TryParse(Console.ReadLine(), out userNumber) && userNumber > 0)
            {
                Console.WriteLine("Musisz podać liczbę większą od zera!");
            }
            return userNumber;
        }
    }
}
