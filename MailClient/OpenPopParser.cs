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
    public partial class OpenPopParser
    {
        /// <summary>
        /// Method to fetch email from mailserver, uses argument in order to sort sent and incomming.
        /// </summary>
        /// <param name="incommingOrSent"> use "incomming", "sent" or "all" in order to fetch the selected emails. </param>
        /// <returns> a list of emails, either sent or incomming.</returns>
        public static List<Message> getIncommingOrSentMessages(string incommingOrSent)
        {
            if (string.IsNullOrWhiteSpace(Users.receiveHostname))
            {
                throw new ArgumentException("Hostname cannot be empty, null or only contain whitespaces");
            }
            else if (Users.receivePort != 995)
            {
                throw new IndexOutOfRangeException("SSL port has to be 995 port is: " + Users.receivePort);
            }
            else if (Users.useSsl == false)
            {
                throw new ArgumentException("UseSsl must be set true, else it won't connect.");
            }
            else
            {
                //Using "using" for auto disposal og object when finished.
                using (Pop3Client client = new Pop3Client())
                {
                    //Connecting to client
                    client.Connect(Users.receiveHostname, Users.receivePort, Users.useSsl);

                    //Authenticating user / login(using 'recent:' due to Gmail is syncing sesson vice. if it is not present, it will only read
                    //the mails the first time, and will not find them afterwards)
                    if (Users.receiveHostname.Contains("gmail")) { client.Authenticate("recent:" + Users.username, Users.password); }
                    else { client.Authenticate(Users.username, Users.password); }
                    
                    //count the number of mail on the server.
                    int messageCount = client.GetMessageCount();

                    //get mails to client from server
                    List<Message> IncommingOrSentMessages = new List<Message>(messageCount);

                    //run through all messages backwards, in order to make the newest email appear in top
                    for (int i = messageCount; i > 0; i--)
                    {
                        //sort: emails to return only incomming / Inbox
                        if (incommingOrSent.ToLower() == "incomming")
                        {
                            if (client.GetMessage(i).Headers.To[0].ToString() == Users.username)
                            {
                                IncommingOrSentMessages.Add(client.GetMessage(i));
                            }
                        }
                        //sort: emails to return only sent
                        else if (incommingOrSent.ToLower() == "sent")
                        {
                            if (client.GetMessage(i).Headers.From.ToString() == Users.username)
                            {
                                IncommingOrSentMessages.Add(client.GetMessage(i));
                            }
                        }
                        //sort: emails to return all emails
                        else if (incommingOrSent.ToLower() == "all")
                        {
                            IncommingOrSentMessages.Add(client.GetMessage(i));
                        }
                    }
                    //returns the email list.
                    return IncommingOrSentMessages;
                }
            }
        }
        
        /// <summary>
        /// method to send mails.
        /// </summary>
        /// <param name="SendTo"> Email address receiver </param>
        /// <param name="subject"> Email subject </param>
        /// <param name="EmailContent"> main email content / Body </param>
        public static void sendMail(string SendTo, string subject, string EmailContent)
        {
            if (string.IsNullOrWhiteSpace(Users.sendHostname))
            {
                throw new ArgumentException("Hostname cannot be empty, null or only contain whitespaces");
            }
            else if (Users.sendPort != 587 && Users.sendPort != 465)
            {
                throw new IndexOutOfRangeException("SSL/TLS port has to be 587 for gmail and 465 for outlook! port is: " + Users.sendPort);
            }

            else if (Users.useSsl == false)
            {
                throw new ArgumentException("UseSsl must be set true, else it won't connect.");
            }
            //Regex used to filter email, only valid goes through
            else if (!Regex.IsMatch(SendTo,
                @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
            {
                throw new FormatException("Not a valid email");
            }
            else
            {
                //Initializes MailMessage and filling it with information about the email
                var message = new MailMessage(Users.username, SendTo);
                message.Subject = subject;
                message.Body = EmailContent;
                //initializes smtpclient, with the mailserver information
                using (SmtpClient sender = new SmtpClient(Users.sendHostname, Users.sendPort))
                {
                    //giving sender login credentials, and the email specifications
                    sender.Credentials = new NetworkCredential(Users.username, Users.password);
                    sender.EnableSsl = Users.useSsl;
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
                //specifies which message to get the body from
                Message getBodyFromMessage = InboxOrSent[MessageIndex];
                //casting messagepart in order to fetch the body from the email(message)
                OpenPop.Mime.MessagePart bodyPart = getBodyFromMessage.FindFirstHtmlVersion();
                if (bodyPart != null)
                {
                    //cast body as html
                    body = bodyPart.GetBodyAsText();
                }
                else
                {
                    bodyPart = getBodyFromMessage.FindFirstPlainTextVersion();
                    if (bodyPart != null)
                    {
                        //cast body as plain text
                        body = bodyPart.GetBodyAsText();
                    }
                }
            }
            //return body as string
            return body;
        }

        /// <summary>
        /// Delete messages.
        /// </summary>
        /// <param name="client"> object with login information (hostname, Port, UseSsl, username, password.) </param>
        /// <param name="messageIds"></param>
        /// <returns></returns>
        public static bool DeleteMessageByMessageId(Pop3Client client, string messageId)
        {
            // Get the number of messages on the POP3 server
            int messageCount = client.GetMessageCount();
            
            // Run through each of these messages and download the headers
            for (int messageItem = messageCount; messageItem > 0; messageItem--)
            {
                // If the Message ID of the current message is the same as the parameter given, delete that message
                if (client.GetMessageHeaders(messageItem).MessageId == messageId)
                {
                    // Delete
                    client.DeleteMessage(messageItem);
                    return true;
                }
            }
            // We did not find any message with the given messageId, report this back
            return false;
        }
    }
}
