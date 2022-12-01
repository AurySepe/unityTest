using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationManager : MonoBehaviour
{

    public static VibrationManager singleton;
    
    // Start is called before the first frame update
    void Start()
    {
        if (singleton && singleton != this)
        {
            Destroy(this);
        }
        else
        {
            singleton = this;
        }
    }

    public void TriggerVibration(AudioClip vibrationAudio, OVRInput.Controller controller)
    {
        OVRHapticsClip clip = new OVRHapticsClip(vibrationAudio);
        
        if (controller == OVRInput.Controller.LTouch)
        {
            //fai vibrare controller sinistroù
            OVRHaptics.LeftChannel.Preempt(clip);
        }
        else if (controller == OVRInput.Controller.RTouch)
        {
            //fai vibrare controller destro
            OVRHaptics.RightChannel.Preempt(clip);
        }
    }
    
    public void TriggerVibration(int iteration,int frequency,int strenght, OVRInput.Controller controller)
    {
        OVRHapticsClip clip = new OVRHapticsClip(iteration);

        for (int i = 0; i < iteration; i++)
        {
            clip.WriteSample(i%frequency == 0 ? (byte) strenght : (byte) 0);
        }
        
        if (controller == OVRInput.Controller.LTouch)
        {
            //fai vibrare controller sinistroù
            OVRHaptics.LeftChannel.Preempt(clip);
        }
        else if (controller == OVRInput.Controller.RTouch)
        {
            //fai vibrare controller destro
            OVRHaptics.RightChannel.Preempt(clip);
        }
    }
}
