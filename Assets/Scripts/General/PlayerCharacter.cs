using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCharacter : Character
{

    protected override void Start(){
        maxHealth = TransformManager.Instance.maxHealth;
        currentHealth = maxHealth;      
    }

    public override void TakeDamage(Attack attacker)
    {
        base.TakeDamage(attacker);
        TransformManager.Instance.currentHealth = currentHealth;
    }

}
