using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;

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

            //pakt event titles
            
            var query = from c in xmldocument.Root.Descendants("event")
                        select c.Element(attribute).Value;

            foreach (var title in query)
            {
                EventList.Add(title);
            }

         
            return EventList.GetCollection();
        }
    }
}
