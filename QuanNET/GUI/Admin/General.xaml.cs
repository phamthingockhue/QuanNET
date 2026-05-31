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

namespace QuanNET.GUI.Admin
{
    /// <summary>
    /// Interaction logic for General.xaml
    /// </summary>
    public partial class General : Window
    {
        public General()
        {
            InitializeComponent();
        }

        private void btnCurrentComp_Click(object sender, RoutedEventArgs e)
        {
            CurrrentComputer currrentComputer = new CurrrentComputer();
            MainContentPanel.Content = currrentComputer;
        }

        private void btnManageComp_Click(object sender, RoutedEventArgs e)
        {
            ComputerManager computerManager = new ComputerManager();
            MainContentPanel.Content = computerManager;
        }

        private void btnRevenue_Click(object sender, RoutedEventArgs e)
        {
            Thongke thongke = new Thongke();
            MainContentPanel.Content = thongke;
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            Login_Regist login = new Login_Regist();
            login.Show();
            this.Close();
        }
    }
}
