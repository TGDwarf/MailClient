using System;
using MailClient;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestMailClient
{
    [TestClass]
    public class TestSendEmails
    {
        string subject = "*Subject* this is a test";
        string EmailContent = "Email content, something, something, something, something, something\nsomething, something, something";

        [TestMethod]
        public void TestSendEmail()
        {
            TestStarter.updateUsers();
            OpenPopParser.sendMail("sniffern@msn.com", subject, EmailContent);
        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestSendEmailWithWrongPort()
        {
            TestStarter.updateUsersInvalidSendPort();
            OpenPopParser.sendMail("sniffern@msn.com", subject, EmailContent);
        }
        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void TestSendEmailWithWrongSSLTLS()
        {
            TestStarter.updateUsersInvalidSSL();
            OpenPopParser.sendMail("sniffern@msn.com", subject, EmailContent);
        }
        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestSendEmailWithInvalidSendToAddress()
        {
            TestStarter.updateUsers();
            OpenPopParser.sendMail("sniffernmsn.com", subject, EmailContent);
        }
    }
}
