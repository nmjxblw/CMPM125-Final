using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    private AudioSource AudioSource;

    [Header("Shared Clips")]
    public AudioClip transformIntoClip;
    public AudioClip diedClip;
    public AudioClip hurtClip;

    [Header("Slime Clips")]
    public AudioClip slimeJumpClip;
    public AudioClip slimeAttackClip;

    [Header("Soldier/Boar Clips")]
    public AudioClip soldierJumpClip;
    public AudioClip soldierAttackClip;

    [Header("Archer Clips")]
    public AudioClip archerAttackClip;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        AudioSource = GetComponent<AudioSource>();
        AudioSource.loop = false;
    }

    public void PlayOnce(AudioClip clip)
    {
        AudioSource.PlayOneShot(clip);
    }
    public void PlayDiedClip()
    {
        AudioSource.PlayOneShot(diedClip);
    }
    public void PlayTransformIntoClip()
    {
        AudioSource.PlayOneShot(transformIntoClip);
    }
    public void PlayhurtClip()
    {
        AudioSource.PlayOneShot(hurtClip);
    }

    public void PlaySlimeJump()
    {
        AudioSource.PlayOneShot(slimeJumpClip);
    }
    public void PlaySoldierJump()
    {
        AudioSource.PlayOneShot(soldierJumpClip, 0.5f);
    }

    public void PlaySlimeAttack()
    {
        AudioSource.PlayOneShot(slimeAttackClip);
    }
    public void PlaySoldierAttack()
    {
        AudioSource.PlayOneShot(soldierAttackClip);
    }
    public void PlayArcherAttack()
    {
        AudioSource.PlayOneShot(archerAttackClip);
    }
}
