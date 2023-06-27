using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : Goblin
{
    public float minY;
    public float maxY;
    public float minX;
    public float maxX;
    private Vector3 Waypoint;
    public float offset;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        //Waypoint = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY),0);
        Waypoint = target.position;
        currhealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 difference = target.position - transform.position;
        float rotz = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotz + offset);

        Waypoint = target.position;
        transform.position = Vector2.MoveTowards(transform.position, Waypoint, speed * Time.deltaTime);
        //if(transform.position == Waypoint)
        //{
        //    Instantiate(MinBlast, BlastPoint.position, Quaternion.identity);
        //    Waypoint = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);
        //    count++;
        //}
        //if(count == 3)
        //{
        //    Waypoint = target.position;
        //}
        //CheckDead
        if (currhealth <= 0)
        {
            //KillEnemy
            Instantiate(Burst, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    public void DestroyProjectile()
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
        else if (collision.isTrigger != true && collision.CompareTag("Enemy") == false)
        {
            DestroyProjectile();
        }
    }
}
