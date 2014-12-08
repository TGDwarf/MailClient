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

        public static string UserEmail;

        public static string UserPassword;

        public static string UserEmailProvider;

        public static List<Tuple<string, string>> listofhostprovidertuples = new List<Tuple<String, String>>();

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
            listofhostprovidertuples.Add(new Tuple<String, String>("Gmail", "pop.gmail.com"));
            listofhostprovidertuples.Add(new Tuple<String, String>("Outlook", "pop3.live.com"));
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
            UserEmail = tbUserEmail.Text;

            UserPassword = pbUserPassword.Password;

            UserEmailProvider = cbEmailProvider.SelectedValue.ToString();

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
