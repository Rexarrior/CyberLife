using System;
using System.Collections.Generic;
using  System.IO;
namespace CyberLife
{
    public class WorldMetadata
    {
        public EnvironmentMetadata EnvironmentMetadata;

        public Dictionary<Int64, LifeFormMetadata> LifeFormsMetadata;

        public string Name;

        public int Age;



        public Protobuff.Metadata.WorldMetadata GetProtoMetadata()
        {
            Protobuff.Metadata.WorldMetadata ret = new Protobuff.Metadata.WorldMetadata();

            ret.Age = Age;
            ret.EnvironmentMetadata = EnvironmentMetadata.GetProtoMetadata();
            ret.Name = Name;
            foreach (var pair in LifeFormsMetadata)
            {
                ret.LifeFormMetadata.Add(pair.Key, pair.Value.GetProtoMetadata());
            }

            return ret;
        }

        


       




        public WorldMetadata(EnvironmentMetadata environmentMetadata, Dictionary<long, LifeFormMetadata> lifeFormsMetadata, string name, int age)
        {
            if (name == "")
                throw new ArgumentException("name shouldn't be empty.", nameof(name));
            if (age < 0)
                throw new ArgumentException("age should be positive.", nameof(age));





            EnvironmentMetadata = environmentMetadata ?? throw new ArgumentNullException(nameof(environmentMetadata));
            LifeFormsMetadata = lifeFormsMetadata ?? throw new ArgumentNullException(nameof(lifeFormsMetadata));
            Name = name;
            Age = age;
        }




        public WorldMetadata(Protobuff.Metadata.WorldMetadata protoMetadata)
        {
            if (protoMetadata == null) 
                throw  new ArgumentNullException(nameof(protoMetadata));
            Name = protoMetadata.Name;
            Age = protoMetadata.Age;
            LifeFormsMetadata = new Dictionary<long, LifeFormMetadata>();
            EnvironmentMetadata = new EnvironmentMetadata(protoMetadata.EnvironmentMetadata);
            foreach (var pair in protoMetadata.LifeFormMetadata)
            {
                LifeFormsMetadata.Add(pair.Key, new LifeFormMetadata(pair.Value));
            }
        }
    }
}