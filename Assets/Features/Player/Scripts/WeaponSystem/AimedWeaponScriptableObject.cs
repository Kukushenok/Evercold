using UnityEngine;
using UnityEngine.Events;
using Zenject;
namespace Feature.Player.WeaponManager
{
    [CreateAssetMenu(menuName = "Game/Aimed weapon")]
    public class AimedWeaponScriptableObject : ScriptableObject, IWeapon
    {
        [field: SerializeField] public WeaponInfo WeaponInfo { get; private set; }
        [field: SerializeField] public GameObject bulletPrefab { get; private set; }
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
                Ray rd = new Ray(locomotion.CameraPosition, locomotion.CameraForwardVec);
                RaycastHit[] ht = Physics.SphereCastAll(rd, 0.8f, 0.8f);
                foreach (RaycastHit info in ht)
                {
                    if(info.collider != null && info.collider.TryGetComponent(out IDamageable dmg))
                    {
                        dmg.TakeDamage(new AttackData(10, null, locomotion.CameraForwardVec * 2 + Vector3.up * 4));
                    }
                    GameObject gm = instantiator.InstantiatePrefab(bullet);
                    gm.transform.position = info.point;
                    Destroy(gm, 5);
                }

            }
            public void Tick()
            {
                if (dt > 0) dt -= Time.deltaTime;
                else if (shootInput.LeftButtonDown())
                {
                    Shoot();
                    dt += 0.5f;
                }
            }
        }

        public void Install(DiContainer instantiator)
        {
            // TODO: do here Bullet Factory.
            instantiator.BindInterfacesAndSelfTo<Logic>().AsSingle().WithArguments(bulletPrefab).NonLazy();
        }
    }
}