using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private int StartingHeath;
    private Collider2D collider;
    private int currentHeath;
    public Rigidbody2D rb;
    [SerializeField] private Animator Anim;
    private string currentAnim;
    public bool IsDeath;
    // move


    // knockback
     public KnockBack knockBack;
    // flash
    Flash Flashing;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collider  = GetComponent<Collider2D>();
        currentHeath = StartingHeath;
        knockBack = GetComponent<KnockBack>();
        Flashing = GetComponent<Flash>();
    }

    public void TakeDamage(int Damage, Transform DamageSource)
    {
        currentHeath -= Damage;
        knockBack.GetKnockedBack(DamageSource, 2f);
        StartCoroutine(Flashing.FlashRoutine());
        StartCoroutine(CheckDetectDeathRoutine());
    }
    private void DetectDeath()
    {
        if(currentHeath <= 0)
        {
            IsDeath = true;
            collider.enabled = false;
            StartCoroutine(DeathRoutine());
        }
    }

    IEnumerator DeathRoutine()
    {
        ChangeAnim("Death");
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }

    private IEnumerator CheckDetectDeathRoutine()
    {
        yield return new WaitForSeconds(Flashing.GetRestoreMatTime());
        DetectDeath();
    }
    public void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            Anim.ResetTrigger(currentAnim);
            currentAnim = animName;
            Anim.SetTrigger(currentAnim);
        }
    }
}
