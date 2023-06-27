using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharpelShot : MonoBehaviour
{
    private Vector3 mousePosition;
    public int lifetime;
    public GameObject Burst;
    public float speed;
    public float distance;
    public GameObject Sharpnel;
    //What can be Hit
    public LayerMask WhatisSolid;
    public float offset;
    public Transform P1;
    public Transform P2;
    public Transform P3;
    public Transform P4;
    public Transform P5;
    public Transform P6;
    // Start is called before the first frame update
    void Start()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Invoke("DestroyProjectile", lifetime);
        StartCoroutine(Life());
    }

    // Update is called once per frame 
    void FixedUpdate()
    {


        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        transform.up = direction;
        transform.position = Vector2.MoveTowards(transform.position, mousePosition, speed * Time.deltaTime);
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, WhatisSolid);
        if (hitInfo.collider != null)
        {
            //CheckEnemy
            if (hitInfo.collider.CompareTag("EnemyProjectile"))
            {
                hitInfo.collider.GetComponent<EnemyProjectile>().DestroyProjectile();
            }

        }
    }
    IEnumerator Life()
    {
        yield return new WaitForSeconds(3/2);
        DestroyProjectile();
        Instantiate(Sharpnel, P1.position, Quaternion.identity);
        Instantiate(Sharpnel, P2.position, Quaternion.identity);
        Instantiate(Sharpnel, P3.position, Quaternion.identity);
        Instantiate(Sharpnel, P4.position, Quaternion.identity);
        Instantiate(Sharpnel, P5.position, Quaternion.identity);
        Instantiate(Sharpnel, P6.position, Quaternion.identity);
    }
    public void DestroyProjectile()
    {
        Destroy(gameObject);
        Instantiate(Burst, transform.position, Quaternion.identity);
    }
}

