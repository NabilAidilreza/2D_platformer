using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flyer : Goblin
{
    private Animator EyeBallAnim;
    public LayerMask playerLayer;
    public bool InRange;
    public Transform WayPoint;
    private float Reset;
    private float TimeT;
    // Start is called before the first frame update
    void Start()
    {
        TimeT = 2f;
        Reset = TimeT;
        currhealth = health;
        oldPosition = transform.position.x;
        EyeBallAnim = this.GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currhealth > health)
        {
            currhealth = health;
        }
        //FLip
        if (transform.position.x > oldPosition) // he's looking right
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        if (transform.position.x < oldPosition) // he's looking left
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        oldPosition = transform.position.x;
        InRange = Physics2D.OverlapCircle(transform.position, PatrolRange, playerLayer);
        DistanceToP = Vector2.Distance(transform.position, target.position);
        if (InRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            if(DistanceToP < Attackrange)
            {
                EyeBallAnim.SetBool("Near", true);
            }
            else
            {
                EyeBallAnim.SetBool("Near", false);
            }
        }
        else
        {
            if(TimeT <= 0)
            {
                WayPoint.position = transform.position + new Vector3(Random.Range(-1,1), Random.Range(-1, 1),0);
                TimeT = Reset;
            }
            else
            {
                TimeT -= Time.deltaTime;
            }
            transform.position = Vector2.MoveTowards(transform.position, WayPoint.position, speed * Time.deltaTime);
        }
        

        //CheckDead
        if (currhealth <= 0)
        {
            //KillEnemy
            Instantiate(Burst, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.isTrigger != true)
        {
            collision.SendMessageUpwards("PlayerDamage", damage);
        }
    }
}
