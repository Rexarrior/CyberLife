using System.Collections.Generic;
using System;
namespace CyberLife
{
    public interface IPhenomen
    {
        void Update(EnvironmentMetadata environmentMetadata);
        List<StateMetadata> GetEffects(Point point, LifeFormMetadata lifeFormMetadata);
        bool isIn(Point point);
        Place GetItPlace();
        PhenomenMetadata GetMetadata();
        
    }
}