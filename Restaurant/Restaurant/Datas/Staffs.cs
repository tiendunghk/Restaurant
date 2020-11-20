using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Datas
{
    public static class Staffs
    {
        public static List<Staff> ListStaffs = new List<Staff>
        {
            new Staff
            {
                Id="nv1",
                Name="Nam",
                Role=Datas.Roles.ListRoles[3].RoleId,
                UserName="a",
                PassWord="1",
                IsActive=false,
                StaffVisa="65478567834567835",
            },
            new Staff
            {
                Id="nv2",
                Name="Bao",
                Role=Datas.Roles.ListRoles[1].RoleId,
                UserName="b",
                PassWord="1",
                IsActive=true,
                StaffVisa="65478567834567835",
            },
            new Staff
            {
                Id="nv3",
                Name="An",
                Role=Datas.Roles.ListRoles[0].RoleId,
                UserName="c",
                PassWord="1",
                IsActive=true,
                StaffVisa="65478567834567835",
            },
            new Staff
            {
                Id="nv4",
                Name="Dinh",
                Role=Datas.Roles.ListRoles[4].RoleId,
                UserName="d",
                PassWord="1",
                IsActive=true,
                StaffVisa="65478567834567835",
            },
            new Staff
            {
                Id="nv5",
                Name="Dung",
                Role=Datas.Roles.ListRoles[2].RoleId,
                UserName="e",
                PassWord="1",
                IsActive=false,
                StaffVisa="65478567834567835",
            },
            new Staff
            {
                Id="nv6",
                Name="Dinh",
                Role=Datas.Roles.ListRoles[4].RoleId,
                UserName="d",
                PassWord="1",
                IsActive=true,
                StaffVisa="65478567834567835",
            },
            new Staff
            {
                Id="nv7",
                Name="Dung",
                Role=Datas.Roles.ListRoles[2].RoleId,
                UserName="e",
                PassWord="1",
                IsActive=false,
                StaffVisa="65478567834567835",
            },
        };
    }
}
