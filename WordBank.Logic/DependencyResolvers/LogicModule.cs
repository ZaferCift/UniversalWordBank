using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordBank.DataAccess.Abstract;
using WordBank.DataAccess.Concrete.EntityFramework;
using WordBank.Logic.Abstract;
using WordBank.Logic.Concrete;
using WordBank.Logic.Players;

namespace WordBank.Logic.DependencyResolvers
{
    public class LogicModule : NinjectModule
    {
        public override void Load()
        {

            Bind<IEngWordDal>().To<EfEngWordDal>().InSingletonScope();
            Bind<IEngTrService>().To<EngTrManager>().InSingletonScope();
            Bind<ITrCategoryDal>().To<EfTrCategoryDal>().InSingletonScope();
            Bind<IInOrderPlayer>().To<InOrderPlayer>().InSingletonScope();
            Bind<ITestPlayer>().To<TestPlayer>().InSingletonScope();   

        }
    }
}
