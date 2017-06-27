using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
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

            Stream xmldoc = Assets.Open ("events.xml");

            /// Linq werkt niet bij android, dus push xmldoc naar de shared code dmv een method
            /// Bewerk xmldoc in de shared code zodat er een string[] uitkomt
            /// Return de bewerkte xmldoc en voeg deze toe in de items array

            // Lees het xml bestand door van voor naar achteren -> je krijgt hierbij de xml tags
            StreamReader reader = new StreamReader(xmldoc);
            string readertext = reader.ReadToEnd();

            // Maak van de gelezen xml een XDocument object
            XDocument xdoc = XDocument.Parse(readertext);
            string[] titleList = new string[10];

            // Nieuw object van myclass
            MyClass myClass = new MyClass();
            
            // Stuurd xml document dmv SendXML en krijg een string[] terug
            string[] processedXML = myClass.SendXML(xdoc);


            List<string> readertextitems = new List<string>();

            foreach (var item in processedXML)
            {
                readertextitems.Add(item.ToString());
            }

            readertextitems.Add(processedXML);

            readertextitems.ToArray();

            readertextitems.Add(processedXML);

            items = new string[] { "Ayy", "Lmao" };
            ListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, readertextitems);
        }

        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            // TODO: add code
        }
    }
}
