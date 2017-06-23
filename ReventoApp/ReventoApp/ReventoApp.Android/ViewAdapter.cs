using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ReventoApp.Droid
{
    public class ViewAdapter : IViewAdapter<View>
    {
        private Activity Activity;
        public ViewAdapter(Activity activity)
        {
            this.Activity = activity;
        }

        public View setView(int resource)
        {
           return Activity.FindViewById(resource);
        }
    }

}