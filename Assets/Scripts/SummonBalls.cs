using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonBalls : MonoBehaviour
{
    public GameObject Burst;
    public GameObject Ball;
    public Transform pos1;
    public Transform pos2;
    public Transform pos3;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Life());
        StartCoroutine(Fire());
    }

    // Update is called once per frame
    IEnumerator Life()
    {
        yield return new WaitForSeconds(6);
        DestroyProjectile();
    }
    IEnumerator Fire()
    {
        yield return new WaitForSeconds(8/9);
        Instantiate(Ball, pos1.position, Quaternion.identity);
        Instantiate(Ball, pos3.position, Quaternion.identity);
        yield return new WaitForSeconds(1);
        Instantiate(Ball, pos2.position, Quaternion.identity);
        Instantiate(Ball, pos1.position, Quaternion.identity);
        yield return new WaitForSeconds(8/9);
        Instantiate(Ball, pos3.position, Quaternion.identity);
        yield return new WaitForSeconds(1);
        Instantiate(Ball, pos1.position, Quaternion.identity);
        Instantiate(Ball, pos2.position, Quaternion.identity);
        yield return new WaitForSeconds(8 / 9);
        Instantiate(Ball, pos2.position, Quaternion.identity);
        Instantiate(Ball, pos3.position, Quaternion.identity);
        yield return new WaitForSeconds(1);
        Instantiate(Ball, pos2.position, Quaternion.identity);
        Instantiate(Ball, pos1.position, Quaternion.identity);
    }
    public void DestroyProjectile()
    {

        Destroy(gameObject);
        Instantiate(Burst, transform.position, Quaternion.identity);
    }
}
