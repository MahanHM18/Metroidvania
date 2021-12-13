using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenitentAnimation : MonoBehaviour
{
    private Animator _anim;
    private PenitentInput _input;
    private PenitentMovement _movement;

    private const string ISRUNNIG = "IsRunning";
    private const string ISGROUNDED = "IsGrounded";
    private const string JUMP = "Jump";
    private const string ISCROUCH = "IsCrouch";
    private const string ISDASH = "IsDash";
    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _input = GetComponent<PenitentInput>();
        _movement = GetComponent<PenitentMovement>();

    }

    void Start()
    {
        _movement.Jump += () => { _anim.SetTrigger(JUMP); };
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
    }

    public bool IsPlaying(string name)
    {
        return _anim.GetCurrentAnimatorStateInfo(0).IsName(name);
    }
}
