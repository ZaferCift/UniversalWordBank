using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordBank.Entities.Concrete;

namespace WordBank.DataAccess.Abstract
{
    
    public interface IEngWordDal:IEntityRepository<EngTrWord>
    {
    }
}
