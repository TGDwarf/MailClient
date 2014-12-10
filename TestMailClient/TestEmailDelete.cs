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
            //giving the client the login information for 
            client.Connect(Users.receiveHostname, Users.receivePort, Users.useSsl);
            if (Users.receiveHostname.Contains("gmail")) { client.Authenticate("recent:" + Users.username, Users.password); }
            else { client.Authenticate(Users.username, Users.password); }
            

            bool result = OpenPopParser.DeleteMessageByMessageId(client, inbox[0].Headers.MessageId);
            client.Disconnect();
        }
        /// <summary>
        /// delete multible emails
        /// </summary>
        [TestMethod]
        public void TestDeleteOfMultibleEmail()
        {
            //populates the usersclass with information
            TestStarter.updateUsers();
            List<OpenPop.Mime.Message> inbox = new List<OpenPop.Mime.Message>();
            List<string> messageIds = new List<string>();

            inbox = OpenPopParser.getIncommingOrSentMessages("incomming");
            messageIds.Add(inbox[0].Headers.MessageId);
            messageIds.Add(inbox[1].Headers.MessageId);
            messageIds.Add(inbox[2].Headers.MessageId);
            messageIds.Add(inbox[3].Headers.MessageId);
            messageIds.Add(inbox[4].Headers.MessageId);

            OpenPop.Pop3.Pop3Client client = new OpenPop.Pop3.Pop3Client();
            foreach (var item in messageIds)
            {
                client.Connect(Users.receiveHostname, Users.receivePort, Users.useSsl);
                if (Users.receiveHostname.Contains("gmail")) { client.Authenticate("recent:" + Users.username, Users.password); }
                else { client.Authenticate(Users.username, Users.password); }
                if (OpenPopParser.DeleteMessageByMessageId(client, item))
                {
                    client.Disconnect();
                }
            }
        }
    }
}
