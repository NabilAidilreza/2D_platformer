using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingSystem : MonoBehaviour
{
    public int speed;
    public float distance;
    public int damage;
    public LayerMask WhatisSolid;
    public GameObject Burst;
    //CheckDir
    public float oldPosition;
    // Update is called once per frame
    void Start()
    {
        oldPosition = transform.position.x;
        StartCoroutine(Life());
    }
    void Update()
    {
        //Flip
        if (transform.position.x < oldPosition) // he's looking right
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }

        if (transform.position.x > oldPosition) // he's looking left
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        oldPosition = transform.position.x;
        FindClosestEnemy();
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, WhatisSolid);
        if (hitInfo.collider != null)
        {
            //CheckEnemy
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                //DamageRegistered
                hitInfo.collider.GetComponent<Goblin>().TakeDamage(damage);
                //ProjectileGone
                DestroyProjectile();
            }
            else if (hitInfo.collider.CompareTag("EnemyProjectile"))
            {
                hitInfo.collider.GetComponent<EnemyProjectile>().DestroyProjectile();
                StartCoroutine(Life());
            }
            else if (hitInfo.collider.CompareTag("CritArea"))
            {
                hitInfo.collider.GetComponent<Goblin>().CritDamage(damage);
                //ProjectileGone
                DestroyProjectile();
            }
            else
            {
                DestroyProjectile();
            }

        }
    }

    void FindClosestEnemy()
    {
        float DtoC = Mathf.Infinity;
        Goblin closestEnemy = null;
        Goblin[] all = GameObject.FindObjectsOfType<Goblin>();
        foreach(Goblin curr in all)
        {
            float DtoE = (curr.transform.position - this.transform.position).sqrMagnitude;
            if(DtoE < DtoC)
            {
                DtoC = DtoE;
                closestEnemy = curr;
            }
        }
        int strength = 1 / 2;
        transform.rotation = Quaternion.Lerp(transform.rotation, (Quaternion.LookRotation(closestEnemy.transform.position - transform.position)), Mathf.Min(strength * Time.deltaTime, 1));
        transform.position = Vector2.MoveTowards(transform.position, closestEnemy.transform.position,speed*Time.deltaTime);
        Debug.DrawLine(this.transform.position, closestEnemy.transform.position);
    }

    IEnumerator Life()
    {
        yield return new WaitForSeconds(3);
        DestroyProjectile();
    }
    public void DestroyProjectile()
    {

        Destroy(gameObject);
        Instantiate(Burst, transform.position, Quaternion.identity);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CritArea") && collision.isTrigger != false)
        {
            collision.SendMessageUpwards("CritDamage", damage);
            DestroyProjectile();
        }
        else if (collision.CompareTag("EnemyProjectile"))
        {
            DestroyProjectile();
        }
    }
}
