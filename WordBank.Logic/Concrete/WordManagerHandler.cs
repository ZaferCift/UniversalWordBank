using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordBank.Entities.Abstract;
using WordBank.Logic.Abstract;

namespace WordBank.Logic.Concrete
{
    /*
     
    WordManagerHandler is used to access the methods inside 
    the user selected WordManager class at runtime.Runs from user interface.
     */
    public class WordManagerHandler
    {
        
        public IWordManager WordManager{ get; set; }// Selected WordManager by user.
         

        public List<string[]> GetRandomWordsByCategory(int category,int wordNumber)
        {
            object[] parameters = new object[2];
            var method = WordManager.GetType().GetMethod("GetRandomWordsByCategory");
            parameters[0] = category;
            parameters[1] = wordNumber;
            
            return (List<string[]>)method.Invoke(WordManager, parameters);

        }

        public IEnumerable GetCategories()
        {
            var  categoryProperty = WordManager.GetType().GetProperty("Categories");
            return (IEnumerable)categoryProperty.GetValue(WordManager);
        }

        public string[] GetWordById(int id)
        {
            object[] parameters = new object[1];
            var method = WordManager.GetType().GetMethod("GetById");
            parameters[0] = id;
            

            return (string[])method.Invoke(WordManager, parameters);
        }

        public int RecordCounter(int categoryId=0)
        {
            
                object[] category = new object[1];
                category[0] = categoryId;
                var recordCount = WordManager.GetType().GetMethod("RecordCount");

                return (int)recordCount.Invoke(WordManager, category);
           
            



        }





    }
}
