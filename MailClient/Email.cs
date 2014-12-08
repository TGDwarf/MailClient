using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient
{
    [Serializable]
    public partial class Email
    {
        public Email()
        {
            this.Attachments = new List<Attachment>();
        }

        public static string From { get; set; }
        public static string Subject { get; set; }
        public static string Body { get; set; }
        public static DateTime DateSent { get; set; }

        public static bool HasAttachment { get; set; }
        public List<Attachment> Attachments { get; set; }

        //public bool HasAttachment()
        //{
        //    if (Attachments.Count != 0)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
    }

    [Serializable]
    public partial class Attachment
    {
        public static string FileName { get; set; }
        public static string ContentType { get; set; }
        public static byte[] Content { get; set; }
    }



}
