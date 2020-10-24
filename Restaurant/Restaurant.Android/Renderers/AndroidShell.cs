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
            return new MyBottomNavigationView(this);
        }
    }

    internal class MyBottomNavigationView : IShellBottomNavViewAppearanceTracker
    {
        private AndroidShell androidShell;

        public MyBottomNavigationView(AndroidShell androidShell)
        {
            this.androidShell = androidShell;
        }

        public void Dispose()
        {
            
        }

        public void ResetAppearance(BottomNavigationView bottomView)
        {
            IMenu menu = bottomView.Menu;
            for (int i = 0; i < bottomView.Menu.Size(); i++)
            {
                IMenuItem menuItem = menu.GetItem(i);
                var title = menuItem.TitleFormatted;
                Typeface typeface = Typeface.CreateFromAsset(MainActivity.Instance.Assets, "UTMAvo.ttf");
                SpannableStringBuilder sb = new SpannableStringBuilder(title);

                sb.SetSpan(new CustomTypefaceSpan("", typeface), 0, sb.Length(), SpanTypes.InclusiveInclusive);
                menuItem.SetTitle(sb);
            }
        }

        public void SetAppearance(BottomNavigationView bottomView, IShellAppearanceElement appearance)
        {
            
        }
    }

    internal class CustomTypefaceSpan : TypefaceSpan
    {
        private Typeface typeface;

        public CustomTypefaceSpan(string family, Typeface typeface):base(family)
        {
            this.typeface = typeface;
        }
        public override void UpdateDrawState(TextPaint ds)
        {
            applyCustomTypeFace(ds, typeface);

        }
        public override void UpdateMeasureState(TextPaint paint)
        {
            applyCustomTypeFace(paint, typeface);
        }
        private static void applyCustomTypeFace(Paint paint, Typeface tf)
        {
            TypefaceStyle oldStyle;
            Typeface old = paint.Typeface;
            if (old == null)
            {
                oldStyle = 0;
            }
            else
            {
                oldStyle = old.Style;
            }

            TypefaceStyle fake = oldStyle & ~tf.Style;
            if ((fake & TypefaceStyle.Bold) != 0)
            {
                paint.FakeBoldText = true;
            }

            if ((fake & TypefaceStyle.Italic) != 0)
            {
                paint.TextSkewX = -0.25f;
            }

            paint.SetTypeface(tf);
        }
    }
}