
using UnityEngine;

namespace Features.SaveSystem
{
    /// <summary>
    /// Сохраняет в и читает из PlayerPrefs
    /// </summary>
    internal class PlayerPrefsSaveLoadStrategy: SaveLoadStrategy
    {
        private string _prefName;

        public PlayerPrefsSaveLoadStrategy(string playerPrefName)
        {
            _prefName = playerPrefName;
        }
        public override void Load(object obj)
        {
            if (obj is ILoadableAndSerializableAs<string> stringable)
            {
                try
                {
                    stringable.LoadFrom(PlayerPrefs.GetString(_prefName, string.Empty));
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
                    PlayerPrefs.SetString(_prefName, stringable.SerializeAs());
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