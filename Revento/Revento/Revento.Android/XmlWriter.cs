using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Text; 
using System.Xml; 
using System.Xml.Linq; 
 
using Android.App; 
using Android.Content; 
using Android.OS; 
using Android.Runtime; 
using Android.Views; 
using Android.Widget; 
 
namespace Revento.Droid
{
    public class XmlWriter
    {
        public static void AddEvent(string file, string title, string description, string date, string adress)
        {
            XmlDocument xd = new XmlDocument();
            xd.Load(file);
            XmlNode nl = xd.SelectSingleNode("resources");
            XmlDocument xd2 = new XmlDocument();
            xd2.LoadXml("<events><title>" + title + "</title><description>" + description + "</description><date>" + date + "</date><adress>" + adress + "</adress></events>");
            XmlNode n = xd.ImportNode(xd2.FirstChild, true);
            nl.AppendChild(n);
            xd.Save(file);
        }
    }
}