using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Priest : Goblin
{
    private Animator PriestAnim;
    // Start is called before the first frame update
    public int HealPoints;
    void Start()
    {
        
        name = "Priest";
        currhealth = health;
        oldPosition = transform.position.x;
        PriestAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        target = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Transform>();
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
        if (DistanceToP > Attackrange)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            PriestAnim.SetBool("PriestHeal", true);
        }
        else if (DistanceToP < Attackrange && DistanceToP > retreatDistance)
        {
            transform.position = this.transform.position;
            PriestAnim.SetBool("PriestHeal", true);
        }
        else if (DistanceToP < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, -speed * Time.deltaTime);
            PriestAnim.SetBool("PriestHeal", true);
        }
        //CheckDead
        if ( currhealth <= 0)
        {
            //KillEnemy
            Instantiate(Burst, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && collision.isTrigger != true)
        {
            collision.SendMessageUpwards("HealEnemy", HealPoints);
        }

    }

}
