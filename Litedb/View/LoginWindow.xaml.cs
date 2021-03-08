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
        Window homewindow = new HomeWindow();
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            //Window homewindow = new HomeWindow();
            UserVM UserVMd = new UserVM();
            User user = UserVMd.GetUserByEmail(tb_email.Text);
            if (user == null)
            {
                MessageBox.Show("User Email not found");
                return;
            }
            string pw = UserVM.GetMD5Hash(tb_pw.Password);
            if (pw.Equals(user.Password))
            {
                //Window homewindow = new HomeWindow();
                MessageBox.Show("Login Successful");
                this.Close();
                homewindow.Show();
            }
            else
            {
                //password is wrong
                MessageBox.Show("Password incorrect");
                return;
            }
        }
    }
}
