using UnityEngine;

namespace Features.SaveSystem
{
    /// <summary>
    /// Абстрактный менеджер данных сохранения
    /// </summary>
    public abstract class BaseDataSaver : ScriptableObject
    {
        public abstract string GetIdentifier();
        public abstract void Load(IPropertyGetter values);

        public abstract void Save(IPropertySetter values);
    }
}
