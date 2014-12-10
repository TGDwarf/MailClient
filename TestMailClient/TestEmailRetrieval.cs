using System;
using MailClient;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestMailClient
{
    [TestClass]
    public class TestEmailRetrieval
    {
        /// <summary>
        /// Test method to retreive emails from server.
        /// </summary>
        [TestMethod]
        public void TestRetrieveEmails()
        {
            //populating users class
            TestStarter.updateUsers();
            // create a list for all incomming mails
            List<OpenPop.Mime.Message> incommingMessages = new List<OpenPop.Mime.Message>();
            //populating list
            incommingMessages = OpenPopParser.getIncommingOrSentMessages("incomming");
            //run through all the messages
            foreach (var message in incommingMessages)
            {
                // strings to verify the emails
                string from = message.Headers.From.ToString();
                string to = message.Headers.To[0].ToString();
                string subject = message.Headers.Subject.ToString();
                string date = message.Headers.DateSent.ToString();
            }
         
        }
        /// <summary>
        /// Test method to retreive emails from server. -- uses invalid Hostname in users class
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestRetrieveEmailsWrongHostname()
        {
            //populates the usersclass with information
            TestStarter.updateUsersEmptyReceiveHostname();
            //testing Hostname is null, empty or only contains whitespaces.
            List<OpenPop.Mime.Message> EmailList = OpenPopParser.getIncommingOrSentMessages("incomming");

        }
        /// <summary>
        /// Test method to retreive emails from server. -- uses invalid Sslport in users class
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestRetrieveEmailsWithNoOrMissingSslPort()
        {
            //populates the usersclass with information
            TestStarter.updateUsersWrongReceiveport();
            //testing SslPort, if not 995, throw exception.
            List<OpenPop.Mime.Message> EmailList = OpenPopParser.getIncommingOrSentMessages("incomming");

        }
        /// <summary>
        /// Test method to retreive emails from server.  -- uses invalid SSL (set to false) in users class
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestRetrieveEmailsWithDisabledSsl()
        {
            //populates the usersclass with information
            TestStarter.updateUsersInvalidSSL();
            //testing UseSsl, if not true, throw exception.
            List<OpenPop.Mime.Message> EmailList = OpenPopParser.getIncommingOrSentMessages("incomming");

        }
        /// <summary>
        /// Test method to retreive the body string format from a email
        /// </summary>
        [TestMethod]
        public void TestGetBody()
        {
            //populates the usersclass with information
            TestStarter.updateUsers();
            //creating the list for all incomming email
            List<OpenPop.Mime.Message> Inbox = new List<OpenPop.Mime.Message>();
            //populating the list with the inbox
            Inbox = OpenPopParser.getIncommingOrSentMessages("incomming");
            // strings to verify the emails
            string from = Inbox[0].Headers.From.ToString();
            string to = Inbox[0].Headers.To[0].ToString();
            string subject = Inbox[0].Headers.Subject.ToString();
            string date = Inbox[0].Headers.DateSent.ToString();
            string BodyIndex = OpenPopParser.Body(0, Inbox);
        }
        /// <summary>
        /// Test method to retreive the body string format from a email -- Uses a too big index 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestGetBodyWithTooBigIndex()
        {
            //populates the usersclass with information
            TestStarter.updateUsers();
            //creating list for emails
            List<OpenPop.Mime.Message> Inbox = new List<OpenPop.Mime.Message>();
            //populating the list with emails.
            Inbox = OpenPopParser.getIncommingOrSentMessages("incomming");
            // try getting body with wrong index - resolves in IndexOutOfRangeException
            string BodyIndex = OpenPopParser.Body(Inbox.Count + 2, Inbox);
        }
    }
}
