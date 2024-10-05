using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SaveSystem;

namespace Game.SaveSystem
{
    /// <summary>
    /// Пример менеджера сохранения. Можно реализовать в нём интерфейс (по типу public int GetCount()),
    /// и какой-то скрипт будет обращаться к этому менеджеру, имея ссылку на ScriptableObject!
    /// </summary>
    [CreateAssetMenu(fileName = "Example", menuName = "Game/SaveSystem/Example")]
    public class BasicSaveDataManager : BaseDataSaver
    {
        [SerializeField] private int _count;
        [SerializeField] private string _data = "Privet!";
        [SerializeField] private string _defaultData = "Hello";
        public override string GetIdentifier() => name;
        public override void Load(IPropertyGetter values)
        {
            _count = values.TryGetProperty(nameof(_count), 0);
            _data = values.TryGetProperty(nameof(_data), _defaultData); 
        }

        public override void Save(IPropertySetter values)
        {
            values.SetProperty(nameof(_count), _count);
            values.SetProperty(nameof(_data), _data);
        }
    }
}