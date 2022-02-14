using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordBank.DataAccess.Abstract;
using WordBank.DataAccess.Concrete.EntityFramework;
using WordBank.Entities.Abstract;
using WordBank.Entities.Concrete;
using WordBank.Logic.Abstract;
using WordBank.Logic.DependencyResolvers;

namespace WordBank.Logic.Concrete
{
    /*

       This manager class created for access to EngTrWords table data and Turkish
       category names.If you create another language pair table in database, the manager must
       be inherited from EntityManagerRepositoryBase and implemented its interface.
       ___________________________________________________________________________________________
      
       Example:For Spanish to German words table, the class will be;

       public class SpnGerManager:EntityManagerRepositoryBase
       <EfEntityRepositoryBase<SpnGerWord, WordBankContext>, SpnGerWord>,ISpnGerService
    
       public List<SpnCategory> Categories{get; set;}

       public SpnGerManager()
       {
            ISpnCategoryDal spnCategoryDal = InstanceFactory.GetInstance<ISpnCategoryDal>();
            Categories = spnCategoryDal.GetList();
            
       }
    
     
     */
    public class EngTrManager : EntityManagerRepositoryBase<EfEntityRepositoryBase<EngTrWord, WordBankContext>, EngTrWord>,IEngTrService
    {
        public List<TrCategory> Categories { get; set; }
       

        public EngTrManager()
        {
            ITrCategoryDal trCategoryDal = InstanceFactory.GetInstance<ITrCategoryDal>();
            Categories = trCategoryDal.GetList();
            
        }

        
    }
}
