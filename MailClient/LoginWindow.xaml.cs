using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MailClient
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {

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
                cbEmailProvider.Items.Add(listofhostprovidertuples[i].Item1);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cbEmailProvider_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

    }
}
