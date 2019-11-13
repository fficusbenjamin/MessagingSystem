using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SoftEngCoursework
{
    /// <summary>
    /// Interaction logic for MessageDetail.xaml
    /// </summary>
    public partial class MessageDetail : Window
    {
        public MessageDetail()
        {
            InitializeComponent();
        }

        private void _bckBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageWin newWin = new MessageWin();
            newWin.Show();
            this.Close();
        }
    }
}
