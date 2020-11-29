using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Restaurant.Datas
{
    public static class Staffs
    {
        public static ObservableCollection<Staff> ListStaffs = new ObservableCollection<Staff>
        {
            new Staff
            {
                Id="nv1",
                Name="Trần Văn Nam",
                Role=Datas.Roles.ListRoles[3].RoleId,
                UserName="a",
                PassWord="1",
                IsActive=false,
                StaffVisa="65478567834567835",
            },
            new Staff
            {
                Id="nv2",
                Name="Lê Trí Bảo",
                Role=Datas.Roles.ListRoles[1].RoleId,
                UserName="b",
                PassWord="1",
                IsActive=true,
                StaffVisa="65478567834567835",
            },
            new Staff
            {
                Id="nv3",
                Name="Phan Bảo An",
                Role=Datas.Roles.ListRoles[0].RoleId,
                UserName="c",
                PassWord="1",
                IsActive=true,
                StaffVisa="65478567834567835",
            },
            new Staff
            {
                Id="nv4",
                Name="Phan Phước Đính",
                Role=Datas.Roles.ListRoles[4].RoleId,
                UserName="d",
                PassWord="1",
                IsActive=true,
                StaffVisa="65478567834567835",
            },
            new Staff
            {
                Id="nv5",
                Name="Nguyễn Thị Lan",
                Role=Datas.Roles.ListRoles[2].RoleId,
                UserName="e",
                PassWord="1",
                IsActive=false,
                StaffVisa="65478567834567835",
            },
            new Staff
            {
                Id="nv6",
                Name="Bùi Xuân Tứ",
                Role=Datas.Roles.ListRoles[4].RoleId,
                UserName="d",
                PassWord="1",
                IsActive=true,
                StaffVisa="65478567834567835",
            },
            new Staff
            {
                Id="nv7",
                Name="Cao Thị Xa",
                Role=Datas.Roles.ListRoles[2].RoleId,
                UserName="e",
                PassWord="1",
                IsActive=false,
                StaffVisa="65478567834567835",
            },
        };
    }
}
