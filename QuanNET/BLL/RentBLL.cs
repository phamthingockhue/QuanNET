using System;
using System.Collections.Generic;
using QuanNET.DAL;
using QuanNET.DTO;

namespace QuanNET.BLL
{
    public class RentBLL
    {
        private RentDAL rentDAL = new RentDAL();
        private ComputerDAL compDAL = new ComputerDAL();
        private UserDAL userDAL = new UserDAL();

        public bool StartSession(string computerID, string userID)
        {
            Computer comp = compDAL.FindComputerByID(computerID);
            Account acc = userDAL.FindAccountByID(userID);

            if (comp == null || acc == null || comp.ComputerStatus != "Trống")
                return false;

            if (acc.AccountBalance <= 0)
                return false;

            bool isRentAdded = rentDAL.AddRent(computerID, userID, DateTime.Now);

            if (isRentAdded)
            {
                compDAL.UpdateComputer(comp.ComputerID, comp.ComputerName, "Đang sử dụng", comp.ComputerMoney);
                return true;
            }
            return false;
        }
        public bool EndSession(string computerID)
        {
            Rent activeRent = rentDAL.FindRentByComID(computerID);
            if (activeRent == null)
            {
                System.Windows.MessageBox.Show("Lỗi: Không tìm thấy phiên chơi nào đang chạy (RentTimeEnd == null) của máy này!");
                return false;
            }    

            Computer comp = compDAL.FindComputerByID(computerID);
            Account acc = userDAL.FindAccountByID(activeRent.UserID);
            if (comp == null || acc == null)
            {
                System.Windows.MessageBox.Show($"Lỗi: Phiên chơi lưu UserID là '{activeRent.UserID}' nhưng bảng Account không có tài khoản này!");
                return false;
            }    

            DateTime endTime = DateTime.Now;

            DateTime startTime = activeRent.RentTimeStart;

            TimeSpan duration = endTime - startTime;
            double totalMinutes = duration.TotalMinutes;

            if (totalMinutes < 1) totalMinutes = 1;
            decimal pricePerHour = comp.ComputerMoney;
            decimal moneyToPay = (decimal)(totalMinutes / 60.0) * pricePerHour;

            moneyToPay = Math.Round(moneyToPay, 0);
            userDAL.NapThe(acc.AccountID, -moneyToPay);
            rentDAL.UpdateRent(activeRent.RentID, endTime, moneyToPay);
            compDAL.UpdateComputer(comp.ComputerID, comp.ComputerName, "Trống", pricePerHour);

            return true;
        }

        public List<Rent> GetRentList(DateTime start, DateTime end)
        {
            return rentDAL.GetRentHistoryByDate(start, end);
        }

        public decimal SumTotal(List<Rent> list)
        {
            decimal total = 0;
            foreach (Rent rent in list)
            {
                total += rent.TotalMoney.Value;
            } 
            return total;
        }

        public Rent getLastRent(string ComputerID)
        {
            return rentDAL.GetLastRent(ComputerID);
        }

        public List<ThongKeDTO> GetThongKe(DateTime start, DateTime end)
        {
            return rentDAL.GetRentReport(start, end);
        }
    }
}