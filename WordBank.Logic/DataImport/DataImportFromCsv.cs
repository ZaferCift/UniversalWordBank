using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using WordBank.DataAccess.Abstract;
//using System.Windows.Forms;
using WordBank.Entities.Abstract;
using WordBank.Entities.Concrete;


namespace WordBank.Logic.DataImport
{

    /*
     
     If words are wanted to be added to the database from a csv file, this class 
    can be used.
     The current version of the class does not support multiple encoding.
    
     */
    public class DataImport<TEntity, TRepository> where TEntity : class, IEntity, new()
        where TRepository : class, IEntityRepository<TEntity>, new()

    {
        private TRepository _entityRepository;
        private StreamReader _streamReader;
        private TEntity _instance;
        private PropertyInfo[] _properties;
        
        private Type _entity;

        public DataImport(string fileName)
        {
            
            
            FileStream csvFile = new FileStream(fileName, FileMode.Open);
            _streamReader = new StreamReader(csvFile, Encoding.GetEncoding("iso-8859-9"), false);
            
            _entity = typeof(TEntity);
            _instance = (TEntity)Activator.CreateInstance(_entity);
            _properties = _instance.GetType().GetProperties();


        }

        public List<TEntity> CsvToEntityList()
        {
            string subString = "";
            string line;
            List<TEntity> entityList = new List<TEntity>();
            var entity = typeof(TEntity);
            int propertyCounter = 0;

            while (!_streamReader.EndOfStream)
            {

                line = _streamReader.ReadLine();
                line += ";";
                subString = "";
                for (int a = 0; a < line.Length; a++)
                {
                    if (line.Substring(a, 1) != ";")
                    {
                        subString += line.Substring(a, 1);

                    }
                    else if (line.Substring(a, 1) == ";")
                    {
                        propertyCounter++;
                        _properties[propertyCounter].SetValue(_instance, subString);

                        if (propertyCounter == _properties.Length - 1)
                        {
                            propertyCounter = 0;
                        }
                        subString = "";
                    }
                }

                entityList.Add(_instance);
                _instance = (TEntity)Activator.CreateInstance(_entity);
            }
            return entityList;
        }

        public void CsvToTable()
        {
            _entityRepository = new TRepository();
            var entityList = CsvToEntityList();
            foreach (var entity in entityList)
            {
                _entityRepository.Add(entity);
                
            }
        }




    }


}
