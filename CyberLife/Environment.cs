using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberLife
{
    public class Environment
    {
        #region field
        private List<IPhenomen> _naturalPhenomena;

        private MapSize _size;

        #endregion




        #region properties
        internal List<IPhenomen> NaturalPhenomena { get => _naturalPhenomena; }

        internal MapSize Size { get => _size; }
        #endregion




        #region methods
        /// <summary>
        /// Получает эффект воздействия окружающей среды на данную форму жизни
        /// на основании ее метаданных
        /// </summary>
        /// <param name="metadata">Метаданные формы жизни</param>
        /// <returns></returns>
        public List<StateMetadata> GetEffects(LifeFormMetadata metadata)
        {
            List<StateMetadata> ret = new List<StateMetadata>(); 

            foreach (var phenomen in _naturalPhenomena)
            {
                // Получаем список точек, находящихся в области действия природного явления.
                List<Point> points = phenomen.GetItPlace().Intersect(metadata.Place).Points;
                // Получаем воздействие природного явления на каждую точку. 
                foreach (var point in points)
                {
                    ret.AddRange(phenomen.GetEffects(point, metadata));
                }
            }

            return ret;
        }


        /// <summary>
        /// Получает метаданные этой окружающей среды. 
        /// </summary>
        /// <returns>Метаданные окружающей среды</returns>
        public EnvironmentMetadata GetMetadata()
        {
            return new EnvironmentMetadata(_size, _naturalPhenomena.Select(x => x.GetMetadata()).ToList());
        }

        
        /// <summary>
        /// Вызывает операцию для всех природных явлений, 
        /// принадлежащих этой окружающей среде. 
        /// </summary>
        public void Update()
        {
            EnvironmentMetadata metadata = GetMetadata();
            foreach (IPhenomen phenomen in _naturalPhenomena)
                phenomen.Update(metadata);
        }
        #endregion
        

        #region constructor
        /// <summary>
        /// Инициализирует окружающую среду из ее размера и списка природных явлений
        /// </summary>
        /// <param name="naturalPhenomena">Природные явления</param>
        /// <param name="size"></param>
        public Environment(List<IPhenomen> naturalPhenomena, MapSize size)
        {
            _naturalPhenomena = naturalPhenomena;
            _size = size;
        }


        /// <summary>
        /// Инициализирует экземпляр окружающей среды из ее метаданныхи фабрики природных явлений.
        /// Фабрика природных явлений должна "уметь" разбирать 
        /// все природные являения, содержащиеся в метаданных.
        /// </summary>
        /// <param name="environmentMetadata">Метаданные окружающей среды</param>
        /// <param name="phenomenaFabrica">Фабрика природных явлений</param>
        public Environment(EnvironmentMetadata environmentMetadata, PhenomenaFabrica phenomenaFabrica)
        {
            _size = environmentMetadata.Size;
            _naturalPhenomena = new List<IPhenomen>();
            foreach (var phenomenMetadata in environmentMetadata.Values)
            {

                _naturalPhenomena.Add(phenomenaFabrica.ReconstructPhenomen(phenomenMetadata));
            }
        }

        #endregion

    }
}
