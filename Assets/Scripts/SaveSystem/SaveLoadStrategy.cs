using Newtonsoft.Json.Linq;

namespace SaveSystem
{
    public abstract class SaveLoadStrategy
    {
        public abstract JObject Load();
        public abstract void Save(JObject obj);
    }
}