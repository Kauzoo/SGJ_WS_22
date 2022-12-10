using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRenderer : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Transform _playerTransform;
    private float _distance;

    [Header("Settings"), SerializeField] private float drawDistanceHorizontal;
    [SerializeField]
    private float drawDistanceVertical;
    [SerializeField]
    private float deleteDistanceHorizontal;
    [SerializeField]
    private float deleteDistanceVertical;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_spriteRenderer == null)
            Debug.LogError($"Missing {nameof(SpriteRenderer)} on {gameObject.name}");
    }

    // Start is called before the first frame update
    void Start()
    {
        var playerGo = GameObject.FindWithTag("Player").transform;
        if (playerGo == null)
            Debug.LogError($"@{gameObject.name}.{nameof(BackgroundRenderer)} Failed to find Object with Tag Player");
        _playerTransform = playerGo.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}