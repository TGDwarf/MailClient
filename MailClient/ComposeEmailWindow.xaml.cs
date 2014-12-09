using System;
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

        public void SaveMail()
        {

        }

        private void btnEmailSend_Click(object sender, RoutedEventArgs e)
        {

        }

        private void imgEmailFormatOptions_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // TODO:
            //if (FormatOptions = 0)
            //{
            //    Add row to grid above icon line
            //}
            //else
            //{
            //    Remove row from grid
            //}
        }

        private void imgEmailAttach_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void imgEmailSmiley_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void imgEmailDiscard_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

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
