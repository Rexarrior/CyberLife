using System;

namespace CyberLife
{
    public class StateMetadata
    {
        public string Name { get; set; }


        public double Value { get; set; }



        public Protobuff.Metadata.StateMetadata GetProtoMetadata()
        {

            Protobuff.Metadata.StateMetadata ret = new Protobuff.Metadata.StateMetadata();
            ret.Value = Value;
            ret.Name = Name;

            return ret;
        }



        




        public StateMetadata(string stateName, double value)
        {
            if (stateName == "")
                throw new ArgumentException("stateNmae shouldn't be empty", nameof(stateName));
            if (double.IsNaN(value))
                throw new ArgumentException("value shouldn't be NaN", nameof(value));

            Name = stateName;
            Value = value;
            

        }

        

        public StateMetadata(Protobuff.Metadata.StateMetadata protoMetadata)
        {
            if (protoMetadata == null)
                throw  new ArgumentNullException(nameof(protoMetadata));
            Name = protoMetadata.Name;
            Value = protoMetadata.Value;
        }

    }
}