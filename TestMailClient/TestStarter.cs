using System;
using MailClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestMailClient
{
    public partial class TestStarter
    {
        public static void updateUsers()
        {
            Users.username = "tgdxof@gmail.com";
            Users.password = "MailClient";
            Users.useSsl = true;
            Users.receiveHostname = "pop.gmail.com";
            Users.receivePort = 995;
            Users.sendHostname = "smtp.gmail.com";
            Users.sendPort = 587;
        }
        public static void updateUsersWrongReceiveport()
        {
            Users.username = "tgdxof@gmail.com";
            Users.password = "MailClient";
            Users.useSsl = true;
            Users.receiveHostname = "pop.gmail.com";
            Users.receivePort = 22;
            Users.sendHostname = "smtp.gmail.com";
            Users.sendPort = 587;
        }
        public static void updateUsersInvalidSSL()
        {
            Users.username = "tgdxof@gmail.com";
            Users.password = "MailClient";
            Users.useSsl = false;
            Users.receiveHostname = "pop.gmail.com";
            Users.receivePort = 995;
            Users.sendHostname = "smtp.gmail.com";
            Users.sendPort = 587;
        }
        public static void updateUsersEmptyReceiveHostname()
        {
            Users.username = "tgdxof@gmail.com";
            Users.password = "MailClient";
            Users.useSsl = true;
            Users.receiveHostname = "";
            Users.receivePort = 995;
            Users.sendHostname = "smtp.gmail.com";
            Users.sendPort = 587;
        }
        public static void updateUsersEmptySendHostname()
        {
            Users.username = "tgdxof@gmail.com";
            Users.password = "MailClient";
            Users.useSsl = true;
            Users.receiveHostname = "pop.gmail.com";
            Users.receivePort = 995;
            Users.sendHostname = "";
            Users.sendPort = 587;
        }
        public static void updateUsersInvalidSendPort()
        {
            Users.username = "tgdxof@gmail.com";
            Users.password = "MailClient";
            Users.useSsl = true;
            Users.receiveHostname = "pop.gmail.com";
            Users.receivePort = 995;
            Users.sendHostname = "smtp.gmail.com";
            Users.sendPort = 56;
        }
    }
}
