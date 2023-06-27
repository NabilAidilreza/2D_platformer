using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ChildSpread : EnemyProjectile
{
    private Vector3 shootDir;
    // Start is called before the first frame update
    private void Start()
    {
        shootDir = new Vector3(Random.Range(-3, 3), -2,0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += shootDir * speed * Time.deltaTime;
    }
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
