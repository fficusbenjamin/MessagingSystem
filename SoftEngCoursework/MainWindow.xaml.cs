using System.Windows;

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

        }
    }
}
