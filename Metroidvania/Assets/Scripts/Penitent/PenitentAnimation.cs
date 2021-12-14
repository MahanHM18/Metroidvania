using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenitentAnimation : MonoBehaviour
{
    private Animator _anim;
    private PenitentInput _input;
    private PenitentMovement _movement;
    private PenitentAttack _attack;

    [HideInInspector] public bool canLand;

    private const string ISRUNNIG = "IsRunning";
    private const string ISGROUNDED = "IsGrounded";
    private const string JUMP = "Jump";
    private const string ISCROUCH = "IsCrouch";
    private const string ISDASH = "IsDash";
    private const string CANLAND = "CanLand";
    private const string ATTACK = "Attack";
    private const string UP = "Up";
    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _input = GetComponent<PenitentInput>();
        _movement = GetComponent<PenitentMovement>();
        _attack = GetComponent<PenitentAttack>();


    }

    void Start()
    {
        SetDelegate();
    }

    private void FixedUpdate()
    {
        Animation();
    }
    bool PlayerRun()
    {
        if (_input.LeftKey && _input.RighttKey)
            return false;
        else if (_input.LeftKey || _input.RighttKey)
            return true;

        return false;
    }

    void Animation()
    {
        _anim.SetBool(ISRUNNIG, PlayerRun());
        _anim.SetBool(ISGROUNDED, _movement.IsGrounded);
        _anim.SetBool(ISCROUCH, _movement.IsCrouch);
        _anim.SetBool(ISDASH, _movement.IsDash);
        _anim.SetBool(CANLAND, canLand);
        _anim.SetBool(UP, _input.UpKey);

        if (_movement.IsGrounded)
            canLand = false;

    }

    public bool IsPlaying(string name)
    {
        return _anim.GetCurrentAnimatorStateInfo(0).IsName(name);
    }

    private void SetDelegate()
    {
        _movement.Jump += () => { _anim.SetTrigger(JUMP); canLand = false; };

        _attack.Attack += () => { _anim.SetTrigger(ATTACK); };
    }
}
