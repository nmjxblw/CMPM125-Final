using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyUI : MonoBehaviour
{
    [Header("Hp bar setting")]
    private Image HpBarImage;
    private Image HpBarEffectImage;
    [SerializeField] private float hpBarEffectDuration = 0.5f;
    private Coroutine updateCoroutine;

    void Update()
    {
        // make hp bar face camera
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
    }

    private void OnEnable()
    {
        //get components from children
        HpBarImage = transform.Find("HpBar").Find("HpBarImage").GetComponent<Image>();
        HpBarEffectImage = transform.Find("HpBar").Find("HpBarEffectImage").GetComponent<Image>();
    }

    public void UpdateHpDisplay()
    {
        //TODO: Update hp bar, need enemy states script.
        // HpBar.fillAmount = (float)enemyScript.currentHp / (float)enemyScript.maxHp;
        if (updateCoroutine != null)
        {
            StopCoroutine(updateCoroutine);
        }
        updateCoroutine = StartCoroutine(UpdateHpEffect());
    }

    private IEnumerator UpdateHpEffect()
    {
        float effectLength = HpBarEffectImage.fillAmount - HpBarImage.fillAmount;
        float elapsedTime = 0f;

        while (elapsedTime < hpBarEffectDuration && effectLength != 0)
        {
            elapsedTime += Time.deltaTime;
            HpBarEffectImage.fillAmount = Mathf.Lerp(HpBarImage.fillAmount + effectLength, HpBarImage.fillAmount, elapsedTime / hpBarEffectDuration);
            yield return null;
        }
        HpBarEffectImage.fillAmount = HpBarImage.fillAmount;
    }
}
