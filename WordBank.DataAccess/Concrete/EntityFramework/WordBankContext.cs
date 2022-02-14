using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordBank.Entities;
using WordBank.Entities.Concrete;

namespace WordBank.DataAccess.Concrete.EntityFramework
{
    public class WordBankContext:DbContext
    {
        public WordBankContext()
        {
            Database.SetInitializer<WordBankContext>(null);
        }
        public DbSet<EngTrWord> EngTrWords { get; set; }
        public DbSet<TrCategory> TrCategories { get; set; }

    }
}
