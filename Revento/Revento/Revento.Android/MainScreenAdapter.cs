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
            view = Context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleExpandableListItem2, null);
            view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = Items[position];
            
            view.FindViewById<TextView>(Android.Resource.Id.Text2).Text = Descroption[position];
            return view;
        }
    }
}