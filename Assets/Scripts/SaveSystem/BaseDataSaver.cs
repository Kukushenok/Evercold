using Newtonsoft.Json.Linq;
using UnityEngine;

namespace SaveSystem
{
    public abstract class BaseDataSaver : MonoBehaviour
    {
        public abstract string GetIdentifier();
        public virtual BaseDataSaver GetChild(string identifier) => null;
        public abstract void Load(JObject values);

        public abstract JObject Save();
    }
}
