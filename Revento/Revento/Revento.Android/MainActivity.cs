using System;
using System.IO;
using System.Xml.Linq;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Android.OS;
using Android.Support.V7.CardView;

namespace Revento.Droid
{
	[Activity (Label = "Revento", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : ListActivity
	{

        string[] EventList, Description;
        
        protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

            // Set our view from the "main" layout resource
            Stream xmldoc = Assets.Open("events.xml");

            /// Linq werkt niet bij android, dus push xmldoc naar de shared code dmv een method
            /// Bewerk xmldoc in de shared code zodat er een string[] uitkomt
            /// Return de bewerkte xmldoc en voeg deze toe in de items array

            // Lees het xml bestand door van voor naar achteren -> je krijgt hierbij de xml tags
            StreamReader reader = new StreamReader(xmldoc);
            string readertext = reader.ReadToEnd();

            // Maak van de gelezen xml een XDocument object
            XDocument xdoc = XDocument.Parse(readertext);
            //string[] ProcessedXML = XMLProcesser.SendXML(xdoc, "description");
            List<string> TitleList = new List<string>();


            ListView.FastScrollEnabled = true;
            //EventList = new string[] { "De Parade", "Oh ja joh hoezo dan?!", "Hart voor de zaak", "Klein geluk" };
            EventList = XMLProcesser.SendXML(xdoc, "title"); 
            Description = XMLProcesser.SendXML(xdoc, "description"); 
            //Description = new string[] {"Het grootste theater festival van Rotterdam","De gepasioneerde schiedammer" ,"Een tour door schiedam zuid","De schiedamse natuur en zijn verhalen"  };
            

		    //XMLReader xmlreader = new XMLReader("");
		    //EventList = xmlreader.RetrieveXML();

            this.ListAdapter = new MainScreenAdapter(EventList,Description,this);
           
        }
        protected override void OnListItemClick(ListView l, View v, int position, long id) {
            var t = EventList[position];
            //Android.Widget.Toast.MakeText(this, t, Android.Widget.ToastLength.Short).Show();
            var NextActivity = new Intent(this,typeof(DetailsActivity));
            NextActivity.PutExtra("title", EventList[id]);
            NextActivity.PutExtra("description", Description[id]);
            
            StartActivity(NextActivity);
    }

    }
}


