using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //Projectile Variables
    public float lifeTime;
    public float distance;
    public int damage;
    //What can be Hit
    public LayerMask WhatisSolid;
    //CheckForFaceRight
    private bool facingRight;
    

    //Effect
    public GameObject Burst;

    // Start is called before the first frame update
    private void Start()
    {
        
        facingRight = true;
        //Call Function over Lifetimes
        Invoke("DestroyProjectile", lifeTime);
        float Hor = Input.GetAxis("Horizontal");
        ShootingLeft(Hor);

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        CheckDmg();
        
    }
    //DamageFunc 
    public void CheckDmg()
    {
        //HitInfo
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, WhatisSolid);
        if (hitInfo.collider != null)
        {
            //CheckEnemy
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                //DamageRegistered
                hitInfo.collider.GetComponent<Goblin>().TakeDamage(damage);
            }
            else if (hitInfo.collider.CompareTag("EnemyProjectile"))
            {
                hitInfo.collider.GetComponent<EnemyProjectile>().DestroyProjectile();
            }
            else if (hitInfo.collider.CompareTag("CritArea"))
            {
                hitInfo.collider.GetComponent<Goblin>().CritDamage(damage);
            }
            //ProjectileGone
            DestroyProjectile();
        }
    }
    //DestroyFunc
    public void DestroyProjectile()
    {
        
        Destroy(gameObject);
        Instantiate(Burst, transform.position, Quaternion.identity);
    }
    //CheckForLeft    FAIL
    private void ShootingLeft(float Hor)
    {
        if (Hor > 0 && !facingRight || Hor < 0 && facingRight)
        {
            facingRight = !facingRight;
            transform.Rotate(0f, 180f, 0f);
        }

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CritArea") && collision.isTrigger != false)
        {
            collision.SendMessageUpwards("CritDamage", damage);
            DestroyProjectile();
        }

    }
}
