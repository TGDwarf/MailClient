using System;
using MailClient;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestMailClient
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestRetrieveEmails()
        {
            List<OpenPop.Mime.Message> EmailList = OpenPopParser.getAllMessages("pop.gmail.com", 995, true, "tgdxof@gmail.com", "MailClient");

        }
        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestRetrieveEmailsWrongHostname()
        {
            List<OpenPop.Mime.Message> EmailList = OpenPopParser.getAllMessages("", 995, true, "tgdxof@gmail.com", "MailClient");

        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestRetrieveEmailsWithNoOrMissingSSL()
        {
            List<OpenPop.Mime.Message> EmailList = OpenPopParser.getAllMessages("pop.gmail.com", 996, true, "tgdxof@gmail.com", "MailClient");

        }



    }
}
