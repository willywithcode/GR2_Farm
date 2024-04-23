using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    enum State
    {
        Roaming
    }

    [SerializeField] private int StartingHeath;
    private Collider2D collider;
    private int currentHeath;
    private Rigidbody2D rb;
    [SerializeField] private Animator Anim;
    private string currentAnim;
    private bool IsDeath;
    // move
    private State state;
    [SerializeField] float MoveSpeed = 2f;
    Vector2 moveDir;
    // knockback
    [SerializeField] private float KnockBackTime = .2f;
    private bool GettingKnockBack;
    // flash
    [SerializeField] private Material WhiteFlashMat;
    private Material defaulMat;
    [SerializeField] private float restoreDefaultMat;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider  = GetComponent<Collider2D>();
        defaulMat = spriteRenderer.material;
        state = State.Roaming;
        currentHeath = StartingHeath;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RoamingRoutine());
    }

    private void FixedUpdate()
    {
        if(GettingKnockBack || IsDeath)
        {
            return;
        }
        rb.MovePosition(rb.position + moveDir * MoveSpeed * Time.fixedDeltaTime);
    }
    public void MoveTo(Vector2 targetPos)
    {
        moveDir = targetPos;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator RoamingRoutine()
    {
       while(state == State.Roaming)
        {
            Vector2 roamingPos = GetRoamingPos();
            MoveTo(roamingPos);
            yield return new WaitForSeconds(1f);
        }
    }

    private Vector2 GetRoamingPos()
    {
        return new Vector2(Random.Range(-1f,1f), Random.Range(-1f,1f)).normalized;
    }

    public void TakeDamage(int Damage, Transform DamageSource)
    {
        //Debug.Log("Hit Dame");
        currentHeath -= Damage;
        GetKnockBack(DamageSource, 2f);
        StartCoroutine(FlashRoutine());


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

    public void GetKnockBack(Transform damageSource, float knockBackThrust)
    {
        GettingKnockBack = true;
        Vector2 difference = (transform.position - damageSource.position).normalized * knockBackThrust * rb.mass;
        rb.AddForce(difference, ForceMode2D.Impulse);
        StartCoroutine(KnockRoutine());
    }


    IEnumerator DeathRoutine()
    {
        ChangeAnim("Death");
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
    private IEnumerator KnockRoutine()
    {
        yield return new WaitForSeconds(KnockBackTime);
        rb.velocity = Vector2.zero;
        GettingKnockBack = false;
    }


    IEnumerator FlashRoutine()
    {
        spriteRenderer.material = WhiteFlashMat;
        yield return new WaitForSeconds(restoreDefaultMat);
        spriteRenderer.material = defaulMat;
        DetectDeath();
    }
    void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            Anim.ResetTrigger(currentAnim);
            currentAnim = animName;
            Anim.SetTrigger(currentAnim);
        }
    }
}
