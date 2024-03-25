using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{

    [SerializeField] Animator Anim;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] LayerMask GroundLayer;
    private string currentAnim;
    private bool IsGround;

    private Vector3 direction;
    public float speed;
    public Inventory inventory;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        direction = new Vector3(horizontal, vertical, 0).normalized;

    }
    private void FixedUpdate()
    {
        if (Vector3.Distance(direction, Vector3.zero) > 0.01f)
        {
            float moveAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            DetermineQuadrant(moveAngle);
            if (CheckGround(direction))
            {
                transform.position += direction * speed * Time.deltaTime;

            }
        }
    }

    public bool CheckGround(Vector3 direc)
    {
        Debug.DrawRay(this.transform.position, Vector3.forward,  Color.red, 5f);
        RaycastHit2D hit;
        Vector2 pos = new Vector2(this.transform.position.x, this.transform.position.y) + new Vector2(direc.x, direc.y);
        if(Physics2D.Raycast(pos, Vector3.forward ,5f, GroundLayer))
        {
            return true;
        }
        return false;

    }
    void DetermineQuadrant(float angle)
    {

        if (angle > 0 && angle < 180f)
        {
            ChangeAnim("runtop");
        }
        else if (-180f< angle && angle < 0) 
        {
            ChangeAnim("rundown");
        }
        else if (angle - 0 <= 0.01f)
        {
            ChangeAnim("runright");
        }
        else if(angle - 180 <= .01f)
        {
            ChangeAnim("runleft");
        }
    }


    void ChangeAnim(string animName)
    {
        if(currentAnim != animName)
        {
            Anim.ResetTrigger(currentAnim);
            currentAnim = animName;
            Anim.SetTrigger(currentAnim);
        }
    }
}
