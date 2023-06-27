using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFrog : MonoBehaviour
{
    private Vector3 mousePosition;
    public int lifetime;
    public GameObject Burst;
    public float speed;
    public float distance;
    public int damage;
    public GameObject frog;
    //What can be Hit
    public LayerMask WhatisSolid;
    public float offset;
    private GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        //Invoke("DestroyProjectile", lifetime);
        StartCoroutine(Life());
    }

    // Update is called once per frame 
    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, enemy.transform.position, speed * Time.deltaTime);
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, WhatisSolid);
        if (hitInfo.collider != null)
        {
            //CheckEnemy
            if (hitInfo.collider.CompareTag("Enemy") && hitInfo.collider.GetComponent<Goblin>().currhealth < 55)
            {
                //DamageRegistered
                hitInfo.collider.GetComponent<Goblin>().TakeDamage(damage);
                Instantiate(frog, transform.position, Quaternion.identity);
                //ProjectileGone

                DestroyProjectile();
            }
            else if (hitInfo.collider.CompareTag("EnemyProjectile"))
            {
                hitInfo.collider.GetComponent<EnemyProjectile>().DestroyProjectile();
                StartCoroutine(Life());
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
        yield return new WaitForSeconds(5);
        DestroyProjectile();
    }
    public void DestroyProjectile()
    {
        Destroy(gameObject);
        Instantiate(Burst, transform.position, Quaternion.identity);
    }
    public void UpdateEnemy(GameObject Enemy)
    {
        enemy = Enemy;
    }
}
