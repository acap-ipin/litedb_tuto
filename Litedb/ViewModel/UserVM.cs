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
        
        public IList<User2> GetUserList()
        {
            var userlist = new List<User2>();
            try
            {
                using (var db = new LiteDatabase(strcon))
                {
                    var userdb = db.GetCollection<User2>("users");
                    var results = userdb.FindAll();
                    foreach (User2 u in results)
                    {
                        userlist.Add(u);
                    }
                    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return userlist;

        }

        public User GetUserByEmail(string email)
        {
            User user = null;
            try
            {
                using (var db = new LiteDatabase(strcon))
                {
                    var userdb = db.GetCollection<User>("users");
                    var result = userdb.Find(x => x.Email == email).First();
                    user = result;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("User email not found" + e.ToString());
            }
            return user;
        }

        public int InsertUser(User user)
        {
            if (GetUserByEmail(user.Email) != null)
            {
                Console.WriteLine("User email has already been registered");
                return 2;
            }
            try
            {
                using (var db = new LiteDatabase(strcon))
                {
                    var userdb = db.GetCollection<User>("users");
                    userdb.Insert(user);
                    return 0;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return 1;
            }
        }

        public bool UpdateUser(User user)
        {
            try
            {
                using (var db = new LiteDatabase(strcon))
                {
                    int id = user.Id;
                    var userdb = db.GetCollection<User>("users");

                    var result = userdb.Find(x => x.Id == id).First();
                    if (string.IsNullOrEmpty(user.Password))
                    {
                        user.Password = result.Password;
                    }
                    result = user;
                    userdb.Update(result);
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }

        public bool DeleteUser(string id2)
        {
            int id = Int16.Parse(id2);
            try
            {
                using (var db = new LiteDatabase(strcon))
                {
                    var userdb = db.GetCollection<User>("users");
                    var result = userdb.Find(x => x.Id == id).First();
                    User user = result;
                    userdb.Delete(id);
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
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
