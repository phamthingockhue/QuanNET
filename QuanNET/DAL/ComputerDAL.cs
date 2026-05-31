using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanNET.DAL
{
    internal class ComputerDAL
    {
        public List<Computer> GetAllComputer()
        {
            using (QuanNetDataContext db = new QuanNetDataContext())
            {
                return db.Computers.ToList();
            }
        }

        public List<Computer> GetComputersByName(string name)
        {
            using (QuanNetDataContext db = new QuanNetDataContext())
            {
                return db.Computers.Where(c => c.ComputerName.ToLower().Contains(name.ToLower())).ToList();
            }
        }

        public Computer FindComputerByID(string id)
        {
            using (QuanNetDataContext db = new QuanNetDataContext())
            {
                return db.Computers.SingleOrDefault(c => c.ComputerID == id);
            }
        }

        public bool AddComputer(string id, string name, string status, decimal money)
        {
            using (QuanNetDataContext db = new QuanNetDataContext())
            {
                Computer computer = new Computer();
                computer.ComputerID = id;
                computer.ComputerName = name;
                computer.ComputerStatus = status;
                computer.ComputerMoney = money;

                db.Computers.InsertOnSubmit(computer);
                db.SubmitChanges();
                return true;
            }
        }

        public bool DeleteComputer(string id)
        {
            using (QuanNetDataContext db = new QuanNetDataContext())
            {
                Computer computer = db.Computers.SingleOrDefault(c => c.ComputerID == id);
                if (computer != null)
                {
                    db.Computers.DeleteOnSubmit(computer);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            }
        }

        public bool UpdateComputer(string id, string newName, string newStatus, decimal money)
        {
            using (QuanNetDataContext db = new QuanNetDataContext())
            {
                Computer computer = db.Computers.SingleOrDefault(c => c.ComputerID == id);
                if (computer != null)
                {
                    computer.ComputerName = newName;
                    computer.ComputerStatus = newStatus;
                    computer.ComputerMoney = money;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            }
        }
    }
}