using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PenitentMovement : AbstractPenitentMovement
{
    [SerializeField] private float MoveSpeed, SoftTime, JumpForce, DashTime, DashSpeed;
    [HideInInspector] public float Speed;
    private PenitentInput _input;

    private bool canPlayLandRunSFX;
    private PenitentAudio _audio;
    private PenitentAnimation _anim;
    private PenitentAttack _attack;

    public event JumpDelegate Jump;
    private float sfTimer;
    public bool IsCrouch { get; private set; }
    public bool IsDash { get; set; }
    private bool canDash;

    [SerializeField] private Transform JumpDustPoint;
    [SerializeField] private GameObject JumpDust;
    protected override void OnAwake()
    {
        base.OnAwake();
        _input = GetComponent<PenitentInput>();
        _audio = transform.GetChild(1).GetComponent<PenitentAudio>();
        _anim = GetComponent<PenitentAnimation>();
        _attack = GetComponent<PenitentAttack>();
    }
    protected override void OnStart()
    {
        Jump += () => { AddForce(new Vector2(0, JumpForce)); };
        base.OnStart();
        Speed = MoveSpeed;
        canDash = true;
        Jump += JumpDustEffect;
    }

    protected override void OnUpdate()
    {
        PlayerDash();
        PlayerCrouch();
        PlayerMovement();
        PlayerJump();
        LandRunSFX();

    }

    private void PlayerMovement()
    {
        if (_attack.IsAttack && IsGrounded)
        {
            _rb.velocity = new Vector2(0,_rb.velocity.y);
            return;
        }
        if (IsDash)
            return;
        if (_input.LeftKey && !_input.RighttKey)
        {
            Flip(Dir.Left);
        }
        else if (_input.RighttKey && !_input.LeftKey)
        {
            Flip(Dir.Right);
        }


        if (IsCrouch )
        {
            Stop(SoftTime);
            return;
        }

        if (_input.LeftKey && _input.RighttKey)
        {
            Stop(SoftTime);
        }
        else if (_input.RighttKey )
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
        if (_input.JumpKey && !_attack.IsAttack)
        {
            if (IsGrounded)
            {
                Jump();
            }
        }
    }

    private void LandRunSFX()
    {
        Jump += () => { sfTimer = 0.1f; };
        if (!IsGrounded)
        {
            canPlayLandRunSFX = true;
            sfTimer = 0.1f;
        }

        if (IsGrounded && canPlayLandRunSFX && (_input.LeftKey || _input.RighttKey) && sfTimer > 0 && _rb.velocity.y < 0)
        {

            canPlayLandRunSFX = false;
            _audio.PlayLand(LandSFX.Running);
        }
        if (IsGrounded && sfTimer >= 0)
            sfTimer -= Time.deltaTime;

    }
    private void PlayerCrouch()
    {
        if (IsDash || _attack.IsAttack)
            return;
        Jump += () => { IsCrouch = false; };
        if (!IsGrounded)
        {
            IsCrouch = false;
            return;
        }

        if (_input.CrouchKey)
            IsCrouch = true;
        else
            IsCrouch = false;
    }

    private void PlayerDash()
    {
        if (IsDash)
        {
            _rb.velocity = new Vector2(DashSpeed * transform.localScale.x, _rb.velocity.y);
            _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        Jump += () => { IsDash = false; canDash = true; };
        if (!IsGrounded)
        {
            IsDash = false;
            canDash = true;
            return;
        }
        if (_input.DashKey && canDash && IsGrounded)
        {
            StartCoroutine(dash());
        }
    }

    IEnumerator dash()
    {
        IsDash = true;
        canDash = false;
        yield return new WaitForSeconds(DashTime);
        canDash = true;
    }

    private void JumpDustEffect()
    { 
        GameObject dust = Instantiate(JumpDust,JumpDustPoint.position,Quaternion.identity);
        Destroy(dust, 0.2f);
    }

}
