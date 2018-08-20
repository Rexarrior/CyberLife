using System;
using System.Collections.Generic;
namespace CyberLife
{
    public class PhenomenMetadata
    {


        /// <summary>
        /// Название природного явления
        /// </summary>
        public string Name { get; set; }
       


        /// <summary>
        /// Пространство, занимаемое природным явлением
        /// </summary>
        public Place Place { get; set; }



        //TODO Add the field for phenomen Type


        /// <summary>
        /// Получает прототип метаданных.
        /// </summary>
        /// <returns>Прототип GoogleProtobuf</returns>
        public Protobuff.Metadata.PhenomenMetadata GetProtoMetadata()
        {
            Protobuff.Metadata.PhenomenMetadata ret = new Protobuff.Metadata.PhenomenMetadata();
            ret.Place = Place.GetProtoPlace();
            ret.Name = Name;
            
            return ret;
        }









        /// <summary>
        /// Инициализирует метаданные природного явления из 
        /// его названия, занимаемого им пространства и списка его параметров
        /// </summary>
        /// <param name="phenomenName">Название природного явления</param>
        /// <param name="place">Пространство, занимаемое природным явлением</param>
        /// <param name="parameters">Дополнительные параметры природного явления</param>
        public PhenomenMetadata(string phenomenName, Place place, Dictionary<string, object> parameters = null)
        {
            if (phenomenName == "")
                throw new ArgumentException("phenomenName shouldn\'t be empty", nameof(phenomenName));


            Name = phenomenName;
            Place = place ?? throw new ArgumentNullException(nameof(place));
        }

        



        /// <summary>
        /// Инициализирует метаданные природного явления из прототипа
        /// </summary>
        /// <param name="protoMetadata">Прототип GoogleProtobuf</param>
        public PhenomenMetadata(Protobuff.Metadata.PhenomenMetadata protoMetadata)
        {
            if (protoMetadata == null)
                throw new ArgumentNullException(nameof(protoMetadata));

            Name = protoMetadata.Name;
            Place = new Place(protoMetadata.Place);
        }

    }
}