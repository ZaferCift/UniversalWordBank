using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordBank.Logic.Concrete;

namespace WordBank.Logic.Abstract
{
    public interface ITestPlayer:IPlayer
    {

        WordManagerHandler WordManagerHandler { get; set; }
       
         List<string[]> WordList { get; set; }
        
        string[] Choices { get; set; }
        string Word { get; set; }
        void Start(int category,int wordNumber,byte easy);
        bool CheckUserChoice(string userChoice);
        string TrueChoice { get; set; }
        bool EndOfList { get; set; }
        void Next();
        void Finish();
       
         

    }
}
