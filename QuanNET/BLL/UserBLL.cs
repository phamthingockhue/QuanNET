using QuanNET.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanNET.BLL
{
    public class UserBLL
    {
        private UserDAL userDAL = new UserDAL();
        public int CheckLogin(string accountID, string password)
        {
            if (string.IsNullOrEmpty(accountID) || string.IsNullOrEmpty(password))
            {
                return -1;
            }

            Account acc = userDAL.GetAccount(accountID, password);
            if (acc != null)
            {
                return acc.AccountRole ?? 1;
            }
            return -1;
        }

        public Account GetAccount(string accountID, string password)
        {
            if (string.IsNullOrEmpty(accountID) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            Account acc = userDAL.GetAccount(accountID, password);
            if (acc != null)
            {
                return acc;
            }
            return null;
        }

        public string RegisterNewAccount(string id, string name, string password, string phone)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password))
            {
                return "Vui lòng nhập đầy đủ các trường thông tin bắt buộc!";
            }
            if (userDAL.FindAccountByID(id) != null)
            {
                return "Tên tài khoản này đã tồn tại trên hệ thống!";
            }

            bool isSuccess = userDAL.AddAccount(id, name, password, phone);
            return isSuccess ? "Success" : "Đăng ký thất bại do lỗi hệ thống CSDL!";
        }

        public bool ExecuteDeposit(string id, decimal money, string inputPassword)
        {
            if (money <= 0) return false;
            Account acc = userDAL.FindAccountByID(id);
            if (acc == null || acc.AccountPassword != inputPassword)
            {
                return false; 
            }

            return userDAL.NapThe(id, money);
        }
    }
}
