using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
namespace Feature.Player
{
    
    public abstract class BasePlayerLocomotion : MonoBehaviour
    {
        
        public Camera playerCamera; // Камера от первого лица

        public abstract Vector3 DesiredDeltaPos { get; set; }
        public abstract Vector3 CameraRotation { get; set; }
        public abstract void Jump();

        [SerializeReference, SubclassSelector]
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