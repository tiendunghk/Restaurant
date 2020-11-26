using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Datas
{
    public static class Roles
    {
        public static List<Role> ListRoles = new List<Role>
        {
            new Role{RoleId="3",RoleName="Busboy"},
            new Role{RoleId="2",RoleName="Manager"},
            new Role{RoleId="4",RoleName="Kitchen"},
            new Role{RoleId="1",RoleName="Waiter"},
            new Role{RoleId="5",RoleName="Cashier"}
        };
    }
}
