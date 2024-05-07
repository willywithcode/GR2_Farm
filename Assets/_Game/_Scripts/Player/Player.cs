using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    [SerializeField] Animator Anim;
    [SerializeField]　private  Rigidbody2D rb;

    private int maxHeath = 100;
    private int currentHeath;
    private int maxHunger = 100;
    private int currentHunger;

    public float speed;
    public Vector2 movement;
    private PlayerAction PlayerAction;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private bool IsAttack;
    private float timeAttack = 0.25f;
    private float attackCounter;
    bool IsDeath;
    KnockBack knockBack;
    bool IsKnockback;
    Flash flash;
    private void Awake()
    {
        PlayerAction = new PlayerAction();
        rb = GetComponent<Rigidbody2D>();
        //Anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        knockBack = GetComponent<KnockBack>();
        flash = GetComponent<Flash>();
    }
    // Start is called before the first frame update
    void Start()
    {
        IsKnockback = false;
        IsDeath = false;
        IsAttack = false;
        currentHeath = maxHeath;
        currentHunger = maxHunger;
        BodyManager.Instance.SetMaxParameter(currentHeath, currentHunger);
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        PlayerAction.Enable();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            TakeDamage(10);
        }
        if(Input.GetKeyDown(KeyCode.K))
        {
            ReduceHunger(10);
        }
        if (IsAttack)
        {
            attackCounter -= Time.deltaTime;
            if(attackCounter <= 0)
            {
                Anim.SetBool("IsAttacking", false);
                IsAttack = false;
            }
        }
        if(!IsKnockback && !IsDeath)
        {
            PlayerInput();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!IsAttack)
            {
                Anim.SetBool("IsAttacking", true);
                IsAttack = true;
                attackCounter = timeAttack;
            }
        }

    }
    private void FixedUpdate()
    {

            Move();           
        if(Mathf.Abs(movement.x) == 1 || Mathf.Abs(movement.y) == 1)
        {
            Idle();
        }
        bool flip = Anim.GetFloat("IdleX") < 0;
        this.transform.rotation = Quaternion.Euler(new Vector3(0f, flip ? 180f : 0f, 0f));

    }

    private void PlayerInput()
    {
        
        movement = PlayerAction.Movement.Move.ReadValue<Vector2>();
        if(movement!= Vector2.zero)
        {
            if (!SoundManager.Instance.StepGrassAudio.isPlaying)
            {
                SoundManager.Instance.StepGrassAudio.Play();
            }
        }
        else
        {
            SoundManager.Instance.StepGrassAudio.Stop();
        }
    }
    void Move()
    {
        
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        Anim.SetFloat("MoveX", movement.x);
        Anim.SetFloat("MoveY", movement.y);
        bool flip = movement.x < 0;
        this.transform.rotation = Quaternion.Euler(new Vector3(0f, flip ? 180f : 0f, 0f));
    }

    void Idle()
    {

        Anim.SetFloat("IdleX", movement.x);
        Anim.SetFloat("IdleY", movement.y);

    }


    public void TakeDamage(int damage)
    {

        if(damage >= currentHeath)
        {
            currentHeath = 0;
            BodyManager.Instance.setHeath(currentHeath);
        }
        else
        {
            currentHeath -= damage;
            BodyManager.Instance.setHeath(currentHeath);
        }

        DetectDeath();

    }

    public void HealHeath(int amount)
    {
        int value = amount + currentHeath;
        currentHeath = value > maxHeath ? maxHeath : value;
        BodyManager.Instance.setHeath(currentHeath);
    }
    public void ReduceHunger(int amount)
    {
        if (amount >= currentHunger)
        {
            currentHunger = 0;
            BodyManager.Instance.setHunger(currentHunger);
        }
        else
        {
            currentHunger -= amount;
            BodyManager.Instance.setHunger(currentHunger);
        }
    }

    public void IncreaseHunger(int amount)
    {
        int value = amount + currentHunger;
        currentHunger = value > maxHunger ? maxHunger : value;
        BodyManager.Instance.setHunger(currentHunger);
    }

    public void DetectDeath()
    {
        if(currentHeath <= 0)
        {
            // TO DO Death
        }
        else
        {            
            StartCoroutine(flash.FlashRoutine());
        }
    }

}
