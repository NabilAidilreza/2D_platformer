using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageEnemy : Goblin
{
    private float timeBtwShots;
    public float startTimeBtwShots;
    public GameObject EnemyProjectile; 
    public Transform ShotPoint1;
    public Transform ShotPoint2;
    public Transform ShotPoint3;
    public float shootingDis;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
        timeBtwShots = startTimeBtwShots;
        oldPosition = transform.position.x;
        currhealth = health;
        name = "Mage";
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
        //Flip[
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
        }else if(DistanceToP < Attackrange && DistanceToP > retreatDistance)
        {
            transform.position = this.transform.position;
        }else if(DistanceToP < retreatDistance)
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
        if (timeBtwShots <= 0)
        {
            if (Vector2.Distance(transform.position, target.position) <= shootingDis)
            {
                Instantiate(EnemyProjectile, ShotPoint1.position, Quaternion.identity);
                Instantiate(EnemyProjectile, ShotPoint2.position, Quaternion.identity);
                Instantiate(EnemyProjectile, ShotPoint3.position, Quaternion.identity);
            }

            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
        
        
    }
}
