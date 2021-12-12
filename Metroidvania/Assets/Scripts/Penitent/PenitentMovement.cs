using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenitentMovement : MonoBehaviour
{
    [SerializeField] private float MoveSpeed, SoftTime, JumpForce;
    [SerializeField] private LayerMask GroundLayer;
    [HideInInspector] public float Speed;
    private Rigidbody2D _rb;
    private PenitentInput _input;
    private float _stepTimer;
    private BoxCollider2D _boxCollider2D;

    public delegate void JumpDelegate();
    public event JumpDelegate Jump; 

    public bool IsGrounded { get { return Physics2D.BoxCast(_boxCollider2D.bounds.center, _boxCollider2D.size, 0, Vector2.down, 0.1f, GroundLayer); } }
    public bool IsRunning { get; private set; }

    private bool canPlayLandRunSFX;
    private PenitentAudio _audio;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _input = GetComponent<PenitentInput>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _audio = transform.GetChild(1).GetComponent<PenitentAudio>();
    }
    void Start()
    {
        Speed = MoveSpeed;
        Jump += () => { _rb.velocity = new Vector2(_rb.velocity.x, JumpForce); };
    }

    void Update()
    {
        PlayerMovement();
        PlayerJump();
        LandRunSFX();
    }

    private void PlayerMovement()
    {
        if (_input.LeftKey && _input.RighttKey)
        {
            IsRunning = false;
            if (_stepTimer >= 0)
                _stepTimer -= Time.deltaTime;
            if (_stepTimer <= 0)
                _rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
            if (IsGrounded)
                _rb.velocity = new Vector2(Mathf.Lerp(_rb.velocity.x, 0, Time.deltaTime * SoftTime), _rb.velocity.y);
            else
                _rb.velocity = new Vector2(0, _rb.velocity.y);
        }
        else if (_input.RighttKey)
        {
            _stepTimer = 0.3f;
            _rb.velocity = new Vector2(Speed, _rb.velocity.y);
            _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            transform.localScale = new Vector2(1, 1);
            IsRunning = true;
        }
        else if (_input.LeftKey)
        {
            _stepTimer = 0.3f;
            _rb.velocity = new Vector2(-Speed, _rb.velocity.y);
            _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            transform.localScale = new Vector2(-1, 1);
            IsRunning = true;
        }
        else if (!_input.LeftKey && !_input.RighttKey)
        {
            IsRunning = false;
            if (_stepTimer >= 0)
                _stepTimer -= Time.deltaTime;
            if (_stepTimer <= 0)
                _rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
            if (IsGrounded)
                _rb.velocity = new Vector2(Mathf.Lerp(_rb.velocity.x, 0, Time.deltaTime * SoftTime), _rb.velocity.y);
            else
                _rb.velocity = new Vector2(0, _rb.velocity.y);
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

        if (IsGrounded && canPlayLandRunSFX)
        {
            canPlayLandRunSFX = false;
            _audio.PlayLand(LandSFX.Running);
            
        }
    }
}
