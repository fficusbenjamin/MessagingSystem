using System.IO;
using System.Windows;
using Microsoft.Win32;
using BusinessLayer;

namespace SoftEngCoursework
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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
                //rdFile(path);
                
            }
        }

        /*private void rdFile(string path) 
        {
            string line;
            StreamReader stringReader = new StreamReader(path);
            while ((line = stringReader.ReadLine()) != null) 
            {
                string[] vs = line.Split(',');
                bool swtch = false;
                MessageFactory factory = null;
                MessageFactory sFact = null;
                switch (vs[0])
                {
                    case "S":
                        factory = new SmsFactory(vs[0]);
                        break;
                    case "E":
                        if (!rSir.IsMatch(getScndLine()))
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
                        //Regex rSir = new Regex(@"SIR ([0-2][0-9]|(3)[0-1])(\/)(((0)[0-9])|((1)[0-2]))(\/)\d{4}");
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
        }*/
    }
}
