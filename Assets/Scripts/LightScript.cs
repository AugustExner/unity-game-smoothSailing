using UnityEngine.Rendering;
using UnityEngine;
using System.Collections;

public class UpdateSky : MonoBehaviour
{
    ReflectionProbe baker;

    void Start()
    {
        baker = gameObject.AddComponent<ReflectionProbe>();
        baker.cullingMask = 0;
        baker.refreshMode = ReflectionProbeRefreshMode.ViaScripting;
        baker.mode = ReflectionProbeMode.Realtime;
        baker.timeSlicingMode = ReflectionProbeTimeSlicingMode.NoTimeSlicing;

        RenderSettings.defaultReflectionMode = DefaultReflectionMode.Custom;
        StartCoroutine(UpdateEnvironment());
    }

    IEnumerator UpdateEnvironment()
    {
        DynamicGI.UpdateEnvironment();
        baker.RenderProbe();
        yield return new WaitForEndOfFrame();
        RenderSettings.customReflectionTexture = baker.texture;
    }
}