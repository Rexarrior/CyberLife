    using System;
using System.Collections.Generic;
using System.Linq;
using  System.IO;
using Google.Protobuf;

namespace CyberLife
{
    /// <summary>
    /// Реаилзует цельный мир.
    /// </summary>
    public class World
    {
        #region fields
        private string _name;
        private Environment _environment;
        private IVisualizer _visualizer;
        private Dictionary<Int64, LifeForm> _lifeForms;
        private int _age;




        #endregion




        #region properties
        public string Name { get => _name; set => _name = value; }
        public Dictionary<Int64, LifeForm> LifeForms { get => _lifeForms;  }
        public Environment Environment { get => _environment;  }
        public IVisualizer Visualizer { get => _visualizer; set => _visualizer = value; }//todo

        #endregion




        #region methods
        /// <summary>
        /// Вызывает обновление всех компонентов мира. 
        /// </summary>
        public void Update()
        {
            _environment.Update();
          //Todo life forms update??  
        }



        /// <summary>
        /// Получает метадату этого мира.
        /// </summary>
        /// <returns></returns>
        public WorldMetadata GetMetadata()
        {
            Dictionary<long, LifeFormMetadata> lifeFormsMetadata = new Dictionary<long, LifeFormMetadata>();
            foreach(var lifeFormRec in _lifeForms)
            {
                lifeFormsMetadata.Add(lifeFormRec.Key, lifeFormRec.Value.GetMetadata());
            }
            return new WorldMetadata(_environment.GetMetadata(), lifeFormsMetadata, _name, _age); 
        }
        

        /// <summary>
        /// Cохраняет этот мир в бинарном формате по протоколу googleProtobuff
        /// </summary>
        /// <param name="fileName">Имя файла для сохранения</param>
        public void SaveToFile(string fileName)
        {
            Protobuff.Metadata.WorldMetadata metadata = this.GetMetadata().GetProtoMetadata();

            try
            {

                FileStream fs = new FileStream(fileName, FileMode.CreateNew, FileAccess.Write);
                metadata.WriteTo(fs);
                fs.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }


        /// <summary>
        /// Загружает мир из бинарного файла в формате googleProtobuff. 
        /// </summary>
        /// <param name="fileName">Имя файла для загрузки</param>
        /// <param name="fabrica">Фабрика природных явлений</param>
        /// <returns>Загруженный мир</returns>
        public static World LoadFromFile(string fileName, PhenomenaFabrica fabrica)
        {
            return new World(new WorldMetadata(
                Protobuff.Metadata.WorldMetadata.Parser.ParseFrom(File.ReadAllBytes(fileName))), fabrica);
        }





        #endregion




        #region constructors
        /// <summary>
        /// Инициализирует мир на основе его компонентов
        /// </summary>
        /// <param name="name">Название мира</param>
        /// <param name="environment">Окружающая среда для этого мира</param>
        /// <param name="visualizer">Визуализатор, предназначенный для отрисовки компонентов мира</param>
        /// <param name="lifeForms">Формы жизни</param>
        public World(string name, Environment environment, IVisualizer visualizer, List<LifeForm> lifeForms)
        {
            if (lifeForms == null)
                throw new ArgumentNullException(nameof(lifeForms));

            if (lifeForms.Count == 0)
                throw new ArgumentException("List of life forms shouldn\t be empty.", nameof(lifeForms));

            if (name == "")
                throw new ArgumentException("Name shouldn't be empty", nameof(name));

            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
            _visualizer = visualizer ?? throw new ArgumentNullException(nameof(visualizer));
            _name = name;
            _lifeForms = new Dictionary<long, LifeForm>();
            foreach (var lifeForm in lifeForms)
            {
                _lifeForms.Add(lifeForm.Id, lifeForm);
            }
        }


        
        /// <summary>
        /// Инициализирует экземпляр мира из его метадаты 
        /// и фабрики природных явлений
        /// </summary>
        /// <param name="metadata">метаданные мира</param>
        /// <param name="phenomenaFabrica">Фабрика для разбора прородных явлений, содержащихся в метаданных</param>
        public World(WorldMetadata metadata, PhenomenaFabrica phenomenaFabrica)
        {
            _environment = new Environment(metadata.EnvironmentMetadata, phenomenaFabrica);
            _name = metadata.Name;
            _age = metadata.Age;
            _lifeForms = new Dictionary<long, LifeForm>();
            foreach (var lifeFormMetadata in metadata.LifeFormsMetadata.Values)
            {
                _lifeForms.Add(lifeFormMetadata.Id, new LifeForm(lifeFormMetadata));
            }
        }
        
        #endregion
    }
}