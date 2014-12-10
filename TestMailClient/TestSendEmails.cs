using System;
using MailClient;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestMailClient
{
    [TestClass]
    public class TestSendEmails
    {
        //
        string subject = "*Subject* this is a test";
        string emailContent = "Email content, something, something, something, something, something\nsomething, something, something";
        string receivingEmail = "tgdxof@hotmail.com";
        string invalidEmail = "MailClientlortemail.dk";
        /// <summary>
        /// testmethod to send emails
        /// </summary>
        [TestMethod]
        public void TestSendEmail()
        {
            //populates the usersclass with information
            TestStarter.updateUsers();
            //send a mail 
            OpenPopParser.sendMail(receivingEmail, subject, emailContent);
        }
        /// <summary>
        /// testmethod to send emails, this uses the wrong port
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestSendEmailWithWrongPort()
        {
            //populates the usersclass with information
            TestStarter.updateUsersInvalidSendPort();
            //try to send mail with a wrong ssl port
            OpenPopParser.sendMail(receivingEmail, subject, emailContent);
        }
        /// <summary>
        /// testmethod to send emails, does not use SSL /TLS
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestSendEmailWithWrongSSLTLS()
        {
            //populates the usersclass with information
            TestStarter.updateUsersInvalidSSL();
            //trying to send mail with a "false" in useSsl
            OpenPopParser.sendMail(receivingEmail, subject, emailContent);
        }
        /// <summary>
        /// testmethod to send emails, uses a email which is invalid.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestSendEmailWithInvalidSendToAddress()
        {
            //populates the usersclass with information
            TestStarter.updateUsers();
            //trying to send a email which is invalid.
            OpenPopParser.sendMail(invalidEmail, subject, emailContent);
        }
    }
}
