using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowerOfXeldris : Goblin
{
    private float timeBtwShots;
    public float startTimeBtwShots;
    public float minX;
    public float maxX;
    public Rigidbody2D projectile;
    public Transform Launcher;
    public float projectileSpeed = 5000f;
    private Animator XeldrisAnim;
    public GameObject Grey;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        name = "FollowerOfXeldris";
        currhealth = health;
        oldPosition = transform.position.x;
        XeldrisAnim = GetComponent<Animator>();
        timeBtwShots = startTimeBtwShots;
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
        if (DistanceToP > Attackrange)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            //XeldrisAnim.SetBool("Move", true);
            //XeldrisAnim.SetBool("Teleport", false);

        }

        else if (DistanceToP < Attackrange & DistanceToP > retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, 1 * Time.deltaTime);
            if (timeBtwShots <= 0)
            {
                if (Vector2.Distance(transform.position, target.position) <= Attackrange)
                {
                    Rigidbody2D projectileInstance;
                    projectileInstance = Instantiate(projectile, Launcher.position, Launcher.rotation) as Rigidbody2D;
                    projectileInstance.AddForce(Launcher.right * projectileSpeed);
                }

                timeBtwShots = startTimeBtwShots;
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
            //Throw Daggers
            //XeldrisAnim.SetBool("Move", false);
            //XeldrisAnim.SetBool("Teleport", true);
        }
        else if (DistanceToP < retreatDistance)
        {
            Instantiate(Grey, transform.position, Quaternion.identity);
            transform.position = new Vector3(Random.Range(minX, maxX), transform.position.y, transform.position.z);
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
