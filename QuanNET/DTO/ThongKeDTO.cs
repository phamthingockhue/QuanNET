using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanNET.DTO
{
    public class ThongKeDTO
    {
        public string ComputerID { get; set; }
        public string ComputerName { get; set; }
        public int TotalRentCount { get; set; }
        public string TotalHoursDisplay { get; set; }
        public decimal Revenue { get; set; }
    }
}
