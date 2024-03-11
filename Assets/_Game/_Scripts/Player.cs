using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{

    [SerializeField] Animator Anim;
    [SerializeField] Rigidbody2D rb;

    private string currentAnim;


    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, vertical, 0).normalized;

        if (Vector3.Distance(movement, Vector3.zero) > 0.01f)
        {
            rb.velocity = new Vector2(horizontal * speed, vertical * speed);
            float moveAngle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            DetermineQuadrant(moveAngle);
        }
        else
        {
            ChangeAnim("rundown");
            rb.velocity = Vector2.zero;

        }



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
