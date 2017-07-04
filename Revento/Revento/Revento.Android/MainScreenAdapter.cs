using Android.App;
using Android.Views;
using Android.Widget;

namespace Revento.Droid
{
    /// <summary>
    /// Draws the list of events on the main screen
    /// </summary>
    public class MainScreenAdapter : BaseAdapter<string>
    {
        string[] Items, Date;
        Activity Context;
        /// <summary>
        /// Initilizes the mainscreenAdapter
        /// </summary>
        /// <param name="items">A string array of all event titles, to be shown</param>
        /// <param name="date">A string array of the events dates, to be shown</param>
        /// <param name="context">The activity on wich the list of events must be drawns</param>
        public MainScreenAdapter(string[] items, string[] date ,Activity context) : base()
        {
            this.Context = context;
            this.Items = items;
            this.Date = date;
        }

        public override string this[int position] { get { return Items[position] + " " + Date[position]; } }

        public override int Count { get { return Items.Length; } }

        public override long GetItemId(int position)
        {
            return position;
        }
        /// <summary>
        /// Returns the ListView to the activity
        /// </summary>
        /// <param name="position">To be added</param>
        /// <param name="convertView">A custom view</param>
        /// <param name="parent">The viewgroup, listviews,menuviews etc.</param>
        /// <returns></returns>
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;
            view = Context.LayoutInflater.Inflate(Resource.Layout.Main, null);
            view.FindViewById<TextView>(Resource.Id.textView3).Text = Items[position];
            
            view.FindViewById<TextView>(Resource.Id.textView2).Text = Date[position];
            return view;
        }
    }
}