using Acr.UserDialogs;
using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Essentials;

namespace Restaurant
{
    public class AppContext
    {
        public AppContext()
        {
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }
        void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (e.NetworkAccess != NetworkAccess.Internet)
                UserDialogs.Instance.Toast("NetworkWarningMessage");
        }


        ~AppContext()
        {
            Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;
        }
        public Dictionary<string, OrderModel> CurrentOrder = new Dictionary<string, OrderModel>();
        public Staff CurrentStaff { get; set; } = new Staff();
        public List<Table> Tables = new List<Table>();
        public int KitchenClickCount = 0;
        public int OrderClickCount = 0;
        public int TableClickCount = 0;
        public Dictionary<string, ObservableCollection<OrderDetailUI>> ListOrderDetailUI = new Dictionary<string, ObservableCollection<OrderDetailUI>>();
    }
}
