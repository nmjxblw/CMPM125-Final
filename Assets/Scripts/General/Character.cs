using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [Header("Character parameters")]
    public float maxHealth;
    public float currentHealth;

    [Header("Invincible After Taking Damage")]
    public float invulnerableDuration;
    private float invulnerableCounter;
    public bool invulnerable;
    public UnityEvent<Transform> OnTakeDamage;
    public UnityEvent OnDeath;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (invulnerable)
        {
            invulnerableCounter -= Time.deltaTime;
            if (invulnerableCounter <= 0)
            {
                invulnerable = false;
            }
        }
    }

    public virtual void TakeDamage(Attack attacker)
    {
        if (invulnerable)
        {
            return;
        }
        currentHealth = Mathf.Clamp((currentHealth - attacker.damage), 0, maxHealth);
        if (currentHealth > 0)
        {
            TriggerInvulnerable();
            //触发受伤
            OnTakeDamage?.Invoke(attacker.transform);
        }
        else
        {
            //触发死亡
            OnDeath?.Invoke();
        }
        transform.Find("HpDisplay")?.gameObject.SetActive(true);
        GetComponentInChildren<EnemyUI>()?.UpdateHpDisplay();
    }

    public void TriggerInvulnerable()
    {
        if (!invulnerable)
        {
            invulnerable = true;
            invulnerableCounter = invulnerableDuration;
        }
    }
}
