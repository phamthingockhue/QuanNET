using System;
using System.Collections.Generic;
using QuanNET.DAL;

namespace QuanNET.BLL
{
    public class ComputerBLL
    {
        private ComputerDAL compDAL = new ComputerDAL();

        public List<Computer> GetAll()
        {
            return compDAL.GetAllComputer();
        }

        public List<Computer> SearchByName(string name)
        {
            if (string.IsNullOrEmpty(name)) return GetAll();
            return compDAL.GetComputersByName(name);
        }

        public string AddNewComputer(string id, string name, string status, decimal money)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(name)) return "Mã và tên máy không được để trống!";
            if (money <= 0) return "Đơn giá máy không hợp lệ!";

            if (compDAL.FindComputerByID(id) != null) return "Mã máy tính này đã tồn tại!";

            bool result = compDAL.AddComputer(id, name, status, money);
            return result ? "Success" : "Thêm máy thất bại!";
        }

        public string UpdateComputerInfo(string id, string newName, string newStatus, decimal newMoney)
        {
            if (string.IsNullOrEmpty(newName)) return "Tên máy không được để trống!";
            if (newMoney < 0) return "Đơn giá máy không thể âm!";

            bool result = compDAL.UpdateComputer(id, newName, newStatus, newMoney);
            return result ? "Success" : "Cập nhật thông tin thất bại!";
        }

        public bool DeleteComputerByID(string id)
        {
            Computer comp = compDAL.FindComputerByID(id);
            if (comp != null && comp.ComputerStatus == "Đang sử dụng")
            {
                return false;
            }
            return compDAL.DeleteComputer(id);
        }
    }
}