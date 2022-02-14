using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WordBank.Logic.Concrete
{
    /*
      Similarity Calculator checks the similarity between correct answer and 
    user answer.If correct answer and user answer are similar, Calculate 
    method returns true.
      The class simply consists of two parts.First part calculates similarity result,
    second part comments the result and returns true or false;

    */
    public class SimilarityCalculator
    {
        int _counter = 1;
        public char[,] _correctAnswerChars;
        public char[,] _userAnswerChars; 
        public string[] _result;
        public bool Tolerated { get; set; }
        public bool Calculate(string userAnswer,string correctAnswer)
        {
            Tolerated = false;
            _result = new string[correctAnswer.Length];
            

            if ((correctAnswer.Length-userAnswer.Length>-2) && (correctAnswer.Length-userAnswer.Length < 2)&&(userAnswer!=""))
            {
                _userAnswerChars = StrTo2DCharArray(userAnswer);
                _correctAnswerChars = StrTo2DCharArray(correctAnswer);

                _userAnswerChars=NumberTheSameChars(_userAnswerChars);
                _correctAnswerChars=NumberTheSameChars(_correctAnswerChars); 
                

                for (int a = 0; a < correctAnswer.Length; a++)
                {
                    for (int b = 0; b < userAnswer.Length; b++)
                    {
                        if (_correctAnswerChars[0,a] == _userAnswerChars[0,b]&& _correctAnswerChars[1,a]==_userAnswerChars[1,b]) 
                        {
                            _result[a] = (b-a).ToString();

                        }
                       
                    }
                    if (_result[a]==null)
                    {
                        _result[a] = "!";
                    }
                }
                
                if (CommentTheResult())
                {
                    if (userAnswer != correctAnswer) { Tolerated = true; }
                    
                    return true;
                }
                else
                {
                    
                    return false;
                }

 
            }
            else
            {
                
                return false;
            }
        }

        private char[,] NumberTheSameChars(char[,] chars)
        {
            char[,] charArray = new char[2, chars.GetLength(1)];
            for (int i = 0; i < chars.GetLength(1); i++)
            {
                charArray[0,i] = chars[0,i];
            }

            for (int i = 0; i < charArray.GetLength(1) ; i++)
            {
                if (charArray[1, i] == 0) {charArray[1, i] = Convert.ToChar("1"); }
                for (int k = 0; k < charArray.GetLength(1); k++)
                {
                    if (i != k && charArray[0, i] == charArray[0, k] && charArray[1, i] == Convert.ToChar("1"))
                    {
                        _counter++;
                        charArray[1, k] = Convert.ToChar(_counter.ToString());

                    }
                   
                }
                _counter = 1;
            }
            return charArray;
        }
        private char[,] StrTo2DCharArray(string answer)
        {
            char[,] splittedStr = new char[2,answer.Length];
            for (int i = 0; i < answer.Length; i++)
            {
                splittedStr[0, i] = Convert.ToChar(answer.Substring(i, 1));
            }
            return splittedStr;
            
        }

        private bool CommentTheResult()
        {
            
            
            bool flag=true;
            int exclamationAmount=0;
            foreach (var chr in _result)
            {
                if (chr == "!")
                {
                    exclamationAmount++;
                }

                if ((chr!="!" && Convert.ToInt32(chr) < -1) || (chr!="!" && Convert.ToInt32(chr) > 1)
                    || exclamationAmount > 1 || ((_userAnswerChars.GetLength(1)!=_correctAnswerChars.GetLength(1))
                     &&exclamationAmount>1))
                {
                    exclamationAmount = 0;
                    
                    flag = false;
                    break;
                }
                


            }
            return flag;
        }

    }
}
