using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml;

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

        public string[] SendXML(XDocument xmldocument)
        {
            string[] titles = new string[10];

            var query = from c in xmldocument.Root.Descendants("event")
                select c.Element("title").Value;

            foreach (string title in query)
            {
                // titles.Add(title);
            }

            titles[0] = "Hallo";

            return titles;
        }
    }
}
