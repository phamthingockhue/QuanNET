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

namespace QuanNET.GUI
{
    /// <summary>
    /// Interaction logic for Regist.xaml
    /// </summary>
    public partial class Regist : Window
    {
        private UserBLL user = new UserBLL();
        public Regist()
        {
            InitializeComponent();
        }

        private void btnConfirmRegister_Click(object sender, RoutedEventArgs e)
        {
            string name = txtFullName.Text;
            string phone = txtPhone.Text;
            string id = txtRegUsername.Text;
            string pass = txtRegPassword.Password;

            string result = user.RegisterNewAccount(id, name, pass, phone);
            MessageBox.Show(result);
        }

        private void btnBackToLogin_Click(object sender, RoutedEventArgs e)
        {
            Login_Regist login = new Login_Regist();
            login.Show();
            this.Close();
        }
    }
}
