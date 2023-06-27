using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CystalRunner : Goblin
{
    private Animator CystalRunnerAnim;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        currhealth = health;
        oldPosition = transform.position.x;
        CystalRunnerAnim = GetComponent<Animator>();
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
            //Patrol
            CystalRunnerAnim.SetBool("Run", true);
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
            //Run To Player
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            CystalRunnerAnim.SetBool("Run", true);

        }
        else if (DistanceToP < Attackrange)
        {
            //Explode
            CystalRunnerAnim.SetBool("Run", false);
            currhealth -= 100;
        }


        //CheckDead
        if (currhealth <= 0)
        {
            //KillEnemy
            Instantiate(Burst, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
