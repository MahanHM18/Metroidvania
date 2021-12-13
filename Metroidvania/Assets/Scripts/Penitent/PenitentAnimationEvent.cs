using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenitentAnimationEvent : MonoBehaviour
{
    private PenitentAudio _audio;
    private PenitentMovement _movement;
    private void Awake()
    {
        _audio = transform.GetChild(1).GetComponent<PenitentAudio>();
        _movement = GetComponent<PenitentMovement>();
    }

    private void PlayFootStep()
    {
        _audio.PlayFootStep();
    }
    private void PlayLand()
    {
        _audio.PlayLand(LandSFX.Normal);
    }

    private void EndDash()
    {
        _movement.IsDash = false;
    }
    private void Dash()
    {
        _audio.PlayDash();
    }
}
