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
    }
}
