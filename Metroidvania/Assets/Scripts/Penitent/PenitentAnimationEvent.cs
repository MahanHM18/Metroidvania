using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenitentAnimationEvent : MonoBehaviour
{
    private PenitentAudio _audio;
    private PenitentMovement _movement;
    private PenitentAnimation _anim;
    private PenitentAttack _attack;
    private GameObject _attackCollider;
    private void Awake()
    {
        _audio = transform.GetChild(1).GetComponent<PenitentAudio>();
        _movement = GetComponent<PenitentMovement>();
        _anim = GetComponent<PenitentAnimation>();
        _attack = GetComponent<PenitentAttack>();
        _attackCollider = transform.GetChild(2).gameObject;
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

    private void CanLand()
    {
        _anim.canLand = true;
    }

    private void EndAttack()
    {
        _attack.IsAttack = false;
    }
    private void AttackSfX()
    {
        _attackCollider.SetActive(false);
        _attackCollider.SetActive(true);
        _audio.PlaySlash();
    }
}
