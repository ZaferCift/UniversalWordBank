# UniversalWordBank

This application has been developed with the aim of enabling people to learn the words in foreign languages divided into 
categories in a catchy way.The application consists of 2 parts. 1) Answering by typing 2)Test.

**1)** **Answering by typing:** The application creates a list of foreign words according to the language pair and category chosen 
by the user and shows the words in this list to the user in order, asking them to write one of the 3 meanings corresponding 
to each word.If the user does not know the answer, it gives a blank answer and the application shows the user the meanings of 
the word and moves on to the next word.The user restarts and replies the same words when the list is finished.
The user repeats this until they answers all the words in the list correctly.As the user constantly repeats and answers 
by typing, words become easier to remember.

**Note:** If the user makes a typo while typing the answer, the application tolerates it up to a certain level and accepts 
the answer as correct.

**2)** **Test:** As in the typing answer section, it creates a list according to language pair and category then shows the words 
in the list to the user in order, but presents the answers with 4 choices and waits for the user to choose the correct choice.
If the user ticks the wrong option, it shows the correct answer to the user and waits for the user to move on to the next word.
The purpose of this section is for the user to self-test.According to the test result, user can improve their vocabulary 
by working on the relevant category in the answering by typing section.It is very easy to find the correct answer as the easy 
mode brings the options from other categories.(Because the user knows which category they are working in).This mod can be 
used to learn vocabulary.

The hard mode, brings all the choices from the category determined by the user, so it is difficult to 
guess the correct answer.Hard mode is more suitable for the user to see their own level.


Currently, there is an English-Turkish table (EngTrWord object)in the database only for users whose native language is Turkish but
the project complies with S.O.L.I.D principles.In this way, tables in other languages can be added to the database and related 
database objects can be created even new teaching methods can be added by designing different players.These objects require 
some interface implementations and inheritance.

Eventually a great vocabulary tutorial app with so many language options may emerge.In fact, there is so much that 
can be done in this project, and it seems like there is no end to what can be done.If you are interested in this project or 
if you are a student, you can support this project to improve both yourself and the project.

The project includes these technologies:

-ADO.Net <br>
-Entity Framework <br>
-Ninject <br>
-WPF <br>

Database backup is in the Database backup folder.You need to Microsoft SQL Server Management Studio for restoring the backup.
 Connection String is: <br>
**add name="WordBankContext" connectionString="data source=(localdb)\mssqllocaldb;initial catalog=WordBank;integrated security=true" providerName="System.Data.SqlClient"**
	           

_________________________________________________________________________

### For Example:

For a native German speaker to be able to study Spanish words, the following tables must be added to the database.<br>
-SpnGerWords <br>
-GerCategories

Tables must have the same properties as existing tables.For the test player, for each category in the SpnGerWords table,
there must be minimum 4 records.Then the following objects and methods must be added to related namespaces.


namespace WordBank.Entities.Concrete

    public class SpnGerWord : IEntity, ILogicEntity
    {

        public int Id { get; set; }
        public string Word { get; set; }
        public string Mean1 { get; set; }
        public string Mean2 { get; set; }
        public string Mean3 { get; set; }
        public int Category { get; set; }

    }

namespace WordBank.Entities.Concrete

    public class GerCategory : IEntity
    {
        public int Id { get; set; }
        public string Category { get; set; }

    }



namespace WordBank.DataAccess.Abstract

    public interface ISpnGerWordDal: IEntityRepository<SpnGerWord>
    {

    }

namespace WordBank.DataAccess.Abstract

    public interface IGerCategoryDal : IEntityRepository<GerCategory>
    {

    }

namespace WordBank.DataAccess.Concrete.EntityFramework

    public class EfSpnGerWordDal: EfEntityRepositoryBase<SpnGerWord, WordBankContext>, ISpnGerWordDal
    {


    }

namespace WordBank.DataAccess.Concrete.EntityFramework

    public class EfGerCategoryDal: EfEntityRepositoryBase<GerCategory, WordBankContext>, IGerCategoryDal
    {

    }


namespace WordBank.DataAccess.Concrete.EntityFramework

       //Add these 2 properties to WordBankContext class

        public DbSet<SpnGerWord> SpnGerWords { get; set; }
        public DbSet<GerCategory> GerCategories { get; set; }


namespace WordBank.Logic.Abstract

    public interface ISpnGerService : IEntityManagerRepository<SpnGerWord>, IWordManager
    {
        List<GerCategory> Categories { get; set; }


    }

namespace WordBank.Logic.Abstract

    public interface IGerCategoryService
    {
        
            void Add(GerCategory trCategory);
            void Delete(GerCategory trCategory);
            void Update(GerCategory trCategory);
            List<GerCategory> GetList();
            GerCategory GetByWord(string word);
            GerCategory GetById(int id);
        
    }


namespace WordBank.Logic.Concrete

    public class SpnGerManager : EntityManagerRepositoryBase<EfEntityRepositoryBase<SpnGerWord, WordBankContext>, SpnGerWord>, ISpnGerService
    {
        public List<GerCategory> Categories { get; set; }


        public SpnGerManager()
        {
            IGerCategoryDal GerCategoryDal = InstanceFactory.GetInstance<IGerCategoryDal>();
            Categories = gerCategoryDal.GetList();

        }


    }

namespace WordBank.Logic.Concrete

        
        public class GerCategoryManager : IGerCategoryService
        {
            IGerCategoryDal _gerCategoryDal;
            public GerCategoryManager(IGerCategoryDal gerCategoryDal)
            {
                _gerCategoryDal = gerCategoryDal;
            }

            public void Add(GerCategory gerCategory)
            {
                _gerCategoryDal.Add(gerCategory);
            }

            public void Delete(GerCategory gerCategory)
            {
                _gerCategoryDal.Delete(gerCategory);
            }

            public GerCategory GetById(int id)
            {
                return _gerCategoryDal.Get(p => p.Id == id);
            }

            public GerCategory GetByWord(string word)
            {
                return _gerCategoryDal.Get(p => p.Category == word);
            }

            public List<GerCategory> GetList()
            {
                return _gerCategoryDal.GetList();
            }

            public void Update(GerCategory gerCategory)
            {
                _gerCategoryDal.Update(gerCategory);
            }
        }


namespace WordBank.Logic.DependencyResolvers

       //Add these bindings to LogicModule class

       Bind<ISpnGerWordDal>().To<EfSpnGerWordDal>().InSingletonScope();
       Bind<ISpnGerService>().To<SpnGerManager>().InSingletonScope();
       Bind<IGerCategoryDal>().To<EfGerCategoryDal>().InSingletonScope();


namespace WordBank.WpfAppUI
         
        // Add a combobox item which content is "SpnGer" to cbxLanguages combobox and add a field like below 
        //to MainWindow class
        
        ISpnGerService _spnGerService;

        private void ComboBoxItem_Selected_1(object sender, RoutedEventArgs e)
        {
            _spnGerService = InstanceFactory.GetInstance<ISpnGerService>();

            WordManagerHandler.WordManager = _spnGerService;
            SetUnits();
            
        }



