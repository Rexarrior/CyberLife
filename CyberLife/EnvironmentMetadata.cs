using System;
using System.Collections.Generic;
namespace CyberLife
{
    public class EnvironmentMetadata: Dictionary<string, PhenomenMetadata>
    {
        public MapSize Size;



        public Protobuff.Metadata.EnvironmentMetadata GetProtoMetadata()
        {
            Protobuff.Metadata.EnvironmentMetadata ret = new Protobuff.Metadata.EnvironmentMetadata();
            ret.MapSize = Size.GetProtoMapSize();
            foreach (var phenomen in this.Values)
            {
                ret.PhenomenaMetadata.Add(phenomen.GetProtoMetadata());
            }

            return ret;
        }


        public EnvironmentMetadata(MapSize size, List<PhenomenMetadata> phenomenaMetadata)
        {
            Size = size ?? throw new ArgumentNullException(nameof(size));
            foreach (var phenomen in phenomenaMetadata ?? throw  new ArgumentNullException(nameof(phenomenaMetadata)))
            {
                this.Add(phenomen.Name, phenomen);
            }
        }



        public EnvironmentMetadata(Protobuff.Metadata.EnvironmentMetadata protoMetadata)
        {
            Size = new MapSize(protoMetadata.MapSize);

            foreach (var phenomenMetadata in protoMetadata.PhenomenaMetadata)
            {
                this.Add(phenomenMetadata.Name, new PhenomenMetadata(phenomenMetadata));
            }
        }
    }
}