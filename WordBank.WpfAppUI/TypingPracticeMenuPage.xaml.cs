using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using WordBank.Logic.Players;

namespace WordBank.WpfAppUI
{
    /// <summary>
    /// Interaction logic for TypingPracticeMenuPage.xaml
    /// </summary>
    public partial class TypingPracticeMenuPage : Page
    {
        public TypingPracticeMenuPage()
        {
            InitializeComponent();
 
        }
        
        public IWordManager WordManager { get; set; }
        public IInOrderPlayer Player { get; set; }
        public WordManagerHandler WordManagerHandler { get; set; }


        private object[] entityType = new object[2];

        int _wordLimit;
        int _category;

        TypingPracticeWindow typingPracticeWindow = new TypingPracticeWindow();

        private void ComboBox_DropDownOpened(object sender, EventArgs e)
        {

            cbxCategories.ItemsSource = WordManagerHandler.GetCategories();
            cbxCategories.DisplayMemberPath = "Category";
            cbxCategories.SelectedValuePath = "Id";
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
                      
            typingPracticeWindow.Player = Player;

            if ((chkbxAllCategories.IsChecked == true)&& (tbxWordNumber.Text != "")||(cbxCategories.SelectedIndex != -1) && (tbxWordNumber.Text != "")  )
            {
                typingPracticeWindow.Start(_category, Convert.ToInt32(tbxWordNumber.Text));
            }else
            {
                MessageBox.Show("You didn't select a category or entered word number");
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
                lblWordLimit.Content = _wordLimit.ToString()+" words";
            }
            else
            {
                cbxCategories.Text = "-Select a category-";
                cbxCategories.IsEnabled = true;
                lblWordLimit.Content ="";
            }
        }

        private void cbxCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            startButton.IsEnabled = true;
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

        public void RecordCounter(int categoryId)
        {
            _wordLimit=WordManagerHandler.RecordCounter(categoryId);    

        }

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

        private void rbtnUserChoice_Click(object sender, RoutedEventArgs e)
        {
            tbxWordNumber.Text = "";
            tbxWordNumber.IsEnabled = false;
        }
    }
}
