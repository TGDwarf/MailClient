﻿using OpenPop.Mime;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MailClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static string programName = "MailClient";

        public static double programVersion = 0.2;

        public List<Message> messages = new List<Message>();

        public MainWindow()
        {
            InitializeComponent();

            Mailview_Datagrid_AddColumns();

            GetAllMail();

            Mailview_DataGrid_AddItems();
        }

        private void GetAllMail()
        {
            messages = OpenPopParser.getAllMessages(LoginWindow.UserEmailProvider, 995, true, LoginWindow.UserEmail, LoginWindow.UserPassword);
        }

        private void Mailview_DataGrid_AddItems()
        {
            foreach (Message item in messages)
            {
                Mailview_DataGrid.Items.Add(item);
                item.Headers.From.ToString(); //senders
                item.Headers.Subject.ToString(); // subject
                item.Headers.DateSent.ToString(); // tid afstendt
                List<MessagePart> atttachment = item.FindAllAttachments();
                foreach (MessagePart att in atttachment)
                {
                    string filename = att.FileName;
                    string contentType = att.ContentType.MediaType;
                    byte[] content = att.Body;
                }
            }

        }

        private void Mailview_Datagrid_AddColumns()
        {
            DataGridCheckBoxColumn checkboxColumn = new DataGridCheckBoxColumn();
            checkboxColumn.Header = "Checkbox";
            checkboxColumn.Binding = new Binding("Checkbox");
            checkboxColumn.Width = 25;
            Mailview_DataGrid.Columns.Add(checkboxColumn);

            //DataGridTextColumn starColumn = new DataGridTextColumn();
            //starColumn.Header = "Star";
            //starColumn.Binding = new Binding("Star");
            //starColumn.Width = 25;
            //Mailview_DataGrid.Columns.Add(starColumn);

            //DataGridTextColumn etiketColumn = new DataGridTextColumn();
            //etiketColumn.Header = "Etiket";
            //etiketColumn.Width = 25;

            //Style style = new Style();
            //style.TargetType = typeof(DataGridCell);

            //DataTrigger DT = new DataTrigger();
            //Binding DataTriggerBinding = new Binding("Etiket");
            //DataTriggerBinding.Mode = BindingMode.Default;
            //DataTriggerBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            //DT.Binding = DataTriggerBinding;
            //DT.Value = null;
            //Setter DataTriggerSetter = new Setter();
            //DataTriggerSetter.Property = DataGridCell.BackgroundProperty;
            //DataTriggerSetter.Value = Brushes.LightGreen;
            //DT.Setters.Add(DataTriggerSetter);
            //style.Triggers.Add(DT);

            //etiketColumn.CellStyle = style; 

            //Mailview_DataGrid.Columns.Add(etiketColumn);

            DataGridTextColumn senderColumn = new DataGridTextColumn();
            senderColumn.Header = "Sender";
            senderColumn.Binding = new Binding("Sender");
            senderColumn.Width = 100;
            Mailview_DataGrid.Columns.Add(senderColumn);

            DataGridTextColumn subjectColumn = new DataGridTextColumn();
            subjectColumn.Header = "Subject";
            subjectColumn.Binding = new Binding("Subject");
            subjectColumn.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            Mailview_DataGrid.Columns.Add(subjectColumn);

            DataGridTextColumn attachmentColumn = new DataGridTextColumn();
            attachmentColumn.Header = "Attachment";
            attachmentColumn.Binding = new Binding("Attachment");
            attachmentColumn.Width = 25;
            Mailview_DataGrid.Columns.Add(attachmentColumn);

            DataGridTextColumn timeColumn = new DataGridTextColumn();
            timeColumn.Header = "Time";
            timeColumn.Binding = new Binding("Time");
            timeColumn.Width = 75;
            Mailview_DataGrid.Columns.Add(timeColumn);
        }
    }
}
