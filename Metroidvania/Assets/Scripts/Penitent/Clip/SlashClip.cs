using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashClip : MonoBehaviour
{
    [SerializeField] private AudioClip[] SlashSFX;
    private AudioSource _auido;
    private void Awake()
    {
        _auido = GetComponent<AudioSource>();
    }
    public void PlayRandomSFX()
    {
        _auido.clip = SlashSFX[Random.Range(0, SlashSFX.Length)];
        _auido.Play();
    }
}
