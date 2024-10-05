using Newtonsoft.Json.Linq;
using UnityEngine;

namespace SaveSystem
{
    /// <summary>
    /// ������� �������� ����������, �������������� ���������� ���������� ���������� 
    /// </summary>
    [CreateAssetMenu(fileName = "Composite Data Saver",menuName ="Game/SaveSystem/Composite")]
    public class BaseCompositeDataSaver : UniformCompositeDataSaver<BaseDataSaver>
    {
        public override string GetIdentifier() => name;
    }
}
