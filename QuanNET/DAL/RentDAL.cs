using QuanNET.BLL;
using QuanNET.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanNET.DAL;

namespace QuanNET.DAL
{
    internal class RentDAL
    {
        public bool AddRent(string comID, string urID, DateTime start)
        {
            using (QuanNetDataContext db = new QuanNetDataContext())
            {
                Rent rent = new Rent();
                rent.ComputerID = comID;
                rent.UserID = urID;
                rent.RentTimeStart = start;
                rent.RentTimeEnd = null;
                rent.TotalMoney = 0;

                db.Rents.InsertOnSubmit(rent);
                db.SubmitChanges();
                return true;
            }
        }

        public bool UpdateRent(int rentID, DateTime end, decimal total)
        {
            using (QuanNetDataContext db = new QuanNetDataContext())
            {
                Rent rent = db.Rents.FirstOrDefault(r => r.RentID == rentID);
                if (rent != null)
                {
                    rent.RentTimeEnd = end;
                    rent.TotalMoney = total;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            }
        }

        public Rent FindRentByComID(string id)
        {
            using (QuanNetDataContext db = new QuanNetDataContext())
            {
                return db.Rents.FirstOrDefault(r => r.ComputerID == id && r.RentTimeEnd == null);
            }
        }

        public List<Rent> GetRentHistoryByDate(DateTime fromDate, DateTime toDate)
        {
            using (QuanNetDataContext db = new QuanNetDataContext())
            {
                return db.Rents
                    .Where(r => r.RentTimeStart >= fromDate && r.RentTimeStart <= toDate && r.RentTimeEnd != null)
                    .ToList();
            }
        }

        public Rent GetLastRent(string computerID)
        {
            using (QuanNetDataContext db = new QuanNetDataContext())
            {
                return db.Rents
                         .Where(r => r.ComputerID == computerID)
                         .OrderByDescending(r => r.RentTimeStart)
                         .FirstOrDefault();
            }
        }

        public List<ThongKeDTO> GetRentReport(DateTime start, DateTime end)
        {
            using (QuanNetDataContext db = new QuanNetDataContext())
            {
                List<Rent> rents = db.Rents
                    .Where(r => r.RentTimeStart >= start && r.RentTimeStart <= end && r.RentTimeEnd != null)
                    .ToList();

                ComputerDAL computer = new ComputerDAL();
                List<Computer> computers = computer.GetAllComputer();

                var report = rents.Join(computers,
                                        r => r.ComputerID,
                                        c => c.ComputerID,
                                        (r, c) => new { Rent = r, Computer = c })
                                        .GroupBy(x => x.Computer.ComputerID)
                                        .Select(g =>
                                        {
                                            var first = g.First();
                                            string name = first.Computer.ComputerName;

                                            double totalMinutes = g.Sum(x => (x.Rent.RentTimeEnd.Value - x.Rent.RentTimeStart).TotalMinutes);
                                            int hour = (int)(totalMinutes / 60);
                                            int mins = (int)(totalMinutes % 60);
                                            string duration = hour > 0 ? $"{hour}giờ {mins} phút" : $"{mins} phút";

                                            return new ThongKeDTO
                                            {
                                                ComputerID = g.Key,
                                                ComputerName = name,
                                                TotalRentCount = g.Count(),
                                                TotalHoursDisplay = duration,
                                                Revenue = g.Sum(x => x.Rent.TotalMoney ?? 0)
                                            };
                                        }).ToList();

                return report;
            }
        }
    }
}