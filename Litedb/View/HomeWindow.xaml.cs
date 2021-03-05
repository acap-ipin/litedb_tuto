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
using System.Windows.Shapes;
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
                Password = tb_password.Password,
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
            User user = new User
            {
                Name = tb_name.Text,
                Email = tb_email.Text,
                Phone = tb_phone.Text,
                Password = tb_password.Password,
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
    }
}
