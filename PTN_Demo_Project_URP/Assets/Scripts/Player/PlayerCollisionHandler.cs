﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField]
    private float stickCollisionForce;

    [SerializeField][Range(0f, 2f)]
    private float respawnTimer;

    private PlayerMovementController playerMovementController;

    private Transform _transform, respawnPoint;

    private Rigidbody _rb;

    private void Awake()
    {
        _transform = transform;
        playerMovementController = GetComponent<PlayerMovementController>();
    }

    void Start()
    {
        respawnPoint = GameObject.FindGameObjectWithTag("Respawn Point").transform;
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rb.isKinematic = false;
            _rb.AddForce(Vector3.forward * 10f, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("hit!!!!!!");
        bool isObstacle = other.gameObject.GetComponent<Obstacle>();
        
        if (!isObstacle) return;
        
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        playerMovementController.IsPlaying = false;
        _rb.isKinematic = false;
        yield return new WaitForSeconds(respawnTimer);
        _rb.isKinematic = true;
        _transform.position = respawnPoint.position;
        playerMovementController.IsPlaying = true;
    }
}
