using OpenPop.Mime;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MailClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        //Declaration of all the methods to be used in the class.
        public static string programName = "MailClient";

        public static double programVersion = 0.2;

        public List<Message> allEmails = new List<Message>();

        public List<Message> allIncomingEmails = new List<Message>();

        public List<Message> allOutGoingEmails = new List<Message>();

        public List<Message> Drafts = new List<Message>();

        public List<Message> Spam = new List<Message>();

        public List<Message> Trash = new List<Message>();

        public MainWindow()
        {
            InitializeComponent();

            this.Width = System.Windows.SystemParameters.WorkArea.Width;
            this.Height = System.Windows.SystemParameters.WorkArea.Height;

            GetAllMail();

            Mailview_DataGrid.ItemsSource = allIncomingEmails;
        }

        /// <summary>
        /// Gets all the mails from the currently selected mail account via the method located in OpenPopParser, see for info about variables.
        /// </summary>
        private void GetAllMail()
        {
            allEmails = OpenPopParser.getAllMessages();
            foreach (Message item in allEmails)
            {
                if (item.Headers.From.ToString() != Users.username)
                {
                    allIncomingEmails.Add(item);
                }
                else
                {
                    allOutGoingEmails.Add(item);
                }
            }

        }

        private void lblCompose_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Mailview_DataGrid.Visibility = Visibility.Hidden;
            ComposeEmailWindow cew = new ComposeEmailWindow();
            cew.Show();
            cew.WindowStartupLocation = WindowStartupLocation.Manual;
            cew.Left = (this.Left + this.Width) - cew.Width;
            cew.Top = (this.Top + this.Height) - cew.Height;
        }

        private void lblInbox_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Mailview_DataGrid.Visibility = Visibility.Visible;
            Mailview_DataGrid.ItemsSource = allIncomingEmails;
        }

        private void lblSentMail_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Mailview_DataGrid.Visibility = Visibility.Visible;
            Mailview_DataGrid.ItemsSource = allOutGoingEmails;
        }

        private void lblDrafts_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Mailview_DataGrid.Visibility = Visibility.Visible;
            Mailview_DataGrid.ItemsSource = Drafts;
        }

        private void lblSpam_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Mailview_DataGrid.Visibility = Visibility.Visible;
            Mailview_DataGrid.ItemsSource = Spam;
        }

        private void lblTrash_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Mailview_DataGrid.Visibility = Visibility.Visible;
            Mailview_DataGrid.ItemsSource = Trash;
        }

        //private void Mailview_DataGrid_AddItems()
        //{
        //    foreach (Message item in messages)
        //    {
        //        if (item.Headers.From.ToString() != LoginWindow.UserEmail)
        //        {
        //            Mailview_DataGrid.Items.Add(item) {Mailview_DataGrid   = item.Headers.From.ToString() };
        //            Mailview_DataGrid.Items.Add(item);
        //            item.Headers.From.ToString(); //senders
        //            item.Headers.Subject.ToString(); // subject
        //            item.Headers.DateSent.ToString(); // tid afstendt
        //            List<MessagePart> atttachment = item.FindAllAttachments();
        //            foreach (MessagePart att in atttachment)
        //            {
        //                string filename = att.FileName;
        //                string contentType = att.ContentType.MediaType;
        //                byte[] content = att.Body;
        //            }
        //        }
        //    }

        //}

        //private void Mailview_Datagrid_AddColumns()
        //{
        //    DataGridCheckBoxColumn checkboxColumn = new DataGridCheckBoxColumn();
        //    checkboxColumn.Header = "Checkbox";
        //    checkboxColumn.Binding = new Binding("Checkbox");
        //    checkboxColumn.Width = 25;
        //    Mailview_DataGrid.Columns.Add(checkboxColumn);

        //    DataGridTextColumn starColumn = new DataGridTextColumn();
        //    starColumn.Header = "Star";
        //    starColumn.Binding = new Binding("Star");
        //    starColumn.Width = 25;
        //    Mailview_DataGrid.Columns.Add(starColumn);

        //    DataGridTextColumn etiketColumn = new DataGridTextColumn();
        //    etiketColumn.Header = "Etiket";
        //    etiketColumn.Width = 25;

        //    Style style = new Style();
        //    style.TargetType = typeof(DataGridCell);

        //    DataTrigger DT = new DataTrigger();
        //    Binding DataTriggerBinding = new Binding("Etiket");
        //    DataTriggerBinding.Mode = BindingMode.Default;
        //    DataTriggerBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
        //    DT.Binding = DataTriggerBinding;
        //    DT.Value = null;
        //    Setter DataTriggerSetter = new Setter();
        //    DataTriggerSetter.Property = DataGridCell.BackgroundProperty;
        //    DataTriggerSetter.Value = Brushes.LightGreen;
        //    DT.Setters.Add(DataTriggerSetter);
        //    style.Triggers.Add(DT);

        //    etiketColumn.CellStyle = style;

        //    Mailview_DataGrid.Columns.Add(etiketColumn);

        //    DataGridTextColumn senderColumn = new DataGridTextColumn();
        //    senderColumn.Header = "From";
        //    senderColumn.Binding = new Binding(Email.From);
        //    senderColumn.Width = 100;
        //    Mailview_DataGrid.Columns.Add(senderColumn);

        //    DataGridTextColumn subjectColumn = new DataGridTextColumn();
        //    subjectColumn.Header = "Subject";
        //    subjectColumn.Binding = new Binding(Email.Subject);
        //    subjectColumn.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
        //    Mailview_DataGrid.Columns.Add(subjectColumn);

        //    DataGridCheckBoxColumn attachmentColumn = new DataGridCheckBoxColumn();
        //    attachmentColumn.Header = "Attachment";
        //    attachmentColumn.Binding = new Binding(CheckForAttachment);
        //    attachmentColumn.Width = 25;
        //    Mailview_DataGrid.Columns.Add(attachmentColumn);

        //    DataGridTextColumn timeColumn = new DataGridTextColumn();
        //    timeColumn.Header = "Time";
        //    timeColumn.Binding = new Binding(".");
        //    timeColumn.Width = 75;
        //    Mailview_DataGrid.Columns.Add(timeColumn);
        //}

    }

    public class IntToImageConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ImageSource result = null;
            Message msg = (Message)value;
            //var boolValue = msg.MessagePart.IsAttachment;

            if (msg.FindAllAttachments().Count > 0)
            {
                //result = new BitmapImage(new Uri(@"D:\Google Drive\Projects\Programming\MailClient\MailClient\MailClient\Images\PaperClip.png"));
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
