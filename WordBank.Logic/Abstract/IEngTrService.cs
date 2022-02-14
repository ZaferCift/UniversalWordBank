using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordBank.Entities.Concrete;

namespace WordBank.Logic.Abstract
{
    public interface IEngTrService:IEntityManagerRepository<EngTrWord>,IWordManager
    {
        List<TrCategory> Categories { get; set; }
        

    }
}
