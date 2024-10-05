using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SaveSystem
{
    /// <summary>
    /// Объект данных, которые храняться как JSON.
    /// </summary>
    public class JObjectBaseDataObject: BaseDataObject, ILoadableAndSerializableAs<string>
    {
        protected JObject _object;
        public JObjectBaseDataObject(JObject obj)
        {
            _object = obj;
        }

        public override IPropertySetter CreateChild(string name)
        {
            JObject jobj = new JObject();
            JObjectBaseDataObject obj = new JObjectBaseDataObject(jobj);
            if (_object == null) _object = new JObject();
            _object[name] = jobj;
            return obj;
        }

        public void LoadFrom(string obj)
        {
            try
            {
                _object = JObject.Parse(obj);
            }
            catch
            {
                _object = null;
            }
        }

        public string SerializeAs()
        {
            if (_object == null) return "";
            return JsonConvert.SerializeObject(_object);
        }

        public override void SetProperty<T>(string name, T value)
        {
            if (_object == null) _object = new JObject();
            _object[name] = new JValue(value);
        }

        public override IPropertyGetter TryGetProperty(string name)
        {
            JObject child = SafelyGet<JObject>(name);
            if (child == null) return new JObjectBaseDataObject(null);
            return new JObjectBaseDataObject(child);
        }

        public override T TryGetProperty<T>(string name, T defaultValue = default)
        {
            return SafelyGet(name, defaultValue);
        }
        private T SafelyGet<T>(string key, T def = default)
        {
            T result = def;
            if (_object == null) return result;
            if (_object.TryGetValue(key, out JToken token))
            {
                try
                {
                    result = token.ToObject<T>();
                }
                finally { }
            }
            return result;
        }
    }
    public class JObjectDataObjectCreator: BaseDataObjectCreator
    {
        public override BaseDataObject Create() => new JObjectBaseDataObject(null);
    }
}
