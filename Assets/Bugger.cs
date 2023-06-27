using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bugger : Goblin
{
    public float spread;
    public GameObject BlastBullet;
    public Transform BlastPoint;
    private Vector3 Waypoint;
    private int count;
    public float offset;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Waypoint = new Vector3(target.position.x + Random.Range(-spread,spread),target.position.y + Random.Range(15,20),0);
        count = 0;
        currhealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 difference = target.position - transform.position;
        float rotz = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotz + offset);

        transform.position = Vector2.MoveTowards(transform.position, Waypoint, speed * Time.deltaTime);

        if (transform.position == Waypoint)
        {
            Instantiate(BlastBullet, BlastPoint.position, Quaternion.identity);
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            Waypoint = new Vector3(target.position.x + Random.Range(-spread, spread), target.position.y + Random.Range(15, 20), 0);
            count++;
        }
        if (count == 4)
        {
            Instantiate(Burst, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
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
}
