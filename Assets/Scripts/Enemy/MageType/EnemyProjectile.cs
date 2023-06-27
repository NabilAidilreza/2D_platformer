using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed;
    private Transform player;
    
    private Vector2 target;
    public GameObject Burst;
    public float lifeTime;
    public int damage;
    //What can be Hit
    public LayerMask WhatisSolid;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        target = new Vector2(player.position.x, player.position.y);
        Invoke("DestroyProjectile", lifeTime);

    }

    // Update is called once per frame
    void Update()
    {

        //Targeting
       
        if (Vector2.Distance(transform.position,player.position) > 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        }
        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            DestroyProjectile();
        }
    }
    
    public void DestroyProjectile()
    {

        Destroy(gameObject);
        Instantiate(Burst, transform.position, Quaternion.identity);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.isTrigger != true)
        {
            collision.SendMessageUpwards("PlayerDamage", damage);
            DestroyProjectile();
        }


    }
}
