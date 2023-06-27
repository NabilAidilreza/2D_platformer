using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    private Animator GobAnim;
    public int health;
    public float currhealth;
    public GameObject Burst;
    public float speed;
    public Transform target;
    //DistanceToPlayer
    public float DistanceToP;
    public float Attackrange;
    public float PatrolRange;
    public float retreatDistance;
    public bool MoveRight;
    //CheckDir
    public float oldPosition;
    //Damage Variables
    public int damage;
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
        MoveRight = true;
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
    //DamageFunc
    public void TakeDamage(int damage)
    {
        //TakeDamage
        Instantiate(Burst, transform.position, Quaternion.identity);
        currhealth -= damage;
    }
    //HealFunc
    public void HealEnemy(int HealPoints)
    {
        currhealth += HealPoints;
        Debug.Log(name + " HEALTH NOW: " + currhealth);
    }
    //CritFunc
    public void CritDamage(int damage)
    {
        //TakeDamage
        Instantiate(Burst, transform.position, Quaternion.identity);
        currhealth -= damage * 7;
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
