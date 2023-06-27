using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownAttack : EnemyProjectile
{
    private Vector2 Point;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Point = new Vector2(transform.position.x,player.position.y);
    }

    // Update is called once per frame
    void Update()
    {

        //Targeting
        transform.position = Vector2.MoveTowards(transform.position, Point, speed * Time.deltaTime);
        if (transform.position.x == Point.x && transform.position.y == Point.y)
        {
            DestroyProjectile();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.isTrigger != true)
        {
            collision.SendMessageUpwards("PlayerDamage", damage);
            DestroyProjectile();
        }
        else if(collision.isTrigger != true)
        {
            DestroyProjectile();
        }


    }
}
