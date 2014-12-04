using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenPop.Pop3;
using OpenPop.Mime;

namespace MailClient
{
    public partial class OpenPopParser
    {
        /// <summary>
        ///  Method to pull all messages from the mailserver
        /// </summary>
        /// <param name="hostname">Hostname: pop.gmail.com</param>
        /// <param name="port">SSL port: 995</param>
        /// <param name="useSsl">use SSL: true</param>
        /// <param name="username">Email / Username: tgdxof@gmail.com</param>
        /// <param name="password">Password: MailClient</param>
        /// <returns></returns>
        public static List<Message> getAllMessages(string hostname, int port, bool useSsl, string username, string password)
        {
            if (string.IsNullOrWhiteSpace(hostname))
            {
                throw new FormatException("Hostname cannot be empty, null or only contain whitespaces");
            }
            else if (port != 995)
            {
                throw new IndexOutOfRangeException("SSL port has to be 995 port is: " + port);
            }
            //Using "using" for auto disposal og object when finished.
            using (Pop3Client client = new Pop3Client())
            {
                //Connecting to client
                client.Connect(hostname, port, useSsl);

                //Authenticating user / login
                client.Authenticate(username, password);

                //count the number of mail on the server.
                int messageCount = client.GetMessageCount();

                //get mails to client from server
                List<Message> allMessages = new List<Message>(messageCount);

                // Messages are numbered in the interval: [1, messageCount]
                // Ergo: message numbers are 1-based.
                // Most servers give the latest message the highest number
                for (int i = messageCount; i > 0; i--)
                {
                    allMessages.Add(client.GetMessage(i));
                }

                return allMessages;
            }
            
        }

    }
}
