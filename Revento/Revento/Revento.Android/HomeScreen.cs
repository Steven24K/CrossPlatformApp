using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace Revento.Droid
{
    [Activity(Label = "ListCheck", MainLauncher = true, Icon = "@drawable/icon")]
    public class HomeScreen : ListActivity
    {
        string[] items;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var xmldoc = Assets.Open ("events.xml");

            /// Linq kan niet bij android, dus push xmldoc naar de shared code dmv een method
            /// Bewerk xmldoc in de shared code zodat er een string[] uitkomt
            /// Return de bewerkte xmldoc en voeg deze toe in de items array

            items = new string[] { "Groente", "Fruit", "Swag" };
            ListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, items);
        }

        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            // TODO: add code
        }
    }
}
