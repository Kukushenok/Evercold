using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace SaveSystem
{
    /// <summary>
    /// Сохраняет в и читает из файла в Appications.persistentDataPath
    /// Максимальный размер файла для чтения - 5 КБ.
    /// Это сделано по простой причине - чтобы игрок не положил туда ТБайт каких то данных
    /// и функция ReadToEnd не читала это полностью.
    /// </summary>
    public class PersistentPathSaveLoadStrategy: SaveLoadStrategy
    {
        private const long SAVE_MAX_SIZE = 5 * 1024; // 5 KB
        private string _saveDir;

        public PersistentPathSaveLoadStrategy(string saveFileName)
        {
            _saveDir = Application.persistentDataPath + "/" + saveFileName;
        }
        public override void Save(object obj)
        {
            if (obj is ILoadableAndSerializableAs<string> stringable)
            {
                using (StreamWriter file = File.CreateText(_saveDir))
                {
                    file.WriteLine(stringable.SerializeAs());
                }
            }
            else
            {
                Debug.LogError("No type supportance :(");
            }
        }
        public override void Load(object obj)
        {
            if (obj is ILoadableAndSerializableAs<string> stringable)
            {
                ReadFromFile(_saveDir, stringable);
            }
            else
            {
                Debug.LogError("No type supportance :(");
            }
        }
        void ReadFromFile(string fileDir, ILoadableAndSerializableAs<string> obj)
        {
            if (File.Exists(fileDir))
            {
                try
                {
                    FileInfo info = new FileInfo(fileDir);
                    if (info.Length > SAVE_MAX_SIZE)
                    {
                        Debug.LogWarning("Save data has large size. Ignoring. If you disagree, change SAVE_MAX_SIZE");
                    }
                    else
                    {
                        string fileContent;
                        using (StreamReader file = File.OpenText(fileDir))
                        {
                            fileContent = file.ReadToEnd();
                        }
                        obj.LoadFrom(fileContent);
                    }
                }
                catch
                {
                    Debug.LogError("An exception occured while reading the file.");
                }
            }
        }
    }
}