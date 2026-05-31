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
using QuanNET.GUI;

namespace QuanNET.GUI
{
    /// <summary>
    /// Interaction logic for Login_Regist.xaml
    /// </summary>
    public partial class Login_Regist : Window
    {
        private UserBLL urbll = new UserBLL();
        public Login_Regist()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Password;

            int role = urbll.CheckLogin(username, password);
            if (role == 0)
            {
                Admin.General adminscreen = new Admin.General();
                adminscreen.Show();
                this.Close();
            } 
            else if (role == 1)
            {
                Account currentAcc = urbll.GetAccount(username, password);
                User.General urscreen = new User.General(currentAcc);
                urscreen.Show();
                this.Close();
            } 
            else
            {
                MessageBox.Show("Tài khoản không hợp lệ!");
                return;
            }       
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            Regist regist = new Regist();
            regist.Show();
            this.Close();
        }
    }
}
