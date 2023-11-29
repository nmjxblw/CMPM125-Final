using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowAttack : Attack
{
    protected override void OnTriggerStay2D(Collider2D other)
    {
        base.OnTriggerStay2D(other);
        gameObject.SetActive(false);
    }
}
