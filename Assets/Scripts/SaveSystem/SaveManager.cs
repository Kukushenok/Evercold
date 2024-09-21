using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace SaveSystem
{
    public class SaveManager : BaseCompositeDataSaver
    {
        private static SaveManager instance;
        public const string GAME_SAVE_FILE_NAME = "saved_game.save";
        [Tooltip("Нужно ли созранятся при выходе из игры?")]
        public bool saveOnQuit = false;
        private SaveLoadStrategy saveLoadStrategy;

        private void Awake()
        {
            // I guess I should use a factroy buuut the butt.
#if UNITY_WEBGL
            saveLoadStrategy = new PlayerPrefsSaveLoadStrategy(GAME_SAVE_FILE_NAME);
#else
            saveLoadStrategy = new PersistentPathSaveLoadStrategy(GAME_SAVE_FILE_NAME);
#endif
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
                LoadData();
            }
            else
            {
                Destroy(gameObject);
            }
        }
        private void SaveData()
        {
            JObject saveData = Save();
            saveLoadStrategy.Save(saveData);
        }
        private void LoadData()
        {
            JObject saveData = saveLoadStrategy.Load();
            Load(saveData);
        }
        private void OnApplicationQuit()
        {
            if (saveOnQuit)
            {
                SaveData();
            }
        }
        public static BaseDataSaver GetDataSaver(string resolutionPath)
        {
            BaseDataSaver something = instance;
            string[] parts = resolutionPath.Split('/');
            for (int i = 0; i < parts.Length && something != null; i++)
            {
                something = something.GetChild(parts[i]);
            }
            return something;
        
        }
        public override string GetIdentifier()
        {
            throw new NotImplementedException();
        }
    }
    public static class SaveManagerDelusion
    {
        public static T SafelyGet<T>(this JObject obj, string key, T def = default)
        {
            T result = def;
            if (obj == null) return result;

            if (obj.TryGetValue(key, out JToken token))
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
}