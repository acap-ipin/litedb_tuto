using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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
                    u.Password = "xxxxxx";
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
                var result = userdb.Find(x => x.Id == id).First();
                result = user;
                userdb.Update(result);
                try
                {
                    
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
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
                    var result = userdb.Find(x => x.Id == id).First();
                    User user = result;
                    userdb.Delete(id);
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    return false;
                }

            }
        }

        public static string GetMD5Hash(string text)
        {
            if (String.IsNullOrEmpty(text))
                return String.Empty;

            using (var sha = new System.Security.Cryptography.MD5CryptoServiceProvider())
            {
                byte[] textData = System.Text.Encoding.UTF8.GetBytes(text);
                byte[] hash = sha.ComputeHash(textData);
                return BitConverter.ToString(hash).Replace("-", String.Empty);
            }
        }


    }
}
