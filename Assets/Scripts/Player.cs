using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using UnityEngine.Rendering.PostProcessing;

public class Player : MonoBehaviour
{
    public bool isGrounded;
    public int Points;
    public int Direction;


    [SerializeField] private Rigidbody2D _player;

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    private Vector2 _horizontalMove;

    private int PointsAmount;
    private int MaxPointsAmount;

    [SerializeField] private Image[] _points;
    [SerializeField] private Image _BonusButton;

    [SerializeField] private PostProcessVolume _postProcessVolume;
    private ColorGrading _colorGrading;

    private int _randomNum;
    public BlockGenerator BlockGenerator;

    private void Start()
    {
        _postProcessVolume.profile.TryGetSettings(out _colorGrading);
        _BonusButton.enabled = false;
        
        PointsAmount = 0;
        MaxPointsAmount = 5;

        

        for (int i = 0; i < _points.Length; i++)
        {
            _points[i].enabled = false;
        }
        //_isOnCooldown = false;
        //_dashBar.fillAmount = 1;
    }

    private void Update()
    {
        //if (_dashBar.fillAmount < 1)
        //{
        //    _dashBar.fillAmount += Time.deltaTime * 0.2f;
        //}
    }

    private void FixedUpdate()
    {
        _horizontalMove = new Vector2(Direction * _speed, 0);
        transform.Translate(_horizontalMove);
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
        Direction = x;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Point" && PointsAmount < MaxPointsAmount)
        {
            Modify(1);
            Destroy(other.gameObject);
        }
    }

    public void Modify(int value)
    {
        PointsAmount += value;
        for (int i = 0; i < _points.Length; i++)
        {
            if (_points[i].tag == "EmptyPoint")
            {
                _points[i].enabled = true;
                _points[i].tag = "FilledPoint";
                break;
            }
        }

        if (PointsAmount == 5)
        {
            _BonusButton.enabled = true;
        }
    }

    public void LevelUp()
    {
        ResetColors();
        _randomNum = Random.Range(1,4);
        ChooseColor(_randomNum);

        PointsAmount = 0;
        BlockGenerator.BlockSpeed += 0.3f;
        for (int i = 0; i < _points.Length; i++)
        {
            _points[i].enabled = false;
            _points[i].tag = "EmptyPoint";
        }
        _BonusButton.enabled = false;
    }

    private void ChooseColor(int value)
    {
        switch (value)
        {
            case 1:
                _colorGrading.mixerRedOutGreenIn.value = 40;
                break;
            case 2:
                _colorGrading.mixerRedOutGreenIn.value = -50;
                break;
            case 3:
                _colorGrading.mixerGreenOutRedIn.value = 75;
                break;
            case 4:
                _colorGrading.mixerBlueOutRedIn.value = -80;
                break;
        }
    }

    private void ResetColors()
    {
        _colorGrading.mixerRedOutGreenIn.value = 0;
        _colorGrading.mixerGreenOutRedIn.value = 0;
        _colorGrading.mixerBlueOutRedIn.value = 0;
    }

    /*
    private IEnumerator CoolDown()
    {
        _isOnCooldown = true;
        _dashBar.fillAmount = 0f;

        yield return new WaitForSeconds(_dashCooldown);
        _isOnCooldown = false;
    }

    public void Dash()
    {
        if (!_isOnCooldown)
        {
            Vector2 _dashVector = new Vector2(3,3);
            _player.AddForce(_dashVector * _dashSpeed, ForceMode2D.Impulse);
            AudioManager.PlaySound("Dash");
            StartCoroutine("CoolDown");
        }
    }
    */
}
