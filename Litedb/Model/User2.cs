using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Litedb.Model
{
    class User2 { 

    private int id;
    private string name;
    private string email;
    private string phone;
    private string admin;

    public int Id
    {
        get
        {
            return id;
        }
        set
        {
            id = value;
        }
    }

    public string Name
    {
        get
        {
            return name;
        }
        set
        {
            name = value;
        }
    }

    public string Email
    {
        get
        {
            return email;
        }
        set
        {
            email = value;
        }
    }

    public string Phone
    {
        get
        {
            return phone;
        }
        set
        {
            phone = value;
        }
    }

    public string Admin
    {
        get
        {
            return admin;
        }
        set
        {
            admin = value;
        }
    }
}
}
