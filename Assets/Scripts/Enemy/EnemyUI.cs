using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyUI : MonoBehaviour
{
    [Header("Hp bar setting")]
    private Character character;
    private Image HpBarImage;
    private Image HpBarEffectImage;
    [SerializeField] private float hpBarEffectDuration = 0.5f;
    private Coroutine updateCoroutine;
    private Coroutine disableCoroutine;

    private void Update(){
        transform.localScale = new Vector3(transform.parent.localScale.x, 1, 1);
    }
    private void OnEnable()
    {
        //get components from children
        HpBarImage = transform.Find("HpBar").Find("HpBarImage").GetComponent<Image>();
        HpBarEffectImage = transform.Find("HpBar").Find("HpBarEffectImage").GetComponent<Image>();
        character = transform.parent.GetComponent<Character>();
        HpBarImage.fillAmount = (float)character.currentHealth / (float)character.maxHealth;
        HpBarEffectImage.fillAmount = HpBarImage.fillAmount;
    }

    public void UpdateHpDisplay()
    {
        //TODO: Update hp bar, need enemy states script.
        HpBarImage.fillAmount = (float)character.currentHealth / (float)character.maxHealth;
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
        if(disableCoroutine != null){
            StopCoroutine(disableCoroutine);
        }
        disableCoroutine = StartCoroutine(DisableSelf());
    }

    private IEnumerator DisableSelf(){
        yield return new WaitForSeconds(15f);
        gameObject.SetActive(false);
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,0.1f);
    }
}
