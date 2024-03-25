using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Collectable : MonoBehaviour
{

    public ItemType type;
    private float speed = 5f;
    public float PickupDistance = 3f;
    public float Distance;

    
    // Update is called once per frame
    void Update()
    {
        Distance = Vector3.Distance(this.transform.position, GameManager.Instance.PlayerTF.position);
        if(Distance > PickupDistance)
        {
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, GameManager.Instance.PlayerTF.position,speed*Time.deltaTime);
        if (Distance < 0.1f)
        {
            GameManager.Instance.player.inventory.AddItem(this.type);
            Destroy(this.gameObject);
        }
    }
}
