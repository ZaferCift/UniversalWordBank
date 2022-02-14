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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WordBank.Logic.Abstract;
using WordBank.Logic.Concrete;

namespace WordBank.WpfAppUI
{
    /// <summary>
    /// Interaction logic for TestPage.xaml
    /// </summary>
    public partial class TestPage : Page
    {
        public TestPage()
        {
            InitializeComponent();
        }
        int _wordLimit;
        int _category;
        public WordManagerHandler WordManagerHandler { get; set; }
        public ITestPlayer Player { get; set; }
        TestWindow testWindow = new TestWindow();
        byte _easy = 0;

        private void tbxWordNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbxWordNumber.Text != "")
            {

                if (Convert.ToInt32(tbxWordNumber.Text) > _wordLimit)
                {
                    tbxWordNumber.Text = _wordLimit.ToString();
                    MessageBox.Show("Maximum " + tbxWordNumber.Text + " words for this category.");

                }
            }

        }

        private void tbxWordNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);

        }

        private void ComboBox_DropDownOpened(object sender, EventArgs e)
        {
            cbxCategories.ItemsSource = WordManagerHandler.GetCategories();
            cbxCategories.DisplayMemberPath = "Category";
            cbxCategories.SelectedValuePath = "Id";

        }

        private void cbxCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _category = Convert.ToInt32(cbxCategories.SelectedValue);

            if (cbxCategories.SelectedIndex != -1)
            {
                RecordCounter(_category);

                lblWordLimit.Content = _wordLimit.ToString() + " words";

            }
            else
            {
                lblWordLimit.Content = "";
            }

        }

        private void chkbxAllCategories_Click(object sender, RoutedEventArgs e)
        {
            if (chkbxAllCategories.IsChecked == true)
            {
                cbxCategories.SelectedIndex = -1;

                cbxCategories.IsEnabled = false;

                _category = 0;
                RecordCounter(_category);
                lblWordLimit.Content = _wordLimit.ToString() + " words";
            }
            else
            {
                cbxCategories.Text = "-Select a category-";
                cbxCategories.IsEnabled = true;
                lblWordLimit.Content = "";
            }

        }
        public void RecordCounter(int categoryId)
        {
           _wordLimit = WordManagerHandler.RecordCounter(categoryId);

        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            if ((cbxCategories.SelectedIndex != -1) && (tbxWordNumber.Text != "")||(chkbxAllCategories.IsChecked!=false)&& (tbxWordNumber.Text != ""))
            {
                testWindow.Player = Player;
                Player.Start(_category, Convert.ToInt32(tbxWordNumber.Text), _easy);
                testWindow.SetWords();
                testWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("You didn't select a category or entered word number");
            }
        
        }

        private void chkbxEasy_Click(object sender, RoutedEventArgs e)
        {
            if (chkbxEasy.IsChecked == true)
            {
                _easy = 1;
            }
            else
            {
                _easy = 0;
            }

        }

        private void chkbxAllCategories_Checked(object sender, RoutedEventArgs e)
        {
            chkbxEasy.IsChecked = false;
            chkbxEasy.IsEnabled = false;
            _easy = 1;
        }

        private void chkbxAllCategories_Unchecked(object sender, RoutedEventArgs e)
        {
            chkbxEasy.IsEnabled = true;
            
        }
    }
}
