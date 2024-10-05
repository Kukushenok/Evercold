using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem
{
    /// <summary>
    /// Абстрактный универсальный менеджер данных сохранения, который представляет собой композицию
    /// </summary>
    public abstract class UniformCompositeDataSaver<T>: BaseDataSaver, IEnumerable where T: BaseDataSaver
    {
        [SerializeField] protected T[] _dataSavers;
        private Dictionary<string, T> _cachedDataSavers;
        public override void Load(IPropertyGetter values)
        {
            foreach (BaseDataSaver ds in _dataSavers)
            {
                IPropertyGetter obj = values.TryGetProperty(ds.GetIdentifier());
                ds.Load(obj);
            }
        }
        public override void Save(IPropertySetter values)
        {
            foreach (BaseDataSaver ds in _dataSavers)
            {
                ds.Save(values.CreateChild(ds.GetIdentifier()));
            }
        }

        public void CacheDictionary()
        {
            _cachedDataSavers = new Dictionary<string, T>();
            foreach (T ds in _dataSavers) if (ds != null) _cachedDataSavers.Add(ds.GetIdentifier(), ds);
        }
        public IEnumerator GetEnumerator()
        {
            return _dataSavers.GetEnumerator();
        }
    }
}
