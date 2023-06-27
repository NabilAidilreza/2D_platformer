using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blast : EnemyProjectile
{
    private Vector3 Waypoint;
    private int count;
    public float offset;
    private Transform player;
    private Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        target = new Vector3(player.position.x , player.position.y - 1,0);
        Invoke("DestroyProjectile", lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 difference = target - transform.position;
        float rotz = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotz + offset);

        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.isTrigger != true)
        {
            collision.SendMessageUpwards("PlayerDamage", damage);
            DestroyProjectile();
        }
        else if (collision.isTrigger != true && collision.CompareTag("Enemy") == false)
        {
            DestroyProjectile();
        }
    }
}
