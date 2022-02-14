using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WordBank.DataAccess.Abstract;
using WordBank.Entities.Abstract;
using WordBank.Logic.Abstract;

namespace WordBank.Logic.Concrete
{
    /*
      
      All ILogicEntity objects are language pair.So there is no difference between them.
    Only contents are different.So EntityManagerRepositoryBase class is base of all manager classes 
    
    */
    public class EntityManagerRepositoryBase<TRepository,TEntity>:IEntityManagerRepository<TEntity>
        where TEntity:class,ILogicEntity,new() where TRepository : class, IEntityRepository<TEntity>, new()
    {
        TRepository _repository = new TRepository();
       
        UniqueRandomNumber _randomNumberList = UniqueRandomNumber.CreateAsSingleton();

        public int RecordCount(int category=0)
        {
            if (category != 0)
            {
                return _repository.GetList(p => p.Category == category).Count;
            }
            else
            {
                return _repository.GetList().Count;
            }
            
                
            
            
            
        } 
        public List<string[]> GetRandomWordsByCategory(int category=0,int amount=0)
        {
            
            List<TEntity> entityList=new List<TEntity>();
            if (amount == 0)
            {
                
                entityList=category==0 ?_repository.GetList(): _repository.GetList(p => p.Category == category);
            }
            else
            {
               
                List<int> randomNumbers;
                List<TEntity> list;
                
                

                if (category==0)
                {
                   list = _repository.GetList();
                    randomNumbers = new List<int>();
                    randomNumbers= _randomNumberList.GetRandomNumberList(list.Count, amount);
                    
                    
                    

                    foreach (var randomNumber in randomNumbers)
                    {
                        entityList.Add(list[randomNumber]);

                    }

                    
                }
                else
                {
                    list = _repository.GetList(p => p.Category == category);
                    
                    randomNumbers = new List<int>();
                   
                    randomNumbers=_randomNumberList.GetRandomNumberList(list.Count, amount);
                    foreach (var randomNumber in randomNumbers)
                    {
                        entityList.Add(list[randomNumber]);
                    }
                   
                    
                }


            }
            return EntityListToStringArrayList(entityList);

        }

        private List<string[]> EntityListToStringArrayList(List<TEntity> entityList)
        {
            List<string[]> WordList = new List<string[]>();
            
            foreach (var entity in entityList)
            {

                string[] wordArray = new string[4];
                wordArray[0] = entity.Word;
                wordArray[1] = entity.Mean1;
                wordArray[2] = entity.Mean2;
                wordArray[3] = entity.Mean3;
                WordList.Add(wordArray);

            }
            return WordList;
        }

        
        public void Add(TEntity entity)
        {
            _repository.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            _repository.Delete(entity);
        }

        public void Update(TEntity entity)
        {
            _repository.Update(entity);
        }

        public List<TEntity> GetList(Expression<Func<TEntity, bool>> filter)
        {
            return _repository.GetList(filter);
        }
        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            return _repository.Get(filter);
        }

        public TEntity GetByWord(string word)
        {
            throw new NotImplementedException();
        }

        public List<string[]> GetById(int id)
        {
            List<TEntity> words = new List<TEntity>();
            words.Add(_repository.Get(p => p.Id == id));

            var wordWithMeans = EntityListToStringArrayList(words);
            return wordWithMeans;
        }
    }
}
