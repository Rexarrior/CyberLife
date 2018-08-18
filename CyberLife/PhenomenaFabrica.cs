using System;
using System.Collections.Generic;
using System.Linq;
namespace CyberLife
{
    public class PhenomenaFabrica
    {
        

        public virtual IPhenomen ReconstructPhenomen(PhenomenMetadata phenomenMetadata)
        {
            throw new NotImplementedException();
        }

        public static object PhenomenFromString(string pairValue)
        {
            throw new NotImplementedException();
        }
    }
}