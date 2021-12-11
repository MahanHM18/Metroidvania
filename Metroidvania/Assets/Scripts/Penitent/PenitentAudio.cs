using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenitentAudio : MonoBehaviour
{
    [SerializeField] private AudioClip[] RunSFX;

    private AudioSource _runAudio;

    private void Awake()
    {
        _runAudio = transform.GetChild(0).GetComponent<AudioSource>();
    }
    public void PlayFootStep()
    {
        if (!transform.parent.GetComponent<PenitentMovement>().IsGrounded)
            return;
        _runAudio.clip = RunSFX[Random.Range(0,RunSFX.Length)];
        _runAudio.Play();
    }
}
