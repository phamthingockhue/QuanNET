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

namespace QuanNET.GUI.User
{
    /// <summary>
    /// Interaction logic for General.xaml
    /// </summary>
    public partial class General : Window
    {
        Account AccUser;
        public General(Account user)
        {
            InitializeComponent();
            this.AccUser = user;
        }
        private void btnChooseComp_Click(object sender, RoutedEventArgs e)
        {
            ChooseComputer chooseComputer = new ChooseComputer(AccUser);
            UserMainContentPanel.Content = chooseComputer;
        }

        private void btnDepositMoney_Click(object sender, RoutedEventArgs e)
        {
            Naptien naptien = new Naptien(AccUser);
            UserMainContentPanel.Content = naptien;
        }

        private void btnUserLogout_Click(object sender, RoutedEventArgs e)
        {
            Login_Regist login = new Login_Regist();
            login.Show();
            this.Close();
        }
    }
}
