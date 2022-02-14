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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WordBank.Logic.Abstract;
using WordBank.Logic.Concrete;
using WordBank.Logic.DependencyResolvers;
using WordBank.Logic.Players;

namespace WordBank.WpfAppUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            labels[0] = lblTest;
            labels[1] = lblTyping;
            
           
        }
                
        IInOrderPlayer _inOrderPlayer = InstanceFactory.GetInstance<IInOrderPlayer>();
        ITestPlayer _testPlayer=InstanceFactory.GetInstance<ITestPlayer>();
        Color mouseMoveColor = new Color() { R = 179, G = 240, B = 253, A = 100 };
        TestPage testPage = new TestPage();
        TypingPracticeMenuPage typingPracticeMenuPage = new TypingPracticeMenuPage();
        WordManagerHandler WordManagerHandler = new WordManagerHandler();
        IEngTrService engTrService;
       
        SolidColorBrush brush = new SolidColorBrush();
        Label[] labels = new Label[2];

        private void BrushUnselectedLabels(int a)
        {

            for (int i = 0; i < labels.Length; i++)
            {
                if (i != a)
                {
                    labels[i].Foreground = Brushes.White;
                    labels[i].FontWeight = FontWeights.Normal;

                }
            }


        }

        private void testLabel_MouseMove(object sender, MouseEventArgs e)
        {
            if (lblTest.FontWeight != FontWeights.Bold)
            {
                brush.Color = mouseMoveColor;

                lblTest.Foreground = brush;
            }
        }

        private void testLabel_MouseLeave(object sender, MouseEventArgs e)
        {
            if (lblTest.FontWeight != FontWeights.Bold)
            {
                lblTest.Foreground = Brushes.White;
            }
        }

        private void writingLabel_MouseMove(object sender, MouseEventArgs e)
        {
            if (lblTyping.FontWeight != FontWeights.Bold)
            {
                brush.Color = mouseMoveColor;

                lblTyping.Foreground = brush;
            }
            
        }

        private void writingLabel_MouseLeave(object sender, MouseEventArgs e)
        {
            if (lblTyping.FontWeight != FontWeights.Bold)
            {
                lblTyping.Foreground = Brushes.White;
            }
        }

        private void testLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            lblTest.Foreground = Brushes.Black;
            lblTest.FontWeight = FontWeights.Bold;
            BrushUnselectedLabels(0);
            mainFrame.Content = testPage;
            
        }

        private void writingLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            lblTyping.Foreground = Brushes.Black;
            lblTyping.FontWeight = FontWeights.Bold;
            BrushUnselectedLabels(1);
            mainFrame.Content = typingPracticeMenuPage;

        }

        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            
            engTrService = InstanceFactory.GetInstance<IEngTrService>();

            WordManagerHandler.WordManager = engTrService;
            SetUnits();

        }
        private void SetUnits()
        {
            typingPracticeMenuPage.WordManagerHandler = WordManagerHandler;
            _testPlayer.WordManagerHandler = WordManagerHandler;
            testPage.WordManagerHandler = WordManagerHandler;
            testPage.Player = _testPlayer;
            _inOrderPlayer.wordManagerHandler = WordManagerHandler;
            typingPracticeMenuPage.Player = _inOrderPlayer;
        }

       
        private void cbxLanguages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lblTyping.IsEnabled = true;
            lblTest.IsEnabled = true;
            typingPracticeMenuPage.cbxCategories.SelectedIndex = -1;
            typingPracticeMenuPage.cbxCategories.Text = "-Select a category-";
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
