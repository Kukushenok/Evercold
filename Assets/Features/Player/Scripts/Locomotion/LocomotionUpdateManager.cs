using System.Collections;
using System.Collections.Generic;
using Feature.Player;
using UnityEngine;
using Zenject;

namespace Feature.Player
{
    public class LocomotionUpdateManager : ITickable, IFixedTickable
    {
        private List<ILocomotionFeature> _features;

        IPlayerLocomotion _locomotion;

        [Inject]
        public LocomotionUpdateManager(List<ILocomotionFeature> features, IPlayerLocomotion locomotion)
        {
            _features = features;
            _locomotion = locomotion;
        }

        public void Tick()
        {
            foreach (var _feature in _features)
            {
                _feature.OnUpdate(_locomotion);
            }
            _locomotion.Update();
        }

        public void FixedTick()
        {
            foreach (var _feature in _features)
            {
                _feature.OnFixedUpdate(_locomotion);
            }
            _locomotion.FixedUpdate();
        }
    }
}
