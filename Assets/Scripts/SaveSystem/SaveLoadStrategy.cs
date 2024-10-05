using Newtonsoft.Json.Linq;

namespace SaveSystem
{
    /// <summary>
    /// Интерфейс, показывающий возможность сохранения через тип T
    /// Используется для SaveLoadStrategy.
    /// </summary>
    public interface ILoadableAndSerializableAs<T>
    {
        public T SerializeAs();
        public void LoadFrom(T obj);
    }
    /// <summary>
    /// Стратегия загрузки-сохранения объекта.
    /// </summary>
    public abstract class SaveLoadStrategy
    {
        /// <summary>
        /// Загрузить объект. Загрузка МОДИФИЦИРУЕТ obj.
        /// Объект должен иметь интерфейс ILoadableAndSerializableAs.
        /// </summary>
        /// <param name="obj">Объект для загрузки</param>
        public abstract void Load(object obj);
        /// <summary>
        /// Сохранить объект.
        /// Объект должен иметь интерфейс ILoadableAndSerializableAs.
        /// </summary>
        /// <param name="obj">Объект для сохранения</param>
        public abstract void Save(object obj);
    }
}