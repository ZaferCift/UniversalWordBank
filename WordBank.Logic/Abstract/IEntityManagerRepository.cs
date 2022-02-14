using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WordBank.Entities.Abstract;
using WordBank.Entities.Concrete;

namespace WordBank.Logic.Abstract
{
    public interface IEntityManagerRepository<T>
    {
        List<string[]> GetRandomWordsByCategory(int category, int amount);
        int RecordCount(int category);
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        List<T> GetList(Expression<Func<T,bool>> filter);
        T GetByWord(string word);
        List<string[]> GetById(int id);


    }
}
