using Litedb.View;
using Litedb.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Litedb
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            LoginWindow window = new LoginWindow();
            //HomeWindow window = new HomeWindow();
            UserVM VM = new UserVM();
            window.DataContext = VM;
            window.Show();
        }
    }
}
