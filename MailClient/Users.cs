using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient
{
    public partial class Users
    {
        public static string username { get; set; }

        public static string password { get; set; }

        public static string sendHostname { get; set; }

        public static string receiveHostname { get; set; }

        public static bool useSsl { get; set; }

        public static int sendPort { get; set; }

        public static int receivePort { get; set; }

    }
}
