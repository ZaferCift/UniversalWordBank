using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordBank.Logic.Concrete
{
    /*
     This class generates a list of non-repeating 
    random number between 1(1 included) and the maximum value 
    for the given number of times.These random numbers correspond 
    to the id numbers in the table.EntityManagerRepositoryBase class fetches 
    the records according to these random numbers.
     */
    internal class UniqueRandomNumber
    {
        private static UniqueRandomNumber _uniqueRandomNumber;
        private UniqueRandomNumber()
        {

        }
        
        public static UniqueRandomNumber CreateAsSingleton()
        {
            return _uniqueRandomNumber ?? (_uniqueRandomNumber = new UniqueRandomNumber());
            
        } 
       
        public List<int> GetRandomNumberList( int max, int amount)
        {
            Random random = new Random();
            List<int> numberList = new List<int>();
            byte flag;
            int r;

            for (int i = 0; i < amount; i++)
            {
                
                do
                {
                    flag = 0;
                    r = random.Next(max);
                    foreach (var number in numberList)
                    {
                        if (number == r)
                        {
                            flag = 1;

                        }

                    }
                    if (flag == 0)
                    {
                        flag = 2;
                        numberList.Add(r);

                    }
                } while (flag!=2);
            }
            return numberList;
            
            
        }
    }
}
