using System;
using System.Collections.Generic;
using System.Linq;
namespace CyberLife
{
    public class LifeForm
    {
        #region fields
        private Int64 _id; 
        private Place _place;

        private Dictionary<string, LifeFormState> _states;
        #endregion




        #region properties
        internal Dictionary<string, LifeFormState> States { get => _states; set => _states = value; }

        public Place Place { get => _place; set => _place = value; }
        public long Id { get => _id;  }
        #endregion




        #region methods
        public virtual void Update(List<StateMetadata> phenomenEffects)
        {
            foreach (var effect in phenomenEffects)
            {
                if (_states.ContainsKey(effect.Name))
                {
                    _states[effect.Name].Value += effect.Value;
                }
                else
                {
                    _states.Add(effect.Name, new LifeFormState(effect));
                }
                
            }
        }



        public LifeFormMetadata GetMetadata()
        {
            return new LifeFormMetadata(_place,_id, _states.Select(x => x.Value.GetMetadata()).ToList());
        }
        #endregion




        #region constructors
        public LifeForm(Place place, Dictionary<string, LifeFormState> states)
        {
            _place = place ?? throw new ArgumentNullException(nameof(place));
            _states = states ?? throw new ArgumentNullException(nameof(states));
            _id = DateTime.UtcNow.Second;
        }



        public LifeForm(LifeFormMetadata metadata)
        {
            if (metadata == null)
                throw new ArgumentNullException(nameof(metadata));
            _id = metadata.Id;
            _place = metadata.Place;
            States = new Dictionary<string, LifeFormState>();
            foreach (var stateMetadata in metadata.Values)
            {
                States.Add(stateMetadata.Name, new LifeFormState(stateMetadata));
            }

        }
        #endregion
    }
}