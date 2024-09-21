using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SaveSystem;
using Newtonsoft.Json.Linq;

namespace Game.SaveSystem
{
    public class BasicSaveDataManager : BaseDataSaver
    {
        [SerializeField] private string _identifier;
        [SerializeField] private int _count;

        public override string GetIdentifier()
        {
            return _identifier;
        }

        public override void Load(JObject values)
        {
            _count = values.SafelyGet<int>(nameof(_count), _count);
        }
        public override JObject Save()
        {
            JObject obj = new JObject();
            obj[nameof(_count)] = _count;
            return obj;
        }
        
    }
}