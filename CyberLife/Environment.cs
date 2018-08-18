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
        public List<StateMetadata> GetEffects(LifeFormMetadata metadata)
        {
            List<StateMetadata> ret = new List<StateMetadata>(); 

            foreach (var phenomen in _naturalPhenomena)
            {
                List<Point> points = phenomen.GetItPlace().Intersect(metadata.Place).Points;
                foreach (var point in points)
                {
                    ret.AddRange(phenomen.GetEffects(point, metadata));
                }
            }

            return ret;
        }



        public EnvironmentMetadata GetMetadata()
        {
            return new EnvironmentMetadata(_size, _naturalPhenomena.Select(x => x.GetMetadata()).ToList());
        }

        

        public void Update()
        {
            EnvironmentMetadata metadata = GetMetadata();
            foreach (IPhenomen phenomen in _naturalPhenomena)
                phenomen.Update(metadata);
        }
        #endregion
        

        #region constructor
        public Environment(List<IPhenomen> naturalPhenomena, MapSize size)
        {
            _naturalPhenomena = naturalPhenomena;
            _size = size;
        }

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
