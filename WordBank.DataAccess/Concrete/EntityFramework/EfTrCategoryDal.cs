using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordBank.DataAccess.Abstract;
using WordBank.Entities.Concrete;

namespace WordBank.DataAccess.Concrete.EntityFramework
{
    public class EfTrCategoryDal:EfEntityRepositoryBase<TrCategory,WordBankContext>,ITrCategoryDal
    {

    }
}
