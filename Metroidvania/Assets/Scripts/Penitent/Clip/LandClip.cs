using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LandSFX
{
    Normal,
    Running
}

public class LandClip : MonoBehaviour
{
    [SerializeField] private AudioClip[] MerbleSFX;
    private AudioSource _audio;
    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    public void SetSFX(LandSFX land)
    {
        switch (land)
        {
            case LandSFX.Normal:
                {
                    _audio.clip = MerbleSFX[1];
                }
                break;
            case LandSFX.Running:
                {
                    _audio.clip = MerbleSFX[0];
                }
                break;
        }
    }

}
