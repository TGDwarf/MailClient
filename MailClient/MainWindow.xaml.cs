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

		public List<Message> allEmails = new List<Message>();

		public List<Message> allIncomingEmails = new List<Message>();

		public List<Message> allOutGoingEmails = new List<Message>();

		public List<Message> Drafts = new List<Message>();

		public List<Message> Spam = new List<Message>();

		public List<Message> Trash = new List<Message>();

		private BackgroundWorker getMailWorker = new BackgroundWorker();

		public enum mailclientMenu {indbox, sentmail, drafts, spam, trash};

		public mailclientMenu mailclientmenu;

		public MainWindow()
		{
			InitializeComponent();

			this.Width = System.Windows.SystemParameters.WorkArea.Width;
			this.Height = System.Windows.SystemParameters.WorkArea.Height;

			GetAllMail();

			Mailview_DataGrid.ItemsSource = allIncomingEmails;

			StartUp();
		}


		private void StartUp()
		{
			getMailWorker.WorkerReportsProgress = true;
			getMailWorker.ProgressChanged += ProgressChanged;
			getMailWorker.DoWork += DoWork;
			getMailWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
			GetMailWorkerStart();
		}

        private void TestSendMail()
        {
            for (int i = 0; i < 130; i++)
            {
                string subject = "Dette er test mail Nr " + i;
                string emailindhold = "Her er en besked der også er i et for loop, dette er den " + i + "besked";
                OpenPopParser.sendMail("smtp.gmail.com", 587, true, LoginWindow.UserEmail, subject, emailindhold, LoginWindow.UserEmail, LoginWindow.UserPassword);
                Thread.Sleep(10);
            }

        }

		private void GetMailWorkerStart()
		{
			getMailWorker.RunWorkerAsync();
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{

		}

		private void DoWork(object sender, DoWorkEventArgs e)
		{
			for (int i = 0; i <= 9999; i++)
			{
				GetAllMail();
				getMailWorker.ReportProgress(i);
				Thread.Sleep(100);
			}
		}

		private void ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			switch (mailclientmenu)
			{
				case mailclientMenu.indbox:
					Mailview_DataGrid.ItemsSource = null;
					Mailview_DataGrid.ItemsSource = allIncomingEmails;
					break;
				case mailclientMenu.sentmail:
					Mailview_DataGrid.ItemsSource = null;
					Mailview_DataGrid.ItemsSource = allOutGoingEmails;
					break;
				case mailclientMenu.drafts:
					Mailview_DataGrid.ItemsSource = null;
					Mailview_DataGrid.ItemsSource = Drafts;
					break;
				case mailclientMenu.spam:
					Mailview_DataGrid.ItemsSource = null;
					Mailview_DataGrid.ItemsSource = Spam;
					break;
				case mailclientMenu.trash:
					Mailview_DataGrid.ItemsSource = null;
					Mailview_DataGrid.ItemsSource = Trash;
					break;
				default:
					break;
			}
			// This is called on the UI thread when ReportProgress method is called
		}

		private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			// This is called on the UI thread when the DoWork method completes
			// so it's a good place to hide busy indicators, or put clean up code
		}


		/// <summary>
		/// Gets all the mails from the currently selected mail account via the method located in OpenPopParser, see for info about variables.
		/// </summary>
		private void GetAllMail()
		{
			allEmails = OpenPopParser.getAllMessages(LoginWindow.UserEmailProvider, 995, true, LoginWindow.UserEmail, LoginWindow.UserPassword);
			allIncomingEmails.Clear();
			allOutGoingEmails.Clear();
			foreach (Message item in allEmails)
			{
				if (item.Headers.From.ToString() != LoginWindow.UserEmail)
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
			ComposeEmailWindow cew = new ComposeEmailWindow();
			cew.Show();
			cew.WindowStartupLocation = WindowStartupLocation.Manual;
			cew.Left = (this.Left + this.Width) - cew.Width;
			cew.Top = (this.Top + this.Height) - cew.Height;
		}

		private void lblInbox_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			Mailview_DataGrid.ItemsSource = allIncomingEmails;
			mailclientmenu = mailclientMenu.indbox;
		}

		private void lblSentMail_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			Mailview_DataGrid.ItemsSource = allOutGoingEmails;
			mailclientmenu = mailclientMenu.sentmail;
		}

		private void lblDrafts_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			Mailview_DataGrid.ItemsSource = Drafts;
			mailclientmenu = mailclientMenu.drafts;
		}

		private void lblSpam_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			Mailview_DataGrid.ItemsSource = Spam;
			mailclientmenu = mailclientMenu.spam;
		}

		private void lblTrash_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			Mailview_DataGrid.ItemsSource = Trash;
			mailclientmenu = mailclientMenu.trash;
		}
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
