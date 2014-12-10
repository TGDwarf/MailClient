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
        // Folderlocation for the emails
        private static string xmlLocation = @"c:\ProgramData\MailClient\";
        /// <summary>
        /// saving all mails in two xml files, incomming and sent.
        /// </summary>
        public static void saveMailsToXml()
        {
            // check if directory exists
            if (Directory.Exists(xmlLocation))
            {
                //check if files exist
                if (File.Exists(xmlLocation + "incommingMails.xml"))
                {
                    File.Delete(xmlLocation + "incommingMails.xml");
                }
                if (File.Exists(xmlLocation + "sentMails.xml"))
                {
                    File.Delete(xmlLocation + "sentMails.xml");
                }

            }
            //create directory if it does not exist.
            else
            {
                Directory.CreateDirectory(xmlLocation);
            }
            //create lists for the mails
            List<OpenPop.Mime.Message> incommingMails = new List<OpenPop.Mime.Message>();
            List<OpenPop.Mime.Message> sentMails = new List<OpenPop.Mime.Message>();
            //populate the lists with emails
            incommingMails = OpenPopParser.getIncommingOrSentMessages("incomming");
            sentMails = OpenPopParser.getIncommingOrSentMessages("sent");
            // initializes xml writer
            using (XmlWriter writer = XmlWriter.Create(xmlLocation + "incommingMails.xml"))
            {            
                //making the first element.
                writer.WriteStartElement("Incomming Emails");
                int i = 0;
                //running through all the emails
                foreach (var Email in incommingMails)
                {
                    writer.WriteStartElement("Email");                                      //creating the next element in xml
                    writer.WriteElementString("From", Email.Headers.From.ToString());       //writing 'From'
                    writer.WriteElementString("To", Email.Headers.To[0].ToString());        //writing 'To'
                    writer.WriteElementString("Subject", Email.Headers.Subject.ToString()); //writing 'subject'
                    writer.WriteElementString("Time", Email.Headers.DateSent.ToString());   //writing 'TimeSent'
                    writer.WriteElementString("Body", OpenPopParser.Body(i,incommingMails));//writing 'body'
                    writer.WriteEndElement();                                               //ending element
                    i++;
                }
                writer.WriteEndElement();                                                   //ending element           
                writer.Flush();                                                             //writes all data from buffer
            }


            using (XmlWriter writer = XmlWriter.Create(xmlLocation + "sentMails.xml"))
            {
                //making the first element.
                writer.WriteStartElement("Sent Emails");
                int i = 0;
                //running through all the emails
                foreach (var Email in sentMails)
                {
                    writer.WriteStartElement("Email");                                      //creating the next element in xml
                    writer.WriteElementString("From", Email.Headers.From.ToString());       //writing 'From'
                    writer.WriteElementString("To", Email.Headers.To[0].ToString());        //writing 'To'
                    writer.WriteElementString("Subject", Email.Headers.Subject.ToString()); //writing 'subject'
                    writer.WriteElementString("Time", Email.Headers.DateSent.ToString());   //writing 'TimeSent'
                    writer.WriteElementString("Body", OpenPopParser.Body(i,sentMails));     //writing 'body'
                    writer.WriteEndElement();                                               //ending element
                    i++;
                }
                writer.WriteEndElement();                                                   //ending element           
                writer.Flush();                                                             //writes all data from buffer
            }
        }
    }
}
