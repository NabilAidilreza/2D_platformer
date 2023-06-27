using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    //ArrowSpam
    private float timeBtwShots;
    public float startTimeBtwShots;
    //SwordSpam
    private float timeBtwSwing;
    public float startTimeBtwSwing;
    //Sword Attack Variables
    public Transform attackPos;
    public float attackRange;
    public LayerMask WhatIsEnemies;
    public int damage;
    //Arrow Attack Variables
    public Rigidbody2D projectile;
    public Transform Launcher;
    public Transform ShotPoint;
    private float projectileSpeed = 100f;
    private bool facingRight;
    // Mouse //

    // Start is called before the first frame update
    void Start()
    {
        //Rotation = 0
        transform.rotation = Quaternion.identity;
        facingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        float Hor = Input.GetAxis("Horizontal");
        ShootingLeft(Hor);

        
        Vector2 launcherPos = Launcher.position;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - launcherPos;
        Launcher.right = lookDir;
        //CheckForArrowSpam
        if (timeBtwShots <= 0)
        {
            //E Press
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKey(KeyCode.E))
            {
                //Create Projectile
                
                Rigidbody2D projectileInstance;
                projectileInstance = Instantiate(projectile, ShotPoint.position, ShotPoint.rotation) as Rigidbody2D;
                projectileInstance.GetComponent<Rigidbody2D>().velocity = ShotPoint.right * projectileSpeed;
                //Restartaaaaaaaaaaaa
                timeBtwShots = startTimeBtwShots;
            }

            
        }
        else
        {
            //Decrease Over Time
            timeBtwShots -= Time.deltaTime;
        }
        if(timeBtwSwing <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, WhatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<Goblin>().TakeDamage(damage);
                }
                timeBtwSwing = startTimeBtwSwing;
            }
        }
        else
        {
            timeBtwSwing -= Time.deltaTime;
        }

    }
    private void ShootingLeft(float Hor)
    {
        if (Hor > 0 && !facingRight || Hor < 0 && facingRight)
        {
            facingRight = !facingRight;
            Launcher.Rotate(0f, 180f, 0f);
        }

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position,attackRange);
    }
}
