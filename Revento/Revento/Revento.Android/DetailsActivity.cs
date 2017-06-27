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

namespace Revento.Droid
{
    [Activity(Label = "Details")]
    public class DetailsActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Details);
            TextView Title = FindViewById<TextView>(Resource.Id.title);
            TextView Description = FindViewById<TextView>(Resource.Id.description);
            Title.Text= Intent.GetStringExtra("title")??"No data...";
            Description.Text = Intent.GetStringExtra("description")??"No data...";

            

            // Create your application here
        }
    }
}