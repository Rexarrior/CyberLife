using System;
using System.Collections.Generic;
namespace CyberLife
{
    /// <summary>
    /// Метаданный формы жизни
    /// </summary>
    public class LifeFormMetadata: Dictionary<string, StateMetadata>
    {
  


        /// <summary>
        /// Пространство, занимаемое формой жизни
        /// </summary>
        public Place Place { get; set; }


        /// <summary>
        /// Уникальный (для мира) идентификатор формы жизни
        /// </summary>
        public long Id { get; set; }


     
        /// <summary>
        /// Получает прототип метаданных формы жизни
        /// </summary>
        /// <returns>Прототип googleProtobuf</returns>
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


        /// <summary>
        /// Инициализирует метаданные формы жизни из 
        /// занимаемого ей пространства, ее уникального идентификатора и метаданных 
        /// состояний, которыми она обладает. 
        /// </summary>
        /// <param name="place">Пространство, занимаемое формой жизни</param>
        /// <param name="id">Уникальный для мира идентификатор формы жизни</param>
        /// <param name="statesMetadata">Метаданные состояний формы жизни</param>
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



        /// <summary>
        /// Инициализирует метаданные из их прототипа. 
        /// </summary>
        /// <param name="protoMetadata">Прототип googleProtobuff</param>
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