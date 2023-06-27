using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossBow : MonoBehaviour
{
    public float offset;
    public Transform target;
    public float ShootRange;
    public float range;
    public GameObject CrossArrow;
    public Transform CrossPoint;
    private float timeBtwShots;
    public float startTimeBtwShots;
    private Animator CrossBowAnim;
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        timeBtwShots = startTimeBtwShots;
        
    }

    // Update is called once per frame
    void Update()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Vector3 difference = target.position - transform.position;
        float rotz = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotz + offset);
        ShootRange = Vector2.Distance(transform.position, target.position);
        
        if (timeBtwShots <= 0)
        {
            if (ShootRange < range)
            {
                Instantiate(CrossArrow, CrossPoint.position, transform.rotation);
                
            }
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
        
    }
}
