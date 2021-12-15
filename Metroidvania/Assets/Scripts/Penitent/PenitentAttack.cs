using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenitentAttack : MonoBehaviour
{
    [SerializeField] private float AttackTimer;
    private PenitentMovement _movement;
    private PenitentInput _input;
    private PenitentAudio _audio;
    public int AirAttack;
    private PenitentAnimation _animation;

    public bool IsAttack { get; set; }

    public delegate void DAttack();
    public event DAttack Attack;

    public bool HitEnemy { get; set; }
    private void Awake()
    {
        _movement = GetComponent<PenitentMovement>();
        _input = GetComponent<PenitentInput>();
        _audio = transform.GetChild(1).GetComponent<PenitentAudio>();
        _animation = GetComponent<PenitentAnimation>();

    }
    void Start()
    {

    }


    void Update()
    {
        PlayerAttack();
        if (_animation.IsPlaying("clip_player_attack") || _animation.IsPlaying("clip_player_attack_2") || _animation.IsPlaying("clip_player_attack_3"))
            IsAttack = true;
        else
            IsAttack = false;
    }

    void PlayerAttack()
    {
        if (!_movement.IsGrounded)
            IsAttack = false;
        if (!IsAttack && _input.AttackKey && !_movement.IsDash && !HitEnemy)
        {
            Attack();

            StartCoroutine(GroundAttack(AttackTimer));
        }
        else if (_input.AttackKey && !_movement.IsDash && HitEnemy)
        {
            Attack();

            StartCoroutine(GroundAttack(AttackTimer * 2));
        }
    }

    IEnumerator GroundAttack(float t)
    {
        yield return new WaitForSeconds(t);
    }


}
