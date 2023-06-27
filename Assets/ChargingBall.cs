using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingBall : Projectile
{
    private float windUp;
    // Start is called before the first frame update
    void Start()
    {
        windUp = 0.2f;
        Invoke("DestroyProjectile", lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        CheckDmg();
        if(damage >= 20)
        {
            damage = 20;
        }
        if (windUp <= 0)
        {
            windUp = 0.2f;
            damage += 1;
        }
        else
        {
            windUp -= Time.deltaTime;
        }
    }
}
