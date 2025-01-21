using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
namespace Features.VFX
{
    [Serializable, VolumeComponentMenuForRenderPipeline("Game VFX/Meat Post Process",typeof(UniversalRenderPipeline))]
    public class MeatVolumeEffect : VolumeComponent, IPostProcessComponent
    {
        [Header("Simple :3")]
        public FloatParameter myParameter = new FloatParameter(0.5f, true); 
        public bool IsActive()
        {
            return true;
        }

        public bool IsTileCompatible()
        {
            return false;
        }
    }

}