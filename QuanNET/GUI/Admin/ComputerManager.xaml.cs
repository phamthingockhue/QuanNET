using System;
using System.Windows;
using System.Windows.Controls;
using QuanNET.BLL; 

namespace QuanNET.GUI.Admin
{
    public partial class ComputerManager : UserControl
    {
        private ComputerBLL compBLL = new ComputerBLL();

        public ComputerManager()
        {
            InitializeComponent();
            LoadDataGrid();
        }

        private void LoadDataGrid()
        {
            try
            {
                dgComputers.ItemsSource = compBLL.GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi không thể tải danh sách máy tính: " + ex.Message, "Thông báo lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void dgComputers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgComputers.SelectedItem is Computer selectedComp)
            {
                txtComputerID.Text = selectedComp.ComputerID;
                txtComputerName.Text = selectedComp.ComputerName;
                cbComputerStatus.Text = selectedComp.ComputerStatus;
                txtComputerMoney.Text = selectedComp.ComputerMoney.ToString();
                txtComputerID.IsReadOnly = true;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (txtComputerID.IsReadOnly)
            {
                ClearForm();
                return;
            }

            if (string.IsNullOrEmpty(txtComputerID.Text.Trim()) || string.IsNullOrEmpty(txtComputerName.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Mã máy và Tên máy tính!", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!decimal.TryParse(txtComputerMoney.Text.Trim(), out decimal price) || price < 0)
            {
                MessageBox.Show("Đơn giá máy tính nhập vào không hợp lệ hoặc bị âm!", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string status = "Trống";

            string result = compBLL.AddNewComputer(txtComputerID.Text.Trim(), txtComputerName.Text.Trim(), status, price);

            if (result == "Success")
            {
                MessageBox.Show("Thêm thiết bị máy tính mới vào phòng máy thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadDataGrid(); 
                ClearForm();
                LoadDataGrid();
            }
            else
            {
                MessageBox.Show(result, "Thất bại", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            string id = txtComputerID.Text.Trim();

            if (string.IsNullOrEmpty(id))
            {
                MessageBox.Show("Vui lòng chọn một máy tính từ danh sách cần sửa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!decimal.TryParse(txtComputerMoney.Text.Trim(), out decimal price) || price < 0)
            {
                MessageBox.Show("Đơn giá máy tính không hợp lệ!", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string status = cbComputerStatus.Text;

            string result = compBLL.UpdateComputerInfo(id, txtComputerName.Text.Trim(), status, price);

            if (result == "Success")
            {
                MessageBox.Show("Cập nhật thông tin thiết bị thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadDataGrid();
            }
            else
            {
                MessageBox.Show(result, "Thất bại", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            string id = txtComputerID.Text.Trim();

            if (string.IsNullOrEmpty(id))
            {
                MessageBox.Show("Vui lòng chọn máy tính trên bảng cần xóa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBoxResult confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa máy tính {id} ra khỏi hệ thống Keiri_NET không?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (confirm == MessageBoxResult.Yes)
            {
                bool isDeleted = compBLL.DeleteComputerByID(id);

                if (isDeleted)
                {
                    MessageBox.Show("Đã xóa thiết bị máy tính thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadDataGrid();
                    ClearForm();
                }
                else
                {
                    MessageBox.Show("Không thể xóa máy tính này! Thiết bị không tồn tại hoặc hiện đang ở trạng thái 'Đang sử dụng'.", "Lỗi nghiệp vụ", MessageBoxButton.OK, MessageBoxImage.Stop);
                }
            }
        }

        private void ClearForm()
        {
            txtComputerID.Text = "";
            txtComputerID.IsReadOnly = false; 
            txtComputerName.Text = "";
            txtComputerMoney.Text = "";
            cbComputerStatus.SelectedIndex = 0; 

            dgComputers.SelectedItem = null; 
        }
    }
}