using System.IO;
using System.Windows;
using Microsoft.Win32;
using BusinessLayer;
using DB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Text.RegularExpressions;
using SoftEngCoursework;

namespace SoftEngCoursework
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Regex rSir = new Regex(@"SIR ([0-2][0-9]|(3)[0-1])(\/)(((0)[0-9])|((1)[0-2]))(\/)\d{4}");
        string fPth = @"..\..\..\..\SoftEngCoursework\Messages.json";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void message_btn_Click(object sender, RoutedEventArgs e)
        {
            MessageWin newWin = new MessageWin();
            newWin.Show();
            this.Close();

        }

        private void file_btn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string path = openFileDialog.FileName;
                rdFile(path);
                MessageWin newWin = new MessageWin();
                newWin.Show();
                this.Close();

            }
        }


        private void rdFile(string path)
        {


            string line;
            StreamReader stringReader = new StreamReader(path);
            while ((line = stringReader.ReadLine()) != null)
            {
                string[] vs = line.Split(',');
               /* string getScndLine()
                {
                    string[] line = Regex.Split(vs[1], "\r\n|\r|\n");
                    return line[2];
                }
                string getTrdLine()
                {
                    string[] line = vs[1].Split(new string[] { "\r\n" }, 3, StringSplitOptions.None);
                    return line[2];
                }
                string spltTwo()
                {
                    string[] line = vs[1].Split(new string[] { "\r\n" }, 2, StringSplitOptions.None);
                    return line[1];
                }*/

                bool swtch = false;
                MessageFactory factory = null;
                MessageFactory sFact = null;
                switch (vs[0])
                {
                    case "S":
                        factory = new SmsFactory(vs[0]);
                        break;
                    case "E":
                        if (!rSir.IsMatch(vs[3]))
                        {
                            factory = new EmailFactory(vs[0]);
                        }
                        else
                        {
                            sFact = new SirFactory(vs[0]);
                            swtch = true;
                        }

                        break;
                    case "T":
                        factory = new TweetFactory(vs[0]);
                        break;
                    default:
                        break;
                }
                if (swtch == true)
                {
                    Message sir = sFact.GetMessageType();
                    if (rSir.IsMatch(vs[3]))
                    {
                        //string line1 = vs[2].Substring(0, vs[2].IndexOf(Environment.NewLine));
                        sir.ID = vs[1];
                        sir.Sender = vs[2];
                        sir.Subject = vs[3];
                        string conc = vs[4]+"\r\n "+vs[5]+"\r\n "+vs[6];
                        sir.Body = conc;
                        MessageWin.sendMessage.add(sir);
                        wrtJson(sir, MessageWin.sendMessage);

                    }
                }
                else if (swtch == false)
                {

                    Message message = factory.GetMessageType();
                    if (vs[0] == "E")
                    {
                        //Regex rSir = new Regex(@"SIR ([0-2][0-9]|(3)[0-1])(\/)(((0)[0-9])|((1)[0-2]))(\/)\d{4}");
                        //string line1 = vs[2].Substring(0, vs[2].IndexOf(Environment.NewLine));





                        message.ID = vs[1];
                        message.Sender = vs[2];
                        message.Subject = vs[3];
                        //string conc = vs[4] + " " + vs[5];
                        message.Body = vs[4];
                        MessageWin.sendMessage.add(message);
                        wrtJson(message, MessageWin.sendMessage);



                    }
                    if (vs[0] == "S")
                    {
                        message.ID = vs[1];
                        //string line1 = vs[2].Substring(0, vs[2].IndexOf(Environment.NewLine));
                        message.Sender = vs[2];
                        //string conc = vs[4] + " " + vs[5];
                        message.Body = vs[3];
                        MessageWin.sendMessage.add(message);
                        wrtJson(message, MessageWin.sendMessage);

                    }
                    if (vs[0] == "T")
                    {
                        message.ID = vs[1];
                        //string line1 = vs[2].Substring(0, vs[2].IndexOf(Environment.NewLine));
                        message.Sender = vs[2];
                        //string conc = vs[4] + " " + vs[5];
                        message.Body = vs[3];
                        MessageWin.sendMessage.add(message);
                        wrtJson(message, MessageWin.sendMessage);

                    }
                }
            }

            void wrtJson(Message mess, MessageList messageList)
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
                var convJson = JsonConvert.SerializeObject(MessageWin.sendMessage, Formatting.Indented, settings);
                File.WriteAllText(fPth, convJson);
            }
        }
    }
}
