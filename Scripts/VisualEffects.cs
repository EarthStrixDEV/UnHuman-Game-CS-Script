using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualEffects : MonoBehaviour
{
    private GlitchCameraShader GlitchCameraShader;
    //
    float NoiseIntensity = 0.01f;
    float WaveIntensity = 0.001f;
    float GlitchRate = 1f;
    // Start is called before the first frame update
    void Start()
    {
        GlitchCameraShader = GetComponent<GlitchCameraShader>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.isTriggerPlayer) {
            GlitchRate = 0.5f;
            NoiseIntensity = 1f;
            WaveIntensity = 0.01f;
        }
        else {
            GlitchRate = 1f;
            NoiseIntensity = 0.01f;
            WaveIntensity = 0.001f;
        }

        GlitchCameraShader.GlitchRate = GlitchRate;
        GlitchCameraShader.WhiteNoiseIntensity = NoiseIntensity;
        GlitchCameraShader.WaveNoiseIntensity = WaveIntensity;
    }
}
