using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WordBank.Logic.Abstract;

namespace WordBank.WpfAppUI
{
    /// <summary>
    /// Interaction logic for TypingPracticeWindow.xaml
    /// </summary>
    public partial class TypingPracticeWindow : Window
    {
        public TypingPracticeWindow()
        {
            InitializeComponent();

            ClearLabels();
        }


        
        public IInOrderPlayer Player { get; set; }
        public void Start(int category,int wordNumber)
        {
           Player.Start(category, wordNumber);
           lblWord.Content = Player.Word;
           btnAnswer.IsEnabled = true;
           btnNext.IsEnabled = false;
           ShowDialog();

        }
        private void SetLabels(string word, string mean1, string mean2, string mean3)
        {
            lblWord.Content = word;
            lblMean1.Content = mean1;
            lblMean2.Content = mean2;
            lblMean3.Content = mean3;
        }
        private void ClearLabels()
        {
            lblWord.Content = "";
            lblMean1.Content = "";
            lblMean2.Content = "";
            lblMean3.Content = "";
        }
        private void CheckAnswer(string userAnswer)
        {
            if (!Player.CheckAnswer(userAnswer))
            {
               
                imgCorrection.Source = new BitmapImage(new Uri("/WordBank.WpfAppUI;component/Images/wrong.png", UriKind.Relative));

                SetLabels(Player.Word, Player.Meanings[0], Player.Meanings[1], Player.Meanings[2]);
            }
            else
            {
                
                imgCorrection.Source = new BitmapImage(new Uri("/WordBank.WpfAppUI;component/Images/correct.png", UriKind.Relative));
                if (Player.Tolerated)
                {
                    lblToleratedWord.Content = lblWord.Content;
                    lblToleratedCorrectMean.Content = Player.ToleratedCorrectMean;
                    lblToleratedUserAnswer.Content = tbxUserAnswer.Text;
                    grdToleratedMessageBox.Visibility = Visibility.Visible;


                }
                SetLabels(Player.Word, Player.Meanings[0], Player.Meanings[1], Player.Meanings[2]);

            }
        }

        private void btnAnswer_Click(object sender, RoutedEventArgs e)
        {
            CheckAnswer(tbxUserAnswer.Text);

            btnNext.IsEnabled = true;
            btnAnswer.IsEnabled = false;
            if (Player.EndOfList)
            {
                grdFinishDialog.Visibility = Visibility.Visible;
            }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            ClearLabels();
            Player.Next();
            SetLabels(Player.Word, "", "", "");

            btnAnswer.IsEnabled = true;
            btnNext.IsEnabled = false;
            imgCorrection.Source = null;
            tbxUserAnswer.Text = "";
        }

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            ClearLabels();
            Player.Restart = true;
            SetLabels(Player.Word, "", "", "");
            btnAnswer.IsEnabled = true;
            btnNext.IsEnabled = false;
            tbxUserAnswer.Text = "";
            grdFinishDialog.Visibility = Visibility.Hidden;
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            Player.Finish();
            grdFinishDialog.Visibility = Visibility.Hidden;
            Hide();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            grdToleratedMessageBox.Visibility = Visibility.Hidden;
        }
 

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Player != null)
            {
                
                Player.Finish();
            }
            tbxUserAnswer.Text = "";

            ClearLabels();
            Hide();
            e.Cancel = true;
        }

        
    }
}
