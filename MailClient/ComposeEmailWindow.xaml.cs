﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MailClient
{
    /// <summary>
    /// Interaction logic for ComposeEmailWindow.xaml
    /// </summary>
    public partial class ComposeEmailWindow : Window
    {
        public ComposeEmailWindow()
        {
            InitializeComponent();
        }

        public static void SaveMail()
        {

        }

        private void btnEmailSend_Click(object sender, RoutedEventArgs e)
        {
            //OpenPopParser.sendMail()
        }

        private void imgEmailFormatOptions_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // Format options for text
        }

        private void imgEmailAttach_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // POPup Window of selectfile
        }

        private void imgEmailSmiley_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //TODO Line of smileys
        }

        private void imgEmailDiscard_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            tbEmailTo.Clear();
            tbSubject.Clear();
            tbCc2.Clear();
            tbBc2.Clear();
            tbMessage.Clear();

            //TODO Delete local copy of email
        }

        private void lblCc_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CcRow.Height = GridLength.Auto;
        }

        private void lblBcc_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BccRow.Height = GridLength.Auto;
        }
    }
}
