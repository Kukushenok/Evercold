namespace Features.SaveSystem
{
    /// <summary>
    /// Интерфейс чтения данных. Используется BaseDataSaver для загрузки значений
    /// </summary>
    public interface IPropertyGetter
    {
        public T TryGetProperty<T>(string name, T defaultValue = default);
        public IPropertyGetter TryGetProperty(string name);
    }
    /// <summary>
    /// Интерфейс задания данных. Используется BaseDataSaver для сохранения значений
    /// </summary>
    public interface IPropertySetter
    {
        public void SetProperty<T>(string name, T value);
        public IPropertySetter CreateChild(string name);
    }
    /// <summary>
    /// Абстрактный создатель данных. Создаёт пустые данные.
    /// </summary>
    internal abstract class BaseDataObjectCreator
    {
        public abstract BaseDataObject Create();
    }
    /// <summary>
    /// Абстрактный объект данных. Можно и читать, и задавать значения.
    /// </summary>
    public abstract class BaseDataObject : IPropertyGetter, IPropertySetter
    {
        public abstract IPropertySetter CreateChild(string name);
        public abstract void SetProperty<T>(string name, T value);
        public abstract T TryGetProperty<T>(string name, T defaultValue = default);
        public abstract IPropertyGetter TryGetProperty(string name);
    }
}
