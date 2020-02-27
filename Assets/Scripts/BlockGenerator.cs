using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _block;

    [SerializeField] private float _nextLaunch, _minDelay, _maxDelay;

    public float BlockSpeed;

    void Update()
    {
        if (Time.time > _nextLaunch)
        {
            Vector2 _position = new Vector2(20, Random.Range(-4, 3));
            Instantiate(_block, _position, Quaternion.identity);

            _nextLaunch = Time.time + Random.Range(_minDelay, _maxDelay);
        }

        BlockSpeed += 0.000001f;
    }
}
