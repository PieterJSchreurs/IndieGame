﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public abstract class MovingObject : MonoBehaviour
{
    protected Rigidbody2D _rb;
    protected Collider2D _coll;

    protected bool _isPaused = false;
    protected Vector3 _lastVelocity = Vector2.zero;


    protected virtual void Start()
    {
        if (_rb == null)
        {
            _rb = GetComponent<Rigidbody2D>();
        }
        if (_coll == null)
        {
            _coll = GetComponent<Collider2D>();
        }
        SceneManager.GetInstance().AddMovingObject(this);
    }

    protected abstract void Move(bool isFixed);
    protected abstract void HandleCollision(Collision2D collision);
    public void TogglePaused(bool pToggle)
    {
        _isPaused = pToggle;
        if (_isPaused)
        {
            _lastVelocity = _rb.velocity;
            _rb.velocity = Vector2.zero;
            _rb.isKinematic = true;
        }
        else
        {
            _rb.isKinematic = false;
            _rb.velocity = _lastVelocity;
        }
    }
}
