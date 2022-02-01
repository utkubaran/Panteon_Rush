﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentCollisionHandler : MonoBehaviour
{
    [SerializeField][Range(0f, 2f)]
    private float respawnTimer;

    private OpponentMovementController opponentMovementController;

    private OpponentAnimationController opponentAnimationController;

    private Transform _transform, respawnPoint;

    private Rigidbody _rb;

    private void Awake()
    {
        _transform = transform;
        opponentMovementController = GetComponent<OpponentMovementController>();
        opponentAnimationController = GetComponent<OpponentAnimationController>();
    }

    void Start()
    {
        respawnPoint = GameObject.FindGameObjectWithTag("Respawn Point").transform;
        // _rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
        bool isObstacle = other.gameObject.GetComponent<Obstacle>();
        
        if (!isObstacle) return;
        
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        opponentMovementController.IsPlaying = false;
        opponentAnimationController.CurrentState = OpponentAnimationController.OpponentState.Idle;
        GetComponent<Collider>().enabled = false;
        // _rb.isKinematic = false;
        yield return new WaitForSeconds(respawnTimer);
        // _rb.isKinematic = true;
        _transform.position = respawnPoint.position;
        GetComponent<Collider>().enabled = true;
        opponentMovementController.IsPlaying = true;
        opponentAnimationController.CurrentState = OpponentAnimationController.OpponentState.Walking;
    }
}
