using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml;
using System.IO;

namespace Revento
{
    /// <summary>
    /// Shared code
    /// </summary>
	public class XMLProcesser
    {
        public static string[] SendXML(XDocument xmldocument, string attribute)
        {
            Itterator<string> EventList = new ArrayItterator<string>(); 

            //pakt event bij attrubuut
            var query = from c in xmldocument.Root.Descendants("event")
                        select c.Element(attribute).Value;

            foreach (var title in query)
            {
                EventList.Add(title);
            }

            //Delete null values...
            string[] RetEvents = new string[EventList.GetAmountOfItems];
            int index = 0;
            while (index < RetEvents.Length)
            {
                RetEvents[index] = EventList.GetNext().Visit<string>((v) => { return v; }, () => { return ""; });
                index += 1;
            }

            return RetEvents;
        }
    }
}
