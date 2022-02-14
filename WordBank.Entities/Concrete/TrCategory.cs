using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordBank.Entities.Abstract;

namespace WordBank.Entities.Concrete
{
    /*
     Database object for TrCategories table.A category object must be implemented
    from IEntity interface.All category database objects are the same, only
    class name is different.
     */
    public class TrCategory:IEntity
    {
        public int Id { get; set; }
        public string Category { get; set; }

    }
}
