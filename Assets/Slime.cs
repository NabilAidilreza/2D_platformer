using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Goblin
{
    private Animator GobAnim;
    /// <summary>
    /// THIS CAN BE IMPROVED FOR AI AND BUFFS
    /// </summary>
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        name = "Goblin";
        currhealth = health;
        oldPosition = transform.position.x;
        GobAnim = GetComponent<Animator>();
        MoveRight = false;
    }
    void Update()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        //MaxHealth
        if (currhealth > health)
        {
            currhealth = health;
        }
        //Debug.Log(name + " HEALTH NOW: " + currhealth);

        //FLip
        if (transform.position.x > oldPosition) // he's looking right
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }

        if (transform.position.x < oldPosition) // he's looking left
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        oldPosition = transform.position.x;



        //Move
        DistanceToP = Vector2.Distance(transform.position, target.position);
        if (DistanceToP > PatrolRange)
        {
            GobAnim.SetBool("WalkGob", true);
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

            GobAnim.SetBool("WalkGob", true);

        }
        else if (DistanceToP < Attackrange)
        {

            GobAnim.SetBool("WalkGob", false);

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
