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

    public bool IsAttack { get; set; }

    public delegate void DAttack();
    public event DAttack Attack;
    private void Awake()
    {
        _movement = GetComponent<PenitentMovement>();
        _input = GetComponent<PenitentInput>();
        _audio = transform.GetChild(1).GetComponent<PenitentAudio>();

    }
    void Start()
    {

    }


    void Update()
    {
        PlayerAttack();
    }

    void PlayerAttack()
    {
        if (!_movement.IsGrounded)
            IsAttack = false;
        if (!IsAttack && _input.AttackKey && !_movement.IsDash)
        {
            Attack();
            StartCoroutine(GroundAttack());
        }
    }

    IEnumerator GroundAttack()
    {
        IsAttack = true;
        yield return new WaitForSeconds(AttackTimer);
        IsAttack = false;
    }
}
