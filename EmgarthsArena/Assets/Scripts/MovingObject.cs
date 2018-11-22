using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class MovingObject : MonoBehaviour {
    protected Rigidbody2D _rb;

    protected abstract void Move();
    protected abstract void HandleCollision();


}
