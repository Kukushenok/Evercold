using System;
using UnityEngine;
namespace Feature.Player.WeaponManager
{
    [Serializable]
    public struct WeaponInfo
    {
        [field: SerializeField] public string Identifier { get; set; }
        [field: SerializeField] public GameObject WeaponViewPrefab { get; set; }
        public override int GetHashCode()
        {
            return Identifier.GetHashCode();
        }
    }
}