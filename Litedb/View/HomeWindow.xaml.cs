using System;
using System.Windows;
using Litedb.Model;
using Litedb.ViewModel;

namespace Litedb.View
{
    /// <summary>
    /// Interaction logic for HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        public HomeWindow()
        {
            InitializeComponent();
            loaddatagrid();
        }

        private void btninsert_Click(object sender, RoutedEventArgs e)
        {
            UserVM UserVMd = new UserVM();
            User user = new User
            {
                Name = tb_name.Text,
                Email = tb_email.Text,
                Phone = tb_phone.Text,
                Password = UserVM.GetMD5Hash(tb_password.Password),
                Admin = tb_admin.Text
            };
            if (UserVMd.InsertUser(user))
            {
                MessageBox.Show("Insert Success");
                loaddatagrid();
            }
            else
            {
                MessageBox.Show("Insert failed");
            }
 
        }

        private void btnupdate_Click(object sender, RoutedEventArgs e)
        {
            UserVM UserVMd = new UserVM();
            int id = 0;
            try
            {
                id  = Int16.Parse(tb_id.Text);
            }
            catch
            {
                MessageBox.Show("User ID is unacceptable");
            }

            User user = new User
            {
                Id = id,
                Name = tb_name.Text,
                Email = tb_email.Text,
                Phone = tb_phone.Text,
                Password = UserVM.GetMD5Hash(tb_password.Password),
                Admin = tb_admin.Text
            };
            if (UserVMd.UpdateUser(user))
            {
                MessageBox.Show("Update Success");
                loaddatagrid();
            }
            else
            {
                MessageBox.Show("Update failed");
            }
        }

        private void btndelete_Click(object sender, RoutedEventArgs e)
        {
            UserVM UserVMd = new UserVM();
            if (UserVMd.DeleteUser(tb_id.Text))
            {
                MessageBox.Show("Delete Success");
                loaddatagrid();
            }
            else
            {
                MessageBox.Show("Delete failed");
            }
        }

        private void loaddatagrid()
        {
            UserVM UserVMd = new UserVM();
            datagrid.ItemsSource = UserVMd.GetUserList();
        }

        private void tb_id_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(tb_id.Text))
            {
                updategrid.Visibility = Visibility.Hidden;
            }
            else
            {
                updategrid.Visibility = Visibility.Visible;
            }
        }
    }
}
