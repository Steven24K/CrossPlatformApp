using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Xml.Linq;
using Android.Content.Res;

namespace Revento.Droid
{
    public class XMLReader
    {
        public async void LoadXMLData()
        {
            List<string> rawData = null;

            await Task.Factory.StartNew(delegate
            {
                XDocument xdoc = XDocument.Load("events.xml");

                IEnumerable<string> query = from s in xdoc.Descendants("event")
                                            select s.Element("title").Value;

                rawData = query.ToList();
            });
        }
    }
}
