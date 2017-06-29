using Android.App;
using Android.Views;
using Android.Widget;

namespace Revento.Droid
{
    public class MainScreenAdapter : BaseAdapter<string>
    {
        string[] Items, Descroption;
        Activity Context;
        public MainScreenAdapter(string[] items, string[] description ,Activity context) : base()
        {
            this.Context = context;
            this.Items = items;
            this.Descroption = description;
        }

        public override string this[int position] { get { return Items[position] + " " + Descroption[position]; } }

        public override int Count { get { return Items.Length; } }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;
            view = Context.LayoutInflater.Inflate(Resource.Layout.Main, null);
            view.FindViewById<TextView>(Resource.Id.textView3).Text = Items[position];
            
            view.FindViewById<TextView>(Resource.Id.textView2).Text = Descroption[position];
            return view;
        }
    }
}