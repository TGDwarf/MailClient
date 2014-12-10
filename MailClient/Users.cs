using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient
{
    /// <summary>
    /// Class used to hold the login information for Openpop
    /// </summary>
    public partial class Users
    {
        //Login credentials: username
        public static string username { get; set; }
        //Login credentials: password
        public static string password { get; set; }
        //Hostname for smtp
        public static string sendHostname { get; set; }
        //Hostname for POP3
        public static string receiveHostname { get; set; }
        //specifies if SSL or TLS should be used
        public static bool useSsl { get; set; }
        //SSL port for smtp
        public static int sendPort { get; set; }
        //SSL port for POP3
        public static int receivePort { get; set; }

    }
}
