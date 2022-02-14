using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordBank.Entities.Concrete;

namespace WordBank.Logic.Abstract
{
    public interface ITrCategoryService
    {
        void Add(TrCategory trCategory);
        void Delete(TrCategory trCategory);
        void Update(TrCategory trCategory);
        List<TrCategory> GetList();
        TrCategory GetByWord(string word);
        TrCategory GetById(int id);
    }
}
