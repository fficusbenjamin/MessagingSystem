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

///------------------------------------------------------------------------
///   Class:          MessageDetail (Window)
///   Description:    This class hold all the properties and 
///                   actions/methods required to show the lists from 
///                   the files and lists stored from the previous windows. 
///                    
///                   
///   Author:         Francesco Fico (40404272)     Date: 27/11/2019
///------------------------------------------------------------------------



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

        //back button click method
        private void _bckBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageWin newWin = new MessageWin();
            newWin.Show();
            this.Close();
        }
        //when the windows loads show the mentions list
        private void _lstMnts_Loaded(object sender, RoutedEventArgs e)
        {
            showMnts();
        }

        //when the windows loads show the hashtags list
        private void _lstHstgs_Loaded(object sender, RoutedEventArgs e)
        {
            showHtgs();
        }

        //when the windows loads show the SIR list
        private void _lstSir_Loaded(object sender, RoutedEventArgs e)
        {
            showSir();
        }

        //show mentions method
        private void showMnts()
        {
            //creates a list from the mention file
            List<string> mentionsList = File.ReadAllLines(@"..\..\..\..\SoftEngCoursework\mentions.txt").ToList();
            //for each line in the list 
            foreach (var mentions in mentionsList) 
            {
                //add every item to the list
                _lstMnts.Items.Add(mentions);
            }
        }
        //show hashtags method
        private void showHtgs()
        {
            //creates a list from the hashtag file
            List<string> hashtagsList = File.ReadAllLines(@"..\..\..\..\SoftEngCoursework\hashtags.txt").ToList();
            //creates a variable that stores the count of each hashtag in the list
            var wrdCount = hashtagsList.GroupBy(i => i).OrderByDescending(group => group.Count());
            foreach (var word in wrdCount) 
            {
                //shows every item in the list plus the count
                _lstHstgs.Items.Add(word.Key+"("+word.Count()+")");
            }
        }
        //shows SIR method
        private void showSir() 
        {
            //creates a list from the SIR file
            List<string> sirList = File.ReadAllLines(@"..\..\..\..\SoftEngCoursework\sirlist.txt").ToList();
            foreach (var sirs in sirList)
            {
                //add every item to the list
                _lstSir.Items.Add(sirs);
            }
        }
    }
}
