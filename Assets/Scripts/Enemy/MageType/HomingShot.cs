using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingShot : EnemyProjectile
{
    private Transform player;
    private Vector2 target;
    //What can be Hit
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
        Invoke("DestroyProjectile", lifeTime);

    }

    // Update is called once per frame
    void Update()
    {
        target = new Vector2(player.position.x, player.position.y);
        //Targeting

        if (Vector2.Distance(transform.position, player.position) > 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        }
        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            DestroyProjectile();
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.isTrigger == false)
        {
            collision.SendMessageUpwards("PlayerDamage", damage);
            DestroyProjectile();
        }
        else if (collision.isTrigger != true && !collision.CompareTag("Enemy"))
        {
            DestroyProjectile();
        }

    }
}
