using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFollower : EnemyProjectile
{
    public GameObject Follower;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger != true)
        {
            Instantiate(Follower, new Vector2(this.transform.position.x,this.transform.position.y + 1), Quaternion.identity);
        }
    }
}
