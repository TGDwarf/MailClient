using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using OpenPop.Pop3;
using OpenPop.Mime;

namespace MailClient
{
    [Serializable]
    public class Email
    {
        public Email()
        {
            this.Attachments = new List<Attachment>();
        }
        public int MessageNumber { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime DateSent { get; set; }
        public List<Attachment> Attachments { get; set; }
    }

    [Serializable]
    public class Attachment
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
    }
    public partial class OpenPopParser
    {
        /// <summary>
        ///  Method to pull all messages from the mailserver
        /// </summary>
        /// <param name="hostname"> Hostname gmail: pop.gmail.com / Hostname outlook: pop3.live.com </param>
        /// <param name="port"> SSL port: 995 </param>
        /// <param name="useSsl"> use SSL: true </param>
        /// <param name="username"> Email / Username: tgdxof@gmail.com / tgdxof@live.com </param>
        /// <param name="password"> Password: MailClient </param>
        /// <returns></returns>
        public static List<Message> getAllMessages(string hostname, int port, bool useSsl, string username, string password)
        {
            if (string.IsNullOrWhiteSpace(hostname))
            {
                throw new ArgumentException("Hostname cannot be empty, null or only contain whitespaces");
            }
            else if (port != 995)
            {
                throw new IndexOutOfRangeException("SSL port has to be 995 port is: " + port);
            }
            else if (useSsl == false)
            {
                throw new ArgumentException("UseSsl must be set true, else it won't connect.");
            }
            else
            {
                //Using "using" for auto disposal og object when finished.
                using (Pop3Client client = new Pop3Client())
                {
                    //Connecting to client
                    client.Connect(hostname, port, useSsl);

                    //Authenticating user / login(using 'recent:' due to Gmail is syncing sesson vice. if it is not present, it will only read
                    //the mails the first time, and will not find them afterwards)
                    client.Authenticate("recent:" + username, password);
                    
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
        
        /// <summary>
        /// "smtp.gmail.com", 587, true,
        /// </summary>
        /// <param name="Hostname"> Hostname gmail: pop.gmail.com / Hostname outlook: pop3.live.com </param>
        /// <param name="SslPort"> TLS port: 587 </param>
        /// <param name="UseSsl"> use SSL: true </param>
        /// <param name="SendTo"> Email address receiver </param>
        /// <param name="subject"> Email subject </param>
        /// <param name="EmailContent"> main email content / Body </param>
        /// <param name="username1"> Username for sender </param>
        /// <param name="password1"> Password for sender.</param>
        public static void sendMail(string Hostname, int Port, bool UseSsl, string SendTo, string subject, string EmailContent, string username1, string password1)
        {
            if (string.IsNullOrWhiteSpace(Hostname))
            {
                throw new ArgumentException("Hostname cannot be empty, null or only contain whitespaces");
            }
            else if (Port != 587)
            {
                throw new IndexOutOfRangeException("SSL/TLS port has to be 587! port is: " + Port);
            }
            else if (UseSsl == false)
            {
                throw new ArgumentException("UseSsl must be set true, else it won't connect.");
            }
            else if (!Regex.IsMatch(SendTo,
                @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
            {
                throw new FormatException("Not a valid email");
            }
            else
            {
                var message = new MailMessage(username1, SendTo);
                message.Subject = subject;
                message.Body = EmailContent;
                using (SmtpClient sender = new SmtpClient(Hostname, Port))
                {
                    sender.Credentials = new NetworkCredential(username1, password1);
                    sender.EnableSsl = UseSsl;
                    sender.Send(message);
                }
            }
        }
        
        /// <summary>
        /// method to return the body of the emails
        /// </summary>
        /// <param name="MessageIndex"> Integer to define which body to fetch </param>
        /// <param name="InboxOrSent"> List - sent or inbox </param>
        /// <returns>Email body, in plain text </returns>
        public static string Body(int MessageIndex, List<Message> InboxOrSent)
        {
            string body = "";
            if (InboxOrSent.Count < MessageIndex)
            {
                throw new IndexOutOfRangeException("the number is too big for the list.");
            }
            else if (InboxOrSent.Count > MessageIndex)
            {
                
                Message getBodyFromMessage = InboxOrSent[MessageIndex];

                OpenPop.Mime.MessagePart bodyPart = getBodyFromMessage.FindFirstHtmlVersion();
                if (bodyPart != null)
                {
                    body = bodyPart.GetBodyAsText();
                }
                else
                {
                    bodyPart = getBodyFromMessage.FindFirstPlainTextVersion();
                    if (bodyPart != null)
                    {
                        body = bodyPart.GetBodyAsText();
                    }
                }
            }
            
            return body;
        }
    }
}
