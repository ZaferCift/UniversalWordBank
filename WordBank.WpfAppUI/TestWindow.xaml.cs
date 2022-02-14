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
using WordBank.Logic.Abstract;
using WordBank.Logic.Concrete;

namespace WordBank.WpfAppUI
{
    /// <summary>
    /// Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        public TestWindow()
        {
            InitializeComponent();

            choiceLabels[0] = lblA;
            choiceLabels[1] = lblB;
            choiceLabels[2] = lblC;
            choiceLabels[3] = lblD;

        }
        Label[] choiceLabels = new Label[4];
        SolidColorBrush brush = new SolidColorBrush();
        Color mouseMoveColor = new Color() { R = 0, G = 51, B = 255, A = 100 };
        
        public ITestPlayer Player { get; set; }

        internal void SetWords()
        {
            lblWord.Content = Player.Word;
            lblA.Content = Player.Choices[0];
            lblB.Content = Player.Choices[1];
            lblC.Content = Player.Choices[2];
            lblD.Content = Player.Choices[3];
        }

        public void SelectTrueChoice()
        {
            foreach (var choiceLabel in choiceLabels)
            {
                var contentProp = choiceLabel.GetType().GetProperty("Content");
                if ((string)contentProp.GetValue(choiceLabel,null) == Player.TrueChoice)
                {
                    var parentProp = choiceLabel.GetType().GetProperty("Parent");
                    var border = parentProp.GetValue(choiceLabel);
                    var borderProp=border.GetType().GetProperty("Background");
                    borderProp.SetValue(border, Brushes.Green);
                    break;

                }
            }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (!Player.EndOfList)
            {
                lblWordNo.Content = (Convert.ToInt32(lblWordNo.Content) + 1).ToString();
                Player.Next();
                ResetBorderColor();
                imgCorrection.Source = null;
                SetWords();
            }
            else
            {
                grdFinishMessage.Visibility = Visibility.Visible;
            }
        }

        private void ResetBorderColor()
        {
            borderA.Background = null;
            borderB.Background = null;
            borderC.Background = null;
            borderD.Background = null;
        }

        private void MoveMouse(object sender,MouseEventArgs e)
        {
            var foreGround = sender.GetType().GetProperty("Foreground");
            foreGround.SetValue(sender, Brushes.Blue);
  
        }
        private void LeaveMouse(object sender, MouseEventArgs e)
        {

            var foreGround = sender.GetType().GetProperty("Foreground");
            foreGround.SetValue(sender, Brushes.Black);

        }
        
        private void MouseButtonDown(object sender, MouseEventArgs e)
        {
            if (imgCorrection.Source == null)
            {

                var contentProp = sender.GetType().GetProperty("Content");
                var content = contentProp.GetValue(sender, null);

                var foreGround = sender.GetType().GetProperty("Foreground");
                foreGround.SetValue(sender, Brushes.White);

                var parentProp = sender.GetType().GetProperty("Parent");
                var border = parentProp.GetValue(sender, null);

                var borderBackgroundProp = border.GetType().GetProperty("Background");
                

                if (Player.CheckUserChoice((string)content))
                {
                    borderBackgroundProp.SetValue(border, Brushes.Green);
                    imgCorrection.Source = new BitmapImage(new Uri("/WordBank.WpfAppUI;component/Images/correct.png", UriKind.Relative));
                }
                else
                {
                    imgCorrection.Source = new BitmapImage(new Uri("/WordBank.WpfAppUI;component/Images/wrong.png", UriKind.Relative));
                    borderBackgroundProp.SetValue(border, Brushes.Red);
                    SelectTrueChoice();

                }
            }

        }


        private void winTest_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Player != null)
            {

                Player.Finish();
            }

            ClearLabels();
            ResetBorderColor();
            imgCorrection.Source = null;
            grdFinishMessage.Visibility = Visibility.Hidden;

            Hide();
            e.Cancel = true;
        }

        private void ClearLabels()
        {
            lblWord.Content = "";
            lblA.Content = "";
            lblB.Content = "";
            lblC.Content = "";
            lblD.Content = "";
            lblWordNo.Content = "1";
            ResetBorderColor();

            
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            ClearLabels();
            ResetBorderColor();
            imgCorrection.Source = null;
            Player.Finish();
            grdFinishMessage.Visibility = Visibility.Hidden;
            winTest.Visibility = Visibility.Hidden;
        }
    }
}
