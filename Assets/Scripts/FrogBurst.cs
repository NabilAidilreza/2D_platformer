using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogBurst : MonoBehaviour
{
    public GameObject Burst;
    private List<GameObject> NearbyEnemies = new List<GameObject>();
    public Transform centre;
    public float scanRange;
    public LayerMask WhatIsEnemies;
    public GameObject FrogBall;
    private Vector3 mousePosition;
    private int speed;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Life());
        speed = 20;
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        transform.up = direction;
        transform.position = Vector2.MoveTowards(transform.position, mousePosition, speed * Time.deltaTime);
    }

    IEnumerator Life()
    {
        yield return new WaitForSeconds(3);
        DetectEnemies();
        foreach(GameObject ENEMY in NearbyEnemies)
        {
            GameObject BALL;
            BALL = Instantiate(FrogBall, centre.position, Quaternion.identity);
            BALL.GetComponent<ChangeFrog>().UpdateEnemy(ENEMY);
        }
        DestroyProjectile();
    }
    private void DetectEnemies()
    {
        Collider2D[] enemiesDetected = Physics2D.OverlapCircleAll(centre.position, scanRange, WhatIsEnemies);
        for (int i = 0; i < enemiesDetected.Length; i++)
        {
            if (!NearbyEnemies.Contains(enemiesDetected[i].gameObject) && enemiesDetected[i].gameObject.tag != "EnemyProjectile")
                NearbyEnemies.Add(enemiesDetected[i].gameObject);
        }
    }
    public void DestroyProjectile()
    {
        Destroy(gameObject);
        Instantiate(Burst, transform.position, Quaternion.identity);
    }
}
