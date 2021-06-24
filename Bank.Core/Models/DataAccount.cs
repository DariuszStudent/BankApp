using System;

namespace Bank.Core
{
    public class DataAccount
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public decimal PayOnAccount { get; set; }
        public decimal PayOffAccount { get; set; }
        public decimal BringCredit { get; set; }
        public decimal PayOffCredit { get; set; }
        public bool RepaidLoan { get; set; }

        /*public override string ToString()
        {
            return $"{Data.Date};Wpłata na konto: {PayOnAccount} zł.;Wypłata z konta: {PayOffAccount} zł.;";
        }*/

        public DataAccount(int id, decimal payOnAccount, decimal payOffAccount, 
            decimal bringCredit, decimal payOffCredit, bool repaidLoan = false)
        {
            Id = id;
            Data = DateTime.Now;
            PayOnAccount = payOnAccount;
            PayOffAccount = payOffAccount;
            BringCredit = bringCredit;
            PayOffCredit = payOffCredit;
            RepaidLoan = repaidLoan;
        }
    }
}
