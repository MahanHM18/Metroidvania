using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenitentAudio : MonoBehaviour
{
    [Header("///////////// Simple SFX")]
    [SerializeField] private AudioClip JumpSFX;

    private AudioSource _runAudio;
    private AudioSource _landAudio;
    private AudioSource _jumpAudio;

    private PenitentMovement _movement;

    private void Awake()
    {
        _runAudio = transform.GetChild(0).GetComponent<AudioSource>();
        _landAudio = transform.GetChild(1).GetComponent<AudioSource>();
        _jumpAudio = transform.GetChild(2).GetComponent<AudioSource>();
        _movement = transform.parent.GetComponent<PenitentMovement>();
    }
    private void Start()
    {
        _jumpAudio.clip = JumpSFX;
        _movement.Jump += PlayJumpSFX;
    }
    public void PlayFootStep()
    {
        if (!transform.parent.GetComponent<PenitentMovement>().IsGrounded)
            return;
        transform.GetChild(0).GetComponent<RunClip>().SetRandomStoneSFX();
        _runAudio.Play();
    }

    public void PlayLand(LandSFX land)
    {
        transform.GetChild(1).GetComponent<LandClip>().SetSFX(land);
        _landAudio.Play();
    }

    private void PlayJumpSFX()
    {
        _jumpAudio.Play();
    }

}
