using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenitentAnimationEvent : MonoBehaviour
{
    private PenitentAudio _audio;

    private void Awake()
    {
        _audio = transform.GetChild(1).GetComponent<PenitentAudio>();
    }

    private void PlayFootStep()
    { 
        _audio.PlayFootStep();
    }
    private void PlayLand()
    {
        _audio.PlayLand(LandSFX.Normal);
    }
}
