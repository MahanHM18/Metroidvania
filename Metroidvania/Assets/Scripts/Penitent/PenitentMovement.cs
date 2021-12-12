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
    public static event JumpDelegate Jump; 

    public bool IsGrounded { get { return Physics2D.BoxCast(_boxCollider2D.bounds.center, _boxCollider2D.size, 0, Vector2.down, 0.1f, GroundLayer); } }
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _input = GetComponent<PenitentInput>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
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
    }

    private void PlayerMovement()
    {
        if (_input.LeftKey && _input.RighttKey)
        {
            if (_stepTimer >= 0)
                _stepTimer -= Time.deltaTime;
            if (_stepTimer <= 0)
                _rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
            _rb.velocity = new Vector2(Mathf.Lerp(_rb.velocity.x, 0, Time.deltaTime * SoftTime), _rb.velocity.y);
        }
        else if (_input.RighttKey)
        {
            _stepTimer = 0.3f;
            _rb.velocity = new Vector2(Speed, _rb.velocity.y);
            _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            transform.localScale = new Vector2(1, 1);
        }
        else if (_input.LeftKey)
        {
            _stepTimer = 0.3f;
            _rb.velocity = new Vector2(-Speed, _rb.velocity.y);
            _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            transform.localScale = new Vector2(-1, 1);
        }
        else if (!_input.LeftKey && !_input.RighttKey)
        {
            if (_stepTimer >= 0)
                _stepTimer -= Time.deltaTime;
            if (_stepTimer <= 0)
                _rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
            _rb.velocity = new Vector2(Mathf.Lerp(_rb.velocity.x, 0, Time.deltaTime * SoftTime), _rb.velocity.y);
        }
    }
    private void PlayerJump()
    {
        if (_input.Jump)
        {
            if (IsGrounded)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, JumpForce);
                GetComponent<Animator>().SetTrigger("Jump");
            }
        }
    }
}
