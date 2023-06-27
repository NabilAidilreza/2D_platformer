using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadShot : EnemyProjectile
{
    private Transform player;
    public float spread;
    public GameObject Clipoo;
    public Transform P1;
    private float PlayerPosition;
    private Vector2 target;
    private float LIFE;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        target = new Vector2(player.position.x + Random.Range(-spread, spread), player.position.y - 7);
        lifeTime = LIFE;
        LIFE = 0.6f;
        Invoke("DestroyProjectile", LIFE);

        //FLip
        PlayerPosition = player.position.x;

        if (transform.position.x < PlayerPosition) // he's looking right
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }

        if (transform.position.x > PlayerPosition) // he's looking left
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

    }

    // Update is called once per frame
    void Update()
    {
        LIFE -= Time.deltaTime;
        if(LIFE <= 0.05f)
        {
            Instantiate(Clipoo, P1.transform.position, Quaternion.identity);
            Instantiate(Clipoo, P1.transform.position, Quaternion.identity);
            Instantiate(Clipoo, P1.transform.position, Quaternion.identity);
        }
        //Targeting

        if (Vector2.Distance(transform.position, player.position) > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.isTrigger != true)
        {
            collision.SendMessageUpwards("PlayerDamage", damage);
            DestroyProjectile();
        }
        else if (collision.isTrigger != true)
        {
            DestroyProjectile();
        }
    }
}
