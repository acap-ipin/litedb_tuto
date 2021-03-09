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
using MahApps.Metro.Controls;

namespace Litedb.View
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            MainWindow window = (MainWindow)Application.Current.MainWindow;
            window.Height = 270;
            window.Width = 270;
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
            if (!user.Admin.Equals("admin")){
                MessageBox.Show("Only admin login is allowed");
                return;
            }
            string pw = UserVM.GetMD5Hash(tb_pw.Password);
            if (pw.Equals(user.Password))
            {
                MessageBox.Show("Login Successful");
                Uri uri = new Uri("/View/HomePage.xaml", UriKind.Relative);
                this.NavigationService.Navigate(uri);
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
