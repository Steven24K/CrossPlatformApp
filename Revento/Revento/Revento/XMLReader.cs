using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Java.Net;

namespace Revento
{
    class XMLReader
    {
        private XDocument xdoc;

        public XMLReader(string importedFile)
        {
            var Stream = new FileStream(@"C:\Users\jurri\Desktop\project4\CrossPlatformApp\Revento\Revento\Revento\events.xml", FileMode.Open);
            xdoc = XDocument.Load(Stream);
        }

        public string[] RetrieveXML()
        {
            var query = from c in xdoc.Root.Descendants("event")
                        select c.Element("title").Value;

            return query.ToArray();
        }
    }
}
