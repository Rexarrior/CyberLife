using System;

namespace CyberLife
{
    public class StateMetadata
    {
        public string Name { get; set; }


        public double Value { get; set; }


        /// <summary>
        /// Получает прототип этих метаданных
        /// </summary>
        /// <returns>прототип googleProtobuff</returns>
        public Protobuff.Metadata.StateMetadata GetProtoMetadata()
        {

            Protobuff.Metadata.StateMetadata ret = new Protobuff.Metadata.StateMetadata();
            ret.Value = Value;
            ret.Name = Name;

            return ret;
        }



        



        /// <summary>
        /// Инициализирует класс метаданные состояния из 
        /// имени состояния и его значения
        ///  
        /// </summary>
        /// <param name="stateName">Имя состояния</param>
        /// <param name="value">Значение состояния</param>
        public StateMetadata(string stateName, double value)
        {
            if (stateName == "")
                throw new ArgumentException("stateNmae shouldn't be empty", nameof(stateName));
            if (double.IsNaN(value))
                throw new ArgumentException("value shouldn't be NaN", nameof(value));

            Name = stateName;
            Value = value;
            

        }

        

        /// <summary>
        /// Инициализирует метаданные состояния из их прототипа.
        /// </summary>
        /// <param name="protoMetadata">Прототип GoogleProtobuff</param>
        public StateMetadata(Protobuff.Metadata.StateMetadata protoMetadata)
        {
            if (protoMetadata == null)
                throw  new ArgumentNullException(nameof(protoMetadata));
            Name = protoMetadata.Name;
            Value = protoMetadata.Value;
        }

    }
}