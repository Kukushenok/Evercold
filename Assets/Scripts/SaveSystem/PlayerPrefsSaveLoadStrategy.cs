using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace SaveSystem
{
    /// <summary>
    /// Сохраняет в и читает из PlayerPrefs
    /// </summary>
    public class PlayerPrefsSaveLoadStrategy: SaveLoadStrategy
    {
        private string prefName;

        public PlayerPrefsSaveLoadStrategy(string playerPrefName)
        {
            prefName = playerPrefName;
        }
        public override void Load(object obj)
        {
            if (obj is ILoadableAndSerializableAs<string> stringable)
            {
                try
                {
                    stringable.LoadFrom(PlayerPrefs.GetString(prefName, string.Empty));
                }
                catch(PlayerPrefsException)
                {
                    LogPPrefsError();
                }
            }
            else
            {
                Debug.LogError("No type supportance :(");
            }
        }
        public override void Save(object obj)
        {
            if (obj is ILoadableAndSerializableAs<string> stringable)
            {
                try
                {
                    PlayerPrefs.SetString(prefName, stringable.SerializeAs());
                }
                catch (PlayerPrefsException)
                {
                    LogPPrefsError();
                }
            }
            else
            {
                Debug.LogError("No type supportance :(");
            }

        }
        void LogPPrefsError()
        {
            Debug.LogError("Something went wrong");
        }
    }
}