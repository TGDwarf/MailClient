using System;
using MailClient;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestMailClient
{
    [TestClass]
    public class TestSendEmails
    {
        string username1 = "tgdxof@gmail.com";
        string password1 = "MailClient";
        string subject = "*Subject* this is a test";
        string EmailContent = "Email content, something, something, something, something, something\nsomething, something, something";

        [TestMethod]
        public void TestSendEmail()
        {
            OpenPopParser.sendMail("smtp.gmail.com", 587, true, "sniffern@msn.com", subject, EmailContent, username1, password1);
        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestSendEmailWithWrongPort()
        {
            OpenPopParser.sendMail("smtp.gmail.com", 583, true, "sniffern@msn.com", subject, EmailContent, username1, password1);
        }
        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void TestSendEmailWithWrongSSLTLS()
        {
            OpenPopParser.sendMail("smtp.gmail.com", 587, false, "sniffern@msn.com", subject, EmailContent, username1, password1);
        }
        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestSendEmailWithInvalidSendToAddress()
        {
            OpenPopParser.sendMail("smtp.gmail.com", 587, true, "sniffernmsn.com", subject, EmailContent, username1, password1);
        }
    }
}
