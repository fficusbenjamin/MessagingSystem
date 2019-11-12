using DB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using BusinessLayer;

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
        string fPth = @"..\..\..\..\SoftEngCoursework\Messages.json";

        public MessageWin()
        {
            InitializeComponent();
            deserialize();
            addBx(sendMessage.messageList);
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
                bodyInput = _bdyTxt.Text;             
                
                
            }
            catch (Exception execMsg)
            {
                MessageBox.Show(execMsg.Message);
            }
        }

        private string spltTwo() 
        {
            string[] line = _bdyTxt.Text.Split(new string[] { "\r\n" }, 2, StringSplitOptions.None);
            return line[1];
        }
        
        private string getScndLine()
        {
            string[] line = System.Text.RegularExpressions.Regex.Split(_bdyTxt.Text, "\r\n|\r|\n");
            return line[1];
        }
        private string getTrdLine()
        {
            string[] line = _bdyTxt.Text.Split(new string[] { "\r\n" }, 3, StringSplitOptions.None); 
            return line[2];
        }
        

        private void addMessage()
        {
            try
            {
                MessageFactory factory = null;
                MessageFactory sFact = null;
                switch (typeChoice)
                {
                    case "S":
                        factory = new SmsFactory(typeChoice);
                        break;
                    case "E":
                        factory = new EmailFactory(typeChoice);
                        sFact = new SirFactory(typeChoice);
                        break;
                    case "T":
                        factory = new TweetFactory(typeChoice);
                        break;
                    default:
                        break;
                }
                Message message = factory.GetMessageType();

                
                
                if (typeChoice == "E") 
                {
                    System.Text.RegularExpressions.Regex rSir = new System.Text.RegularExpressions.Regex(@"SIR ([0-2][0-9]|(3)[0-1])(\/)(((0)[0-9])|((1)[0-2]))(\/)\d{4}");
                    string line1 = _bdyTxt.Text.Substring(0, _bdyTxt.Text.IndexOf(Environment.NewLine));


                    if (rSir.IsMatch(getScndLine()))
                    {
                        Message sir = sFact.GetMessageType();

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
                    else 
                    {

                        message.ID = idInput;
                        message.Sender = line1;
                        message.Subject = getScndLine();
                        message.Body = getTrdLine();
                        sendMessage.add(message);
                        wrtJson(message, sendMessage);
                        _dsplType.Text = message.MessageType;
                        _dsplHdr.Text = message.ID;
                        _dsplBd.Text = message.Body;
                    }

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
            catch(Exception diagBox)
            {
                MessageBox.Show(diagBox.Message);
            }
            
            
            
            
        }

        

        private void _hdrTxt_GotFocus(object sender, RoutedEventArgs e)
        {
            _hdrTxt.Clear();
        }

        private void _hdrTxt_LostFocus(object sender, RoutedEventArgs e)
        {
            if (_hdrTxt.Text == "") 
            {
                _hdrTxt.Text = "Insert Header/ID";
            }
            
        }

        private void _bdyTxt_LostFocus(object sender, RoutedEventArgs e)
        {
            if (_bdyTxt.Text == "") 
            {
                _bdyTxt.Text = "Insert Body";
            }
            
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
                string output = JsonConvert.SerializeObject(_sendMessage.messageList);
                _lstAllMessages.Items.Clear();
                showList(sendMessage.messageList);

            }
        }

        private void showList(List<Message> list)
        {
            foreach (Message message in sendMessage.messageList) 
            {
                _lstAllMessages.Items.Add(message.ID+ " " + message.MessageType /*+ " " + message.Sender +" "+ message.Subject +" "+ message.Body*/);

            }


        }

        private void _lstAllMessages_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            string selMsg = _lstAllMessages.SelectedItem.ToString();
            string selId = selMsg.Substring(0, 10);
            
            Message message = find(selId);
            
            _dsplHdr.Text = selId;
            _dsplType.Text = message.MessageType;
            _dsplBd.Text = message.Sender +"\n"+ message.Subject +"\n"+ message.Body;
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

        private void wrtJson(Message mess, MessageList messageList) 
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
            File.WriteAllText(fPth,convJson);
        }

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

        public Message find(string id)
        {
            foreach (Message m in MessageWin.sendMessage.messageList)
            {
                if (id == m.ID)
                {
                    return m;
                }
            }
            return null;
        }







    }
}
