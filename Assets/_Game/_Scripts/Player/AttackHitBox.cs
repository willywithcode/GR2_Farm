using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitBox : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            SoundManager.Instance.OnPlaySword();
            EnemyController enemy = Cache.GetScript(collision);
            enemy.TakeDamage(1, this.transform);
        }
    }
}
