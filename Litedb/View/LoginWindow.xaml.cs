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
using System.Windows.Navigation;

namespace Litedb.View
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("clicked");
            UserVM UserVMd = new UserVM();
            User user = UserVMd.GetUserByEmail(tb_email.Text);
            if (user == null)
            {
                MessageBox.Show("User Email not found");
            }
            string pw = UserVM.GetMD5Hash(tb_pw.Password);
            if (pw.Equals(user.Password))
            {
                MessageBox.Show("Login Successful");
                Window homewindow = new HomeWindow();
                homewindow.Show();
                this.Close();
            }
        }
    }
}
