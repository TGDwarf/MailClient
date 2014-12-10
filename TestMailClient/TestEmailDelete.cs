using System;
using MailClient;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestMailClient
{
    [TestClass]
    public class TestEmailDelete
    {
        /// <summary>
        /// delete a email
        /// </summary>
        [TestMethod]
        public void TestDeleteOfEmail()
        {
            //populates the usersclass with information
            TestStarter.updateUsers();
            //create a list for for inbox
            List<OpenPop.Mime.Message> inbox = new List<OpenPop.Mime.Message>();
            //populating list with emails from server
            inbox = OpenPopParser.getIncommingOrSentMessages("incomming");
            //initializing Pop3Client as client
            OpenPop.Pop3.Pop3Client client = new OpenPop.Pop3.Pop3Client();
            //giving the client the login information from users class
            client.Connect(Users.receiveHostname, Users.receivePort, Users.useSsl);
            //Authenticating up against mailserver with login credentials
            if (Users.receiveHostname.Contains("gmail")) { client.Authenticate("recent:" + Users.username, Users.password); }
            else { client.Authenticate(Users.username, Users.password); }
            //invoke DeleteMessageByMessageId and disconnect / update mailserver if DeleteMessageByMessageId returns true
            if (OpenPopParser.DeleteMessageByMessageId(client, inbox[0].Headers.MessageId))
            {
                client.Disconnect();
            }
        }
        /// <summary>
        /// delete multible emails
        /// </summary>
        [TestMethod]
        public void TestDeleteOfMultibleEmail()
        {
            //populates the usersclass with information
            TestStarter.updateUsers();
            //create lists for inbox and for messages to be deleted.
            List<OpenPop.Mime.Message> inbox = new List<OpenPop.Mime.Message>();
            List<string> messageIds = new List<string>();
            //populate inbox with emails
            inbox = OpenPopParser.getIncommingOrSentMessages("incomming");
            //populate messageIds with the specific messageid in order to delete them.
            messageIds.Add(inbox[0].Headers.MessageId);
            messageIds.Add(inbox[1].Headers.MessageId);
            messageIds.Add(inbox[2].Headers.MessageId);
            messageIds.Add(inbox[3].Headers.MessageId);
            messageIds.Add(inbox[4].Headers.MessageId);
            //initializing Pop3Client as client
            OpenPop.Pop3.Pop3Client client = new OpenPop.Pop3.Pop3Client();
            //run through messageIds in order to delete them
            foreach (var item in messageIds)
            {
                //giving the client the login information from users class
                client.Connect(Users.receiveHostname, Users.receivePort, Users.useSsl);
                //Authenticating up against mailserver with login credentials
                if (Users.receiveHostname.Contains("gmail")) { client.Authenticate("recent:" + Users.username, Users.password); }
                else { client.Authenticate(Users.username, Users.password); }
                //invoke DeleteMessageByMessageId and disconnect / update mailserver if DeleteMessageByMessageId returns true
                if (OpenPopParser.DeleteMessageByMessageId(client, item))
                {
                    client.Disconnect();
                }
            }
        }
    }
}
