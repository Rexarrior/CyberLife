using System;
using System.Collections.Generic;
namespace CyberLife
{
    public class EnvironmentMetadata: Dictionary<string, PhenomenMetadata>
    {
        /// <summary>
        /// Размер поля окружающей среды
        /// </summary>
        public MapSize Size;



        /// <summary>
        /// Получает прототип метаданных этой окружающей среды
        /// </summary>
        /// <returns></returns>
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



        /// <summary>
        /// Инициализирует метаданные окружающей среды из размера поля и 
        /// метаданных природных явлений, действующих в окружающей среде
        /// </summary>
        /// <param name="size">Размер поля</param>
        /// <param name="phenomenaMetadata">Метаданные природных явлений</param>
        public EnvironmentMetadata(MapSize size, List<PhenomenMetadata> phenomenaMetadata)
        {
            Size = size ?? throw new ArgumentNullException(nameof(size));
            foreach (var phenomen in phenomenaMetadata ?? throw  new ArgumentNullException(nameof(phenomenaMetadata)))
            {
                this.Add(phenomen.Name, phenomen);
            }
        }



        /// <summary>
        /// Инициализирует метаданные окружающей среды из их прототипа
        /// </summary>
        /// <param name="protoMetadata"></param>
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