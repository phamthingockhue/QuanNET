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

namespace QuanNET.GUI.User
{
    /// <summary>
    /// Interaction logic for Naptien.xaml
    /// </summary>
    public partial class Naptien : UserControl
    {
        UserBLL userBLL= new UserBLL();
        Account AccUser;
        public Naptien(Account user)
        {
            InitializeComponent();
            this.AccUser = user;
            UpdateBalance();
        }

        private void btnConfirmDeposit_Click(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(txtMoneyAmount.Text.Trim(), out decimal money) && money > 0)
            {
                string pass = txtVerifyPassword.Password;
                bool result = userBLL.ExecuteDeposit(AccUser.AccountID, money, pass);
                if (result)
                {
                    MessageBox.Show("Nạp thẻ thành công");
                    UpdateBalance();
                } 
                else
                {
                    MessageBox.Show("Lỗi nạp thẻ");
                }         
            }
        }

        public void UpdateBalance()
        {
            lblCurrentBalance.Text = string.Format("{0:0.00}", AccUser.AccountBalance);
        }
    }
}
