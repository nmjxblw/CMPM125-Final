using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage;
    public float attackRange;
    public float attackRate;

    protected virtual void OnTriggerStay2D(Collider2D other)
    {
        other.GetComponent<Character>()?.TakeDamage(this);
    }
}
