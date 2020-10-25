﻿using Restaurant.ViewModels.Manager;
using Restaurant.ViewModels.Reports;
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
    public partial class FoodReportView : ContentPage
    {
        public FoodReportView()
        {
            InitializeComponent();
            BindingContext = new FoodReportViewModel();
        }
    }
}