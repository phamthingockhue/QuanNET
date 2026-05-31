using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using QuanNET.BLL;
using QuanNET.DTO;

namespace QuanNET.GUI.Admin
{
    public partial class Thongke : UserControl
    {
        private RentBLL rent = new RentBLL();
        public Thongke()
        {
            InitializeComponent();
            LoadRent();
        }

        public void LoadRent()
        {
            DateTime st = dpFromDate.SelectedDate ?? DateTime.Now;
            DateTime end = dpToDate.SelectedDate ?? DateTime.Now;

            List<ThongKeDTO> list = rent.GetThongKe(st, end);
            dgRevenueDetails.ItemsSource = list;

            decimal total = list.Sum(x => x.Revenue);
            lblTotalRevenue.Text = string.Format("{0:N0} Đ", total);
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            LoadRent();
        }
    }
}
