using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerCharacter : Character
{
    public float defence;

    protected override void Start()
    {
        maxHealth = TransformManager.Instance.maxHealth;
        currentHealth = TransformManager.Instance.currentHealth;
    }

    public override void TakeDamage(Attack attacker)
    {
        if (invulnerable)
        {
            return;
        }
        //float dmgTaken = Mathf.Max((attacker.damage - defence), 1);
        float dmgTaken = Mathf.Max((attacker.damage * (100 - defence) / 100), 1);
        //Debug.Log("Player Take Damage: " + dmgTaken);
        TransformManager.Instance.currentHealth = TransformManager.Instance.currentHealth - dmgTaken;
        
        if (TransformManager.Instance.currentHealth < 1)
        {
            TransformManager.Instance.currentHealth = 0;
        }

        currentHealth = TransformManager.Instance.currentHealth;

        if (currentHealth > 1)
        {
            TriggerInvulnerable();
            AudioManager.Instance.PlayhurtClip();
            //触发受伤
            OnTakeDamage?.Invoke(attacker.transform);
        }
        else
        {
            AudioManager.Instance.PlayDiedClip();
            //触发死亡
            OnDeath?.Invoke();
            //SceneLoader.Instance.GoToLooseScene(1f);
            Invoke("GoToDieScene", 1f);


        }

    }

    public void GoToDieScene()
    {
        SceneManager.LoadScene("Loose");
    }

}
