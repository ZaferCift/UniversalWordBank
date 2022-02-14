using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordBank.DataAccess.Abstract;
using WordBank.Entities.Concrete;
using WordBank.Logic.Abstract;

namespace WordBank.Logic.Concrete
{
    /*
      To access turkish category names.
     */
    public class TrCategoryManager:ITrCategoryService
    {
        ITrCategoryDal _trCategoryDal;
        public TrCategoryManager(ITrCategoryDal trCategoryDal)
        {
            _trCategoryDal = trCategoryDal;
        }

        public void Add(TrCategory trCategory)
        {
            _trCategoryDal.Add(trCategory);
        }

        public void Delete(TrCategory trCategory)
        {
            _trCategoryDal.Delete(trCategory);
        }

        public TrCategory GetById(int id)
        {
            return _trCategoryDal.Get(p => p.Id == id);
        }

        public TrCategory GetByWord(string word)
        {
            return _trCategoryDal.Get(p => p.Category == word);
        }

        public List<TrCategory> GetList()
        {
            return _trCategoryDal.GetList();
        }

        public void Update(TrCategory trCategory)
        {
            _trCategoryDal.Update(trCategory);
        }
    }
}
