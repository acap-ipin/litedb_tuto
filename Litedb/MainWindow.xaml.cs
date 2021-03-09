using System;
using MahApps.Metro.Controls;

namespace Litedb
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>


    public partial class MainWindow : MetroWindow
    {

        public MainWindow()
        {
            InitializeComponent();
            this.mainframe.Navigate(new Uri("/View/LoginPage.xaml", UriKind.Relative));
        }

        public void NavLoginPage()
        {
            this.Height = 270;
            this.Width = 270;
            
        }

        public void NavHomePage()
        {
            this.Height = 430;
            this.Width = 800;
            this.mainframe.Navigate(new Uri("/View/HomePage.xaml", UriKind.Relative));
        }
    }
}