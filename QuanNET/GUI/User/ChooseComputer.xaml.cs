using System;
using System.Windows;
using System.Windows.Controls;
using QuanNET.BLL;
using QuanNET.DAL;

namespace QuanNET.GUI.User
{
    public partial class ChooseComputer : UserControl
    {
        private RentBLL rentBLL = new RentBLL();
        private ComputerBLL computer = new ComputerBLL();
        private UserBLL UserBLL = new UserBLL();
        Account AccUser;

        public ChooseComputer(Account user)
        {
            InitializeComponent();
            LoadComputer();
            this.AccUser = user;
        }

        private void LoadComputer()
        {
            dgChooseComp.ItemsSource = null;
            dgChooseComp.ItemsSource = computer.GetAll();
        }

        private void btnStartPlay_Click(object sender, RoutedEventArgs e)
        {
            if (dgChooseComp.SelectedItem is Computer comp)
            {
                if (comp.ComputerStatus != "Trống") return;

                bool success = rentBLL.StartSession(comp.ComputerID, AccUser.AccountID);
                if (success)
                {
                    MessageBox.Show($"Kích hoạt thành công {comp.ComputerName}!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    btnStartPlay.Visibility = Visibility.Collapsed;
                    btnStopPlay.Visibility = Visibility.Visible;
                    LoadComputer();
                }
                else
                {
                    MessageBox.Show("Kích hoạt thất bại! Máy không sẵn sàng hoặc tài khoản hết tiền.");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một máy tính trên bảng!");
            }
        }

        private void btnStopPlay_Click(object sender, RoutedEventArgs e)
        {
            if (dgChooseComp.SelectedItem is Computer comp)
            {
                bool result = rentBLL.EndSession(comp.ComputerID);
                if (result)
                {
                    Rent lastRent = rentBLL.getLastRent(comp.ComputerID);

                    if (lastRent != null)
                    {
                        DateTime startTime = lastRent.RentTimeStart;
                        DateTime endTime = lastRent.RentTimeEnd ?? DateTime.Now;

                        TimeSpan duration = endTime - startTime;
                        double minutes = Math.Round(duration.TotalMinutes, 0);
                        if (minutes < 1) minutes = 1;

                        MessageBox.Show($"Đã tắt máy thành công!\nSố phút chơi: {minutes} phút.\nTổng tiền thanh toán: {string.Format("{0:N0}", lastRent.TotalMoney)} đ.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    btnStartPlay.Visibility = Visibility.Visible;
                    btnStopPlay.Visibility = Visibility.Collapsed;
                    LoadComputer();
                }
                else
                {
                    MessageBox.Show("Lỗi hệ thống không thể ngừng phiên chơi!", "Thất bại", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn đúng máy tính bạn đang chơi trên bảng để tắt máy!");
            }
        }

        private void dgChooseComp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgChooseComp.SelectedItem is Computer comp)
            {
                if (comp.ComputerStatus == "Đang sử dụng")
                {
                    Rent currenRent = rentBLL.getLastRent(comp.ComputerID);
                    if (currenRent != null && currenRent.UserID == AccUser.AccountID)
                    {
                        btnStartPlay.Visibility = Visibility.Collapsed;
                        btnStopPlay.Visibility = Visibility.Visible;
                        btnStartPlay.IsEnabled = true;
                    }
                    else
                    {
                        btnStartPlay.Visibility = Visibility.Visible;
                        btnStopPlay.Visibility = Visibility.Collapsed;
                        btnStartPlay.IsEnabled = false;
                    }
                }
                else
                {
                    btnStopPlay.Visibility = Visibility.Collapsed;
                    btnStartPlay.Visibility = Visibility.Visible;
                    if (comp.ComputerStatus == "Bảo trì")
                    {
                        btnStartPlay.IsEnabled = false;
                    }
                    else
                    {
                        btnStartPlay.IsEnabled = true;
                    }
                }
            }
        }
    }
}