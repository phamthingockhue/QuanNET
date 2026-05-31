using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanNET.DAL
{
    internal class UserDAL
    {
        public Account GetAccount(string accountID, string password)
        {
            using (QuanNetDataContext db = new QuanNetDataContext())
            {
                return db.Accounts.SingleOrDefault(c => c.AccountID.Trim() == accountID && c.AccountPassword.Trim() == password);
            }
        }

        public bool AddAccount(string id, string name, string password, string phone)
        {
            using (QuanNetDataContext db = new QuanNetDataContext())
            {
                Account account = new Account();
                account.AccountID = id;
                account.AccountPassword = password;
                account.AccountPhone = phone;
                account.AccountName = name;
                account.AccountBalance = 0;
                account.AccountRole = 1;

                db.Accounts.InsertOnSubmit(account);
                db.SubmitChanges();
                return true;
            }
        }

        public Account FindAccountByID(string id)
        {
            using (QuanNetDataContext db = new QuanNetDataContext())
            {
                return db.Accounts.SingleOrDefault(c => c.AccountID == id);
            }
        }

        public bool NapThe(string id, decimal money)
        {
            using (QuanNetDataContext db = new QuanNetDataContext())
            {
                Account ac = db.Accounts.SingleOrDefault(c => c.AccountID == id);
                if (ac != null)
                {
                    ac.AccountBalance += money;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            }
        }
    }
}