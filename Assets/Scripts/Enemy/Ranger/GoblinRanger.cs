using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinRanger : Goblin
{
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        name = "GoblinRanger";
        currhealth = health;
        oldPosition = transform.position.x;       
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
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        if (transform.position.x < oldPosition) // he's looking left
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        oldPosition = transform.position.x;



        //Move
        DistanceToP = Vector2.Distance(transform.position, target.position);
        if (DistanceToP > Attackrange)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        else if (DistanceToP < Attackrange && DistanceToP > retreatDistance)
        {
            transform.position = this.transform.position;
        }
        else if (DistanceToP < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, -speed * Time.deltaTime);
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
