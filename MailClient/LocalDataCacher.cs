using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

public class LocalDataCacher
{
    private static string StoragePath = @"C:\ProgramData\MailClient";

    public static void Startup()
    {

        if (Directory.Exists(StoragePath))
        {
            Console.WriteLine("Storagepath " + StoragePath + " already existed");
        }
        else
        {
            Directory.CreateDirectory(StoragePath);
            Console.WriteLine("Created " + StoragePath + " Successfully");

        }

        #region XML

        using (XmlWriter writer = XmlWriter.Create(StoragePath + "\\Postal.xml"))
        {
            //writer.WriteStartElement("Emails");
            //foreach (var PostalCode in MailList)
            //{

            //    writer.WriteStartElement("Postal");
            //    writer.WriteElementString("Id", Convert.ToString(PostalCode.Id));
            //    writer.WriteElementString("Code", Convert.ToString(PostalCode.Code));
            //    writer.WriteElementString("CityName", PostalCode.CityName);
            //    writer.WriteEndElement();
            //}
            //writer.WriteEndElement();
            //writer.Flush();
        }
        Console.WriteLine("Created Mail.xml Successfully");

        #endregion XML

        Console.ReadLine();
    }
}
