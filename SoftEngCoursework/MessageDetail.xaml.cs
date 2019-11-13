using System;
using System.Collections.Generic;
using System.IO;
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
using BusinessLayer;
using DB;

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

        private void _lstMnts_Loaded(object sender, RoutedEventArgs e)
        {
            showMnts();
        }

        private void _lstHstgs_Loaded(object sender, RoutedEventArgs e)
        {
            showHtgs();
        }

        private void _lstSir_Loaded(object sender, RoutedEventArgs e)
        {
            showSir();
        }


        private void showMnts()
        {
            List<string> mentionsList = File.ReadAllLines(@"..\..\..\..\SoftEngCoursework\mentions.txt").ToList();
            foreach (var mentions in mentionsList) 
            {
                _lstMnts.Items.Add(mentions);
            }

        }

        private void showHtgs()
        {
            List<string> hashtagsList = File.ReadAllLines(@"..\..\..\..\SoftEngCoursework\hashtags.txt").ToList();
            /*foreach (var hashtag in hashtagsList)
            {
                _lstHstgs.Items.Add(hashtag);
            }*/

            var wrdCount = hashtagsList.GroupBy(i => i).OrderByDescending(group => group.Count());
            foreach (var word in wrdCount) 
            {
                _lstHstgs.Items.Add(word.Key+"("+word.Count()+")");
            }

        }

        private void showSir() 
        {
            List<string> sirList = File.ReadAllLines(@"..\..\..\..\SoftEngCoursework\sirlist.txt").ToList();
            foreach (var sirs in sirList)
            {
                _lstSir.Items.Add(sirs);
            }

        }






    }
}
