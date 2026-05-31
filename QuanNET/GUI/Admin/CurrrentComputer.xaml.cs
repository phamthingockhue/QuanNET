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

namespace QuanNET.GUI.Admin
{
    /// <summary>
    /// Interaction logic for CurrrentComputer.xaml
    /// </summary>
    public partial class CurrrentComputer : UserControl
    {
        private ComputerBLL computerBLL = new ComputerBLL();
        private List<Computer> allComputersList = new List<Computer>();
        public CurrrentComputer()
        {
            InitializeComponent();
            LoadComputer();
        }

        private void btnClearFilter_Click(object sender, RoutedEventArgs e)
        {
            cbSearchComputer.SelectedIndex = 0;
            LoadComputer();
        }

        private void LoadComputer()
        {
            try
            {
                allComputersList = computerBLL.GetAll();
                dgComputerList.ItemsSource = allComputersList;

                List<string> searchSource = new List<string>();
                searchSource.Add("Tất cả");
                searchSource.AddRange(allComputersList.Select(c => c.ComputerName));

                cbSearchComputer.ItemsSource = searchSource;
                cbSearchComputer.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load dữ liệu máy: " + ex.Message);
            }
        }

        private void cbSearchComputer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbSearchComputer.SelectedItem != null)
            {
                string selectedValue = cbSearchComputer.SelectedItem.ToString();
                if (selectedValue == "Tất cả")
                {
                    dgComputerList.ItemsSource = computerBLL.GetAll();
                }
                else
                {
                    var filtered = computerBLL.SearchByName(selectedValue);
                    dgComputerList.ItemsSource = filtered;
                }
            }
        }

        private void cbSearchComputer_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filterText = cbSearchComputer.Text.Trim().ToLower();

            if (allComputersList == null || allComputersList.Count == 0) return;

            if (string.IsNullOrEmpty(filterText) || filterText == "tất cả" || filterText == "nhập tên máy để tìm...")
            {
                dgComputerList.ItemsSource = allComputersList;
                return;
            }

            var filteredResult = allComputersList
                .Where(c => c.ComputerName != null && c.ComputerName.ToLower().Contains(filterText))
                .ToList();

            dgComputerList.ItemsSource = filteredResult;
        }
    }
}
