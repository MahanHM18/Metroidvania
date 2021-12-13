using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PenitentMovement : AbstractPenitentMovement
{
    [SerializeField] private float MoveSpeed, SoftTime, JumpForce;
    [HideInInspector] public float Speed;
    private PenitentInput _input;

    private bool canPlayLandRunSFX;
    private PenitentAudio _audio;
    private PenitentAnimation _anim;

    public event JumpDelegate Jump;
    public bool IsCrouch { get; private set; }

    protected override void OnAwake()
    {
        base.OnAwake();
        _input = GetComponent<PenitentInput>();
        _audio = transform.GetChild(1).GetComponent<PenitentAudio>();
        _anim = GetComponent<PenitentAnimation>();
    }
    protected override void OnStart()
    {
        Jump += () => { AddForce(new Vector2(0, JumpForce)); };
        base.OnStart();
        Speed = MoveSpeed;
    }

    protected override void OnUpdate()
    {
        PlayerMovement();
        PlayerJump();
        LandRunSFX();
        PlayerCrouch();
    }

    private void PlayerMovement()
    {
        if (_input.LeftKey)
        {
            Flip(Dir.Left);
        }
        else if (_input.RighttKey)
        {
            Flip(Dir.Right);
        }


        if (IsCrouch)
        {
            Stop(SoftTime);
            return;
        }

        if (_input.LeftKey && _input.RighttKey)
        {
            Stop(SoftTime);
        }
        else if (_input.RighttKey)
        {

            Run(Dir.Right, Speed);
        }
        else if (_input.LeftKey)
        {

            Run(Dir.Left, Speed);
        }
        else if (!_input.LeftKey && !_input.RighttKey)
        {

            Stop(SoftTime);
        }
    }
    private void PlayerJump()
    {
        if (_input.Jump)
        {
            if (IsGrounded)
            {
                Jump();
            }
        }
    }

    private void LandRunSFX()
    {
        if (!IsGrounded)
            canPlayLandRunSFX = true;

        if (IsGrounded && canPlayLandRunSFX && !_anim.IsPlaying("clip_player_start_run"))
        {
            canPlayLandRunSFX = false;
            _audio.PlayLand(LandSFX.Running);
        }
    }
    private void PlayerCrouch()
    {
        Jump += () => { IsCrouch = false; };
        if (!IsGrounded)
            return;
        if (_input.CrouchKey)
            IsCrouch = true;
        else
            IsCrouch = false;
    }
}
