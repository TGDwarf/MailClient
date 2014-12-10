using System;
using MailClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestMailClient
{
    public partial class TestStarter
    {
        //poplulate users class with login information / server information
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
        //poplulate users class with login information / server information -- receivePort is invalid
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
        //poplulate users class with login information / server information -- useSsl is invalid
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
        //poplulate users class with login information / server information -- receiveHostname is invalid
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
        //poplulate users class with login information / server information -- sendHostname is invalid
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
        //poplulate users class with login information / server information --sendPort is invalid
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
