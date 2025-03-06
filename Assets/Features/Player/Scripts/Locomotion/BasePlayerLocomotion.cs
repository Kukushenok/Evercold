using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using Zenject;
namespace Feature.Player
{
    
    public abstract class BasePlayerLocomotion : MonoBehaviour
    {
        [Inject]
        private void Construct(List<IPlayerLocomotionFeature> features){
            _playerFeatures = features;
        }
        
        public abstract Vector3 DesiredDeltaPos { get; set; }
        public abstract Vector3 CameraRotation { get; set; }
        public abstract void Jump();

        private List<IPlayerLocomotionFeature> _playerFeatures = new List<IPlayerLocomotionFeature>();

        protected abstract void OnLocomotionUpdate();
        protected abstract void OnLocomotionFixedUpdate();

        private void FixedUpdate()
        {

            foreach (var feature in _playerFeatures)
            {
                feature.LocomotionFixedUpdate(this);
            }
            OnLocomotionFixedUpdate();
        }

        private void Update()
        {

            foreach (var feature in _playerFeatures)
            {
                feature.LocomotionUpdate(this);
            }
            OnLocomotionUpdate();
        }
    }
}