using System;
using System.Collections.Generic;
using System.Windows;

namespace MailClient
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {

        public static string UserEmailProvider;

        public static List<Tuple<string, string, int, string, int>> listofhostprovidertuples = new List<Tuple<string, string, int, string, int>>();

        public LoginWindow()
        {
            InitializeComponent();

            Startup();
        }

        private void Startup()
        {
            lblName.Content = MainWindow.programName + " " + MainWindow.programVersion;

            AddEmailProvidersToList();

            AddEmailProviderToCB();

        }

        private void AddEmailProvidersToList()
        {
            listofhostprovidertuples.Add(new Tuple<string, string, int, string,int>("Gmail", "pop.gmail.com", 995, "smtp.gmail.com", 587));
            listofhostprovidertuples.Add(new Tuple<string, string, int, string, int>("Outlook", "pop3.live.com", 995, "smtp.live.com", 465));
        }

        private void AddEmailProviderToCB()
        {
            for (int i = 0; i < listofhostprovidertuples.Count; i++)
            {
                cbEmailProvider.Items.Add(listofhostprovidertuples[i]);
            }

            cbEmailProvider.SelectedIndex = 0;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            //populating the user class with the login information to handle mail functions later on
            Users.username = tbUserEmail.Text;
            Users.password = pbUserPassword.Password;
            Users.receiveHostname = cbEmailProvider.SelectedValue.ToString();
            Users.useSsl = true;
            
            if (cbEmailProvider.SelectedIndex == 0)
            {
                Users.receivePort = listofhostprovidertuples[0].Item3;
                Users.sendHostname = listofhostprovidertuples[0].Item4;
                Users.sendPort = listofhostprovidertuples[0].Item5;
            }
            else if (cbEmailProvider.SelectedIndex == 1)
            {
                Users.receivePort = listofhostprovidertuples[1].Item3;
                Users.sendHostname = listofhostprovidertuples[1].Item4;
                Users.sendPort = listofhostprovidertuples[1].Item5;
            }
            

            if (tbUserEmail.Text.Length == 0)
            {
                MessageBoxResult error = MessageBox.Show("Enter Username");
                tbUserEmail.Focus();
            }
            else
            {
                MainWindow mw = new MainWindow();
                mw.Show();
                this.Close();
            }


        }

    }
}
