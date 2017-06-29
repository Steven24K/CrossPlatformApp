﻿using System.IO;
using System.Xml.Linq;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V7.Widget;

namespace Revento.Droid
{
	[Activity (Label = "Revento", Theme ="@style/MyTheme", MainLauncher = true, Icon = "@drawable/Icon")]
	public class MainActivity : ListActivity
	{
        string[] EventTitle, EventDate, EventDescription, EventAddress, EventWebsite;        
        
        protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

            // Open het xml bestand vanuit de assets folder
            Stream xmldoc = Assets.Open("events.xml");

            // Lees het xml bestand door van voor naar achteren -> je krijgt hierbij de xml tags
            StreamReader reader = new StreamReader(xmldoc);
            string readertext = reader.ReadToEnd();

            // Maak van de gelezen xml een XDocument object
            XDocument xdoc = XDocument.Parse(readertext);                                    

            // Laat onze XMLProcessor alle informatie uit het xml bestand halen
            EventTitle = XMLProcesser.SendXML(xdoc, "title");
		    EventDate = XMLProcesser.SendXML(xdoc, "date");
            EventDescription = XMLProcesser.SendXML(xdoc, "description");
		    EventAddress = XMLProcesser.SendXML(xdoc, "address");
		    EventWebsite = XMLProcesser.SendXML(xdoc, "website");   

            // Toon de titel en datum van het evenement in de lijst van het homescreen
            this.ListAdapter = new MainScreenAdapter(EventTitle,EventDate,this);           
        }

        protected override void OnListItemClick(ListView lv, View v, int position, long id) {
            var t = EventTitle[position];
            
            var NextActivity = new Intent(this,typeof(DetailsActivity));

            // Voeg alle items vanuit het xml bestand toe aan het detailedview activity
            NextActivity.PutExtra("title", EventTitle[id]);
            NextActivity.PutExtra("date", EventDate[id]);
            NextActivity.PutExtra("description", EventDescription[id]);
            NextActivity.PutExtra("address", EventAddress[id]);
            NextActivity.PutExtra("website", EventWebsite[id]);
            
            StartActivity(NextActivity);
    }

        //Laat 3 dots menu zien
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.top_menus, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        //Laat zien welk item geklikt is
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Toast.MakeText(this, "Action selected: " + item.TitleFormatted,
                ToastLength.Short).Show();
            return base.OnOptionsItemSelected(item);
        }

    }
}


