using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordBank.Entities.Abstract;

namespace WordBank.Entities.Concrete
{
    /*
     A language pair database object for EngTrWords table.A language pair object
    must be implemented from IEntity and ILogicEntity.IEntity for CRUD operation,
    ILogicEntity for EntityManagerRepositoryBase operations.All language pair 
    database objects are the same, only class name is different.
     */
    public class EngTrWord:IEntity,ILogicEntity
    {
        
        public int Id { get; set; }
        public string Word { get; set; }
        public string Mean1 { get; set; }
        public string Mean2 { get; set; }
        public string Mean3 { get; set; }
        public int Category { get; set; }

    }
}
