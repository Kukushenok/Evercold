using Newtonsoft.Json.Linq;
using UnityEngine;

namespace SaveSystem
{
    /// <summary>
    /// Ѕазовый менеджер сохранений, представл€ющий композицию менеджеров сохранений 
    /// </summary>
    [CreateAssetMenu(fileName = "Composite Data Saver",menuName ="Game/SaveSystem/Composite")]
    public class BaseCompositeDataSaver : UniformCompositeDataSaver<BaseDataSaver>
    {
        public override string GetIdentifier() => name;
    }
}
