using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orge : Goblin
{
    private Animator OrgeAnim;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        name = "Orge";
        currhealth = health;
        oldPosition = transform.position.x;
        OrgeAnim = GetComponent<Animator>();
        MoveRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        //MaxHealth
        if (currhealth > health)
        {
            currhealth = health;
        }
        //Debug.Log(name + " HEALTH NOW: " + currhealth);
        if (transform.position.x > oldPosition) // he's looking right
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        if (transform.position.x < oldPosition) // he's looking left
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        oldPosition = transform.position.x;
        
       
        //Move
        DistanceToP = Vector2.Distance(transform.position, target.position);
        if(DistanceToP > PatrolRange)
        {
            if (MoveRight)
            {
                transform.Translate(Time.deltaTime * speed, 0, 0);
            }
            else
            {
                transform.Translate(Time.deltaTime * -speed, 0, 0);
            }
        }
        else if (DistanceToP > Attackrange && DistanceToP < PatrolRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            OrgeAnim.SetBool("OrgeWalking", true);
            OrgeAnim.SetBool("OrgeAttacking", false);
        }
        else if(DistanceToP < Attackrange)
        {
            OrgeAnim.SetBool("OrgeWalking", false);
            OrgeAnim.SetBool("OrgeAttacking", true);
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
        else if (collision.gameObject.CompareTag("Turn"))
        {
            if (MoveRight)
            {
                MoveRight = false;
            }
            else
            {
                MoveRight = true;
            }
        }

    }

}
