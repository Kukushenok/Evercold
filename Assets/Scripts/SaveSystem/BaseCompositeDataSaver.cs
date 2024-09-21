using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace SaveSystem
{
    public abstract class UniformCompositeDataSaver<T>: BaseDataSaver, IEnumerable where T: BaseDataSaver
    {
        [SerializeField] protected T[] dataSavers;
        private Dictionary<string, T> cachedDataSavers;
        public override void Load(JObject values)
        {
            foreach (BaseDataSaver ds in dataSavers)
            {
                JObject obj = values.SafelyGet<JObject>(ds.GetIdentifier());
                ds.Load(obj);
            }
        }
        public override BaseDataSaver GetChild(string identifier)
        {
            if (cachedDataSavers == null) CacheDictionary();
            if (cachedDataSavers.TryGetValue(identifier, out T ds)) return ds;
            return null;
        }
        public override JObject Save()
        {
            JObject jobject = new JObject();
            foreach (BaseDataSaver ds in dataSavers)
            {
                jobject.Add(ds.GetIdentifier(), ds.Save());
            }
            return jobject;
        }

        public void CacheDictionary()
        {
            cachedDataSavers = new Dictionary<string, T>();
            foreach (T ds in dataSavers) if (ds != null) cachedDataSavers.Add(ds.GetIdentifier(), ds);
        }
        public IEnumerator GetEnumerator()
        {
            return dataSavers.GetEnumerator();
        }
    }
    public abstract class BaseCompositeDataSaver : UniformCompositeDataSaver<BaseDataSaver>
    {
    }
}
