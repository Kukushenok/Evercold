using UnityEngine;
namespace Features.SaveSystem
{
    /// <summary>
    /// Класс, который загружает и сохраняет данные.
    /// В будущем, скорее всего будет одним из singleton-ов.
    /// </summary>
    public class SaveManager : MonoBehaviour
    {
        public const string GAME_SAVE_FILE_NAME = "saved_game.save";
        [Tooltip("Нужно ли сохранятся при выходе из игры?")]
        public bool SaveOnQuit = false;
        [Tooltip("Главный менеджер сохранений")]
        [SerializeField] private BaseDataSaver _dataSaver;
        private SaveLoadStrategy _saveLoadStrategy;

        private BaseDataObjectCreator _dataObjectCreator;


        private void Awake()
        {
            // TODO: вынести это в отдельный класс Settings, который и выдаёт по методам нужные стратегии/креаторы.
#if UNITY_WEBGL
            _saveLoadStrategy = new PlayerPrefsSaveLoadStrategy(GAME_SAVE_FILE_NAME);
#else
            _saveLoadStrategy = new PersistentPathSaveLoadStrategy(GAME_SAVE_FILE_NAME);
#endif
            _dataObjectCreator = new JObjectDataObjectCreator();
            LoadData();
        }
        private void SaveData()
        {
            BaseDataObject obj = _dataObjectCreator.Create();
            _dataSaver.Save(obj);
            _saveLoadStrategy.Save(obj);
        }
        private void LoadData()
        {
            BaseDataObject obj = _dataObjectCreator.Create();
            _saveLoadStrategy.Load(obj);
            _dataSaver.Load(obj);
        }
        private void OnApplicationQuit()
        {
            if (SaveOnQuit)
            {
                SaveData();
            }
        }
    }
}