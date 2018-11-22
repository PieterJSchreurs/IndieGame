using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public abstract class MovingObject : MonoBehaviour {
    protected Rigidbody2D _rb;
    protected Collider2D _coll;

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
    }

    protected abstract void Move(bool isFixed);
    protected abstract void HandleCollision();


}
