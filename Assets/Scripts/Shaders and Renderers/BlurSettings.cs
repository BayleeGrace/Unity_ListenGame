using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[System.Serializable, VolumeComponentMenu("Blur")]
public class BlurSettings : VolumeComponent, IPostProcessComponent
{
    public ClampedFloatParameter strength = new ClampedFloatParameter(0.0f, 0.0f, 100.0f);

    public bool IsActive()
    {
        return (strength.value > 0.0f) && active;
    }

    public bool IsTileCompatible()
    {
        //throw new System.NotImplementedException();
        return false;
    }
}
