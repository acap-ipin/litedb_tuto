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
using System.Configuration;
using LiteDB;
using System.Data;

namespace Litedb
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    
    public partial class MainWindow : Window
    {
        public static string strcon = ConfigurationManager.AppSettings["connstring"];

        public MainWindow()
        {
            InitializeComponent();

            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Phone");
            dt.Columns.Add("IsActive");

            List<Customer> custs = new List<Customer>();
            custs = (List<Customer>)GetAll();
            foreach (Customer c in custs)
            {
                DataRow row = dt.NewRow();
                row["ID"] = c.Id;
                row["Name"] = c.Name;
                row["Phone"] = c.Phone;
                row["IsActive"] = c.IsActive;
                dt.Rows.Add(row);
            }
       
            datagrid.DataContext = dt;
            //custlist.ItemsSource = dt.
            Customer c1 = GetCustomerById(1);
            textblock.Text = c1.toString();
        }

        // Create your POCO class entity
        public class Customer
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public bool IsActive { get; set; }

            public string toString()
            {
                string ans = Convert.ToString(this.Id) + " " + this.Name + " " + this.Phone + " " + this.IsActive;
                return ans;
            }
        }

        public IList<Customer> GetAll()
        {
            var customersToReturn = new List<Customer>();
            using (var db = new LiteDatabase(strcon))
            {
                var customerdb = db.GetCollection<Customer>("customers");
                var results = customerdb.FindAll();
                foreach (Customer cust in results)
                {
                    customersToReturn.Add(cust);
                }
                return customersToReturn;
            }
        }

        public bool InsertCustomer(string name, string phone, bool isActive)
        {
            using (var db = new LiteDatabase(strcon))
            {
                var customerdb = db.GetCollection<Customer>("customers");
                Customer customer = new Customer
                {
                    Name = name,
                    Phone = phone,
                    IsActive = isActive
                };
                try
                {
                    customerdb.Insert(customer);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public Customer GetCustomerById(int id)
        {
            using (var db = new LiteDatabase(strcon))
            {
                var customerdb = db.GetCollection<Customer>("customers");
                try
                {
                    var result = customerdb.Find(x => x.Id == id).First();
                    Customer customer = result;
                    return customer;
                }
                catch
                {
                    Customer customer = new Customer
                    {
                        Name = "Customer not found",
                        Phone = "999",
                        IsActive = false,
                    };
                    return customer;
                }
            }
        }

        public bool UpdateCustomerById(string id2, string name, string phone, bool isActive)
        {
            int id = Int16.Parse(id2);
            using (var db = new LiteDatabase(strcon))
            {
                var customerdb = db.GetCollection<Customer>("customers");
                try
                {
                    var result = customerdb.Find(x => x.Id == id).First();
                    Customer customer = result;
                    customer.Name = tb_name.Text;
                    customer.Phone = tb_phone.Text;
                    customerdb.Update(customer);
                    return true;
                }
                catch
                {
                    return false;
                }

            }
        }

        public bool DeleteCustomerById(string id2)
        {
            int id = Int16.Parse(id2);
            using (var db = new LiteDatabase(strcon))
            {
                var customerdb = db.GetCollection<Customer>("customers");
                try
                {
                    var result = customerdb.Find(x => x.Id == id).First();
                    Customer customer = result;
                    customerdb.Delete(id);
                    return true;
                }
                catch
                {
                    return false;
                }

            }
        }

        private void btninsert_Click(object sender, RoutedEventArgs e)
        {
            if (InsertCustomer(tb_name.Text, tb_phone.Text, true))
            {
                MessageBox.Show("Insert success");
            }
            else
            {
                MessageBox.Show("Insert fail");
            }
        }

        private void btnupdate_Click(object sender, RoutedEventArgs e)
        {
            if (UpdateCustomerById(tb_id.Text, tb_name.Text, tb_phone.Text, true))
            {
                MessageBox.Show("update success");
            }
            else
            {
                MessageBox.Show("update fail");
            }
        }

        private void btndelete_Click(object sender, RoutedEventArgs e)
        {
            if (DeleteCustomerById(tb_id.Text))
            {
                MessageBox.Show("delete success");
            }
            else
            {
                MessageBox.Show("delete fail");
            }
        }
    }
}
