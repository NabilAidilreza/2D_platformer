using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodBall : MonoBehaviour
{
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public int speed;
    private Vector3 Point;
    public GameObject Burst;
    public GameObject Clone;
    // Start is called before the first frame update
    void Start()
    {
        Point = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position,Point, speed * Time.deltaTime);
        if(transform.position == Point)
        {
            Destroy(gameObject);
            Instantiate(Burst, transform.position, Quaternion.identity);
            Instantiate(Clone, transform.position, Quaternion.identity);
        }
    }
}
