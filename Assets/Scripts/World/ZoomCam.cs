using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ZoomCam : MonoBehaviour
{
    public Transform CubeTranform;
    bool IsZoomed;

    public AudioMixer AudMixerMain;
    public AudioMixer AudMixerSub;

    float LPFrequencyMain;
    float LPFrequencySub;

    private void Start()
    {
        LPFrequencyMain = LPFrequencySub = 22000;
    }

    private void LateUpdate()
    {
        PowerUpManager powerUpManager = GameObject.Find("PowerUpSpawn").GetComponent<PowerUpManager>();
        
        if (powerUpManager.IsPowered)
        {
            Vector3 cubePosition = CubeTranform.position;
            cubePosition.z = cubePosition.z - 10; // -10 just for mathing the coordinate of the camera and the cube.
            CameraLerp(Camera.main.orthographicSize, 2.5f,
                Camera.main.transform.position, cubePosition);

            LowPassFilter(600, 0.095f, AudMixerMain, "LowPass", ref LPFrequencyMain);
            LowPassFilter(5000, 0.080f, AudMixerSub, "LowPassSub", ref LPFrequencySub);
        }
        else
        {
            Vector3 defaultPosition = new Vector3(0, 0, -10);
            CameraLerp(Camera.main.orthographicSize, 3.6f,
                Camera.main.transform.position, defaultPosition);

            LowPassFilter(22000, 0.040f, AudMixerMain, "LowPass", ref LPFrequencyMain);
            LowPassFilter(22000, 0.035f, AudMixerSub, "LowPassSub", ref LPFrequencySub);
        }
    }
    
    private void CameraLerp(float fromCameSize, float toCamSize, Vector3 fromPosition, Vector3 toPosition)
    {
        Camera.main.transform.position = Vector3.Lerp(fromPosition, toPosition, 0.08f * Timer.DeltaTimeMod); // Changing the position of the camera smoothy using lerping. 
        Camera.main.orthographicSize = Mathf.Lerp(fromCameSize, toCamSize, 0.08f * Timer.DeltaTimeMod);
    }

    private void LowPassFilter(float endLimit, float rate, AudioMixer mixer, string effect, ref float frequency)
    {
        frequency = Mathf.Lerp(frequency, endLimit, Timer.DeltaTimeMod);

        //if (slider == "SlideDown")
        //    frequency = (frequency > endLimit) ? frequency * rate : endLimit;
        //else if (slider == "SlideUp")
        //    frequency = (frequency < endLimit) ? frequency * rate : endLimit;

        mixer.SetFloat(effect, frequency);
    }
}
