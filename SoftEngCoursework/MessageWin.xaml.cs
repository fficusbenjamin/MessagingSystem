using BusinessLayer;
using DB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;



///-------------------------------------------------------------------
///   Class:          MessageWindow (Window)
///   Description:    This class hold all the properties and 
///                   actions/methods required to interact with the 
///                   form on the window in order to: read valued 
///                   that the user has entered; validate them; 
///                   create a message from them; add messages to 
///                   the message list; display a list of all the  
///                   messages. 
///                   
///   Author:         Francesco Fico (40404272)     Date: 27/11/2019
///-------------------------------------------------------------------

namespace SoftEngCoursework
{
    /// <summary>
    /// Interaction logic for MessageWin.xaml
    /// </summary>
    public partial class MessageWin : Window
    {
        //creates a new list of messages
        private static MessageList sendMessage = new MessageList();
        //get for the private list
        public static MessageList _sendMessage
        {
            get { return sendMessage; }
            //set { return value; }
        }
        //creates the variables to hold the user input
        public string idInput, bodyInput, typeInput, typeChoice;
        //creates a bool to later check the validity of the user input
        private bool isEntryValid;
        //regular expression to check the SIR object
        Regex rSir = new Regex(@"SIR ([0-2][0-9]|(3)[0-1])(\/)(((0)[0-9])|((1)[0-2]))(\/)\d{4}");
        //path for the JSon
        string fPth = @"..\..\..\..\SoftEngCoursework\Messages.json";

        public MessageWin()
        {
            InitializeComponent();
            deserialize();
            addBx(sendMessage.messageList);
        }


