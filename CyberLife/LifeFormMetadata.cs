using System;
using System.Collections.Generic;
namespace CyberLife
{
    public class LifeFormMetadata: Dictionary<string, StateMetadata>
    {
  



        public Place Place { get; set; }

        public long Id { get; set; }


     

        public Protobuff.Metadata.LifeFormMetadata GetProtoMetadata()
        {
            Protobuff.Metadata.LifeFormMetadata ret = new Protobuff.Metadata.LifeFormMetadata();

            ret.Id = Id;
            ret.Place = Place.GetProtoPlace();

            foreach (var state in this.Values)
            {
                ret.StatesMetadata.Add(state.GetProtoMetadata());
            }
            

            return ret;

        }


        public LifeFormMetadata(Place place, Int64 id,  List<StateMetadata> statesMetadata)
        {

            if (Place == null)
                throw new ArgumentNullException(nameof(place));
            Place = place;
            foreach (var state in statesMetadata ?? throw new ArgumentNullException(nameof(statesMetadata)))
            {
                this.Add(state.Name, state);
            }
            Id = id; 
        }




        public LifeFormMetadata(Protobuff.Metadata.LifeFormMetadata  protoMetadata)
        {
            Place = new Place(protoMetadata.Place);
            Id = protoMetadata.Id;

            foreach (var state in protoMetadata.StatesMetadata)
            {
                this.Add(state.Name, new StateMetadata(state));
            }
        }
    }
}