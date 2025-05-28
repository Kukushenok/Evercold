using UnityEngine;
using UnityEngine.Events;
using Zenject;
namespace Feature.Player.WeaponManager
{
    [CreateAssetMenu(menuName = "Game/Debug weapon")]
    public class AimedDebugWeaponScriptableObject : BasicWeaponObject
    {
        [SerializeField] public GameObject spawnPrefab;
        public class Logic : IWeaponLogic, IWeaponShootCallback
        {
            public bool Enabled { get; set; }

            public UnityEvent OnShootButtonPressed { get; private set; } = new UnityEvent();
            private IShootButtonInput shootInput;
            private IPlayerLocomotion locomotion;
            private IInstantiator instantiator;
            private float dt = 0;
            private GameObject bullet;
            public Logic(IShootButtonInput shootInput, IPlayerLocomotion locomotion, IInstantiator inst, GameObject bullet)
            {
                this.shootInput = shootInput;
                this.locomotion = locomotion;
                this.instantiator = inst;
                this.bullet = bullet;
            }

            public void Dispose()
            {
                OnShootButtonPressed.RemoveAllListeners();
            }

            public void FixedTick()
            {

            }

            public void Initialize()
            {

            }
            private void Shoot()
            {
                OnShootButtonPressed?.Invoke();
                //Quaternion eulers = Quaternion.Euler(locomotion.CameraRotation) * Quaternion.Euler(locomotion.PlayerRotation);
                //Vector3 d = eulers * Vector3.forward;
                Ray rd = new Ray(locomotion.CameraGlobalPosition, locomotion.CameraForwardVec);
                if (Physics.SphereCast(rd, 0.5f, out RaycastHit info, 100.0f))
                {
                    GameObject gm = instantiator.InstantiatePrefab(bullet);
                    gm.transform.position = info.point;
                }

            }
            public void Tick()
            {
                if (dt > 0) dt -= Time.deltaTime;
                else if (Enabled && shootInput.LeftButtonDown())
                {
                    Shoot();
                    dt += 0.1f;
                }
                
            }
        }
        public override void Install(DiContainer instantiator)
        {
            instantiator.BindInterfacesAndSelfTo<Logic>().AsSingle().WithArguments(spawnPrefab);
        }
    }
}