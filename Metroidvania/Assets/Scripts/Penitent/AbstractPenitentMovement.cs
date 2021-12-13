using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Dir
{
    Right,
    Left
}

public abstract class AbstractPenitentMovement : MonoBehaviour
{
    [SerializeField] protected LayerMask GroundLayer;
    protected Rigidbody2D _rb;
    protected BoxCollider2D _boxCollider2D;
    protected bool IsRunning;
    protected float _stepTimer;

    public delegate void JumpDelegate();

    private void Awake()
    {
        this.OnAwake();
    }

    private void Start()
    {
        OnStart();
    }
    private void Update()
    {
        OnUpdate();
    }
    protected virtual void OnAwake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }
    protected virtual void OnStart()
    {
    }
    protected virtual void OnUpdate()
    { 
    
    }
    public bool IsGrounded { get { return Physics2D.BoxCast(_boxCollider2D.bounds.center, _boxCollider2D.size, 0, Vector2.down, 0.1f, GroundLayer); } }
    protected void Stop(float softTime)
    {
        IsRunning = false;
        if (_stepTimer >= 0)
            _stepTimer -= Time.deltaTime;
        if (_stepTimer <= 0)
            _rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
        if (IsGrounded)
            _rb.velocity = new Vector2(Mathf.Lerp(_rb.velocity.x, 0, Time.deltaTime * softTime), _rb.velocity.y);
        else
            _rb.velocity = new Vector2(0, _rb.velocity.y);
    }

    protected void Run(Dir dir, float speed)
    {
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        _stepTimer = 0.3f;
        IsRunning = true;
        switch (dir)
        {
            case Dir.Right:
                {
                    _rb.velocity = new Vector2(speed, _rb.velocity.y);
                }
                break;
            case Dir.Left:
                {
                    _rb.velocity = new Vector2(-speed, _rb.velocity.y);
                }
                break;
            default:
                break;
        }
    }
    protected void Flip(Dir dir)
    {
        switch (dir)
        {
            case Dir.Left:
                {
                    transform.localScale = new Vector2(-1, 1);
                }
                break;
            case Dir.Right:
                {
                    transform.localScale = new Vector2(1, 1);
                }
                break;
        }
    }

    protected void AddForce(Vector2 vec)
    {
        _rb.velocity = vec;
    }


}