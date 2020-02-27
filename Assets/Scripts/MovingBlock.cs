using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    [SerializeField] private float _blockSpeed;

    private GameObject _generator;
    private void Start()
    {
        _blockSpeed = GameObject.FindWithTag("BlockGenerator").GetComponent<BlockGenerator>().BlockSpeed;
    }
    void FixedUpdate()
    {
        transform.Translate(Vector2.left * _blockSpeed);
    }
}
