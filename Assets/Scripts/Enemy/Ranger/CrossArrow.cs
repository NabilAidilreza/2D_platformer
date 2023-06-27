using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossArrow : EnemyProjectile
{
    public float lifetime;
    private Transform player;
    private Vector2 target;
    // Start is called before the first frame update
    private void Start()
    {
        Invoke("DestroyCrossArrow", lifetime);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        target = new Vector2(player.position.x, player.position.y);
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            DestroyCrossArrow();
        }
    }
    void DestroyCrossArrow()
        {
            Destroy(gameObject);
            Instantiate(Burst, transform.position, Quaternion.identity);
        }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.isTrigger != true)
        {
            collision.SendMessageUpwards("PlayerDamage", damage);
            DestroyProjectile();
        }
        else if(collision.isTrigger != true && !collision.CompareTag("Enemy"))
        {
            DestroyProjectile();
        }

    }
}
