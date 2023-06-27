using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone : Goblin
{
    // Start is called before the first frame update
    void Start()
    {
        currhealth = health;
        StartCoroutine(Life());
    }

    // Update is called once per frame
    void Update()
    {
        //MaxHealth
        if (currhealth > health)
        {
            currhealth = health;
        }
        //CheckDead
        if (currhealth <= 0)
        {
            //KillEnemy
            Instantiate(Burst, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    IEnumerator Life()
    {
        yield return new WaitForSeconds(1);
        Instantiate(Burst, transform.position, Quaternion.identity);
        Destroy(gameObject);

    }
}
