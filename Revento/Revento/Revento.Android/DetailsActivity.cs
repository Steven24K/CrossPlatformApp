using Android.App;
using Android.OS;
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

            // Toon een backbutton in de actionbar
            ActionBar.SetHomeButtonEnabled(true);
            ActionBar.SetDisplayHomeAsUpEnabled(true);

            // Maak textviews aan voor het evenement
            TextView Title = FindViewById<TextView>(Resource.Id.title);
            TextView Date = FindViewById<TextView>(Resource.Id.date);
            TextView Description = FindViewById<TextView>(Resource.Id.description);
            TextView Address = FindViewById<TextView>(Resource.Id.address);
            TextView Website = FindViewById<TextView>(Resource.Id.website);

            // Toon de textviews op het scherm
            Title.Text= Intent.GetStringExtra("title") ?? "No data...";
            Date.Text = Intent.GetStringExtra("date") ?? "No data..";
            Description.Text = Intent.GetStringExtra("description") ?? "No data...";
            Address.Text = Intent.GetStringExtra("address") ?? "No data..";
            Website.Text = Intent.GetStringExtra("website") ?? "No data..";            
        }

        // Override de functionaliteit van de backbutton zodat het teruggaat naar de mainactivity
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    Finish();
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }
    }
}
