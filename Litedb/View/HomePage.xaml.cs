using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Data;
using Litedb.Model;
using Litedb.ViewModel;
using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;

namespace Litedb.View
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : System.Windows.Controls.Page
    {
        private bool emailvalid = true;
        public HomePage()
        {
           
            InitializeComponent();
            loaddatagrid();
        }

        private void btninsert_Click(object sender, RoutedEventArgs e)
        {
            if (!Formvalid())
            {
                return;
            }
            if (string.IsNullOrEmpty(tb_password.Password))
            {
                MessageBox.Show("Please fill in password");
                return;
            }
            UserVM UserVMd = new UserVM();
            User user = new User
            {
                Name = tb_name.Text,
                Email = tb_email.Text,
                Phone = tb_phone.Text,
                Password = UserVM.GetMD5Hash(tb_password.Password),
                Admin = tb_admin.Text
            };
            int ralat = UserVMd.InsertUser(user);
            switch (ralat)
            {
                case 0:
                    MessageBox.Show("Insert Success");
                    loaddatagrid();
                    break;
                case 1:
                    MessageBox.Show("Insert failed. Refer logs");
                    break;
                case 2: MessageBox.Show("Insert failed. Email has already been registered");
                    break;
            }
 
        }

        private void btnupdate_Click(object sender, RoutedEventArgs e)
        {
            if (!Formvalid())
            {
                return;
            }
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
                loaddatagrid(); //refreshing datagrid causes the binded textbox to refresh
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
            tb_password.Password = "";
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
            tb_password.Password = "";
        }

        private void btnshow_Click(object sender, RoutedEventArgs e)
        {
            tb_password.Visibility = Visibility.Collapsed;
            tb_pw_show.Visibility = Visibility.Visible;
        }

        private void btnshow_up(object sender, RoutedEventArgs e)
        {
            tb_password.Visibility = Visibility.Visible;
            tb_pw_show.Visibility = Visibility.Collapsed;
        }

        private void tb_password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            tb_pw_show.Text = tb_password.Password;
        }
        private void tb_phone_LostFocus(object sender, RoutedEventArgs e)
        {
            string phonestr = tb_phone.Text;
            phonestr = Regex.Replace(phonestr, "[^0-9]", "");
            if (!phonestr.Equals(tb_phone.Text))
            {
                MessageBox.Show("Only use numbers in the phone field", "Fail", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void tb_email_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((!string.IsNullOrEmpty(tb_email.Text)) && (!Regex.IsMatch(tb_email.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$")))
            {
                MessageBox.Show("Please enter a valid email format", "Fail", MessageBoxButton.OK, MessageBoxImage.Error);
                emailvalid = false;
            }
            else
            {
                emailvalid = true;
            }
        }

        private bool Formvalid()
        {
            if (string.IsNullOrEmpty(tb_name.Text))
            {
                MessageBox.Show("Please enter name", "Fail", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (string.IsNullOrEmpty(tb_email.Text))
            {
                MessageBox.Show("Please enter email", "Fail", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (string.IsNullOrEmpty(tb_phone.Text))
            {
                MessageBox.Show("Please enter phone number", "Fail", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (string.IsNullOrEmpty(tb_admin.Text))
            {
                MessageBox.Show("Please choose a role", "Fail", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (!emailvalid)
            {
                MessageBox.Show("Please use correct format for email", "Fail", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else
            {
                return true;
            }
        }

        private void btnexcel_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog
            {
                DefaultExt = ".xlsx",
                Filter = "(.xlsx)|*.xlsx"
            };
            //openfile.ShowDialog();

            var browsefile = openfile.ShowDialog();

            if (browsefile == true)
            {
                string txtFilePath = openfile.FileName;
                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                Workbook excelBook = excelApp.Workbooks.Open(txtFilePath, 0, true, 5, "", "", true, XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                Worksheet excelSheet = (Worksheet)excelBook.Worksheets.get_Item(1); ;
                Range excelRange = excelSheet.UsedRange;

                int rowCnt = 0;
                int colCnt = 0;
                string th = ""; //table header
                int thname = 0;
                int themail = 0;
                int thphone = 0;
                int thpassword = 99;
                int throle = 0;

                for (colCnt = 1; colCnt <= excelRange.Columns.Count; colCnt++)
                {
                    th = (string)(excelRange.Cells[1, colCnt] as Range).Value2;
                    if (string.IsNullOrEmpty(th))
                    {
                        th = "a";
                    }
                    if (th.Equals("name", StringComparison.InvariantCultureIgnoreCase))
                    {
                        thname = colCnt;
                    }
                    else if (th.Equals("email", StringComparison.InvariantCultureIgnoreCase))
                    {
                        themail = colCnt;
                    }
                    else if (th.Equals("phone", StringComparison.InvariantCultureIgnoreCase))
                    {
                        thphone = colCnt;
                    }
                    else if (th.Equals("password", StringComparison.InvariantCultureIgnoreCase))
                    {
                        thpassword = colCnt;
                    }
                    else if (th.Equals("role", StringComparison.InvariantCultureIgnoreCase))
                    {
                        throle = colCnt;
                    }
                }

                for (rowCnt = 2; rowCnt <= excelRange.Rows.Count; rowCnt++)
                {
                    string cellname = Convert.ToString((excelRange.Cells[rowCnt, thname] as Range).Value2);
                    string cellemail = Convert.ToString((excelRange.Cells[rowCnt, themail] as Range).Value2);
                    string cellphone = Convert.ToString((excelRange.Cells[rowCnt, thphone] as Range).Value2);
                    if (cellphone[0] != '0')
                    {
                        cellphone = "0" + cellphone;
                    }
                    string cellpassword = "password";
                    if (thpassword != 99)
                    {
                        cellpassword = Convert.ToString((excelRange.Cells[rowCnt, thpassword] as Range).Value2);
                    }
                    string cellrole = Convert.ToString((excelRange.Cells[rowCnt, throle] as Range).Value2);

                    User user = new User
                    {
                        Name = cellname,
                        Email = cellemail,
                        Phone = cellphone,
                        Password = cellpassword,
                        Admin = cellrole,
                    };
                    UserVM UserVMd = new UserVM();
                    int masuk = UserVMd.InsertUser(user);
                    if(masuk == 2)
                    {
                        MessageBox.Show("Data row:" + rowCnt + ", email: " + cellemail + " is skipped. Email already exists", "Fail", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                MessageBox.Show("Data import successful", "Succesful", MessageBoxButton.OK, MessageBoxImage.Information);
                loaddatagrid();
            }
            else
            {
                MessageBox.Show("Import excel: Browsefile cancelled", "Fail", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }

    public class AdminboolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            switch (value.ToString().ToLower())
            {
                case "admin":
                    return true;
                case "user":
                    return false;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return false;
        }
    }

    public class UserboolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            switch (value.ToString().ToLower())
            {
                case "user":
                    return true;
                case "admin":
                    return false;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return false;
        }
    }

    public class BooladminConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool)value == true)
                {
                    return "admin";
                }
                else
                {
                    return "user";
                }
            }
            return "user";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return false;
        }
    }
}
