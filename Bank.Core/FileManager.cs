using System.Collections.Generic;
using System.IO;

namespace Bank.Core
{
    public class FileManager
    {
        private string _fileNameAccounts = @"../../../accounts.txt";

        public void AccountsToCTor()
        {

            if (!File.Exists(_fileNameAccounts)) return;

            var fileLines = File.ReadAllLines(_fileNameAccounts);
            foreach (var line in fileLines)
            {
                var lineItems = line.Split(';');
                if (int.TryParse(lineItems[0], out var id) && int.TryParse(lineItems[5], out var accountBalance) &&
                    bool.TryParse(lineItems[6], out var adminBlock) && bool.TryParse(lineItems[7], out var creditFlag) &&
                    bool.TryParse(lineItems[8], out var adminFlag) && decimal.TryParse(lineItems[9], out var creditBalance))
                {
                   // AddNewAccount(id, lineItems[1], lineItems[2], lineItems[3], lineItems[4], accountBalance,
                    //    adminBlock, creditFlag, adminFlag, creditBalance);
                }
            }
        }

        //{1;da;da;da;da;1000;0;False;False;False}
        public void AddNewAccount(Account account)
        {
            File.AppendAllLines(_fileNameAccounts, new List<string> { account.ToString() });
        }
    }
}
