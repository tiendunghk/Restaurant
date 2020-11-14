using Newtonsoft.Json;
using Restaurant.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Restaurant.Views.Reports
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderReportView : ContentPage
    {
        public OrderReportView()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            //var output = await HttpService.GetAsync<Todo>("https://jsonplaceholder.typicode.com/todos/1");
            Notification.Push();
        }
        internal class Todo
        {
            [JsonProperty("userId")]
            public int UserId { get; set; }
            [JsonProperty("id")]
            public int Id { get; set; }
            [JsonProperty("title")]
            public string Title { get; set; }
            [JsonProperty("completed")]
            public bool Completed { get; set; }
        }
    }
}