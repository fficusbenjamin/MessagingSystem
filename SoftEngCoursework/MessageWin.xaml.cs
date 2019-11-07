using Data;
using MessageObject;
using Newtonsoft.Json;
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
        //private string[] file = File.ReadAllLines(@"..\..\..\..\SoftEngCoursework\Messages.json");
        string fPth = @"..\..\..\..\SoftEngCoursework\Messages.json";

        public MessageWin()
        {
            InitializeComponent();
        }

        

        private void validateEntry()
        {
            System.Text.RegularExpressions.Regex rID = new System.Text.RegularExpressions.Regex(@"^[SET][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]");

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
                    throw new ArgumentException("ID should start with S,E or T followed by nine numbers", "Message Header/ID");
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
                    System.Console.WriteLine("SMS");
                    break;
                case "E":
                    factory = new EmailFactory(typeChoice);
                    System.Console.WriteLine("Email");
                    break;
                case "T":
                    factory = new TweetFactory(typeChoice);
                    System.Console.WriteLine("Tweet");
                    break;
                default:
                    break;
            }

            Message message = factory.GetMessageType();
            message.ID = idInput;
            //message.MessageText = bodyInput;
            sendMessage.add(message);
            System.Console.WriteLine(typeChoice + "arrivato fino a qui");
            _lstAllMessages.Items.Add("Header/ID: " + message.ID);
            wrtJson(message, sendMessage);
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

        private void _lstAllMessages_Loaded(object sender, RoutedEventArgs e)
        {
            showList(sendMessage.messageList);
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
                string output = JsonConvert.SerializeObject(_sendMessage.messageList);
                //Product deserializedProduct = JsonConvert.DeserializeObject<Product>(output);

            }
        }

        private void showList(List<Message> list)
        {
            //creates an empty string
            string line;
            //creates a variable with the content of the .csv file
            var file = new System.IO.StreamReader(@"..\..\..\..\SoftEngCoursework\Messages.json");
            //while loop to check if the string is different than null
            while ((line = file.ReadLine() ) != null)
            {
                //add the string in the form listbox
                 _lstAllMessages.Items.Add(line);
            }
        }

        private void wrtJson(Message message, MessageList messageList) 
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };

            if (File.Exists(fPth) != true) 
            {
                File.Create(fPth);
            }
            string jSon = File.ReadAllText(fPth);
            messageList = JsonConvert.DeserializeObject<MessageList>(jSon, settings);
            messageList.add(message);
            var convJson = JsonConvert.SerializeObject(sendMessage, Formatting.Indented, settings);
            File.AppendAllText(fPth,convJson);
        }





    }
}
