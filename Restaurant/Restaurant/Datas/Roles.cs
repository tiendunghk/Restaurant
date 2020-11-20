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
            new Role{RoleId="role1",RoleName="Busboy"},
            new Role{RoleId="role2",RoleName="Manager"},
            new Role{RoleId="role3",RoleName="Kitchen"},
            new Role{RoleId="role4",RoleName="Waiter"},
            new Role{RoleId="role5",RoleName="Cashier"}
        };
    }
}
