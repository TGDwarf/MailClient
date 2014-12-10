using OpenPop.Mime;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
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

		public List<Message> currentEmailsList = new List<Message>();

		public List<Message> allEmails = new List<Message>();

		public List<Message> allIncomingEmails = new List<Message>();

		public List<Message> allOutGoingEmails = new List<Message>();

		public List<Message> Drafts = new List<Message>();

		public List<Message> Spam = new List<Message>();

		public List<Message> Trash = new List<Message>();

		private BackgroundWorker getMailWorker = new BackgroundWorker();

		public enum mailclientMenu {indbox, sentmail, drafts, spam, trash};

		public mailclientMenu mailclientmenu;

		/// <summary>
		/// Initializes components.
		/// Sets the height and width of the window to the working area of the user screen.
		/// Sets the inbox to be shown next time the backgroundworker gets mails.
		/// </summary>
		public MainWindow()
		{
			InitializeComponent();

			this.Width = System.Windows.SystemParameters.WorkArea.Width;
			this.Height = System.Windows.SystemParameters.WorkArea.Height;

			mailclientmenu = mailclientMenu.indbox;

			StartUp();
		}


		/// <summary>
		/// sets the currentemailslist used by the datagridrow double click method to allincomingemails, only used for the initial get loads
		/// This method initializes the backgroundworker "getMailWorker"
		/// </summary>
		private void StartUp()
		{
			getMailWorker.WorkerReportsProgress = true;
			getMailWorker.ProgressChanged += ProgressChanged;
			getMailWorker.DoWork += DoWork;
			getMailWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
			GetMailWorkerStart();
		}

		/// <summary>
		/// This method starts the backgroundworker
		/// </summary>
		private void GetMailWorkerStart()
		{
			getMailWorker.RunWorkerAsync();
		}

		/// <summary>
		/// This is the work to be done by the backgroundworker
		/// Gets all mail, reports progress to update GUI and sleeps for 100 miliseconds.
		/// Time could be increased to reduce resource requirement. Kept low for testing purposes.
		/// </summary>
		private void DoWork(object sender, DoWorkEventArgs e)
		{
			for (int i = 0; i <= 9999; i++)
			{
				GetAllMail();
				getMailWorker.ReportProgress(i);
				Thread.Sleep(100);
			}
		}

		/// <summary>
		/// This is called on the UI thread when ReportProgress method is called.
		/// Uses a switch to determine which list to display in the datagrid.
		/// </summary>
		private void ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			switch (mailclientmenu)
			{
				case mailclientMenu.indbox:
					Mailview_DataGrid.ItemsSource = null;
					currentEmailsList = allIncomingEmails;
					Mailview_DataGrid.ItemsSource = currentEmailsList;
					break;
				case mailclientMenu.sentmail:
					Mailview_DataGrid.ItemsSource = null;
					currentEmailsList = allOutGoingEmails;
					Mailview_DataGrid.ItemsSource = currentEmailsList;
					break;
				case mailclientMenu.drafts:
					Mailview_DataGrid.ItemsSource = null;
					currentEmailsList = Drafts;
					Mailview_DataGrid.ItemsSource = currentEmailsList;
					break;
				case mailclientMenu.spam:
					Mailview_DataGrid.ItemsSource = null;
					currentEmailsList = Spam;
					Mailview_DataGrid.ItemsSource = currentEmailsList;
					break;
				case mailclientMenu.trash:
					Mailview_DataGrid.ItemsSource = null;
					currentEmailsList = Trash;
					Mailview_DataGrid.ItemsSource = currentEmailsList;
					break;
				default:
					break;
			}
		}

		/// <summary>
		/// This is called on the UI thread when the DoWork method completes, currently does nothing but is included nontheless.
		/// </summary>
		private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			/// Include a call to start the worker again.
		}


		/// <summary>
		/// Gets all the mails from the currently selected mail account via the method located in OpenPopParser, see for info about variables.
		/// </summary>
		private void GetAllMail()
		{
			allIncomingEmails = OpenPopParser.getIncommingOrSentMessages("incomming");
			allOutGoingEmails = OpenPopParser.getIncommingOrSentMessages("sent");
		}

		/// <summary>
		/// When the label compose is clicked with the left mouse button, this method oppens a new window.
		/// The window is set to be in the lower right corner of the mainwindow, 
		/// this is determined by using the top and left position + mainwindow size, substracted by the compose window width and height
		/// </summary>
		private void lblCompose_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			ComposeEmailWindow cew = new ComposeEmailWindow();
			cew.Show();
			cew.WindowStartupLocation = WindowStartupLocation.Manual;
			cew.Left = (this.Left + this.Width) - cew.Width;
			cew.Top = (this.Top + this.Height) - cew.Height;
		}

		/// <summary>
		/// Sets the datagrid to show all incoming mails and makes the enum reflect this
		/// </summary>
		private void lblInbox_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			mailViewer.Visibility = Visibility.Hidden;
			currentEmailsList = allIncomingEmails;
			Mailview_DataGrid.ItemsSource = currentEmailsList;
			mailclientmenu = mailclientMenu.indbox;
		}

		/// <summary>
		/// Sets the datagrid to show all outgoing mails and makes the enum reflect this
		/// </summary>
		private void lblSentMail_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			mailViewer.Visibility = Visibility.Hidden;
			currentEmailsList = allOutGoingEmails;
			Mailview_DataGrid.ItemsSource = currentEmailsList;
			mailclientmenu = mailclientMenu.sentmail;
		}

		/// <summary>
		/// Sets the datagrid to show all drafts and makes the enum reflect this
		/// </summary>
		private void lblDrafts_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			mailViewer.Visibility = Visibility.Hidden;
			currentEmailsList = Drafts;
			Mailview_DataGrid.ItemsSource = currentEmailsList;
			mailclientmenu = mailclientMenu.drafts;
		}

		/// <summary>
		/// Sets the datagrid to show all spam mails and makes the enum reflect this
		/// </summary>
		private void lblSpam_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			mailViewer.Visibility = Visibility.Hidden;
			currentEmailsList = Spam;
			Mailview_DataGrid.ItemsSource = currentEmailsList;
			mailclientmenu = mailclientMenu.spam;
		}

		/// <summary>
		/// Sets the datagrid to show all trash and makes the enum reflect this
		/// </summary>
		private void lblTrash_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			mailViewer.Visibility = Visibility.Hidden;
			currentEmailsList = Trash;
			Mailview_DataGrid.ItemsSource = currentEmailsList;
			mailclientmenu = mailclientMenu.trash;
		}

		private void DataGridRow_DoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			var currentRowIndex = Mailview_DataGrid.Items.IndexOf(Mailview_DataGrid.CurrentItem);
			string html = OpenPopParser.Body(currentRowIndex, currentEmailsList);
			//Message message = (Message)Mailview_DataGrid.SelectedItem;
			//string html = message.MessagePart.GetBodyAsText();
			Mailview_DataGrid.Visibility = Visibility.Hidden;
			mailViewer.Visibility = Visibility.Visible;
			mailViewer.Navigate(html);
		}
	}

	/// <summary>
	/// Used by the datagrid to determine whether to show a imagefile indicating a attached file in the  mail
	/// </summary>
	public class IntToImageConverter : IValueConverter
	{

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			ImageSource result = null;
			Message msg = (Message)value;
			//var boolValue = msg.MessagePart.IsAttachment;

			if (msg.FindAllAttachments().Count > 0)
			{
				result = new BitmapImage(new Uri(@"D:\Google Drive\Projects\Programming\MailClient\MailClient\MailClient\Images\PaperClip.png"));
			}
			return result;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
