using UnityEngine;
using System.Collections;

public class CursedDagger : EnemyProjectile
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.isTrigger != true)
        {
            collision.SendMessageUpwards("PlayerDamage", damage);
            DestroyProjectile();
        }
        else if (collision.isTrigger != true)
        {
            DestroyProjectile();
        }


    }
}