        //method to validate the user input
        private void validateEntry()
        {
            //regular expression for the message header/id
            Regex rID = new Regex(@"^[SET][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]");

            try
            {
                //set the boolean to true
                isEntryValid = true;
                //if header blank
                if (_hdrTxt.Text == "")
                {
                    //set bool to false
                    isEntryValid = false;
                    //throw exception message
                    throw new ArgumentException("Field cannot be blank", "Message Header/ID");
                }
                else
                //if header matches with the regex
                if (!rID.IsMatch(_hdrTxt.Text))
                {
                    //set bool to false
                    isEntryValid = false;
                    //throw exception message
                    throw new ArgumentException("ID should start with S,E or T followed by nine numbers", "Message Header/ID");
                }
                //set the variable with the user input
                idInput = _hdrTxt.Text;
                //stores the first letter of the header
                typeChoice = idInput.Substring(0, 1);
                //set the variable with the user input and validate it in the various classes
                bodyInput = _bdyTxt.Text;
            }
            catch (Exception execMsg)
            {
                MessageBox.Show(execMsg.Message);
            }
        }
        //method that split the body in two taking out the first line
        private string spltTwo()
        {
            string[] line = _bdyTxt.Text.Split(new string[] { "\r\n" }, 2, StringSplitOptions.None);
            return line[1];
        }
        //method that split the body taking the second line
        private string getScndLine()
        {
            string[] line = Regex.Split(_bdyTxt.Text, "\r\n|\r|\n");
            return line[1];
        }
        //method that split the body taking the third line
        private string getTrdLine()
        {
            string[] line = _bdyTxt.Text.Split(new string[] { "\r\n" }, 3, StringSplitOptions.None);
            return line[2];
        }
        //method that creates the factory and the object for the specific message classes
        //almost identical to the one in the MainWindow but instead of reading the file reads and validates
        //all the user input
        private void addMessage()
        {
            try
            {
                bool swtch = false;
                MessageFactory factory = null;
                MessageFactory sFact = null;
                switch (typeChoice)
                {
                    case "S":
                        factory = new SmsFactory(typeChoice);
                        break;
                    case "E":
                        if (!rSir.IsMatch(getScndLine()))
                        {
                            factory = new EmailFactory(typeChoice);
                        }
                        else
                        {
                            sFact = new SirFactory(typeChoice);
                            swtch = true;
                        }

                        break;
                    case "T":
                        factory = new TweetFactory(typeChoice);
                        break;
                    default:
                        break;
                }
                if (swtch == true)
                {
                    Message sir = sFact.GetMessageType();
                    if (rSir.IsMatch(getScndLine()))
                    {
                        string line1 = _bdyTxt.Text.Substring(0, _bdyTxt.Text.IndexOf(Environment.NewLine));
                        sir.ID = idInput;
                        sir.Sender = line1;
                        sir.Subject = getScndLine();
                        sir.Body = getTrdLine();
                        sendMessage.add(sir);
                        wrtJson(sir, sendMessage);
                        _dsplType.Text = sir.MessageType;
                        _dsplHdr.Text = sir.ID;
                        _dsplBd.Text = sir.Subject + "\n" + sir.Body;
                    }
                }
                else if (swtch == false)
                {
                    Message message = factory.GetMessageType();
                    if (typeChoice == "E")
                    {
                        string line1 = _bdyTxt.Text.Substring(0, _bdyTxt.Text.IndexOf(Environment.NewLine));
                        message.ID = idInput;
                        message.Sender = line1;
                        message.Subject = getScndLine();
                        message.Body = getTrdLine();
                        sendMessage.add(message);
                        wrtJson(message, sendMessage);
                        _dsplType.Text = message.MessageType;
                        _dsplHdr.Text = message.ID;
                        _dsplBd.Text = message.Subject + "\n" + message.Body;
                    }
                    if (typeChoice == "S")
                    {
                        message.ID = idInput;
                        string line1 = _bdyTxt.Text.Substring(0, _bdyTxt.Text.IndexOf(Environment.NewLine));
                        message.Sender = line1;
                        message.Body = spltTwo();
                        sendMessage.add(message);
                        wrtJson(message, sendMessage);
                        _dsplType.Text = message.MessageType;
                        _dsplHdr.Text = message.ID;
                        _dsplBd.Text = message.Body;
                    }
                    if (typeChoice == "T")
                    {
                        message.ID = idInput;
                        string line1 = _bdyTxt.Text.Substring(0, _bdyTxt.Text.IndexOf(Environment.NewLine));
                        message.Sender = line1;
                        message.Body = spltTwo();
                        sendMessage.add(message);
                        wrtJson(message, sendMessage);
                        _dsplType.Text = message.MessageType;
                        _dsplHdr.Text = message.ID;
                        _dsplBd.Text = message.Body;
                    }
                }
            }

            catch (Exception message)
            {
                MessageBox.Show(message.Message);
            }
        }
        //method that clear the box when take focus
        private void _hdrTxt_GotFocus(object sender, RoutedEventArgs e)
        {
            _hdrTxt.Clear();
        }
        //method that set the helptext once the box loose focus
        private void _hdrTxt_LostFocus(object sender, RoutedEventArgs e)
        {
            if (_hdrTxt.Text == "")
            {
                _hdrTxt.Text = "Insert Header/ID";
            }
        }
        //method that set the helptext once the box loose focus
        private void _bdyTxt_LostFocus(object sender, RoutedEventArgs e)
        {
            if (_bdyTxt.Text == "")
            {
                _bdyTxt.Text = "Insert Body";
            }
        }
        //method that shows the message list when the listbox loads
        private void _lstAllMessages_Loaded(object sender, RoutedEventArgs e)
        {
            showList(sendMessage.messageList);
        }
        //method that clear the box when take focus
        private void _bdyTxt_GotFocus(object sender, RoutedEventArgs e)
        {
            _bdyTxt.Clear();
        }
        //method click for the back button
        private void _bckBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow newWin = new MainWindow();
            newWin.Show();
            this.Close();
        }
        //send button click method
        private void _sndBtn_Click(object sender, RoutedEventArgs e)
        {
            //validate the entry
            validateEntry();
            //if the validation passes
            if (isEntryValid == true)
            {
                //add the message
                addMessage();
                //writes the list on the file
                string output = JsonConvert.SerializeObject(_sendMessage.messageList);
                //clear the listbox
                _lstAllMessages.Items.Clear();
                //rewrite the listbox with the new items
                showList(sendMessage.messageList);
            }
        }
        //method that shows the message list
        private void showList(List<Message> list)
        {
            //for every line in the list
            foreach (Message message in sendMessage.messageList)
            {
                //show only the id and the type
                _lstAllMessages.Items.Add(message.ID + " " + message.MessageType);
            }
        }
        //method that shows the message selected in the list in the appropriate text boxes on the right
        private void _lstAllMessages_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                //stores the selected message in a string
                string selMsg = _lstAllMessages.SelectedItem.ToString();
                //stores the message id in a string
                string selId = selMsg.Substring(0, 10);
                //find the message by the id
                Message message = find(selId);
                //display the id in the appropriate boxes
                _dsplHdr.Text = selId;
                _dsplType.Text = message.MessageType;
                _dsplBd.Text = message.Sender + "\n" + message.Subject + "\n" + message.Body;
            }
            catch { }
        }

        private void addBx(List<Message> list)
        {
            foreach (Message message in sendMessage.messageList)
            {
                _dsplType.Text = message.MessageType;
                _dsplHdr.Text = message.ID;
                _dsplBd.Text = message.Body;
            }
        }
        //method for the finish session button that open the MessageDetail window
        private void _fnshBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageDetail newWin = new MessageDetail();
            newWin.Show();
            this.Close();
        }
        //write JSon method identical to the one in the MainWindow
        public void wrtJson(Message mess, MessageList messageList)
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
            messageList.add(mess);
            var convJson = JsonConvert.SerializeObject(sendMessage, Formatting.Indented, settings);
            File.WriteAllText(fPth, convJson);
        }
        //method that deserialise the JSon file
        private void deserialize()
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
            sendMessage = JsonConvert.DeserializeObject<MessageList>(jSon, settings);
        }
        //method that find the message from the list
        public Message find(string id)
        {
            //for every message in the list
            foreach (Message m in MessageWin.sendMessage.messageList)
            {
                //if the id is present
                if (id == m.ID)
                {
                    //return it
                    return m;
                }
            }
            //else is null
            return null;
        }
    }
}
