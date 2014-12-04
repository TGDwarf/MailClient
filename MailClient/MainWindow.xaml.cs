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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MailClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Mailview_Datagrid_AddColumns();
            Mailview_DataGrid_AddItems();
        }

        private void Mailview_DataGrid_AddItems()
        {
            Mailview_DataGrid.Items.Add("Test");
        }

        private void Mailview_Datagrid_AddColumns()
        {
            DataGridTextColumn checkboxColumn = new DataGridTextColumn();
            checkboxColumn.Header = "Checkbox";
            checkboxColumn.Binding = new Binding("Checkbox");
            checkboxColumn.Width = 25;
            Mailview_DataGrid.Columns.Add(checkboxColumn);

            DataGridTextColumn starColumn = new DataGridTextColumn();
            starColumn.Header = "Star";
            starColumn.Binding = new Binding("Star");
            starColumn.Width = 25;
            Mailview_DataGrid.Columns.Add(starColumn);

            DataGridTextColumn etiketColumn = new DataGridTextColumn();
            etiketColumn.Header = "Etiket";
            etiketColumn.Binding = new Binding("Etiket");
            etiketColumn.Width = 25;
            Mailview_DataGrid.Columns.Add(etiketColumn);

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
