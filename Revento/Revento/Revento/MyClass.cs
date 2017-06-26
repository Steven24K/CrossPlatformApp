using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Revento
{
    /// <summary>
    /// Shared code
    /// </summary>
	public class MyClass
    {
        public MyClass()
        {

        }

        public static void RetrieveXML()
        {
            XDocument xdoc = XDocument.Load("events.xml");

            var query = from c in xdoc.Root.Descendants("event")
                        select c.Element("title").Value;
        }
    }
}
