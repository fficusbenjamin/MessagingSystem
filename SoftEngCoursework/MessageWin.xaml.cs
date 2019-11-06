using Data;
using MessageObject;
using System;
using System.Collections.Generic;
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
            System.Text.RegularExpressions.Regex rID = new System.Text.RegularExpressions.Regex(@"^[SET]");

            try
            {
                isEntryValid = true;
                if (_hdrTxt.Text == "")
                {
                    isEntryValid = false;
                    throw new ArgumentException("Field cannot be blank", "Message Header/ID");
                } else
                if (!rID.IsMatch(_hdrTxt.Text)) 
                {
                    isEntryValid = false;
                    throw new ArgumentException("ID should start with S,E or T", "Message Header/ID");
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
                    //System.Console.WriteLine("SMS");
                    break;
                case "E":
                    factory = new EmailFactory(typeChoice);
                    //System.Console.WriteLine("Email");
                    break;
                case "T":
                    factory = new TweetFactory(typeChoice);
                    //System.Console.WriteLine("Tweet");
                    break;
                default:
                    break;
            }

            Message message = factory.GetMessageType();
            message.ID = idInput;
            message.MessageText = bodyInput;
            sendMessage.add(message);
            System.Console.WriteLine(typeChoice + "arrivato fino a qui");
        }

        

        private void _hdrTxt_GotFocus(object sender, RoutedEventArgs e)
        {
            _hdrTxt.Clear();
        }

        private void _hdrTxt_LostFocus(object sender, RoutedEventArgs e)
        {
           // _hdrTxt.Undo();
        }

        private void _bdyTxt_LostFocus(object sender, RoutedEventArgs e)
        {
            //_bdyTxt.Undo();
        }

        private void _bdyTxt_GotFocus(object sender, RoutedEventArgs e)
        {
            _bdyTxt.Clear();
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
                addMessage();
                System.Console.WriteLine(_sendMessage.createFile());
            }
        }


        


    }
}
