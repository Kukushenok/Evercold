using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace SaveSystem
{
    public class PersistentPathSaveLoadStrategy: SaveLoadStrategy
    {
        private string saveDir;

        public PersistentPathSaveLoadStrategy(string saveFileName)
        {
            saveDir = Application.persistentDataPath + "/" + saveFileName;
        }
        public override void Save(JObject obj)
        {
            using (StreamWriter file = File.CreateText(saveDir))
            using (JsonTextWriter writer = new JsonTextWriter(file))
            {
                obj.WriteTo(writer);
            }
        }
        public override JObject Load()
        {
            JObject saveData = new JObject();
            if (File.Exists(saveDir))
            {
                try
                {
                    using (StreamReader file = File.OpenText(saveDir))
                    using (JsonTextReader reader = new JsonTextReader(file))
                    {
                        saveData = (JObject)JToken.ReadFrom(reader);
                    }
                }
                catch
                {
                    Debug.LogError("An exception occured while reading the file.");
                }
            }
            return saveData;
        }
    }
}