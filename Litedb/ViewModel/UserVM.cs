using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Litedb.Model;
using LiteDB;

namespace Litedb.ViewModel
{ 
    class UserVM
    {
        public static string strcon = ConfigurationManager.AppSettings["connstring"];
        
        public IList<User> GetUserList()
        {
            var userlist = new List<User>();
            using (var db = new LiteDatabase(strcon))
            {
                var userdb = db.GetCollection<User>("users");
                var results = userdb.FindAll();
                foreach (User u in results)
                {
                    userlist.Add(u);
                }
                return userlist;
            }
        }

        public bool InsertUser(User user)
        {
            using (var db = new LiteDatabase(strcon))
            {
                var userdb = db.GetCollection<User>("users");
                try
                {
                    userdb.Insert(user);
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    return false;
                }
            }
        }

        public bool UpdateUser(User user)
        {
            using (var db = new LiteDatabase(strcon))
            {
                int id = user.Id;
                var userdb = db.GetCollection<User>("users");
                try
                {
                    var result = userdb.Find(x => x.Id == id).First();
                    result = user;
                    userdb.Update(result);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool DeleteUser(string id2)
        {
            int id = Int16.Parse(id2);
            using (var db = new LiteDatabase(strcon))
            {
                var userdb = db.GetCollection<User>("users");
                try
                {
                    var result = userdb.Find(x => x.Id == id).FirstOrDefault();
                    User user = result;
                    userdb.Delete(id);
                    return true;
                }
                catch
                {
                    return false;
                }

            }
        }


    }
}
