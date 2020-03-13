using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _block, _score;

    [SerializeField] private float _nextLaunch, _minDelay, _maxDelay;

    public float BlockSpeed;
    private float _randomTime, _randomPositionY, _randomNum;

    private int positionX = 1500;
    private void Start()
    {
        StartCoroutine("Spawn"); 
    }
    void FixedUpdate()
    {
        BlockSpeed += 0.00002f;
    }


    private IEnumerator Spawn()
    {
        _randomNum = Random.Range(1, 10);
        _randomTime = Random.Range(1f, 2f);
        _randomPositionY = Random.Range(-450, 150);
        Vector2 _position = new Vector2(positionX, _randomPositionY);
        
        Instantiate(_block, _position, Quaternion.identity);
        if (_randomNum > 7)
        {
            _position.y += 50;
            Instantiate(_score, _position, Quaternion.identity);
        }
        

        yield return new WaitForSeconds(_randomTime);
        StartCoroutine("Spawn");
    }
}
