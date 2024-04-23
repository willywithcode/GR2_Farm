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
    private bool IsGround;
    public Inventory inventory;

    public float speed;
    Vector2 movement;
    Vector2 direction;
    private PlayerAction PlayerAction;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        PlayerAction = new PlayerAction();
        rb = GetComponent<Rigidbody2D>();
        //Anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        PlayerAction.Enable();
    }

    void Update()
    {
        PlayerInput();
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
    //public bool CheckGround(Vector3 direc)
    //{
    //    Debug.DrawRay(this.transform.position, Vector3.forward,  Color.red, 5f);
    //    RaycastHit2D hit;
    //    Vector2 pos = new Vector2(this.transform.position.x, this.transform.position.y) + new Vector2(direc.x, direc.y);
    //    if(Physics2D.Raycast(pos, Vector3.forward ,5f, GroundLayer))
    //    {
    //        return true;
    //    }
    //    return false;

    //}




    //void ChangeAnim(string animName)
    //{
    //    if(currentAnim != animName)
    //    {
    //        Anim.ResetTrigger(currentAnim);
    //        currentAnim = animName;
    //        Anim.SetTrigger(currentAnim);
    //    }
    //}
}
