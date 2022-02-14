using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WordBank.Entities.Abstract;
using WordBank.Entities.Concrete;
using WordBank.Logic.Abstract;
using WordBank.Logic.Concrete;

namespace WordBank.Logic.Players
{

    /*
      This class plays the word list given to it in order and checks user answer.
    Typing practice window runs this class.

     */
    public class InOrderPlayer:IInOrderPlayer
    {
        public InOrderPlayer()
        {
            WordList = new List<string[]>();
            Meanings = new string[3];

        }
        
        public List<string[]> WordList { get; set; } //Words to display to the user in order.
        public IWordManager WordManager { get; set; } //User selected word manager.
        public  string UserAnswer { get; set; }// User's answer to the next word.
        
        public  string Word { get; set; }
        public  string[] Meanings { get; set; }
        public  bool EndOfList { get; set; }
        public  bool Tolerated { get; set; }
        public  string ToleratedCorrectMean { get; set; }
        public  object _result;
        object[] parameters = new object[2];
        int _counter = 0;
        public WordManagerHandler wordManagerHandler { get; set; }
        

        public bool Restart 
        { 
          set
          {  if (value) 
             {
                    _counter = 0;
                    EndOfList = false;
                    Next();
                    
                
             } 
            
          } 

        }

        public  void Start(int category, int wordNumber)
        {
            EndOfList = false;
            WordList=wordManagerHandler.GetRandomWordsByCategory(category, wordNumber);
            Next();
        }

        public void Finish()
        {
            _counter = 0;
            Word = "";
            WordList.Clear();

        }

        public  void Next()
        {
            
            if (_counter == WordList.Count - 1)
            {
                EndOfList = true;
            }

            if (_counter == WordList.Count)
            {
                _counter = 0;
                
            }
            else
            {
                Word = WordList[_counter][0];
                Meanings[0]= WordList[_counter][1];
                Meanings[1] = WordList[_counter][2];
                Meanings[2] = WordList[_counter][3];

                _counter++;

            }
            
            
        }
        
        public  bool CheckAnswer(string userAnswer)
        {
            SimilarityCalculator similarityCalculator = new SimilarityCalculator();
            bool stat=false;
            
            foreach (var mean in Meanings)
            {
                if (mean != null)
                {
                   stat=similarityCalculator.Calculate(userAnswer,mean);
                    if (stat == true) 
                    {
                        Tolerated=similarityCalculator.Tolerated;
                        ToleratedCorrectMean = mean;
                        break; 
                    }
                }

            }
            return stat;
            
        }
    }
}
