using UnityEngine;

namespace Features.SaveSystem
{
    /// <summary>
    /// Базовый менеджер сохранений, представляющий композицию менеджеров сохранений 
    /// </summary>
    [CreateAssetMenu(fileName = "Composite Data Saver",menuName ="Game/SaveSystem/Composite")]
    public class BaseCompositeDataSaver : UniformCompositeDataSaver<BaseDataSaver>
    {
        public override string GetIdentifier() => name;
    }
}
