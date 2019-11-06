using Data;
using MessageObject;
using System;
using System.IO;
using System.Windows;

namespace SoftEngCoursework
{
    /// <summary>
    /// Interaction logic for MessageWin.xaml
    /// </summary>
    public partial class MessageWin : Window
    {
        private static MessageList sendMessage = new MessageList();
        public static MessageList _sendMessage { get { return sendMessage; } }
        private string idInput, bodyInput, typeInput, typeChoice;
        private bool isEntryValid;
        private string[] file = File.ReadAllLines(@"..\..\..\..\SoftEngCoursework\Messages.json");

        public MessageWin()
        {
            InitializeComponent();
        }

        

        private void validateEntry()
        {
            try
            {
                isEntryValid = true;
                if (_hdrTxt.Text == "")
                {
                    isEntryValid = false;
                    throw new ArgumentException("Field cannot be blank", "Message Header/ID");
                }

                idInput = _hdrTxt.Text;
                typeChoice = idInput.Substring(0, 1);
                System.Console.WriteLine(typeChoice);
            }
            catch (Exception execMsg)
            {
                MessageBox.Show(execMsg.Message);
            }

        }

        private void addMessage()
        {
            MessageFactory factory = null;
            switch (typeChoice)
            {
                case "S":
                    factory = new SmsFactory(typeChoice);
                    break;
                case "E":
                    factory = new EmailFactory(typeChoice);
                    break;
                case "T":
                    factory = new TweetFactory(typeChoice);
                    break;
                default:
                    break;
            }
        }


        private void _bckBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow newWin = new MainWindow();
            newWin.Show();
            this.Close();
        }

        private void _sndBtn_Click(object sender, RoutedEventArgs e)
        {
            validateEntry();
            if (isEntryValid == true)
            {
                //addMessage();
            }
        }


    }
}
