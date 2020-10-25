using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Text;
using Android.Text.Style;
using Android.Views;
using Android.Widget;
using Restaurant;
using Restaurant.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly:ExportRenderer(typeof(AppShell),typeof(AndroidShell))]
namespace Restaurant.Droid.Renderers
{
    public class AndroidShell: ShellRenderer
    {
        public AndroidShell(Context c) : base(c)
        {

        }
        protected override IShellBottomNavViewAppearanceTracker CreateBottomNavViewAppearanceTracker(ShellItem shellItem)
        {
            return new CustomBottomNavAppearance();
        }
    }

    public class CustomBottomNavAppearance : IShellBottomNavViewAppearanceTracker
    {
        public void Dispose()
        {

        }

        public void ResetAppearance(BottomNavigationView bottomView)
        {

        }


        public void SetAppearance(BottomNavigationView bottomView, IShellAppearanceElement appearance)
        {
            IMenu menu = bottomView.Menu;
            for (int i = 0; i < bottomView.Menu.Size(); i++)
            {
                IMenuItem menuItem = menu.GetItem(i);
                var title = menuItem.TitleFormatted;
                SpannableStringBuilder sb = new SpannableStringBuilder(title);

                int a = sb.Length();

                sb.SetSpan(new AbsoluteSizeSpan(20, true), 0, a, SpanTypes.ExclusiveExclusive);

                menuItem.SetTitle(sb);
            }
        }
    }
}