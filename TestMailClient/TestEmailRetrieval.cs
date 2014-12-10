using System;
using MailClient;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestMailClient
{
    [TestClass]
    public class TestEmailRetrieval
    {
        [TestMethod]
        public void TestRetrieveEmails()
        {
            TestStarter.updateUsers();
            //This test is to verify that retrieving the Email list actually works
            List<OpenPop.Mime.Message> allMessages = new List<OpenPop.Mime.Message>();
            allMessages = OpenPopParser.getAllMessages();
            foreach (var message in allMessages)
            {
                if (message.Headers.From.ToString() == Users.username)
                {
                    string from = message.Headers.From.ToString();
                    string subject = message.Headers.Subject.ToString();
                    OpenPop.Mime.MessagePart body = message.FindFirstHtmlVersion();
                    if (body != null)
                    {
                        string Body = body.GetBodyAsText();
                    }
                    else
                    {
                        body = message.FindFirstPlainTextVersion();
                        if (body != null)
                        {
                            string Body = body.GetBodyAsText();
                        }
                    }
                }
            }
         
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestRetrieveEmailsWrongHostname()
        {
            TestStarter.updateUsersEmptyReceiveHostname();
            //testing Hostname is null, empty or only contains whitespaces.
            List<OpenPop.Mime.Message> EmailList = OpenPopParser.getAllMessages();

        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestRetrieveEmailsWithNoOrMissingSslPort()
        {
            TestStarter.updateUsersWrongReceiveport();
            //testing SslPort, if not 995, throw exception.
            List<OpenPop.Mime.Message> EmailList = OpenPopParser.getAllMessages();

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestRetrieveEmailsWithDisabledSsl()
        {
            TestStarter.updateUsersInvalidSSL();
            //testing UseSsl, if not true, throw exception.
            List<OpenPop.Mime.Message> EmailList = OpenPopParser.getAllMessages();

        }

        [TestMethod]
        public void TestGetBody()
        {
            TestStarter.updateUsers();
            List<OpenPop.Mime.Message> Inbox = new List<OpenPop.Mime.Message>();
            List<OpenPop.Mime.Message> allMessages = new List<OpenPop.Mime.Message>();
            allMessages = OpenPopParser.getAllMessages();
            foreach (var message in allMessages)
            {
                if (message.Headers.From.ToString() != Users.username)
                {
                    Inbox.Add(message);
                }
            }
            string subby = allMessages[0].Headers.Subject.ToString();
            string BodyIndex = OpenPopParser.Body(0, Inbox);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestGetBodyWithTooBigIndex()
        {
            TestStarter.updateUsers();
            List<OpenPop.Mime.Message> Inbox = new List<OpenPop.Mime.Message>();
            List<OpenPop.Mime.Message> allMessages = new List<OpenPop.Mime.Message>();
            allMessages = OpenPopParser.getAllMessages();
            foreach (var message in allMessages)
            {
                if (message.Headers.From.ToString() != Users.username)
                {
                    Inbox.Add(message);
                }
            }
            string BodyIndex = OpenPopParser.Body(allMessages.Count + 2, Inbox);
        }
    }
}
