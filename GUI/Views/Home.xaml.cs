using Database;
using GUI.Models;
using GUI.ViewModels;
using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace GUI.Views
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        private Action _resetInfor;
        private AccountModel account;
        public string DisplayName { get; set; }
        public Action ResetInfor { get => _resetInfor; set => _resetInfor = value; }

        public Home(AccountModel account)
        {
            InitializeComponent();
            LoadData();
            this.account = account;
            UserAuthorization(account);
        }

        private void UserAuthorization(AccountModel account)
        {
            Manager.IsEnabled = account.Auth == "ADMIN" ? true : false;
            DisplayName = account.Displayname;
            txtDisplayName.Text = DisplayName;
        }

        private void LoadData()
        {
            DataContext = new HomeViewModel();
        }

        private int GetBillId(int ID)
        {
            try
            {
                return (int)DataProvider.Instance.ExecuteQuery("SELECT * FROM BILL WHERE TABLEID = @ID AND STATUS = 0", new object[] { ID }).Rows[0]["ID"];
            }
            catch
            {
                return -1;
            }
        }

        private void Income_Click(object sender, RoutedEventArgs e)
        {
            Income income = new Income();
            income.ShowDialog();
        }

        private void Staff_Click(object sender, RoutedEventArgs e)
        {
            Staff staff = new Staff(account);
            staff.ShowDialog();
        }

        private void DetailBill_Click(object sender, RoutedEventArgs e)
        {
            Invoice invoice = new Invoice();
            invoice.ShowDialog();
        }

        private void Table_Click(object sender, RoutedEventArgs e)
        {
            Table table = new Table();
            table.ShowDialog();
        }

        private void Drink_Click(object sender, RoutedEventArgs e)
        {
            Drink drink = new Drink();
            drink.ShowDialog();
        }

        private void Category_Click(object sender, RoutedEventArgs e)
        {
            Category category = new Category();
            category.ShowDialog();
        }

        private void Storage_Click(object sender, RoutedEventArgs e)
        {
            Storage storage = new Storage(account);
            this.Hide();
            storage.ShowDialog();
            this.ShowDialog();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                ResetInfor.Invoke();
                this.Close();
            }
        }

        private void numDiscount_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var numericUpDown = sender as Xceed.Wpf.Toolkit.IntegerUpDown;
                if (numericUpDown != null)
                {
                    // Cập nhật giá trị từ Text hiển thị vào thuộc tính Discount của ViewModel
                    BindingExpression binding = numericUpDown.GetBindingExpression(Xceed.Wpf.Toolkit.IntegerUpDown.ValueProperty);
                    if (binding != null)
                    {
                        binding.UpdateSource();
                    }
                }
            }
        }

        private void btnAddDrink_Click(object sender, RoutedEventArgs e)
        {
            // Lấy số lượng từ NumericUpDown (sử dụng MaterialDesign hoặc Xceed)
            if (numCount.Value == 0)
            {
                MessageBox.Show("Vui lòng nhập số lượng đồ cần thêm/bớt!",
                                "Thông báo",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                return;
            }

            // Lấy bàn hiện đang được chọn từ ViewModel (hoặc từ lbTables.SelectedItem)
            TableModel table = lbTables.SelectedItem as TableModel;
            if (table == null)
            {
                MessageBox.Show("Bạn chưa chọn bàn!",
                                "Thông báo",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                return;
            }

            // Lấy id bill của bàn hiện tại (nếu chưa có bill mở thì trả về -1)
            int billId = GetBillId(table.ID);

            // Lấy đồ uống được chọn từ ListBox hiển thị card đồ uống
            DrinkModel drink = lbDrinks.SelectedItem as DrinkModel;
            if (drink == null)
            {
                MessageBox.Show("Vui lòng chọn đồ uống!",
                                "Thông báo",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                return;
            }

            // Nếu bàn chưa có bill nào chưa thanh toán
            if (billId == -1)
            {
                // Tạo bill mới
                DataProvider.Instance.ExecuteNonQuery("EXECUTE SP_AddBill @ID, @CNT", new object[] { table.ID, numCount.Value });
                // Lấy ID bill mới tạo (theo logic của bạn)
                int newBillId = (int)DataProvider.Instance.ExecuteScalar("SELECT MAX(ID) FROM BILL");
                // Thêm hoặc cập nhật thông tin bill cho đồ uống đã chọn
                DataProvider.Instance.ExecuteNonQuery("SP_AddOrUpdateBillInfor @IDBILL, @IDDRINK, @COUNT", new object[] { newBillId, drink.Id, numCount.Value });

                if (numCount.Value > 0)
                {
                    DataProvider.Instance.ExecuteNonQuery(@"UPDATE TABLES SET STATUS = @STATUS WHERE ID = @ID ", new object[] { "Có khách", table.ID });
                }
            }
            else
            {
                // Nếu đã có bill mở cho bàn đó, cập nhật thông tin bill
                DataProvider.Instance.ExecuteNonQuery("SP_AddOrUpdateBillInfor @IDBILL, @IDDRINK, @COUNT", new object[] { billId, drink.Id, numCount.Value });

                // Kiểm tra lại sau khi cập nhật, nếu bill không còn tồn tại (số lượng = 0) thì cập nhật trạng thái bàn
                int currentBillid = GetBillId(table.ID);

                if (currentBillid == -1)
                {
                    DataProvider.Instance.ExecuteNonQuery(@"UPDATE TABLES SET STATUS = @STATUS WHERE ID = @ID ", new object[] { "Trống", table.ID });
                }
            }
            MessageBox.Show("Thành Công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            LoadData();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btnMerge_Click(object sender, RoutedEventArgs e)
        {
            var table1 = lbTables.SelectedItem as TableModel;
            var table2 = cbTables.SelectedItem as TableModel;


            if (table1 == null || table2 == null)
            {
                MessageBox.Show("Vui lòng chọn bàn!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (table2.Status == "Khách đặt trước")
            {
                MessageBox.Show("Bàn đã được đặt trước!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (GetBillId(table1.ID) == -1 || GetBillId(table2.ID) == -1 || table2.ID == table1.ID)
            {
                MessageBox.Show("Không thể gộp bàn!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            if (MessageBox.Show($"Bạn có chắc muốn gộp {table1.TableName} vào {table2.TableName}", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                DataProvider.Instance.ExecuteNonQuery("EXECUTE SP_MergeTable @IDL, @IDR", new object[] { table1.ID, table2.ID });
                MessageBox.Show("Gộp bàn thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadData();
            }
        }

        private void btnSwitch_Click(object sender, RoutedEventArgs e)
        {
            var table1 = lbTables.SelectedItem as TableModel;
            var table2 = cbTables.SelectedItem as TableModel;

            if (table1 == null || table2 == null)
            {
                MessageBox.Show("Vui lòng chọn bàn!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (table2.Status == "Khách đặt trước")
            {
                MessageBox.Show("Bàn đã được đặt trước!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (GetBillId(table1.ID) == -1 || table2.ID == table1.ID)
            {
                MessageBox.Show("Không thể chuyển bàn!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (MessageBox.Show($"Bạn có chắc muốn chuyển từ {table1.TableName} sang {table2.TableName}", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                DataProvider.Instance.ExecuteNonQuery("EXECUTE SP_SwapTable @IDL, @IDR", new object[] { table1.ID, table2.ID });
                MessageBox.Show("Chuyển bàn thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadData();
            }
        }

        private void btnTableregister_Click(object sender, RoutedEventArgs e)
        {
            var table = cbTables.SelectedItem as TableModel;
            if (table == null)
            {
                MessageBox.Show("Vui lòng chọn bàn!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (GetBillId(table.ID) != -1)
            {
                MessageBox.Show("Không thể đặt bàn đã có khách!\nVui lòng chọn bàn trống", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            DataProvider.Instance.ExecuteNonQuery(@"UPDATE TABLES SET STATUS = @STATUS WHERE ID = @ID ", new object[] { "Khách đặt trước", table.ID });
            LoadData();
        }

        private void btnPay_Click(object sender, RoutedEventArgs e)
        {
            var table = lbTables.SelectedItem as TableModel;
            if (table == null)
            {
                MessageBox.Show("Vui lòng chọn bàn!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var billId = GetBillId(table.ID);
            if (billId != -1)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn thanh toán?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    MessageBox.Show("Thanh toán thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    if (double.TryParse(txtPriceSum.Text.Replace("₫", "").Replace(".", ""), out double totalPrice))
                    {
                        DataProvider.Instance.ExecuteNonQuery("UPDATE BILL SET TimeCheckOut = GETDATE(), Status = 1, Discount = @DISCOUNT, TotalPrice = @TOTALPRICE WHERE TableID = @IDTABLE AND ID = @ID", new object[] { numDiscount.Value, totalPrice, table.ID, billId });
                        DataProvider.Instance.ExecuteNonQuery("UPDATE TABLES SET STATUS = @STATUS WHERE ID = @ID", new object[] { "Trống", table.ID });
                        LoadData();
                    }
                }
            }
            else
            {
                MessageBox.Show("Không có hóa đơn để thanh toán!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PrintInvoice_Click(object sender, RoutedEventArgs e)
        {
            var table = lbTables.SelectedItem as TableModel;
            if (table == null)
            {
                MessageBox.Show("Vui lòng chọn bàn!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if(table.Status == "Trống")
            {
                MessageBox.Show("Bàn không có khách!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int tableId = table.ID;
            int discount = (int)numDiscount.Value;
            if (string.IsNullOrEmpty(txtClient.Text))
            {
                MessageBox.Show("Vui lòng nhập tên khách hàng!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            BillPrint bill = new BillPrint(tableId, discount, txtClient.Text);
            bill.ShowDialog();
        }

        private void LoadNameAfterUpdate(string displayname)
        {
            txtDisplayName.Text = account.Displayname = displayname;
        }

        private void Infor_Click(object sender, RoutedEventArgs e)
        {
            UserInfor user = new UserInfor(account);
            user.UpdateInfo += LoadNameAfterUpdate;
            user.ShowDialog();
        }

        private void ChangePass_Click(object sender, RoutedEventArgs e)
        {
            PasswordChange passwordChange = new PasswordChange(account);
            passwordChange.ShowDialog();
        }
    }

}
