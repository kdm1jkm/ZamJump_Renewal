using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float jumpPower;
    [SerializeField] private float maxSpeed;
    public float gravity;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _renderer;

    [SerializeField] private Sprite jump;
    [SerializeField] private Sprite hold;
    [SerializeField] private Sprite idle;

    private float _horizontalSpeed;
    private float _verticalSpeed;

    private bool _isCollideWall;
    private PlayerState _currentState;
    private Direction _currentDirection;


    enum PlayerState
    {
        HOLD,
        JUMP,
        IDLE
    }


    enum Direction
    {
        LEFT = -1,
        RIGHT = 1
    }

    private PlayerState CurrentState
    {
        get => _currentState;
        set
        {
            _currentState = value;
            switch (value)
            {
                case PlayerState.HOLD:
                    _renderer.sprite = hold;
                    break;
                case PlayerState.JUMP:
                    _renderer.sprite = jump;
                    break;
                case PlayerState.IDLE:
                    _renderer.sprite = idle;
                    break;
            }
        }
        
    }

    private Direction CurrentDirection
    {
        get => _currentDirection;
        set
        {
            _currentDirection = value;
            switch (value)
            {
                case Direction.LEFT:
                    _renderer.flipX = true;
                    break;
                case Direction.RIGHT:
                    _renderer.flipX = false;
                    break;
            }
        }
    }


    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();

        CurrentState = PlayerState.HOLD;
        CurrentDirection = Direction.LEFT;

        _horizontalSpeed = 4 * Mathf.Sqrt(-gravity / 2 / 4);
        _verticalSpeed = Mathf.Sqrt(-2 * gravity * jumpPower) / 2;
    }

    void Update()
    {
        switch (CurrentState)
        {
            case PlayerState.HOLD:
                if (Input.GetMouseButtonDown(0))
                {
                    CurrentState = PlayerState.JUMP;
                    _rigidbody.velocity = new Vector2(_verticalSpeed * (float) CurrentDirection, _horizontalSpeed);
                }

                break;

            case PlayerState.JUMP:
                if (_isCollideWall)
                {
                    CurrentState = PlayerState.HOLD;
                    CurrentDirection = CurrentDirection == Direction.LEFT ? Direction.RIGHT : Direction.LEFT;
                    _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
                    _isCollideWall = false;
                }

                break;
        }

        _rigidbody.velocity += Vector2.up * (gravity * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (CurrentState != PlayerState.JUMP)
        {
            return;
        }

        if (other.gameObject.CompareTag("Wall"))
        {
            _isCollideWall = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            _isCollideWall = false;
        }
    }
}