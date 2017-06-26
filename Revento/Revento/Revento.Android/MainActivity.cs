using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Android.OS;
using System.Xml;
using System.Xml.Linq;
using Android.Content.Res;
using Android.Util;

namespace Revento.Droid
{
    [Activity(Label = "Revento", MainLauncher = false, Icon = "@drawable/icon")]
    public class MainActivity : ListActivity
    {
        //private string EventList[];

        //protected override void OnCreate(Bundle bundle)
        //{
        //    base.OnCreate(bundle);

        //    XmlDocument doc = new XmlDocument();
        //    doc.Load("events.xml");

        //    foreach (var item in doc.FirstChild.ChildNodes)
        //    {
        //        // listView.Items.Add(((XmlNode) item).InnerText);
        //    }

        //    this.ListAdapter = new MainScreenAdapter(EventList, EventLogTags.Description, this);
        //}

        //protected override void OnListItemClick(ListView l, View v, int position, long id)
        //{
        //    var t = EventList[position];
        //    Android.Widget.Toast.MakeText(this, t, Android.Widget.ToastLength.Short).Show();
        //}
    }
}
