using System;
namespace CyberLife
{
    public class LifeFormState
    {
        private string _name;
        private double _value;
        

        public string Name { get => _name; }
        public double Value { get => _value; set => _value = value; }

        public virtual StateMetadata GetMetadata()
        {
            return new StateMetadata(_name, _value);
        }



        public LifeFormState(string name, double value)
        {
            if (name == "")
                throw new ArgumentException("name shouldn't be empty", "name");
            if (double.IsNaN(value))
                throw new ArgumentException("value shouldn't be NaN", "value");


            _name = name;
            _value = value;
        }




        public LifeFormState(StateMetadata metadata)
        {
            if (metadata == null)
                throw new ArgumentNullException("metadata");


            _name = metadata.Name;
            _value = metadata.Value;

        }
    }
}