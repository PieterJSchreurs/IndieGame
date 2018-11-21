using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MovingObject
{

    protected int damage;
    protected int knockback;
    protected SpellDatabase.Element firstElement;
    protected SpellDatabase.Element secondElement;
    private Rigidbody2D rb;

}
