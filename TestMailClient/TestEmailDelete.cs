using System;
using MailClient;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestMailClient
{
    [TestClass]
    public class TestEmailDelete
    {
        //[TestMethod]

        //public void TestDeleteOfEmail()
        //{
        //    TestStarter.updateUsers();
        //    List<OpenPop.Mime.Message> allMessages = new List<OpenPop.Mime.Message>();
        //    List<string> messageIds = new List<string>();
        //    allMessages = OpenPopParser.getIncommingOrSentMessages();

        //    OpenPop.Pop3.Pop3Client client = new OpenPop.Pop3.Pop3Client();
        //    client.Connect("pop.gmail.com",995,true);
        //    client.Authenticate("recent:" + Users.username, Users.password);
        //    bool result = OpenPopParser.DeleteMessageByMessageId(client, allMessages[0].Headers.MessageId);
        //    client.Disconnect();
        //}
        //[TestMethod]
        //public void TestDeleteOfMultibleEmail()
        //{
        //    TestStarter.updateUsers();
        //    List<OpenPop.Mime.Message> allMessages = new List<OpenPop.Mime.Message>();
        //    List<string> messageIds = new List<string>();

        //    allMessages = OpenPopParser.getAllMessages();
        //    messageIds.Add(allMessages[0].Headers.MessageId);
        //    messageIds.Add(allMessages[1].Headers.MessageId);
        //    messageIds.Add(allMessages[2].Headers.MessageId);
        //    messageIds.Add(allMessages[3].Headers.MessageId);
        //    messageIds.Add(allMessages[4].Headers.MessageId);

        //    OpenPop.Pop3.Pop3Client client = new OpenPop.Pop3.Pop3Client();
        //    foreach (var item in messageIds)
        //    {
        //        client.Connect("pop.gmail.com", 995, true);
        //        client.Authenticate("recent:" + Users.username, Users.password);
        //        if (OpenPopParser.DeleteMessageByMessageId(client, item))
        //        {
        //            client.Disconnect();
        //        }
        //    }
        //}
    }
}
