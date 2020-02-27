using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;

public class Player : MonoBehaviour
{
    public bool isGrounded;

    private Rigidbody2D _player;

    [SerializeField] private float _speed = 0.5f;
    [SerializeField] private float _jumpForce = 5f;

    private float _moveHorizontal;
    private float _moveVertical;
    private Vector2 _movement;

    private float _dashSpeed = 5f;
    private float _dashDuration = 0.1f;
    private float _dashCooldown = 5f;
    private Touch _touch;
    

    private bool _isOnCooldown;
    
    [SerializeField] private Image _dashBar;

    //[SerializeField] private Button _moveLeftButton, _moveRightButton, _jumpButton;

    [SerializeField]
    private Joystick _joystick;

    private Vector3 _touchStartPosition;
    private Vector3 _touchEndPosition;

    private void Start()
    {
        _player = GetComponent<Rigidbody2D>();
        _isOnCooldown = false;
        _dashBar.fillAmount = 1;
    }

    private void Update()
    {
        _moveHorizontal = _joystick.Horizontal;
        _moveVertical = _joystick.Vertical;

        if (_moveVertical >= 0.5f && isGrounded)
        {
            Jump();
        }

        if (Input.touchCount > 0 && !_isOnCooldown)
        {
            StartCoroutine("Dash");
        }

        if (_dashBar.fillAmount < 1)
        {
            _dashBar.fillAmount += Time.deltaTime * 0.2f;
        }
    }

    private void FixedUpdate()
    {
        _movement = new Vector2(_moveHorizontal, 0);

        transform.Translate(_movement * _speed);
    }

    private IEnumerator Dash()
    {
        Touch touch = Input.GetTouch(0);
        switch (touch.phase)
        {
            case TouchPhase.Began:
                _touchStartPosition = Camera.main.ScreenToWorldPoint(_touch.position);
                _touchStartPosition.z = 0;
                Debug.Log(_touchStartPosition);
                break;

            case TouchPhase.Ended:
                _touchEndPosition = Camera.main.ScreenToWorldPoint(_touch.position);
                _touchEndPosition.z = 0;
                Debug.Log(_touchEndPosition);
                Vector2 _dashVector = _touchEndPosition - _touchStartPosition;

                AudioManager.PlaySound("Dash");

                _player.velocity = _dashVector * _dashSpeed;

                _isOnCooldown = true;
                yield return new WaitForSeconds(_dashDuration);
                _player.velocity = Vector2.zero;
                _dashBar.fillAmount = 0f;

                yield return new WaitForSeconds(_dashCooldown);
                _isOnCooldown = false;
                break;
        }
        
    }

    public void Jump()
    {
        if (isGrounded)
        {
            _player.velocity = Vector2.up * _jumpForce;
            AudioManager.PlaySound("Jump");
        }
    }

    public void Move(int x)
    {
        _movement = new Vector2(x, 0);
    }
}
