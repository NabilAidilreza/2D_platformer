using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeDrain : MonoBehaviour
{
    public int damage;
    private void Start()
    {
        damage = 10;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && collision.isTrigger != true)
        {
            collision.SendMessageUpwards("TakeDamage", damage);
            
        }
    }
}
