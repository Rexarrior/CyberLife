using System;
using System.Collections.Generic;
namespace CyberLife
{
    public class PhenomenMetadata
    {
        


        public string Name { get; set; }
       



        public Place Place { get; set; }





        public Protobuff.Metadata.PhenomenMetadata GetProtoMetadata()
        {
            Protobuff.Metadata.PhenomenMetadata ret = new Protobuff.Metadata.PhenomenMetadata();
            ret.Place = Place.GetProtoPlace();
            ret.Name = Name;
            
            return ret;
        }










        public PhenomenMetadata(string phenomenName, Place place, Dictionary<string, object> parameters = null)
        {
            if (phenomenName == "")
                throw new ArgumentException("phenomenName shouldn\'t be empty", nameof(phenomenName));


            Name = phenomenName;
            Place = place ?? throw new ArgumentNullException(nameof(place));
        }

        




        public PhenomenMetadata(Protobuff.Metadata.PhenomenMetadata protoMetadata)
        {
            if (protoMetadata == null)
                throw new ArgumentNullException(nameof(protoMetadata));

            Name = protoMetadata.Name;
            Place = new Place(protoMetadata.Place);
        }

    }
}