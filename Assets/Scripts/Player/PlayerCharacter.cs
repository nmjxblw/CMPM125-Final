using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCharacter : Character
{
    public float defence;

    protected override void Start(){
        maxHealth = TransformManager.Instance.maxHealth;
        currentHealth = maxHealth;      
    }

    public override void TakeDamage(Attack attacker)
    {
         if (invulnerable)
        {
            return;
        }

        if (currentHealth - (attacker.damage - defence)  > 0)
        {
            currentHealth -= attacker.damage - defence;
            TriggerInvulnerable();
            //触发受伤
            OnTakeDamage?.Invoke(attacker.transform);
        }
        else
        {
            currentHealth = 0;
            //触发死亡
            OnDeath?.Invoke();
        }
        TransformManager.Instance.currentHealth = currentHealth;
    }

}
