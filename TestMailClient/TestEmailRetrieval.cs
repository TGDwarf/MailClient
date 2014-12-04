using System;
using MailClient;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestMailClient
{
    [TestClass]
    public class TestEmailRetrieval
    {
        string username1 = "tgdxof@gmail.com";
        string password1 = "MailClient";

        [TestMethod]
        public void TestRetrieveEmails()
        {
            //This test is to verify that retrieving the Email list actually works
            List<OpenPop.Mime.Message> EmailList = OpenPopParser.getAllMessages("pop.gmail.com", 995, true, username1, password1);

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestRetrieveEmailsWrongHostname()
        {
            //testing Hostname is null, empty or only contains whitespaces.
            List<OpenPop.Mime.Message> EmailList = OpenPopParser.getAllMessages("", 995, true, username1, password1);

        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestRetrieveEmailsWithNoOrMissingSslPort()
        {
            //testing SslPort, if not 995, throw exception.
            List<OpenPop.Mime.Message> EmailList = OpenPopParser.getAllMessages("pop.gmail.com", 996, true, username1, password1);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestRetrieveEmailsWithDisabledSsl()
        {
            //testing UseSsl, if not true, throw exception.
            List<OpenPop.Mime.Message> EmailList = OpenPopParser.getAllMessages("pop.gmail.com", 995, false, username1, password1);

        }


    }
}
