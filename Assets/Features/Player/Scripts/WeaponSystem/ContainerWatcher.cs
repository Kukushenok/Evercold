using System;
using System.Collections.Generic;
using Zenject;
namespace Feature.Player.WeaponManager
{
    /// <summary>
    /// I hate myself.
    /// </summary>
    public interface IWeaponLogic: IEnableable, ITickable, IFixedTickable, IDisposable, IInitializable
    {

    }
    public class ContainerWatcher: IEnableable, ITickable, IFixedTickable, IDisposable, IInitializable
    {
        private DiContainer container;
        private List<IWeaponLogic> weaponLogics = new List<IWeaponLogic>();
        private List<IEnableable> enableables = new List<IEnableable>();
        public ContainerWatcher(DiContainer container)
        {
            this.container = container;
            
        }
        private bool _enabled = false;
        public bool Enabled { get => _enabled; set
                {
                    _enabled = value;
                    DoWithResolved(weaponLogics, x => x.Enabled = _enabled);
                    DoWithResolved(enableables, x => x.Enabled = _enabled);
                }
        }

        public void DoWithResolved<T>(List<T> prev, Action<T> action)
        {
            if (container == null)
            {
                prev.Clear();
            }
            else if (prev.Count == 0)
            {
                prev.AddRange(container.ResolveAll<T>());
            }
            foreach (T q in prev) action(q);
        }

        public void Tick()
        {
            DoWithResolved(weaponLogics, x => x.Tick());
        }

        public void FixedTick()
        {
            DoWithResolved(weaponLogics, x => x.FixedTick());
        }

        public void Dispose()
        {
            DoWithResolved(weaponLogics, x => x.Dispose());
            container = null;

        }

        public void Initialize()
        {
            DoWithResolved(weaponLogics, x => x.Initialize());
        }
    }
}