using System.Collections;
using System.Collections.Generic;
using Feature.Player;
using UnityEngine;
using Zenject;

public class LocomotionUpdateManager : ITickable, IFixedTickable
{
    List<ILocomotionFeature> _features;

    
    ILocomotionData _data;

    [Inject]
    public LocomotionUpdateManager(List<ILocomotionFeature> features, ILocomotionData data){
        _features = features;
        _data = data;
    }

    public void Tick()
    {
        foreach (var _feature in _features)
            {
                _feature.OnUpdate(_data);
            }
    }

    public void FixedTick()
    {
        foreach (var _feature in _features)
            {
                _feature.OnFixedUpdate(_data);
            }
    }
}
