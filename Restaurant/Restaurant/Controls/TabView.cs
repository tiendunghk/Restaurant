using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Restaurant.Controls
{
    [ContentProperty(nameof(ItemSource))]
    public class TabView : Xam.Plugin.TabView.TabViewControl
    {
        public TabView() : base(new List<Xam.Plugin.TabView.TabItem>(), -1)
        {

        }
    }

    [ContentProperty(nameof(Content))]
    public class TabItem : Xam.Plugin.TabView.TabItem
    {
        public TabItem() : base("", new StackLayout())
        {

        }
    }
}
