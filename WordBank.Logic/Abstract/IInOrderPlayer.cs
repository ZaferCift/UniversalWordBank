using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordBank.Entities.Abstract;
using WordBank.Logic.Concrete;

namespace WordBank.Logic.Abstract
{
    public interface IInOrderPlayer:IPlayer
    {
        WordManagerHandler wordManagerHandler { get; set; }
        IWordManager WordManager { get; set; }
        string UserAnswer { get; set; }
        List<string[]> WordList { get; set; }
        string[] Meanings { get; set; }
        
        bool EndOfList { get; set; }
        bool Restart {  set; }
        void Start(int category,int wordNumber);
        void Finish();

        string Word { get; set; }
        void Next();
        bool CheckAnswer(string userAnswer);
        bool Tolerated{ get; set; }
        string ToleratedCorrectMean { get; set; }
    }
}
