using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberLife.Simple2DWorld
{
    /// <summary>
    /// Природное явление "солнце". Не реализовано.
    /// </summary>
    class SunPhenomen: IPhenomen
    {
        #region fields
        #endregion

        #region  properties



        #endregion

        #region Methods


        public void Update(EnvironmentMetadata environmentMetadata)
        {
            throw new NotImplementedException();
        }

        public List<StateMetadata> GetEffects(Point point, LifeFormMetadata lifeFormMetadata)
        {
            throw new NotImplementedException();
        }

        public bool isIn(Point point)
        {
            throw new NotImplementedException();
        }

        public Place GetItPlace()
        {
            throw new NotImplementedException();
        }

        public PhenomenMetadata GetMetadata()
        {
            throw new NotImplementedException();
        }


        #endregion


        #region  constructors

        

        #endregion
    }
}
