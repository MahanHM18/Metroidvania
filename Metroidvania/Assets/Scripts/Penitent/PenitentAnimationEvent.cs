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

    [SerializeField] private Transform RunDustPoint;
    [SerializeField] private GameObject RunDust;

    [SerializeField] private Transform RunStopDustPoint;
    [SerializeField] private GameObject RunStopDust;

    [SerializeField] private Transform LandDustPoint;
    [SerializeField] private GameObject LandDust;

    [SerializeField] private Transform DashStartDustPoint;
    [SerializeField] private GameObject DashStartDust;

    [SerializeField] private Transform DashEndDustPoint;
    [SerializeField] private GameObject DashEndDust;

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

    private void CreateRunDust()
    {
        GameObject dust = Instantiate(RunDust, RunDustPoint.position, Quaternion.identity) as GameObject;
        dust.transform.localScale = new Vector2(_movement.transform.localScale.x, 1);
        Destroy(dust, 0.2f);
    }

    private void CreateRunStopDust()
    {
        GameObject dust = Instantiate(RunStopDust, RunStopDustPoint.position, Quaternion.identity) as GameObject;
        dust.transform.localScale = new Vector2(_movement.transform.localScale.x, 1);
        Destroy(dust, 0.3f);
    }

    private void CreateLandpDust()
    {
        GameObject dust = Instantiate(LandDust, LandDustPoint.position, Quaternion.identity) as GameObject;
        dust.transform.localScale = new Vector2(_movement.transform.localScale.x, 1);
        Destroy(dust, 0.3f);
    }

    private void CreateDashStartDust()
    {
        GameObject dust = Instantiate(DashStartDust, DashStartDustPoint.position, Quaternion.identity) as GameObject;
        dust.transform.localScale = new Vector2(_movement.transform.localScale.x, 1);
        Destroy(dust, 0.5f);
    }

    private void CreateDashEndDust()
    {
        GameObject dust = Instantiate(DashEndDust, DashEndDustPoint.position, Quaternion.identity) as GameObject;
        dust.transform.localScale = new Vector2(_movement.transform.localScale.x, 1);
        Destroy(dust, 0.5f);
    }

}
