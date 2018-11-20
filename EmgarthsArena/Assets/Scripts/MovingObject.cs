using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour {
    protected abstract void Move();
    protected abstract void HandleCollision();
}
