using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MailClient
{
    public partial class LocalDataCache
    {
        private static string xmlLocation = @"c:\ProgramData\MailClient\";
        public static void saveMailsToXml()
        {
            if (Directory.Exists(xmlLocation))
            {
                if (File.Exists(xmlLocation + "incommingMails.xml"))
                {
                    File.Delete(xmlLocation + "incommingMails.xml");
                }
                if (File.Exists(xmlLocation + "sentMails.xml"))
                {
                    File.Delete(xmlLocation + "sentMails.xml");
                }

            }
            else
            {
                Directory.CreateDirectory(xmlLocation);
            }

            using (XmlWriter writer = XmlWriter.Create(xmlLocation + "AllMails.xml"))
            {            
                writer.WriteStartElement("RegExes");
                foreach (var RegEx in RegExesList)
                {
                    writer.WriteStartElement("RegEx");
                    writer.WriteElementString("Name", RegEx.Name);
                    writer.WriteElementString("Expression", RegEx.Expression);
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.Flush();
            }
   
        }

    }
}
