using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Divide : MonoBehaviour
{
    private Vector3 mousePosition;
    public int lifetime;
    public GameObject Burst;
    public float speed;
    public float distance;
    public int damage;
    //What can be Hit
    public LayerMask WhatisSolid;
    public float offset;
    public GameObject Drops;
    public Transform P1;
    public Transform P2;
    // Start is called before the first frame update
    void Start()
    {
        //mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Invoke("DestroyProjectile", lifetime);
        StartCoroutine(Life());
    }

    // Update is called once per frame 
    void FixedUpdate()
    {


        //mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        //transform.up = direction;
        
        transform.Translate(transform.right *  speed * Time.deltaTime);
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
                Instantiate(Drops, P1.position, Quaternion.identity);
                Instantiate(Drops, P2.position, Quaternion.identity);
            }
            else if (hitInfo.collider.CompareTag("CritArea"))
            {
                hitInfo.collider.GetComponent<Goblin>().CritDamage(damage);
                //ProjectileGone
                DestroyProjectile();
            }
            else
            {
                hitInfo.collider.GetComponent<Goblin>().TakeDamage(10);
                DestroyProjectile();
            }

        }
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

    }
}