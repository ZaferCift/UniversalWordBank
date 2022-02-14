using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordBank.Logic.Abstract;

namespace WordBank.Logic.Concrete
{
    /*
       

       Test Player creates a list of words which includes their correct meanings 
      according to users language pair,category and word number selection.
       While showing the created list to the user in order, it randomly selects one of the 3 
      meanings of the word as the correct choice.
       It randomly determines the other 3 wrong choices either from the same category(hard) or 
      from all categories(easy), depending on whether the user chooses the easy option in the menu

    */
    public class TestPlayer:ITestPlayer
    {
        
        public WordManagerHandler WordManagerHandler { get; set; }// User selected language pair controller.
        public List<string[]> WordList { get; set; }// Word list to show to user
        private List<string[]> WrongWordsList { get; set; }=new List<string[]>();// Randomly selected  3 wrong choices 
        public string[] Choices { get; set; }= new string[4];// All 4 choices including true choice
        public string Word { get; set; } // Foreign word 
        public bool EndOfList { get; set; } = false;// Property that marks end of the WordList property.
        UniqueRandomNumber _randomNumber;// Required for random unique word selection (See UniqueRandomNumber class)
        List<int> numbers;
        public string TrueChoice { get; set; }// The property required to validate user selection.
        int _counter = -1;
        int _category;
        byte _easy;
        int _wordNumber;

        //  Sets the next foreign word from list.
        private void SetWord()
        {
            Word=WordList[_counter][0];
            
        }

        //Gets one meaning randomly from 3 meanings.
        private string GetRandomTrueChoice(string[] word)
        {
            Random random = new Random();   
            
            int meanNumber = 1;
            
            for (int i = 1; i < 4; i++)
            {
                if (word[i] == null)
                {
                    var a = word[0];
                }
                if (word[i] != ""&& word[i]!=null)
                {
                    meanNumber++;
                }
            }

            return word[random.Next(1, meanNumber)];
        }
        
        
        private List<string> GetRandomWrongChoices(List<string[]> wrongWordsList)
        {
            Random random = new Random();
            List<string> wrongChoices = new List<string>();
            int wordNumber = 1;
            foreach (var wrongWords in wrongWordsList)
            {
                for (int i = 1; i < 4; i++)
                {
                    if (wrongWords[i] != "" && wrongWords[i]!=null)
                    {
                        wordNumber++;
                    }
                }
                wrongChoices.Add(wrongWords[random.Next(1, wordNumber)]);
                wordNumber = 0;

            }
            return wrongChoices;
        }
        
        
        // Sets wrong words list
        
        private void SetWrongWordList(byte easy)
        {
            if (easy == 1)
            {    
               do    
               {    
                 WrongWordsList = WordManagerHandler.GetRandomWordsByCategory     (0, 3);    
               }    
               while (CmpWord(Word, WrongWordsList));    
     
            }
            else
            {
               do
               {
                 WrongWordsList = WordManagerHandler.GetRandomWordsByCategory(_category, 3);
                    
               }
               while (CmpWord(Word, WrongWordsList));

            }    
 
        }


        // Sets all choices
        public void SetChoices()
        {
           
            Random random = new Random();
            var uniqueRandom = UniqueRandomNumber.CreateAsSingleton();
             numbers = uniqueRandom.GetRandomNumberList(4, 4);

            
            Choices[numbers[0]] = GetRandomTrueChoice(WordList[_counter]);// True choice
            TrueChoice = Choices[numbers[0]];


            var wrongChoiceList = GetRandomWrongChoices(WrongWordsList);
            Choices[numbers[1]] = wrongChoiceList[0];                   //
            Choices[numbers[2]] = wrongChoiceList[1];                   // Wrong choices
            Choices[numbers[3]] = wrongChoiceList[2];                   //

        }

        //Compares wrong words with each other.
        // if the same, returns true.
        public bool CmpWord(string word,List<string[]> wrongWordList)
        {
            foreach (var wordRow in wrongWordList)
            {
                if (word == wordRow[0])
                {
                    return true;
                }
            }
            return false;
        }

        
        //Compares the wrong word list with
        // every true word.If there is same words
        // returns true.
        public bool CompareWordLists(List<string[]> wordList,List<string[]> wrongWordList)
        {
            foreach (var wrongWord in wrongWordList)
            {
                foreach (var word in wordList)
                {
                    if (wrongWord[0] == word[0])
                    {
                        return true;
                    }
                }
            }
            return false;

        }


        //Begins the Test
        public void Start(int category, int wordNumber,byte easy)
        {
            _wordNumber = wordNumber;
            _easy = easy;
            _category = category;
            WordList = WordManagerHandler.GetRandomWordsByCategory(category,wordNumber);

            Next();

        }


        public bool CheckUserChoice(string userChoice)
        {
            if (userChoice == TrueChoice)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        // Sets next word and choices
        public void Next()
        {
            _counter++;

            if (_counter == _wordNumber - 1)
            {
                EndOfList= true;
            }

            if (_counter == _wordNumber)
            {

                Finish();

            }
            else
            {
                WrongWordsList.Clear();
                SetWord();
                SetWrongWordList(_easy);
                SetChoices();
                
            }

        }

        // Resets the player
        public void Finish()
        {
            _counter = -1;
            EndOfList = false;
            Word = "";
            WordList.Clear();
            Choices = new string[4];

        }


    }
}
