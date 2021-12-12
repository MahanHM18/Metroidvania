using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunClip : MonoBehaviour
{
    [SerializeField] private AudioClip[] StoneSFX;
    private AudioSource _audio;
    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    public void SetRandomStoneSFX()
    {
        _audio.clip = StoneSFX[Random.Range(0, StoneSFX.Length)];
    }

}
