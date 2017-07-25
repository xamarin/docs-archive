using System;
using Android.App;
using Android.Content;
using Android.Views;

namespace com.xamarin.recipes.customcompoundview
{
    public static class ViewHelpers
    {

        public static Activity HostActivity(this View view)
        {
            if (view == null)
            {
                throw new NullReferenceException("Need a valid View reference in order to find the hosting activity.");
            }

            ContextWrapper context = view.Context as ContextWrapper;
            while (context != null)
            {
                if (context is Activity)
                {
                    return (Activity)context;
                }
                context = context.BaseContext as ContextWrapper;
            }

            return null;
        }
    }
}
