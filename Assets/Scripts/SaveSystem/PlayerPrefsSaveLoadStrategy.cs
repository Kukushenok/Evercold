using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace SaveSystem
{
    public class PlayerPrefsSaveLoadStrategy: SaveLoadStrategy
    {
        private string prefName;

        public PlayerPrefsSaveLoadStrategy(string playerPrefName)
        {
            prefName = playerPrefName;
        }
        public override JObject Load()
        {
            string res = PlayerPrefs.GetString(prefName, string.Empty);
            JObject saveData = new JObject();
            try
            {
                saveData = JObject.Parse(res);
            }
            catch
            {
                Debug.LogError("An exception occured while reading the prefs.");
            }
            return saveData;
        }
        public override void Save(JObject obj)
        {
            try
            {
                PlayerPrefs.SetString(prefName, obj.ToString(Formatting.None));
            }
            catch(PlayerPrefsException)
            {
                Debug.LogError("Uh oh! Player prefs has exceeded its size... Sorry!");
            }
        }
    }
}