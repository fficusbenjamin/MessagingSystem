using BusinessLayer;
using DB;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;



///-------------------------------------------------------------------
///   Class:          MainWindow (Window)
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //creates a regular expression to check the SIR message subject
        Regex rSir = new Regex(@"SIR ([0-2][0-9]|(3)[0-1])(\/)(((0)[0-9])|((1)[0-2]))(\/)\d{4}");
        //path for the JSon file
        string fPth = @"..\..\..\..\SoftEngCoursework\Messages.json";
        public MainWindow()
        {
            InitializeComponent();
        }

        //message button click action to open the message window
        private void message_btn_Click(object sender, RoutedEventArgs e)
        {
            MessageWin newWin = new MessageWin();
            newWin.Show();
            this.Close();
        }

        //file button click action to select a file to import data
        private void file_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //open the file dialog window
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    //store the file name and path in a string
                    string path = openFileDialog.FileName;
                    //calling the read file and passing the string with the path 
                    rdFile(path);
                    //open the message window
                    MessageWin newWin = new MessageWin();
                    newWin.Show();
                    this.Close();

                }
            }
            //shows the exception if it's not a valid file
            catch (Exception message)
            {
                MessageBox.Show("File not valid ");
            }
        }

        //read the file, divide it by commas, validate the content and create the message object
        private void rdFile(string path)
        {
            string line;
            StreamReader stringReader = new StreamReader(path);
            //while the file is not empty
            while ((line = stringReader.ReadLine()) != null)
            {
                //split every string in the line by comma and store them in an array of strings
                string[] vs = line.Split(',');
                //creates a bool and set it to false
                bool swtch = false;
                MessageFactory factory = null;
                MessageFactory sFact = null;
                //switch that checks the first letter of the first string and creates the appopriate message object
                switch (vs[0])
                {
                    //if S creates an SMS factory
                    case "S":
                        factory = new SmsFactory(vs[0]);
                        break;
                    //if E then checks if it's an email or a SIR and creates the appropriate factory    
                    case "E":
                        if (!rSir.IsMatch(vs[3]))
                        {
                            factory = new EmailFactory(vs[0]);
                        }
                        else
                        {
                            sFact = new SirFactory(vs[0]);
                            //if a SIR set the bool to true
                            swtch = true;
                        }
                        break;
                    //if T creates a tweet factory    
                    case "T":
                        factory = new TweetFactory(vs[0]);
                        break;
                    default:
                        break;
                }
                //if the bool is true
                if (swtch == true)
                {   
                    //creates a new SIR object taking the type
                    Message sir = sFact.GetMessageType();
                    //if the SIR subject is validated
                    if (rSir.IsMatch(vs[3]))
                    {
                        //set the sir properties with the string in the array 
                        sir.ID = vs[1];
                        sir.Sender = vs[2];
                        sir.Subject = vs[3];
                        string conc = vs[4] + "\r\n " + vs[5] + "\r\n " + vs[6];
                        sir.Body = conc;
                        //add it to the list
                        MessageWin.sendMessage.add(sir);
                        //write the list on the json
                        wrtJson(sir, MessageWin.sendMessage);

                    }
                }
                //if the bool is false
                else if (swtch == false)
                {
                    //creates a new message object taking the type
                    Message message = factory.GetMessageType();
                    if (vs[0] == "E")
                    {
                        //set the message properties with the string in the array
                        message.ID = vs[1];
                        message.Sender = vs[2];
                        message.Subject = vs[3];                        
                        message.Body = vs[4];
                        //add it to the list
                        MessageWin.sendMessage.add(message);
                        //write the list on the json
                        wrtJson(message, MessageWin.sendMessage);
                    }
                    if (vs[0] == "S")
                    {
                        //set the message properties with the string in the array
                        message.ID = vs[1];
                        message.Sender = vs[2];
                        string conc = vs[3] + " " + vs[4];
                        message.Body = conc;
                        //add it to the list
                        MessageWin.sendMessage.add(message);
                        //write the list on the json
                        wrtJson(message, MessageWin.sendMessage);

                    }
                    if (vs[0] == "T")
                    {
                        //set the message properties with the string in the array
                        message.ID = vs[1];
                        message.Sender = vs[2];
                        string conc = vs[3] + " " + vs[4];
                        message.Body = conc;
                        //add it to the list
                        MessageWin.sendMessage.add(message);
                        //write the list on the json
                        wrtJson(message, MessageWin.sendMessage);
                    }
                }
            }


            //method that writes on the JSon file
            void wrtJson(Message mess, MessageList messageList)
            {
                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                };
                //if the file doesn't exist
                if (File.Exists(fPth) != true)
                {
                    //creates it
                    File.Create(fPth);
                }
                //creates a string from the file
                string jSon = File.ReadAllText(fPth);
                //deserialise the json file in the list 
                messageList = JsonConvert.DeserializeObject<MessageList>(jSon, settings);
                //add the messages to the list
                messageList.add(mess);
                //convert the json passing the list in the next window
                var convJson = JsonConvert.SerializeObject(MessageWin.sendMessage, Formatting.Indented, settings);
                File.WriteAllText(fPth, convJson);
            }
        }
    }
}
